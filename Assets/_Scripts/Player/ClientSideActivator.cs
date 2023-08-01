using Cinemachine;
using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

namespace Boxfriend.Player
{
    public class ClientSideActivator : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if(IsLocalPlayer)
            {
                ActivateClients();
            }
        }

        [ContextMenu("ActivateClient")]
        private void ActivateClients()
        {
            var clientSides = transform.root.GetComponentsInChildren<IClientSideComponent>(true);
            foreach (var client in clientSides)
            {
                client.ActivateClient();
            }
        }
    }
}
