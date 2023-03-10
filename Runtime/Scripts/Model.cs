
using UdonSharp;
using VRC.SDKBase;


namespace AutoVRC.Framework
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    abstract public class Model : Base
    {
        public Listener[] Listeners;

        void Start()
        {
            OnSync();
        }

        virtual public void OnSync()
        {
            Log("OnSync");
            foreach (var Listener in Listeners)
            {
                Listener.OnModelSync();
            }
        }

        public void Sync()
        {
            Log("Sync");
            RequestSerialization();
            OnDeserialization();
        }

        public override void OnDeserialization()
        {
            Log("OnDeserialization");
            OnSync();
        }


        public void SetOwner(VRCPlayerApi playerApi = null)
        {
            Log("SetOwner");
            if (playerApi == null)
            {
                playerApi = Networking.LocalPlayer;
            }
            if (!IsOwner(playerApi))
            {
                Networking.SetOwner(playerApi, gameObject);
            }
        }

        public VRCPlayerApi GetOwner()
        {
            Log("GetOwner");
            return Networking.GetOwner(gameObject);
        }

        public bool IsOwner(VRCPlayerApi playerApi = null)
        {
            Log("IsOwner");
            if (playerApi == null)
            {
                playerApi = Networking.LocalPlayer;
            }
            return playerApi.IsOwner(gameObject);
        }

        public override bool OnOwnershipRequest(VRCPlayerApi requestingPlayer, VRCPlayerApi requestedOwner)
        {
            Log("OnOwnershipRequest");
            return true;
        }

        public override void OnOwnershipTransferred(VRCPlayerApi playerApi)
        {
            Log("OnOwnershipTransferred");
            base.OnOwnershipTransferred(playerApi);
        }
    }
}