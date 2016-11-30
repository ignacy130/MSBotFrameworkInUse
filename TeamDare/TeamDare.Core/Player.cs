using System;
using System.Collections.Generic;
using System.Linq;
using TeamDare.Contract;

namespace TeamDare.Core
{
    public class Player : EntityBase, IPlayer
    {
        public Guid GameMasterId { get; set; }
        public virtual GameMaster GameMaster { get; set; }

        public int Level { get; set; }
        public string Nick { get; set; }
        public string AppNick { get; set; }
        public string AppId { get; set; }
        public virtual List<Adventure> GamesHistory { get; set; } = new List<Adventure>();
        public virtual IDictionary<IAdventure, IReward> Rewards { get; set; } = new Dictionary<IAdventure, IReward>();

        public Player(string nick)
        {
            this.Nick = nick;
        }

        public Player()
        {
                
        }

        IList<IAdventure> IPlayer.GamesHistory
        {
            get { return this.GamesHistory.Select(c => c as IAdventure).ToList(); }

            set { this.GamesHistory = value.Cast<Adventure>().ToList(); }
        }
    }
}