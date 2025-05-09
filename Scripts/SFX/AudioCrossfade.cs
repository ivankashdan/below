using System.Collections;
using UnityEngine;

public class AudioCrossfade : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public MusicTrack defaultTrack;
    public bool startWithDefault = true;
    [Space(10)]
    //public bool defaultIsSilent = false;
    public MusicTrack currentTrack;

    bool isPlayingOnSource1 = true;

    bool isBlending = false;
    bool isStopping = false;

    float baseMusicVolume;



    private void Awake()
    {
        baseMusicVolume = audioSource1.volume;

        audioSource1.ignoreListenerPause = true;
        audioSource2.ignoreListenerPause = true; //
    }

    private void Start()
    {
        if (!IsPlaying() && startWithDefault)
        {
            CrossfadeToDefault();
        }
    }

    public void CrossfadeToOnce(MusicTrack track)
    {
        StopAllCoroutines();
        StartCoroutine(CrossfadeRoutine(track));
    }

    public void CrossfadeToDefault(MusicTrack track)
    {
        defaultTrack = track;
        StartCoroutine(CrossfadeRoutine(defaultTrack));
    }

    bool IsDefaultPlaying()
    {
        if (IsPlaying())
        {
            if (currentTrack == defaultTrack)
            {
                return true;
            }
        }
        return false;
    }

    public void CrossfadeToDefault()
    {
        StopAllCoroutines();

        //if (defaultIsSilent)
        //{
        //    StartCoroutine(FadeOutAllRoutine(duration));
        //}
        //else
        //{
            StartCoroutine(CrossfadeRoutine(defaultTrack));
        //}
    }

    public bool IsPlaying()
    {
        return (audioSource1.isPlaying || audioSource2.isPlaying) && !isStopping;
    }

    //public float GetPlayPosition()
    //{
    //    if (audioSource1.isPlaying)
    //    {
    //        return audioSource1.time;
    //    }
    //    else if (audioSource2.isPlaying)
    //    {
    //        return audioSource2.time;
    //    }
    //    return -1;
    //}

    public void SetDefault(MusicTrack track)
    {
        defaultTrack = track;
    }

    //public void SilenceDefault(bool value)
    //{
    //    defaultIsSilent = value;
    //}

    private IEnumerator CrossfadeRoutine(MusicTrack track)
    {
        MusicTrack previousTrack = currentTrack;
        currentTrack = track;

        isBlending = true;

        AudioSource fadingOutSource = isPlayingOnSource1 ? audioSource1 : audioSource2;
        AudioSource fadingInSource = isPlayingOnSource1 ? audioSource2 : audioSource1;

        fadingInSource.clip = track.audioClip;
        fadingInSource.loop = track.loop;
        fadingInSource.time = Mathf.Clamp(track.playPosition, 0, track.audioClip.length - 1);

        if (!fadingInSource.loop && track.playPosition >= track.audioClip.length) { }
        else
        {
            fadingInSource.Play();
        }

        float fadingOutVolume = fadingOutSource.volume;

        float elapsed = 0f;
        while (elapsed < track.fadeIn)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / track.fadeIn;
            fadingOutSource.volume = Mathf.Lerp(fadingOutVolume, 0f, t);
            fadingInSource.volume = Mathf.Lerp(0f, track.volume, t);

            if (previousTrack != null) previousTrack.playPosition = fadingOutSource.time;
            
            yield return null;
        }

        //if (fadingOutSource.isPlaying)
        //{
        //    previousTrack.playPosition = GetPlayPosition();
        //    fadingOutSource.Pause();
        //}

        fadingOutSource.Pause();

        isPlayingOnSource1 = !isPlayingOnSource1;
        isBlending = false;

        //faded = false;
    }

    //private IEnumerator FadeOutAllRoutine(float duration)
    //{
    //    //currentTrack = defaultTrack;

    //    isStopping = true;
    //    float source1VolumeStart = audioSource1.volume;
    //    float source2VolumeStart = audioSource2.volume;

    //    float elapsed = 0f;
    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.unscaledDeltaTime;
    //        float t = elapsed / duration;
    //        audioSource1.volume = Mathf.Lerp(source1VolumeStart, 0f, t);
    //        audioSource2.volume = Mathf.Lerp(source2VolumeStart, 0f, t);
    //        yield return null;
    //    }

    //    audioSource1.Pause();
    //    audioSource2.Pause();
    //    isStopping = false;

    //}

    //public void CrossfadeTo(AudioClip newClip, float duration = 1f)
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(CrossfadeRoutine(newClip, duration, baseMusicVolume));
    //}

    //public void CrossfadeTo(AudioClip newClip, float volume, float duration = 1f)
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(CrossfadeRoutine(newClip, duration, volume));
    //}

    //public void CrossFadeToOneShot(AudioClip newClip, float volume, float duration = 1f)
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(CrossfadeRoutine(newClip, duration, volume, true));
    //}

    //public void StopExisting(float duration = 2f )
    //{
    //    SilenceDefault(true);

    //    StopAllCoroutines();
    //    StartCoroutine(FadeOutAllRoutine(duration));
    //}
}
