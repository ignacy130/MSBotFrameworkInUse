using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

namespace TeamDare.Contract
{
    public interface IAdventure : IEntity
    {
        IPlayer Hero { get; set; }
        IList<IChallenge> Challenges { get; set; }
        bool IsCompleted { get; }
        int PercentageCompleted { get; }
        string Title { get; set; }
        string Description { get; set; }
        int Order { get; set; }

        string FinishedText { get; set; }
        string FinishedImageUrl { get; set; }
    }
}