namespace AutoVRC.Framework
{
    public class Listener : Base
    {
        private Model[] Subscriptions;

        private uint SyncCount = 0;
        private bool FirstSync = true;

        virtual public void FixedUpdate()
        {
            foreach (var sub in Subscriptions)
            {
                if (SyncCount != sub.SyncCount || FirstSync)
                {
                    FirstSync = false;
                    SyncCount = sub.SyncCount;
                    OnModelSync();
                }
            }
        }

        public void Subscribe(Model model)
        {
            var models = new Model[Subscriptions.Length + 1];
            for (var i = 0; i < Subscriptions.Length; i++)
            {
                models[i] = Subscriptions[i];
            }
            models[Subscriptions.Length] = model;
            Subscriptions = models;
        }

        virtual public void OnModelSync()
        {

        }
    }
}