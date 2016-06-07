using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace TicTacToe.Common
{
    public interface IMatchMaking:IService
    {
        IGame GetGame(IPlayer player);
    }

    public interface IPlayer
    {
        Guid Id { get; }
        string DisplayName { get; }
    }

    public interface IGame
    {
        Guid Id { get; }
        IPlayer Player1 { get; }
        IPlayer Player2 { get; }
    }
}
