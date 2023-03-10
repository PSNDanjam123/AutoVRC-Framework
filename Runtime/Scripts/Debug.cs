namespace AutoVRC.Framework
{

    public class Debug
    {
        public static void Log(object caller, string method, object message = null)
        {
            string full = "[<color=red>" + "AutoVRC" + "</color>]";
            full += "[<color=yellow>" + System.DateTime.Now.ToLongTimeString() + "." + System.DateTime.Now.Millisecond + "</color>]";
            full += "[<color=green>" + caller.ToString().Substring(0, caller.ToString().Length - " (VRC.Udon.UdonBehaviour)".Length) + "</color>]";
            full += "[<color=green>" + method + "</color>]";
            if (message != null)
            {
                full += " " + message;
            }
            UnityEngine.Debug.Log(full);
        }
    }

}