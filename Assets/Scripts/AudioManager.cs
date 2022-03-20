using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    [System.Serializable]
    public class CustomAudio 
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private float volume = 1;

        public float Volume { get { return volume; } }

        public void PlayAsMusic() 
        {
            Instance.SourceMusic.clip = clip;
            Instance.SourceMusic.Play();
            Instance.SourceMusic.volume = volume;
            Instance.MusicPlaying = this;
        }

        public void PlayAsSFX(float pitch = 1) 
        {
            Instance.SourceSFX.pitch = pitch;
            Instance.SourceSFX.PlayOneShot(clip, volume);
        }
    }

    public AudioSource SourceMusic;
    public AudioSource SourceSFX;

    [Header("Music")]
    [Audio] public CustomAudio MusicLevel;
    [Audio] public CustomAudio MusicMainMenu;

    [Header("SFX")]
    [Audio] public CustomAudio SfxButtonClick;
    [Audio] public CustomAudio SfxCharacterChange;
    [Audio] public CustomAudio SfxVictory;
    [Audio] public CustomAudio SfxJump, SfxSteam, SfxShrink, SfxGrow;
    [Audio] public CustomAudio SfxLever;
    [Audio] public CustomAudio SfxShowLevel;

    private bool MusicMuted;
    private CustomAudio MusicPlaying;

    public float MusicPlayingVolume { get { return MusicPlaying != null ? MusicPlaying.Volume : 0; } }

    void Update() 
    {
        if(SourceMusic.isPlaying) 
        {
            float MusicVolume = MusicMuted ? 0 : MusicPlayingVolume;
            SourceMusic.volume = Mathf.MoveTowards(SourceMusic.volume, MusicVolume, Time.deltaTime * 2f);
            // Debug.Log(SourceMusic.volume + ", " + MusicVolume);
        }
    }

    // Put this in its own function so it can be called by buttons
    public void PlayButtonClick() 
    {
        SfxButtonClick.PlayAsSFX();
    }

    public void FadeOutMusic() 
    {
        MusicMuted = true;
    }

    public void FadeInMusic() 
    {
        MusicMuted = false;
        SourceMusic.volume = 0;
    }

    public void PauseMusic() 
    {
        SourceMusic.Pause();
    }

    public void ResumeMusic() 
    {
        SourceMusic.Play();
    }
}
