using System;
using System.Collections.Generic;
using CodeBase.Network.Messages;
using Mirror;

namespace CodeBase.Network.Services
{
    public class ServerMessageService : IServerNetworkMessageService
    {
        private readonly Dictionary<int, HashSet<string>> _subscriptions = new();

        public void RegisterAndListen()
        {
            NetworkServer.RegisterHandler<SubscribeMessage>(OnSubscribeReceived);
        }

        private void OnSubscribeReceived(NetworkConnectionToClient connection, SubscribeMessage message)
        {
            if (!_subscriptions.TryGetValue(connection.connectionId, out HashSet<string> set))
            {
                set = new HashSet<string>();
                _subscriptions[connection.connectionId] = set;
            }
            set.Add(message.messageTypeId);
            
            if (message.messageTypeId == typeof(HelloMessage).FullName)
            {
                SendToSubscriber(connection, new HelloMessage { text = "Hello Client!" });
            }
        }

        public void SendToSubscriber<T>(NetworkConnectionToClient connection, T message) where T : struct, NetworkMessage
        {
            if (!IsSubscribed(connection, typeof(T)))
                return;
            
            connection.Send(message);
        }

        public void SendToSubscribers<T>(T message) where T : struct, NetworkMessage
        {
            foreach (NetworkConnectionToClient connection in NetworkServer.connections.Values)
            {
                if (IsSubscribed(connection, typeof(T)))
                {
                    connection.Send(message);
                }
            }
        }

        public void RemoveSubscriptions(NetworkConnectionToClient connection) => _subscriptions.Remove(connection.connectionId);

        private bool IsSubscribed(NetworkConnectionToClient connection, Type type)
        {
            return _subscriptions.TryGetValue(connection.connectionId, out var set)
                   && set.Contains(type.FullName);
        }
    }
}