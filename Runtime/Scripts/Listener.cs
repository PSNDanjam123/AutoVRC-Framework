namespace AutoVRC.Framework
{
    public class Listener : Base
    {
        public Model[] Subscriptions;

        public bool Bootstrapped = false;

        virtual public void OnBootstrap()
        {

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