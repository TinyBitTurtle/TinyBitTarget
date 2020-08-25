using System;
using UnityEngine;

namespace TinyBitTurtle
{
    public partial class  ActionCtrl
    {
        [HideInInspector]
        public Action<string> actionAudioPlay = null;
        [HideInInspector]
        public Action<Collider2D, Transform> actionHitTarget = null;
        [HideInInspector]
        public Action<Collider2D, Vector3> actionHitWall = null;
        [HideInInspector]
        public Action<Collider2D, Vector3> actionHitBlocker = null;
        public Action<string, Vector3> actionCombatText = null;
        [HideInInspector]
        public Action<GameObject> actionSpawn = null;
        [HideInInspector]
        public Action<Vector3> actionEffectPlay = null;
        [HideInInspector]
        public Action<int> actionQuiverReload = null;
        [HideInInspector]
        public Action<int> actionScoreChange = null;
        [HideInInspector]
        public Action<Vector2> OnClicked = null;
        [HideInInspector]
        public Action<Vector2, Vector2> OnSwiped = null;
        [HideInInspector]
        public Action OnPinched = null;
        [HideInInspector]
        public Action actionGameplayStart = null;
        [HideInInspector]
        public Action actionGameplayStop = null;
    }
}