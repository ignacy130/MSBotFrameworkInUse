using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamDare.Contract
{
    public interface IGameMaster
    {

        IList<IPlayer> Players { get; set; }
        IList<IAdventure> Adventures { get; set; }

        IChallenge CreateChallenge(IPlayer player);
        IProgress CheckPlayerProgress(IChallenge challenge);
        void RewardPlayer(IPlayer player, IAdventure adventure, IReward reward);
        void InvitePlayerToChallenge(IPlayer player, IChallenge challenge);
        void AddPlayer(IPlayer player);
        IReward CreateReward(IAdventure adventure);
        IAdventure CreateAdventure(IPlayer player, IList<IChallenge> challenges );
    }
}
