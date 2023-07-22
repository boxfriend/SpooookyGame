using System;
using Boxfriend.Player;
using NaughtyAttributes;
using UnityEngine;

namespace Boxfriend
{
    public class Flashlight : MonoBehaviour
    {
        [SerializeField] private PlayerInputProvider _inputProvider;
        [SerializeField] private Light _light;

        [ShowNonSerializedField] private float _lightCharge = 1f;
        [SerializeField] private float _lightDuration;

        public event Action<float> OnChargeChange;

        private void Awake ()
        {
            _inputProvider.OnToggleLight += ToggleLight;
        }

        private void ToggleLight()
        {
            if(_lightCharge > 0)
            {
                _light.enabled = !_light.enabled;
                //TODO: Add sounds and/or animations
            }
        }

        private void Update ()
        {
            if(_light.enabled)
            {
                _lightCharge -= Time.deltaTime / _lightDuration;
                _lightCharge = Math.Max(0, _lightCharge);
                OnChargeChange?.Invoke(_lightCharge);

                if(_lightCharge <= 0)
                {
                    _light.enabled = false;

                }
            }
        }

#if UNITY_EDITOR
        [Button]
        private void Recharge () => TryRecharge(100);
#endif


        public float TryRecharge(float rechargeAmount)
        {
            if(rechargeAmount <= 0 || _lightCharge >= 1f) return rechargeAmount;

            var remaining = 0f;
            if(_lightCharge + rechargeAmount > 1f)
                remaining = _lightCharge + rechargeAmount - 1f;

            _lightCharge = Mathf.Clamp(_lightCharge + rechargeAmount, 0, 1f);
            OnChargeChange?.Invoke(_lightCharge);

            return remaining;
        }
    }
}
