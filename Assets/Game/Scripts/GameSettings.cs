using UnityEngine;
using System;

namespace TinyBitTurtle
{
    [CreateAssetMenu]
    public partial class GameSettings : ScriptableObject
    {
        public float NumLevels;

        [Header("ARROW")]
        [Space(10)]
        public AnimationCurve ArrowCountCurve;
        public float ArrowCountMin = 3;
        public float ArrowCountMax = 10;
        public AnimationCurve ArrowSpeedCurve;
        public float ArrowSpeedMin = 10;
        public float ArrowSpeedMax = 10;
        [Header("TARGET")]
        [Space(10)]
        public AnimationCurve TargetSpeedCurve;
        public float TargetSpeedMin = 10;
        public float TargetSpeedMax = 10;
        public AnimationCurve TargetSizeCurve;
        public float TargetSizeMin = 10;
        public float TargetSizeMax = 10;
        [Header("BLOCKER")]
        [Space(10)]
        public AnimationCurve BlockerCountCurve;
        public float BlockerCountMin = 10;
        public float BlockerCountMax = 10;
        public AnimationCurve BlockerSpeedCurve;
        public float BlockerSpeedMin = 10;
        public float BlockerSpeedMax = 10;
        public AnimationCurve BlockerSizeCurve;
        public float BlockerSizeMin = 10;
        public float BlockerSizeMax = 10;
        [Header("TIMER")]
        [Space(10)]
        public AnimationCurve ActiveTimerCurve;
        public float ActiveTimerMax = 10;

        // arrow
        public float GetArrowCountCurve(float LevelIndex)
        {
            return (ArrowCountCurve.Evaluate(LevelIndex / NumLevels) * ArrowCountMax) + ArrowCountMin;
        }
        public float GetArrowSpeedCurve(float LevelIndex)
        {
            return (ArrowSpeedCurve.Evaluate(LevelIndex / NumLevels) * ArrowSpeedMax) + ArrowSpeedMin;
        }
        // target
        public float GetTargetSizeCurve(float LevelIndex)
        {
            return (TargetSizeCurve.Evaluate(LevelIndex / NumLevels) * TargetSizeMax) + TargetSizeMin;
        }
        public float GetTargetSpeedCurve(float LevelIndex)
        {
            return (TargetSpeedCurve.Evaluate(LevelIndex / NumLevels) * TargetSpeedMax) + TargetSpeedMin;
        }
        // blocker
        public float GetBlockerCountCurve(float LevelIndex)
        {
            return (BlockerCountCurve.Evaluate(LevelIndex / NumLevels) * BlockerCountMax) + BlockerCountMin;
        }
        public float GetBlockerSpeedCurve(float LevelIndex)
        {
            return (BlockerSpeedCurve.Evaluate(LevelIndex / NumLevels) * BlockerSpeedMax) + BlockerSpeedMin;
        }
        public float GetBlockerSizeCurve(float LevelIndex)
        {
            return (BlockerSizeCurve.Evaluate(LevelIndex / NumLevels) * BlockerSizeMax) + BlockerSizeMin;
        }
        // Active
        public float GetActiveTimerCurve(int LevelIndex)
        {
            return (ActiveTimerCurve.Evaluate(LevelIndex / NumLevels) * ActiveTimerMax);
        }
        
        [Header("Spawners")]
        [Space(10)]
        public Vector2 pos = Vector2.zero;
        public Vector2 space = new Vector2(0, 50);

        [Header("XPs")]
        [Space(10)]
        public AnimationCurve XPCurve;
        [HideInInspector]
        public int MinXP = 0;
        public int MaxXP = 100000;
        public float XPIncrScale = 0.1f;
        [HideInInspector]
        public int MinLevel = 1;
        public int MaxLevel = 99;
    }
}