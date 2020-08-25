using UnityEngine;
namespace TinyBitTurtle
{
    public sealed class StateGamePlay : GameFlowCtrl.GameFlowState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            // turn on the input update
            //InputCtrl.Instance.SetEnabled(true);

            // reset all the data
            GameCtrl.Instance.Init();

            ActionCtrl.Instance.actionAudioPlay?.Invoke("GameplayMusic");

            // start the game logic
            ActionCtrl.Instance.actionGameplayStart?.Invoke();
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            // END the game logic
            ActionCtrl.Instance.actionGameplayStop?.Invoke();
        }
    }
}
