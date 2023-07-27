using KinematicCharacterController;
using Unity.Netcode;
using UnityEngine;

namespace Boxfriend.Networking
{
    public class PlayerStateCommunicator : NetworkBehaviour
    {
        [SerializeField] private KinematicCharacterMotor _characterMotor;
        [SerializeField] private Flashlight _flashlight;

        private void FixedUpdate ()
        {
            
        }

        [ServerRpc]
        private void UpdateStateServerRPC()
        {

        }
    }
}
