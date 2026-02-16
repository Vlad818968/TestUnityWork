using AxGrid.Base;
using AxGrid.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectSerivice : MonoBehaviourExtBind
{
    private List<ParticleSystem> _effects;

    [OnAwake]
    private void Init()
    {
        _effects = Resources.LoadAll<ParticleSystem>("Effects").ToList();
    }

    [Bind("CreateEffect")]
    private void CreateEffect(string name, RectTransform container)
    {
        var effect = _effects.Find(s => s.name == name);
        if (effect == null)
        {
            return;
        }

        Instantiate(effect, container.position, Quaternion.identity, container);
    }
}
