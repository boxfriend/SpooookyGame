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
}
