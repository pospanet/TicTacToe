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
        Task RegisterPlayer(IPlayer player);
        Task UnregisterPlayer(IPlayer player);
        Task<IGame> GetGame(IPlayer player);
    }

    public interface IPlayer
    {
        Guid Id { get; }
        string DisplayName { get; }
    }

}
