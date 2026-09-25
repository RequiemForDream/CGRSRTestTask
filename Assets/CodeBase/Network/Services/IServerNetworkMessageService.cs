using Mirror;

namespace CodeBase.Network.Services
{
    public interface IServerNetworkMessageService
    {
        void RegisterAndListen();
        void SendToSubscriber<T>(NetworkConnectionToClient connection, T message) where T : struct, NetworkMessage;
        void SendToSubscribers<T>(T message) where T : struct, NetworkMessage;
        void RemoveSubscriptions(NetworkConnectionToClient connection);
    }
}