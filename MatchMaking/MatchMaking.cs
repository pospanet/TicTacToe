using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using TicTacToe.Common;

namespace TicTacToe.MatchMaking
{
    internal sealed class MatchMaking : StatefulService, IMatchMaking
    {
        private IReliableDictionary<Guid, IPlayer> _players;
        private readonly object _playersLock;
        private IReliableDictionary<Guid, IGame> _games;
        private readonly object _gamesLock;

        private readonly CancellationTokenSource _cancellationTokenSource;

        public MatchMaking(StatefulServiceContext context)
            : base(context)
        {
            _players = null;
            _playersLock = new object();
            _games = null;
            _gamesLock = new object();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            yield return
                new ServiceReplicaListener(this.CreateServiceRemotingListener<MatchMaking>);
        }

        public async Task RegisterPlayer(IPlayer player)
        {
            ITransaction transaction = this.StateManager.CreateTransaction();
            _players = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, IPlayer>>("TicTacToe_Players");
            await _players.AddOrUpdateAsync(transaction, player.Id, player, (key, value) => value);
        }

        public async Task UnregisterPlayer(IPlayer player)
        {
            ITransaction transaction = this.StateManager.CreateTransaction();
            _players = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, IPlayer>>("TicTacToe_Players");
            await _players.TryRemoveAsync(transaction, player.Id);
        }

        public async Task<IGame> GetGame(IPlayer player)
        {
            ITransaction transaction = this.StateManager.CreateTransaction();
            _games = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, IGame>>("TicTacToe_Games");
            IAsyncEnumerable<KeyValuePair<Guid, IGame>> gamesEnumeration =
                await _games.CreateEnumerableAsync(transaction, EnumerationMode.Unordered);
            using (IAsyncEnumerator<KeyValuePair<Guid, IGame>> enumerator = gamesEnumeration.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync(_cancellationTokenSource.Token))
                {
                    if (enumerator.Current.Value.Player1.Id.Equals(player.Id))
                    {
                        return enumerator.Current.Value;
                    }
                    if (enumerator.Current.Value.Player2.Id.Equals(player.Id))
                    {
                        return enumerator.Current.Value;
                    }
                }
            }
            _players = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, IPlayer>>("TicTacToe_Players");
            if (!await _players.ContainsKeyAsync(transaction, player.Id))
            {
                return null;
            }
            ActorId actorId = ActorId.CreateRandom();

            IGameActor gameActor = ActorProxy.Create<IGameActor>(actorId, new Uri("fabric:/TicTacToe/GameActorService"));

            await gameActor.Init(actorId.GetGuidId());
            return await gameActor.GetStateAsync();
        }
    }
}