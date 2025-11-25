using System;
using Extensions;
using UnityEngine;

namespace FightingSystem
{
    [Serializable]
    public struct AttackParameters
    {
        [field: SerializeField] public float StartDelay { get; private set; }
        [field: SerializeField] public float EndDelay { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public AnimationClip Clip { get; private set; }
        [field: SerializeField] public AttackPart RequiredPart { get; private set; }
        [field: SerializeField] public AttackType RequiredState { get; private set; }
    }
}