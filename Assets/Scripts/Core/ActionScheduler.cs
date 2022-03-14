using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action)
        {
            // Only cancel action that is different from previous action
            if (currentAction == action) { return; }
            if (currentAction != null) {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
        
    }
}