using UnityEngine;

namespace TinyBitTurtle
{
    abstract public class Limits : MonoBehaviour
    {
        protected Quiver arrow;

        private void Start()
        {
            arrow = GameObject.FindGameObjectWithTag("arrow").GetComponent<Quiver>();
        }

        virtual protected void OnTriggerEnter2D(Collider2D other)
        {   
            // stop arrow
            arrow.isFiring = false;
        }
    }
}

