using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using TicTacToe.Common;

namespace MatchMaking
{
    internal sealed class MatchMaking : StatefulService, IMatchMaking
    {
        private IReliableDictionary<Guid, IPlayer> _players;
        private readonly object _playersLock;
        private IReliableDictionary<Guid, IGame> _games;
        private readonly object _gamesLock;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private IReliableDictionary<Guid, IPlayer> Players
        {
            get
            {
                if (_players == null)
                {
                    lock (_playersLock)
                        if (_players == null)
                        {
                            Task<IReliableDictionary<Guid, IPlayer>> task =
                                StateManager.GetOrAddAsync<IReliableDictionary<Guid, IPlayer>>("TicTacToe_Players");
                            Task.WaitAll(task);
                            _players = task.Result;
                        }
                }
                return _players;
            }
        }

        private IReliableDictionary<Guid, IGame> Games
        {
            get
            {
                if (_games == null)
                {
                    lock (_gamesLock)
                        if (_games == null)
                        {
                            Task<IReliableDictionary<Guid, IGame>> task =
                                StateManager.GetOrAddAsync<IReliableDictionary<Guid, IGame>>("TicTacToe_Games");
                            Task.WaitAll(task);
                            _games = task.Result;
                        }
                }
                return _games;
            }
        }

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
            await Players.AddOrUpdateAsync(transaction, player.Id, player, (key, value) => value);
        }

        public async Task UnregisterPlayer(IPlayer player)
        {
            ITransaction transaction = this.StateManager.CreateTransaction();
            await Players.TryRemoveAsync(transaction, player.Id);
        }

        public async Task<IGame> GetGame(IPlayer player)
        {
            ITransaction transaction = this.StateManager.CreateTransaction();
            IAsyncEnumerable<KeyValuePair<Guid, IGame>> gamesEnumeration =
                await Games.CreateEnumerableAsync(transaction, EnumerationMode.Unordered);
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
            if (!await Players.ContainsKeyAsync(transaction, player.Id))
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }
}