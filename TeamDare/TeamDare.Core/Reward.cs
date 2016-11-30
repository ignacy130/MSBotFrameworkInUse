using System.Runtime.Serialization.Formatters;
using TeamDare.Contract;

namespace TeamDare.Core
{
    public class Reward : EntityBase, IReward
    {
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int AdventureId { get; set; }
        public virtual Adventure Adventure { get; set; }

        public string Title { get; set; }
        public int Value { get; set; }

        public Reward()
        {
            
        }
    }
}