using Unity.Netcode.Components;

namespace Boxfriend.Networking.ClientAuthority
{
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative () => false;

        public override void OnGainedOwnership () => base.OnGainedOwnership();
    }
}
