using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        /// <summary>
        /// Manage starting and stopping different actions between components.
        /// </summary>
        /// <remarks>
        /// Used to prevent components that trigger actions from depending from each other.
        /// </remarks>

        IAction currentAction;

        public void StartAction(IAction newAction)
        {
            // Only cancel action that is different from previous action
            if (currentAction == newAction) { return; }
            if (currentAction != null) {
                currentAction.Cancel();
            }
            currentAction = newAction;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
        
    }
}