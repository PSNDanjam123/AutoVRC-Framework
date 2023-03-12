
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AutoVRC.Framework
{
    public class Bootstrap : Base
    {
        public Listener[] Listeners;
        public Model[] Models;
        public Controller[] Controllers;
        void Start()
        {
            Load();
        }

        protected void Load()
        {
            Debug.Log(this, "Load", "Bootstrap Started ---------------------------------------");
            foreach (var Listener in Listeners)
            {
                Listener.OnBootstrap();
            }

            bootstrapModels(Models, Listeners);

            Debug.Log(this, "Load", "Listeners Loaded " + Listeners.Length);
            Debug.Log(this, "Load", "Models Loaded " + Models.Length);
            Debug.Log(this, "Load", "Bootstrap Finished ---------------------------------------");
        }

        private void bootstrapModels(Model[] models, Listener[] listeners)
        {
            foreach (var model in models)
            {
                model.Subscribers = filterListenersBySubscription(listeners, model);
                model.OnSync();
            }
        }

        private Listener[] filterListenersBySubscription(Listener[] listeners, Model model)
        {
            var count = 0;
            foreach (var listener in listeners)
            {
                foreach (var subscription in listener.Subscriptions)
                {
                    if (subscription == model)
                    {
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                return new Listener[0];
            }

            Listener[] data = new Listener[count];
            var i = 0;
            foreach (var listener in listeners)
            {
                foreach (var subscription in listener.Subscriptions)
                {
                    if (subscription == model)
                    {
                        data[i] = listener;
                        i++;
                    }
                }
            }
            return data;
        }
    }
}