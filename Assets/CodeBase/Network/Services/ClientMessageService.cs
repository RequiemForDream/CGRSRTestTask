using System;
using CodeBase.Network.Messages;
using Mirror;

namespace CodeBase.Network.Services
{
    public class ClientMessageService : IClientNetworkMessageService
    {
        public void Subscribe<T>(Action<T> handler) where T : struct, NetworkMessage
        { 
            NetworkClient.RegisterHandler(handler);
            NetworkClient.Send(new SubscribeMessage { messageTypeId = typeof(T).FullName });
        }
    }
}