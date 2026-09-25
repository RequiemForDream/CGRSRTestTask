using Mirror;

namespace CodeBase.Network.Messages
{
    public struct HelloMessage : NetworkMessage
    {
        public string text;
    }
}