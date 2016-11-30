using System;
using System.Collections.Generic;
using TeamDare.Contract;

namespace TeamDare.Core
{
    public class Challenge : EntityBase, IChallenge
    {
        public Guid AdventureId { get; set; }
        public virtual Adventure Adventure { get; set; }

        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public Guid HeroId { get; set; }
        public virtual Player Hero { get; set; }
        IPlayer IChallenge.Hero
        {
            get { return this.Hero; }

            set
            {
                this.Hero = (Player)value;
            }
        }
        public virtual IList<IPlayer> Participants { get; set; } = new List<IPlayer>();
        public int Order { get; set; }
    }
}