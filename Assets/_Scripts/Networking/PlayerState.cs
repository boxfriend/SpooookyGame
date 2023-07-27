using KinematicCharacterController;
using MessagePack;

namespace Boxfriend.Networking
{
    [MessagePackObject]
    public readonly struct PlayerState
    {
        [Key(0)] public FlashlightState FlashlightState { get; }
        [Key(1)] public KinematicCharacterMotorState CharacterMotorState { get; }

        [SerializationConstructor]
        public PlayerState(FlashlightState flashlightState, KinematicCharacterMotorState characterMotorState)
        {
            FlashlightState = flashlightState;
            CharacterMotorState = characterMotorState;
        }
    }

    [MessagePackObject]
    public readonly struct FlashlightState
    {
        [Key(0)] public bool Enabled { get; }
        [Key(1)] public float Charge { get; }

        [SerializationConstructor]
        public FlashlightState (bool enabled, float charge)
        {
            Enabled = enabled;
            Charge = charge;
        }
    }
}
