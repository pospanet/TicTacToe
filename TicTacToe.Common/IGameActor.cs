using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace TicTacToe.Common
{
    public interface IGameActor : IActor
    {
        Task<IState> GetStateAsync();
        Task SetMoveAsync(IMove move);

    }
}
