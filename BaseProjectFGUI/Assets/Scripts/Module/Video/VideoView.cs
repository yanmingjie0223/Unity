using FairyGUI;
using UnityEngine;
using UnityEngine.Video;

public class VideoView : BaseView
{

    private GButton _btnStart;
    private GButton _btnStop;
    private GLoader _mv;
    private VideoPlayer m_videoPlayer;
    private float m_maxValue = 0;

    public VideoView() : base(new() { "video", "common" }, "VideoView", ViewType.VIEW, ViewLayerType.MIDDLE_LAYER)
    {
    }

    protected override void OnInit()
    {
        _mv = contentPane.GetChild("mv").asLoader;

        _btnStart = contentPane.GetChild("btnStart").asButton;
        _btnStart.onClick.Add(StartVideo);
        _btnStop = contentPane.GetChild("btnStop").asButton;
        _btnStop.onClick.Add(StopVideo);

        var gameObject = GameObject.Find("Video Player");
        m_videoPlayer = gameObject.GetComponent<VideoPlayer>();
        if (m_videoPlayer == null)
        {
            m_videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (null != m_videoPlayer && m_videoPlayer.isPlaying)
        {
            m_maxValue = m_videoPlayer.frameCount / m_videoPlayer.frameRate;
            if (Mathf.Abs((int)m_videoPlayer.time - (int)m_maxValue) == 0)
            {
                m_videoPlayer.frame = (long)m_videoPlayer.frameCount;
                m_videoPlayer.Stop();
                Debug.Log("≤•∑≈ÕÍ≥…£°");
                return;
            }
            if (m_videoPlayer.isPrepared)
            {
                _mv.texture = new NTexture(m_videoPlayer.texture);
            }
        }
    }

    private void StartVideo()
    {
        if (m_videoPlayer != null && m_videoPlayer.clip != null)
        {
            FadeInOut(0, 1, 1.5f);
            m_videoPlayer.Play();
            return;
        }

        var videoClip = Resources.Load<VideoClip>("Videos/fyz");
        if (videoClip == null)
        {
            return;
        }

        m_videoPlayer.clip = videoClip;
        m_videoPlayer.playOnAwake = false;
        m_videoPlayer.isLooping = false;
        m_videoPlayer.waitForFirstFrame = true;
        m_videoPlayer.Prepare();
        m_maxValue = m_videoPlayer.frameCount / m_videoPlayer.frameRate;
        FadeInOut(0, 1, 1.5f);

        if (!m_videoPlayer.isPrepared)
        {
            m_videoPlayer.prepareCompleted -= OnPrepareFinished;
            m_videoPlayer.prepareCompleted += OnPrepareFinished;
        }
        else
        {
            m_videoPlayer.Play();
        }
    }

    private void StopVideo()
    {
        m_videoPlayer.Pause();
    }

    private void FadeInOut(float start, float end, float time)
    {
        var tween = GTween.To(start, end, time);
        tween.OnUpdate(() =>
        {
            if (m_videoPlayer != null)
            {
                m_videoPlayer.SetDirectAudioVolume(0, tween.value.x);
            }
        });
    }

    private void OnPrepareFinished(VideoPlayer player)
    {
        player.Play();
    }

}
