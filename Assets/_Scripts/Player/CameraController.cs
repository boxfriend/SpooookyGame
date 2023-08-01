using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace Boxfriend.Player
{
    public class CameraController : MonoBehaviour, IClientSideComponent
    {
        [Required("VCam must be assigned")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        public void ActivateClient ()
        {
            _virtualCamera.enabled = true;
        }

        private void Reset () => _virtualCamera = transform.root.GetComponentInChildren<CinemachineVirtualCamera>();
    }
}
