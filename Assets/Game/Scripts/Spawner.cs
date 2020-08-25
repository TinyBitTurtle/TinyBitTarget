using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyBitTurtle
{
    public class Spawner : EZObjectPool
    {
        public string spawnSound;
        public float delay;

        static public bool allowSpawning = true;

        protected virtual void spawnCallback(GameObject newGameObject)
        {
            TweenPosition tweenPosition = newGameObject.GetComponent<TweenPosition>();
            tweenPosition.from.y = newGameObject.transform.localPosition.y;
            tweenPosition.to.y = newGameObject.transform.localPosition.y;
        }

        public void Init()
        {
            if (instatiateOnInit)
            {
                ObjectList = new List<GameObject>(PoolSize);
                AvailableObjects = new List<GameObject>(PoolSize);
                InstantiatePool();
            }
        }

        public virtual void spawnAction(Vector3 pos)
        {
            // spawn and positionned newly object
            GameObject newGameObject;
            TryGetNextObject(transform.position, transform.rotation, out newGameObject);
            newGameObject.transform.localPosition = pos;

            // do callback
            ActionCtrl.Instance.actionSpawn?.Invoke(newGameObject);

            // play spawn sound
            ActionCtrl.Instance.actionAudioPlay?.Invoke(spawnSound);
        }

        public void spawn(Vector3 pos, int th = 1)
        {
            // wait X seconds before spawning
            if (delay != 0)
            {
                StartCoroutine(delayedSpawn(spawnAction, delay * th, pos));
            }
            // spawm immediately
            else
            {
                spawnAction(pos);
            }
        }

        private static IEnumerator delayedSpawn(Action<Vector3> spawnAction, float duration, Vector3 pos)
        {
            // wait
            yield return new WaitForSecondsRealtime(duration);

            // spawn
            spawnAction(pos);
        }

        public void ResetPool()
        {
            for (int i = 0; i < ObjectList.Count; i++)
            {
                if (!ObjectList[i].activeInHierarchy || (ObjectList[i].activeInHierarchy))
                {
                    AvailableObjects.Add(ObjectList[i]);
                    ObjectList[i].SetActive(false);
                }
            }
        }
    }
}