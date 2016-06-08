using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
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

namespace TTTApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class GameController : ApiController
    {
        private IMatchMaking matchMaking;

        public GameController()
        {
            matchMaking = ServiceProxy.Create<IMatchMaking>(new Uri("fabric:/TickTackToeServerApp/MatchMaking"));
        }

        [HttpGet]
        public string Get() => "Alive";

        // POST api/game/move
        [HttpPost]
        [Route("api/game/move")]
        public string Move([FromBody] string data)
        {
            return data;
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
            return result.Id.ToString();
        }
    }
}
