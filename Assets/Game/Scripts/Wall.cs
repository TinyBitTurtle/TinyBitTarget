using UnityEngine;
using UnityEngine.Events;

namespace TinyBitTurtle
{
    public class Wall : Limits
    {
        public static UnityAction<Collider2D, Vector3>  OnHitWall;

        // miss the target
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            // prevent double collision
            if (!arrow.isFiring)
                return;

            base.OnTriggerEnter2D(other);

            ActionCtrl.Instance.actionHitWall?.Invoke(other, transform.position);
        }
    }
}