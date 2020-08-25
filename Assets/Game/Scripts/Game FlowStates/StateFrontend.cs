using UnityEngine;

namespace TinyBitTurtle
{
    public sealed class StateFrontend : GameFlowCtrl.GameFlowState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            // turn off the input
            InputCtrl.Instance.SetEnabled(false);

            ActionCtrl.Instance.actionAudioPlay?.Invoke("frontEndMusic");
        }
    }
}