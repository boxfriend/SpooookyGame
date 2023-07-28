using KinematicCharacterController;
using MessagePack;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boxfriend.Networking
{
    [MessagePackObject]
    public readonly struct PlayerState
    {
        [Key(0)] public FlashlightState FlashlightState { get; }
        [Key(1)] public KCCMotorState CharacterMotorState { get; }

        [SerializationConstructor]
        public PlayerState(FlashlightState flashlightState, KCCMotorState characterMotorState)
        {
            FlashlightState = flashlightState;
            CharacterMotorState = characterMotorState;
        }
    }

    [MessagePackObject]
    public struct KCCMotorState
    {
        [Key(0)] public Vector3 Position { get; set; }
        [Key(1)] public Quaternion Rotation { get; set; }
        [Key(2)]public Vector3 BaseVelocity { get; set; }
        [Key(3)]public bool MustUnground { get; set; }
        [Key(4)]public float MustUngroundTime { get; set; }
        [Key(5)]public bool LastMovementIterationFoundAnyGround { get; set; }

        public static explicit operator KCCMotorState(KinematicCharacterMotorState state)
        {
            return new()
            {
                Position = state.Position,
                Rotation = state.Rotation,
                BaseVelocity = state.BaseVelocity,
                MustUnground = state.MustUnground,
                MustUngroundTime = state.MustUngroundTime,
                LastMovementIterationFoundAnyGround = state.LastMovementIterationFoundAnyGround,
            };
        }

        public static explicit operator KinematicCharacterMotorState(KCCMotorState state)
        {
            return new()
            {
                Position = state.Position,
                Rotation = state.Rotation,
                BaseVelocity = state.BaseVelocity,
                MustUnground = state.MustUnground,
                MustUngroundTime = state.MustUngroundTime,
                LastMovementIterationFoundAnyGround = state.LastMovementIterationFoundAnyGround,

            };
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
