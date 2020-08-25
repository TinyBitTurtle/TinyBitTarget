using UnityEngine;

namespace TinyBitTurtle
{
    public class GamePieceSpawner : Spawner
    {
        void OnEnable()
        {
            // hook up the action to the callback func
            ActionCtrl.Instance.actionSpawn += spawnCallback;
            ActionCtrl.Instance.actionGameplayStop += StopAllGSpawning;
        }

        void OnDisable()
        {
            // de-register all spawn events
            ActionCtrl.Instance.actionSpawn -= spawnCallback;
            ActionCtrl.Instance.actionGameplayStop -= StopAllGSpawning;
        }

        protected override void spawnCallback(GameObject newGameObject)
        {
            base.spawnCallback(newGameObject);

            newGameObject.transform.localScale = new Vector3(GameCtrl.Instance.gameSettings.GetBlockerSizeCurve(90), 35, 1);

            TweenPosition tweenPosition = newGameObject.GetComponent<TweenPosition>();
            tweenPosition.duration = 1.0f / GameCtrl.Instance.gameSettings.GetBlockerSpeedCurve(99);
        }

        private void StopAllGSpawning()
        {
            // stop spawning
            Spawner.allowSpawning = false;
        }

        public override void spawnAction(Vector3 pos)
        {
            if (!allowSpawning)
                return;

            base.spawnAction(pos);
        }
    }
}