
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
        public Listener[] Listeners;
        public Model[] Models;

        public int Limiter = 100;

        private bool finished = false;
        private bool listenersBootstrapped = false;
        private bool modelsBootstrapped = false;

        void Start()
        {
            Load();
        }

        void FixedUpdate()
        {
            if (finished)
            {
                return;
            }
            if (!listenersBootstrapped)
            {
                bootstrapListeners(Limiter);
            }
            else if (!modelsBootstrapped)
            {
                bootstrapModels(Limiter);
            }
            else
            {
                finished = true;
            }
        }

        private void bootstrapListeners(int limit)
        {
            var count = Listeners.Length;
            var processed = 0;
            for (var i = 0; i < count; i++)
            {
                if (processed > limit)
                {
                    return;
                }
                var listener = Listeners[i];
                if (listener.Bootstrapped)
                {
                    continue;
                }
                listener.OnBootstrap();
                listener.Bootstrapped = true;
                processed++;
            }
            listenersBootstrapped = true;
        }

        protected void Load()
        {
            Listeners = Scene.GetAllChildrensComponent<Listener>(Root);
            Models = Scene.GetAllChildrensComponent<Model>(Root);
        }

        private void bootstrapModels(int limit)
        {
            var count = Models.Length;
            var processed = 0;
            for (var i = 0; i < count; i++)
            {
                if (processed > limit)
                {
                    return;
                }
                var model = Models[i];
                if (model.Bootstrapped)
                {
                    continue;
                }
                model.Subscribers = filterListenersBySubscription(Listeners, model);
                model.Bootstrapped = true;
                model.OnSync();
                processed++;
            }
            modelsBootstrapped = true;
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