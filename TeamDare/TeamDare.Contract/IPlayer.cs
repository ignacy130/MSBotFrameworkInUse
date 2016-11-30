using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamDare.Contract
{
    public interface IPlayer : IEntity
    {
        int Level { get; set; }
        string Nick { get; set; }
        IList<IAdventure> GamesHistory { get; set; }
        IDictionary<IAdventure,IReward> Rewards { get; set; }        
    }
}
