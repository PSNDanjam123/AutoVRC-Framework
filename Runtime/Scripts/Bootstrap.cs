
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework.Helpers;

namespace AutoVRC.Framework
{
    public class Bootstrap : Base
    {
        public GameObject Root;

        void Start()
        {
            Load();
        }

        protected void Load()
        {
            Debug.Log(this, "Load", "Bootstrap Started ---------------------------------------");
            var listeners = Scene.GetAllChildrensComponent<Listener>(Root);
            var models = Scene.GetAllChildrensComponent<Model>(Root);
            var controllers = Scene.GetAllChildrensComponent<Controller>(Root);

            foreach (var listener in listeners)
            {
                listener.OnBootstrap();
            }

            bootstrapModels(models, listeners);


            Debug.Log(this, "Load", "Listeners Loaded " + listeners.Length);
            Debug.Log(this, "Load", "Models Loaded " + models.Length);
            Debug.Log(this, "Load", "Controllers Loaded " + controllers.Length);
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