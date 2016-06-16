using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TicTacToe.Common;

namespace TicTacToe.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class GameController : ApiController
    {
        private IMatchMaking matchMaking;

        public GameController()
        {
            ServicePartitionKey servicePartitionKey = new ServicePartitionKey(0);
            matchMaking = ServiceProxy.Create<IMatchMaking>(new Uri("fabric:/TicTacToe/MatchMaking"), servicePartitionKey);
        }

        [HttpGet]
        public string Get() => "Alive";

        // POST api/game/move
        [HttpPost]
        [Route("api/game/move")]
        public async Task<string> Move([FromBody] IMove data)
        {
            var result = await matchMaking.GetGame(data.PlayerId);
            ActorId actorId = new ActorId(result.Id);

            IGameActor gameActor = ActorProxy.Create<IGameActor>(actorId, new Uri("fabric:/TicTacToe/GameActorService"));

            await gameActor.SetMoveAsync(data);

            IGame state = await gameActor.GetStateAsync();

            return JsonConvert.SerializeObject(state);
        }

        // POST api/game/join
        [HttpPost]
        public async Task<string> Join([FromBody] IPlayer data)
        {
            // přidat uživatele do fronty IPlayer (PlayerID, DisplayName)
            // matchmaking -> register player
            // unregister player
            //IPlayer pl = JsonConvert.DeserializeObject<IPlayer>(data);

            await matchMaking.RegisterPlayer(data);
            // get game -> null nebo IGame
            var result = await matchMaking.GetGame(data);

            return JsonConvert.SerializeObject(result);
        }
    }
}
