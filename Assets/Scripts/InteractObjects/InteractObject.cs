using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractObject : MonoBehaviour, IEquatable<InteractObject>
{
    abstract public Sprite Icon { get; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        InteractObject io = obj as InteractObject;
        if (io as InteractObject == null) return false;
        return this.name == io.name;    
    }

    public bool Equals(InteractObject obj)
    {
        if (obj == null) return false;
        return this.name == obj.name;
    }

    public override int GetHashCode()
    {
        return this.name.Length;
    }

    abstract public void WhenCatched();
    
}
