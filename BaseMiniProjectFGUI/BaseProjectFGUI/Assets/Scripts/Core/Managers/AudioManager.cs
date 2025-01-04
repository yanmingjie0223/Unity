using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    private readonly Dictionary<string, List<AudioSource>> _audioDic = new();
    private readonly List<AudioSource> _audioPool = new();

    public void OnPlayOneShot(string soundName)
    {
        CheckRecycle();
        var audioClip = GetAudioClip(soundName);
        var audioSource = CreateAudioSource(soundName);
        audioSource.clip = audioClip;

        List<AudioSource> sources;
        if (_audioDic.ContainsKey(soundName))
        {
            sources = _audioDic[soundName];
        }
        else
        {
            sources = new();
            _audioDic.Add(soundName, sources);
        }
        sources.Add(audioSource);

        audioSource.PlayOneShot(audioClip);
    }

    public void OnResumeop(string soundName)
    {
        if (_audioDic.ContainsKey(soundName))
        {
            var sources = _audioDic[soundName];
            foreach (var source in sources)
            {
                if (source.clip != null)
                {
                    source.Play();
                }
            }
        }
    }

    public void OnStop(string soundName)
    {
        if (_audioDic.ContainsKey(soundName))
        {
            var sources = _audioDic[soundName];
            foreach (var source in sources)
            {
                if (source.clip != null)
                {
                    source.Stop();
                }
            }
        }
    }

    private void CheckRecycle()
    {
        foreach (var sources in _audioDic.Values)
        {
            for (var i = sources.Count - 1; i > -1; --i)
            {
                var source = sources[i];
                if (source.clip)
                {
                    if (source.time >= source.clip.length || !source.isPlaying)
                    {
                        _audioPool.Add(source);
                        sources.RemoveAt(i);
                        source.Stop();
                        source.time = 0;
                        source.clip = null;
                    }
                }
                else
                {
                    _audioPool.Add(source);
                    sources.RemoveAt(i);
                    source.Stop();
                    source.time = 0;
                    source.clip = null;
                }
            }
        }
    }

    private AudioClip GetAudioClip(string soundName)
    {
        var clip = ResManager.GetInstance().GetAssetSync<AudioClip>(GameConfig.yooPackageName, GroupType.Sound, soundName);
        if (clip == null)
        {
            Debug.LogError("not found sound: " + name);
        }

        return clip;
    }

    private AudioSource CreateAudioSource(string soundName)
    {
        AudioSource audioSource;
        if (_audioPool.Count > 0)
        {
            audioSource = _audioPool[0];
            _audioPool.RemoveAt(0);
        }
        else
        {
            audioSource = (new GameObject()).AddComponent<AudioSource>();
            audioSource.gameObject.transform.parent = transform;
        }

        return audioSource;
    }

}
