
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
            Debug.Log(this, "Start", "started!");
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
                Debug.Log(this, "Finished", "finished!");
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
                var models = listener.Subscriptions;
                foreach (var model in models)
                {
                    var subscibers = model.Subscribers;
                    var data = new Listener[subscibers.Length + 1];
                    for (var j = 0; j < subscibers.Length; j++)
                    {
                        data[j] = subscibers[j];
                    }
                    data[subscibers.Length] = listener;
                    model.Subscribers = data;
                }
                listener.Bootstrapped = true;
                processed++;
            }
            listenersBootstrapped = true;
        }

        private void bootstrapModels(int limit)
        {
            foreach (var model in Models)
            {
                model.Bootstrapped = true;
                model.OnSync();
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