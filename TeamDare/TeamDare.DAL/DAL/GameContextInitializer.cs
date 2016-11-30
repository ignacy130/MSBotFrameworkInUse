using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TeamDare.Contract;
using TeamDare.Core;

namespace TeamDare.DAL.DAL
{
    class GameContextInitializer : DropCreateDatabaseIfModelChanges<GameContext>
    {

        protected override void Seed(GameContext context)
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
