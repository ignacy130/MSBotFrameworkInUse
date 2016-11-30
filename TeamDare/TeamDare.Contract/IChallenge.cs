using System.Collections.Generic;

namespace TeamDare.Contract
{
    public interface IChallenge : IEntity
    {
        string Title { get; set; }
        bool IsCompleted { get; set; }
        IPlayer Hero { get; set; }
        IList<IPlayer> Participants { get; set; }
        int Order { get; set; }
    }
}