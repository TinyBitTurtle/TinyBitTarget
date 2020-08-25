using UnityEngine;

namespace TinyBitTurtle
{
    public class Quiver : MonoBehaviour
    {
        [HideInInspector]
        public int arrowUsed = 0;

        public void Fire()
        {
            //arrowUsed = (arrowUsed <= GameCtrl.Instance.GetArrowMaxCount(GameCtrl.Instance.currentLevel)) ? ++arrowUsed : 0;
        }

        public void Reset()
        {
            arrowUsed = 0;
        }
        public void OnConsumeArrow()
        { }

        [HideInInspector]
        public bool isFiring { get; set; }

        void OnEnable()
        {
            UICamera.onClick += OnFire;
            //ActionCtrl.Instance.OnSwiped += OnSwipe;
        }

        void OnDisable()
        {
            UICamera.onClick -= OnFire;
            //ActionCtrl.Instance.OnSwiped -= OnSwipe;
        }

        private void OnFire(GameObject go)
        {
            if (go.tag != "UI Root")
                return;

            isFiring = true;
        }

        void Update()
        {
            if (isFiring)
              transform.Translate(new Vector2(0.0f, GameCtrl.Instance.GetArrowSpeed(LevelCtrl.Instance.currentLevel) * Time.deltaTime));
        }
    }
}
