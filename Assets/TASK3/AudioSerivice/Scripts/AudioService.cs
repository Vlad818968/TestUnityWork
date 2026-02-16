using AxGrid.Base;
using AxGrid.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioService : MonoBehaviourExtBind
{
    private List<AudioClip> _sounds;
    private OneShotSound _oneShotSoundPrefab;

    [OnAwake]
    private void Init()
    {
        _sounds = Resources.LoadAll<AudioClip>("Sounds").ToList();
        _oneShotSoundPrefab = Resources.Load<OneShotSound>("OneShotSound");
    }

    [Bind("SoundPlay")]
    private void PlayOneShoot(string soundName)
    {
        var sound = _sounds.Find(s => s.name == soundName);
        if (sound == null)
        {
            return;
        }

        var source = Instantiate(_oneShotSoundPrefab);
        source.Play(sound);
    }
}
