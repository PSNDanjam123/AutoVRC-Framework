
using UdonSharp;
using UnityEngine;

namespace AutoVRC.Framework
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    abstract public class Base : UdonSharpBehaviour
    {
        [Header("Debug"), Header("AutoVRC"), Tooltip("Toggles log output for specific methods")]
        public bool LogEnabled = false;
        [Tooltip("The allowed methods when LogEnabled = true")]
        public string[] LogMethods = { };
        public void Log(string method, object message = null)
        {
            if (canDebug(method))
            {
                Debug.Log(this, method, message);
            }
        }

        private bool canDebug(string method)
        {
            if (!LogEnabled || LogMethods.Length == 0)
            {
                return false;
            }
            foreach (var allowedMethod in LogMethods)
            {
                if (allowedMethod == method)
                {
                    return true;
                }
            }
            return false;
        }
    }

}