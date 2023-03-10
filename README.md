# AutoVRC Framework

A VRChat Udon/UdonSharp Framework for building worlds.

## Installation

`git clone` the repository and import via Unity Package Manager (import through git url is WIP).

## Concepts

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

        public override void OnSync()
        {
            var material = GetComponent<MeshRenderer>().material;
            var color = Color.red;
            if(Mirror.IsTurnedOn)
            {
                color = Color.green;
            }
            material.SetColor("_Color", color);
        }
    }
```
