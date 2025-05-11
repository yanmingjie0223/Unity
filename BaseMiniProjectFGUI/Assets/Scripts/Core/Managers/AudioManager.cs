using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    private readonly Dictionary<string, List<AudioSource>> _audioDic = new();
    private readonly List<AudioSource> _audioPool = new();

    public void OnPlay(string soundName, bool isLoop = false, bool isFadeIn = false, bool isFadeOut = false)
    {
        CheckRecycle();
        var audioClip = GetAudioClip(soundName);
        var audioSource = CreateAudioSource();
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

        audioSource.gameObject.GetComponent<AudioFader>().SetFade(isFadeIn, isFadeOut);

        audioSource.loop = isLoop;
        audioSource.Play();
    }

    public void OnResumeStop(string soundName)
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
                var fader = source.gameObject.GetComponent<AudioFader>();
                if (source.clip)
                {
                    if (source.time >= source.clip.length || !source.isPlaying || !source.loop)
                    {
                        _audioPool.Add(source);
                        sources.RemoveAt(i);
                        source.Stop();
                        source.time = 0;
                        source.clip = null;
                        fader.SetFade(false, false);
                    }
                }
                else
                {
                    _audioPool.Add(source);
                    sources.RemoveAt(i);
                    source.Stop();
                    source.time = 0;
                    source.clip = null;
                    source.loop = false;
                    fader.SetFade(false, false);
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

    private AudioSource CreateAudioSource()
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
            audioSource.gameObject.AddComponent<AudioFader>();
            audioSource.gameObject.transform.parent = transform;
        }

        return audioSource;
    }

}
