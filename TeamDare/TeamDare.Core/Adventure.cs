using System;
using System.Collections.Generic;
using System.Linq;
using TeamDare.Contract;

namespace TeamDare.Core
{
    public class Adventure : EntityBase, IAdventure
    {
        public Guid GameMasterId { get; set; }
        public virtual GameMaster GameMaster { get; set; }
        public Guid HeroId { get; set; }
        public virtual Player Hero { get; set; }
        public virtual List<Challenge> Challenges { get; set; } = new List<Challenge>();
        public bool IsCompleted {
            get { return Challenges.All(x => x.IsCompleted); }
        }
        public int PercentageCompleted { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        public string FinishedText { get; set; }
        public string FinishedImageUrl { get; set; }


        IPlayer IAdventure.Hero
        {
            get { return this.Hero; }

            set
            {
                this.Hero = (Player)value;
            }
        }

        IList<IChallenge> IAdventure.Challenges
        {
            get { return this.Challenges.Select(c=>c as IChallenge).ToList(); }

            set { this.Challenges = value.Cast<Challenge>().ToList(); }
        }
    }
}