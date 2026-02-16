using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OneShotSound : MonoBehaviour
{
    public float Volume
    {
        get => _source.volume;
        set => _source.volume = value;
    }

    public float Pitch
    {
        get => _source.pitch;
        set => _source.pitch = value;
    }

    [SerializeField] private AudioSource _source;

    public void Play(AudioClip audioClip)
    {
        _source.PlayOneShot(audioClip);
        StartCoroutine(AutoDestroy());
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator AutoDestroy()
    {
        while (_source.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        _source = GetComponent<AudioSource>();
    }

#endif
}
