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
using TicTacToe.Common;

namespace TTTApi.Controllers
{
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
        public string Join([FromBody] string data)
        {
            // přidat uživatele do fronty IPlayer (PlayerID, DisplayName)
            // matchmaking -> register player
            // unregister player
            IPlayer pl = JsonConvert.DeserializeObject<IPlayer>(data);

            matchMaking.RegisterPlayer(pl);
            // get game -> null nebo IGame
            var result = matchMaking.GetGame(pl);
            return "ABCD";
        }
    }
}
