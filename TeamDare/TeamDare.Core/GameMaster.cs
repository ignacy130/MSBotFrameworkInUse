using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamDare.Contract;

namespace TeamDare.Core
{
    public class GameMaster : EntityBase, IGameMaster
    {
        public virtual List<Player> Players { get; set; } = new List<Player>();
        public virtual IList<IAdventure> Adventures { get; set; } = new List<IAdventure>();

        public IChallenge CreateChallenge(IPlayer player)
        {
            return new Challenge() { Hero = (Player)player };            
        }

        public IPlayer ChallengePlayer()
        {
            throw new NotImplementedException();
        }

        public IProgress CheckPlayerProgress(IChallenge challenge)
        {
            throw new NotImplementedException();
        }

        public void RewardPlayer(IPlayer player, IAdventure adventure, IReward reward)
        {
            player.Rewards.Add(adventure, reward);
        }

        public void InvitePlayerToChallenge(IPlayer player, IChallenge challenge)
        {
            if (!challenge.Participants.Any(p => p.Nick == player.Nick) &&
                !player.GamesHistory.Any(c => c.Equals(challenge)))
            {
                challenge.Participants.Add(player);
                player.GamesHistory.Single(x=>x.Challenges.Any(c=>c.Id==challenge.Id)).Challenges.Add(challenge);
            }
        }

        public void AddPlayer(IPlayer player)
        {
            if (!this.Players.Any(x => x.Nick == player.Nick))
            {
                this.Players.Add((Player)player);
                ((Player) player).GameMaster = this;
            }
        }

        public IReward CreateReward(IAdventure adventure)
        {
            return new Reward()
            {
                Value = 1000,
                Title = "Super!"
            };
        }

        public IAdventure CreateAdventure(IPlayer player, IList<IChallenge> challenges)
        {
            var adventure =  new Adventure();
            adventure.Hero = (Player)player;
            player.GamesHistory.Add(adventure);
            adventure.Challenges = challenges.Cast<Challenge>().ToList();
            adventure.GameMaster = this;
            return adventure;
        }

        IList<IPlayer> IGameMaster.Players
        {
            get { return this.Players.Select(c => c as IPlayer).ToList(); }

            set { this.Players = value.Cast<Player>().ToList(); }
        }

    }
}
