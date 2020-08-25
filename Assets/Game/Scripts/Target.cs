using UnityEngine;

namespace TinyBitTurtle
{
    public class Target : Limits
    {
        // hit the target
        override protected void OnTriggerEnter2D(Collider2D other)
        {
            // call the parent class
            base.OnTriggerEnter2D(other);

            //OnHitTarget?.Invoke(transform, other);

            ActionCtrl.Instance.actionHitTarget?.Invoke(other, transform);
        }

        private void Update()
        {
            Vector3 vDirection = transform.position - arrow.transform.position;
            float mag = vDirection.magnitude;
            // cast ray on "UI' layer
            int layerMask = LayerMask.GetMask("UI");
            RaycastHit2D hit = Physics2D.Raycast(arrow.transform.position, Vector3.up, mag, layerMask);
            Debug.DrawLine(arrow.transform.position, arrow.transform.position + (Vector3.up * mag), Color.red);
            if (hit.collider == GetComponent<BoxCollider2D>())
            {
                GetComponent<SpriteRenderer>().color = Color.white;

            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }
}
