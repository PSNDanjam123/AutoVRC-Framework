
using UdonSharp;
using VRC.SDKBase;


namespace AutoVRC.Framework
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    abstract public class Model : Base
    {
        [UdonSynced]
        public uint SyncCount = 0;   // Used by listeners to detect updates

        public void Sync()
        {
            Log("Sync");
            SyncCount++;
            RequestSerialization();
            OnDeserialization();
        }

        public override void OnDeserialization()
        {
            Log("OnDeserialization");
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