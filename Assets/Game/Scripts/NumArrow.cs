using UnityEngine;

namespace TinyBitTurtle
{
    public class NumArrow : MonoBehaviour
    {
        [HideInInspector]
        public int arrowUsed = 0;

        void Start()
        {
           // GetComponent<UILabel>().text = GameCtrl.Instance.GetArrowMaxCount(GameCtrl.Instance.currentLevel).ToString();
        }

        public void Fire()
        {
           /// arrowUsed = (arrowUsed <= GameCtrl.Instance.GetArrowMaxCount(GameCtrl.Instance.currentLevel)) ? ++arrowUsed : 0;
           // GetComponent<UILabel>().text = (GameCtrl.Instance.GetArrowMaxCount(GameCtrl.Instance.currentLevel) - arrowUsed).ToString();
        }

        public void Reset()
        {
            arrowUsed = 0;
            //GetComponent<UILabel>().text = (GameCtrl.Instance.GetArrowMaxCount(GameCtrl.Instance.currentLevel) - arrowUsed).ToString();
        }
        public void OnConsumeArrow()
        { }
    }
}
