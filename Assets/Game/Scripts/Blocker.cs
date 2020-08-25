using UnityEngine;
using UnityEngine.Events;

namespace TinyBitTurtle
{
    public class Blocker : Limits
    {
        public static UnityAction<Collider2D, Vector3> OnHitBlocker;

        override protected void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

           // raise an event
            ActionCtrl.Instance.actionHitBlocker?.Invoke(other, transform.position);
        }
    }
}