using System;
using Mirror;

namespace CodeBase.Network.Services
{
    public interface IClientNetworkMessageService
    {
        void Subscribe<T>(Action<T> handler) where T : struct, NetworkMessage;
    }
}