using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<string, AudioSource> _currentlyPlayingClips = new Dictionary<string, AudioSource>();

    public void PlaySfx(string id, AudioClip clip, float pitch = 1, bool loop = true, bool randomizePitch = true)
    {
        if(_currentlyPlayingClips.ContainsKey(id))
            return;
        var sourceGameObject = new GameObject($"{clip.name} Source");
        sourceGameObject.transform.SetParent(transform);
        var source = sourceGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        if (randomizePitch)
            source.pitch = Random.Range(-pitch + 0.5f, pitch + 0.5f);

        if (loop)
            _currentlyPlayingClips.Add(id, source);
        source.Play();
    }
    
    public void PlayMusic(string id, AudioClip clip, bool loop = true, float pitch = 1, float volume = 1f)
    {
        if(_currentlyPlayingClips.ContainsKey(id))
            return;
        
        var sourceGameObject = new GameObject($"{clip.name} Source");
        sourceGameObject.transform.SetParent(transform);
        var source = sourceGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.pitch = pitch;
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
