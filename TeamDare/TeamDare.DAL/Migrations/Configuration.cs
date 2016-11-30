using TeamDare.Core;

namespace TeamDare.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TeamDare.DAL.GameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeamDare.DAL.GameContext context)
        {
            var factory = new GameMasterFactory();

            var gameMaster = (GameMaster)factory.CreateGameMaster();

            var player = (Player)factory.CreatePlayer("tester");
            player = (Player)factory.InitializeGameForPlayer(player, gameMaster);
            player.AppId = "000000";
            player.AppNick = "tester";
            context.GameMasters.Add(gameMaster);
            foreach (var adv in player.GamesHistory)
            {
                context.Adventures.Add((Adventure)adv);
                foreach (var challenge in adv.Challenges)
                {
                    context.Challenges.Add((Challenge)challenge);
                }
            }
            context.Players.Add(player);
            context.SaveChanges();


            base.Seed(context);
        }
    }
}
