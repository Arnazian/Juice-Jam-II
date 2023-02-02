using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private Dictionary<AudioClip, AudioSource> _currentlyPlayingClips = new Dictionary<AudioClip, AudioSource>();

    public void PlaySfx(AudioClip clip, bool loop, float volume = 1f, bool randomizePitch = true)
    {
        var sourceGameObject = new GameObject($"{clip.name} Source");
        sourceGameObject.transform.SetParent(transform);
        var source = sourceGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = sfxMixerGroup;
        source.loop = loop;
        source.volume = volume;
        if (randomizePitch)
            source.pitch = Random.Range(-1.5f, 1.5f);

        if (loop)
        {
            if(!_currentlyPlayingClips.ContainsKey(clip))
                _currentlyPlayingClips.Add(clip, source);
        }
        source.Play();
    }
    
    public void PlayMusic(AudioClip clip, bool loop, float volume = 1f, bool randomizePitch = true)
    {
        var sourceGameObject = new GameObject($"{clip.name} Source");
        sourceGameObject.transform.SetParent(transform);
        var source = sourceGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = musicMixerGroup;
        source.loop = loop;
        source.volume = volume;
        if (randomizePitch)
            source.pitch = Random.Range(-1.5f, 1.5f);

        if (loop)
        {
            if(!_currentlyPlayingClips.ContainsKey(clip))
                _currentlyPlayingClips.Add(clip, source);
        }
        source.Play();
    }
    
    public void Stop(AudioClip clip)
    {
        if (_currentlyPlayingClips.TryGetValue(clip, out var source))
        {
            source.Stop();
            _currentlyPlayingClips.Remove(clip);
            Destroy(source.gameObject);
        }
    }
}
