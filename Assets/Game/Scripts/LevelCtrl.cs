using UnityEngine;
using UnityEngine.Events;

namespace TinyBitTurtle
{
    public class LevelCtrl : SingletonMonoBehaviour<LevelCtrl>
    {
        [HideInInspector]
        public int currentLevel = 0;

        private int debugCurrentXP = 0;
        private int debugXPIncr = 0;

        private UnityAction levelUpEvent;

        private void Start()
        {
            InvokeRepeating("AddXP", 1f, 1f);
        }

        private void AddXP()
        {
            UpdateXP(5);
        }

        private void UpdateXP(int xp)
        {
            float range = GameCtrl.Instance.gameSettings.MaxLevel - GameCtrl.Instance.gameSettings.MinLevel;

            // get level normalized
            float normalizedLevel = currentLevel / range;

            // xp to reach the next level
            int XPToNextLevel = (int)(GameCtrl.Instance.gameSettings.XPCurve.Evaluate(normalizedLevel) * (range));

            // how to we move the XP meter by? use a linear curve
            debugXPIncr = (int)(XPToNextLevel * GameCtrl.Instance.gameSettings.XPIncrScale);

            // prevent overflow
            if (currentLevel + debugXPIncr >= GameCtrl.Instance.gameSettings.MaxXP)
                return;

            // add experience
            debugCurrentXP += debugXPIncr;

            // level uo
            if (debugCurrentXP >= XPToNextLevel)
            {
                if (levelUpEvent != null )
                    levelUpEvent();

                ++currentLevel;
                debugCurrentXP -= XPToNextLevel;
            }
        }
    }
}
