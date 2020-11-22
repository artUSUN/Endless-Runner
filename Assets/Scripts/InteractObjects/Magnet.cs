using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : InteractObject
{
    [SerializeField] private ParticleSystem framingEffect;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Sprite icon;


    public override Sprite Icon { get { return icon; } }
    public override void WhenCatched()
    {
        StateBus.Boost += this;
        mesh.SetActive(false);
        framingEffect.gameObject.SetActive(false);
    }
}
