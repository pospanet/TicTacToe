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
        Task<IGame> RegisterPlayer(IPlayer player);
        Task<IGame> UnregisterPlayer(IPlayer player);
        Task<IGame> GetGame(IPlayer player1, IPlayer player2);
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
