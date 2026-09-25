using CodeBase.Network.Messages;
using CodeBase.Network.Services;
using Mirror;
using UnityEngine;
using Zenject;

namespace CodeBase.Network
{
    public class SubscriptionNetworkManager : NetworkManager
    {
        private IServerNetworkMessageService _serverService;
        private IClientNetworkMessageService _clientService;

        [Inject]
        private void Construct(IServerNetworkMessageService serverService, IClientNetworkMessageService clientService)
        {
            _serverService = serverService;
            _clientService = clientService;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            _serverService.RegisterAndListen();
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            _serverService.RemoveSubscriptions(conn);
            base.OnServerDisconnect(conn);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            _clientService.Subscribe<HelloMessage>(message =>
            {
                Debug.Log($"Client received: {message.text}");
            });
        }
    }
}