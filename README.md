# AutoVRC Framework

A VRChat Udon/UdonSharp Framework for building worlds.

## Installation

Either through Unity Package Manager (Add package from git URL) or VRChat Creator Companion

## Concepts

### Bootstrap

- **Naming Convention:** "Bootstrap"
- **Scene Location:** At the root of your project

The bootstrap class is required by AutoVRC to initialize the project on load. For the bootstrapper to work, assign all your Models & Listeners to it.

**Example Code**

```
    using AutoVRC.Framework;

    public class Bootstrap : Framework.Bootstrap
    {
    }
```

### Models

- **Naming Convention:** No prefix/suffix _(e.g Mirror)_
- **Scene Location:** Empty GameObject as child of related object

**Example Code**

```
    using AutoVRC.Framework;

    public class Mirror : Model
    {
        public bool IsTurnedOn = false;
    }
```

### Controllers

- **Naming Convention:** Suffix of Controller _(e.g MirrorController)_
- **Scene Location:** Child of "Controllers" empty GameObject

**Example Code**

```
    using AutoVRC.Framework;

    public class MirrorController: Controller
    {
        public void Toggle(Mirror Mirror)
        {
            Mirror.SetOwner();
            Mirror.IsTurnedOn = !Mirror.IsTurnedOn;
            Mirror.Sync();
        }
    }
```

### Listeners

- **Naming Convention:** Suffix of Listener _(e.g MirrorButtonListener)_
- **Scene Location:** Attached to GameObject you expect to trigger the event

**Example Code**

```
    using AutoVRC.Framework;

    public class MirrorButtonListener
    {
        public Mirror Mirror;

        void Interact()
        {
            MirrorController.Toggle(Mirror);
        }

        public override void OnBootstrap()
        {
            Subscribe(Mirror);
        }

        public override void OnSync()
        {
            var material = gameObject.GetComponent<MeshRenderer>().material;
            var color = Color.red;
            if(Mirror.IsTurnedOn)
            {
                color = Color.green;
            }
            material.SetColor("_Color", color);
        }
    }
```
