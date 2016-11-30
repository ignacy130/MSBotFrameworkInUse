using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamDare.Contract
{
    public interface ICreateGame
    {
        IGameMaster CreateGameMaster();
        IPlayer CreatePlayer(string nick);
        IPlayer InitializeGameForPlayer(IPlayer player, IGameMaster gameMaster);
    }
     
}
