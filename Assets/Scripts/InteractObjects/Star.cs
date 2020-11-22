using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : InteractObject
{
    [SerializeField] private ParticleSystem FramingEffect;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Sprite icon;

    public override Sprite Icon { get { return icon; } }

    public override void WhenCatched()
    {
        StateBus.Boost += this;
        mesh.SetActive(false);
        FramingEffect.gameObject.SetActive(false);
    }
}
