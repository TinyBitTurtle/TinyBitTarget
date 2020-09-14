using System.Collections;
using UnityEngine;
using TinyBitTurtle.Core;

namespace TinyBitTurtle
{
    public class GameCtrl : SingletonMonoBehaviour<GameCtrl>
    {
        [HideInInspector]
        public GameSettings gameSettings;
        public GamePieceSpawner blockSpawner;
        public UILabel countdown;
        private Coroutine coRoutineCountdown;

        private void Awake()
        {
            gameSettings = Resources.Load<GameSettings>("GameSettings") as GameSettings;
        }

        private void StartAllGameplay()
        {
            // turn on input
            InputCtrl.Instance.SetEnabled(true);

            // start the count down
            coRoutineCountdown = StartCoroutine(Countdown(gameSettings.GetActiveTimerCurve(0)));
            countdown.enabled = true;

            Spawner.allowSpawning = true;
        }

        private void StopAllGameplay()
        {
            // turn off the input
            InputCtrl.Instance.SetEnabled(false);

            // reset the count down
            StopCoroutine(coRoutineCountdown);
            countdown.enabled = false;

            blockSpawner.ResetPool();
        }

        IEnumerator Countdown(float seconds)
        {
            int counter = (int)seconds;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);

                // count down
                counter--;

                // show number
                countdown.text = counter.ToString();
            }
            // out of time, display a "failed" message
            // return to the front end
        }

        private void OnEnable()
        {
            // hook up the action to the callback func
            ActionCtrl.Instance.actionHitTarget += HitTarget;
            ActionCtrl.Instance.actionHitBlocker += HitBlocker;
            ActionCtrl.Instance.actionHitWall += HitWall;
            ActionCtrl.Instance.actionCombatText += ShowCombatText;
            ActionCtrl.Instance.actionGameplayStart += StartAllGameplay;
            ActionCtrl.Instance.actionGameplayStop += StopAllGameplay;
        }

        private void OnDisable()
        {
            // de-register all events
            ActionCtrl.Instance.actionHitTarget -= HitTarget;
            ActionCtrl.Instance.actionHitBlocker -= HitBlocker;
            ActionCtrl.Instance.actionHitWall -= HitWall;
            ActionCtrl.Instance.actionCombatText -= ShowCombatText;
            ActionCtrl.Instance.actionGameplayStart -= StartAllGameplay;
            ActionCtrl.Instance.actionGameplayStop -= StopAllGameplay;
        }

        // action base on event
        private void ShowCombatText(string text, Vector3 pos)
        {
            CombatTextCtrl.Instance.add(text, pos, CombatTextCtrl.eLevel.normal);
        }

        private void HitBlocker(Collider2D other, Vector3 pos)
        {
            ActionCtrl.Instance.actionCombatText?.Invoke("blocker", pos);

            // update the score
            ActionCtrl.Instance.actionScoreChange?.Invoke(-1);

            other.transform.localPosition = new Vector3(0, -340, transform.localPosition.z);
        }

        private void HitWall(Collider2D other, Vector3 pos)
        {
            //CombatTextCtrl.Instance.add("wall", pos, CombatTextCtrl.eLevel.normal);
            ActionCtrl.Instance.actionCombatText?.Invoke("wall", pos);

            // update the score
            ActionCtrl.Instance.actionScoreChange?.Invoke(-1);

            // reset posion
            other.transform.localPosition = new Vector3(0, -342, transform.localPosition.z);
        }

        // action callback
        private void HitTarget(Collider2D other, Transform targetTransform)
        {
            CameraShakeCtrl.Instance.StartShake();

            ActionCtrl.Instance.actionCombatText?.Invoke("target", transform.position);

            // update the score
            //addScore(10);
            ActionCtrl.Instance.actionScoreChange(10);

            // attach to
            other.transform.SetParent(targetTransform);
        }
      

        public void Init()
        {
            ScoreCtrl.Instance.Reset();

            SetupBlocks(0);
            SetupTarget();
            SetupQuiver();
        }

        private void SetupBlocks(int level = 0)
        {
            level = 99;

            // fill the pool
            blockSpawner.Init();

            // per level
            int NumOfBlocks = (int)gameSettings.GetBlockerCountCurve(level);
            float size = gameSettings.GetBlockerSizeCurve(level);
            float speed = gameSettings.GetBlockerSpeedCurve(level);
            for (int i = 0; i < NumOfBlocks; ++i)
            {
                float startXPos = Random.Range(i * gameSettings.pos.x, i * gameSettings.pos.x + gameSettings.space.x);
                float startYPos = i * gameSettings.space.y;

                Vector3 worldPos = new Vector3(startXPos, startYPos, 0.0f);
                Vector3 localPos = worldPos - blockSpawner.transform.position;
                blockSpawner.spawn(localPos, i + 1);
            }
        }

        private void SetupTarget(int level = 0)
        {
        }

        private void SetupQuiver(int level = 0)
        {
        }

        // arrow
        public int GetArrowMaxCount(float LevelIndex)
        {
            return (int)(gameSettings.GetArrowCountCurve(LevelIndex));
        }

        public float GetArrowSpeed(float LevelIndex)
        {
            return gameSettings.GetArrowSpeedCurve(LevelIndex);
        }

        // target
        public float GetTargetSize(float LevelIndex)
        {
            return gameSettings.GetTargetSizeCurve(LevelIndex);
        }
        public float GetTargetSpeed(float LevelIndex)
        {
            return gameSettings.GetTargetSpeedCurve(LevelIndex);
        }

        // blocker
        public float GetBlockerCount(float LevelIndex)
        {
            return gameSettings.GetBlockerCountCurve(LevelIndex);
        }
        public float GetBlockerSpeed(float LevelIndex)
        {
            return gameSettings.GetBlockerSpeedCurve(LevelIndex);
        }
    }
}
