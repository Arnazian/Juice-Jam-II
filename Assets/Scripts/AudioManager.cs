using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private Dictionary<string, AudioSource> _currentlyPlayingClips = new Dictionary<string, AudioSource>();

    public void PlaySfx(string id, AudioClip clip, bool loop = true, bool randomizePitch = true)
    {
        if(_currentlyPlayingClips.ContainsKey(id))
            return;
        var sourceGameObject = new GameObject($"{clip.name} Source");
        sourceGameObject.transform.SetParent(transform);
        var source = sourceGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = sfxMixerGroup;
        source.loop = loop;
        if (randomizePitch)
            source.pitch = Random.Range(-1.5f, 1.5f);

        if (loop)
            _currentlyPlayingClips.Add(id, source);
        source.Play();
    }
    
    public void PlayMusic(string id, AudioClip clip, bool loop = true, float volume = 1f)
    {
        if(_currentlyPlayingClips.ContainsKey(id))
            return;
        
        var sourceGameObject = new GameObject($"{clip.name} Source");
        sourceGameObject.transform.SetParent(transform);
        var source = sourceGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = musicMixerGroup;
        source.loop = loop;
        source.volume = volume;

        if (loop)
            _currentlyPlayingClips.Add(id, source);
        source.Play();
    }
    
    public void Stop(string id)
    {
        if (_currentlyPlayingClips.TryGetValue(id, out var source))
        {
            source.Stop();
            _currentlyPlayingClips.Remove(id);
            Destroy(source.gameObject);
        }
    }

    public AudioSource GetAudioSource(string id)
    {
        if (_currentlyPlayingClips.TryGetValue(id, out var source))
            return source;
        return null;
    }
}
