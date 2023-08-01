using Unity.Netcode.Components;

namespace Boxfriend.Networking.ClientAuthority
{
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative () => false;
    }
}
