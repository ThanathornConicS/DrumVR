using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TracksManager : MonoBehaviour
{
    public AudioClip[] clips;

    [SerializeField]
    private int currentTrack;
     [SerializeField]
    private bool isPause;
    private AudioSource source;

    public TMP_Text clipTitleText;
    public TMP_Text clipTimeText;

    private int fullLength;
    private int playTime;
    private int seconds;
    private int minutes;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        currentTrack = 0;
        isPause = false;

        source.clip = clips[currentTrack];

        ShowCurrentTitle();

        //PlayTrack();
    }

    public void PlayTrack()
    {
        if(source.isPlaying)
        {
            return;
        }

        currentTrack--;
        if(currentTrack < 0)
        {
            currentTrack = clips.Length - 1;
        }
        StartCoroutine("WaitForTrackEnd");
    }
    public void PauseTrack()
    {
        isPause = !isPause;

        if(isPause)
        {
            source.Pause();
        }
        else
        {
            source.Play();
        }
    }
    public void StopTrack()
    {
        StopCoroutine("WaitForTrackEnd");
        source.Stop();
    }
    

    IEnumerator WaitForTrackEnd()
    {
        while(source.isPlaying)
        {
            playTime = (int)source.time;
            ShowPlayTime();
            yield return null;
        }
        NextTrack();
    }

    public void NextTrack()
    {
        source.Stop();
        currentTrack++;
        if(currentTrack > clips.Length - 1)
        {
            currentTrack = 0;
        }

        source.clip = clips[currentTrack];
        source.Play();

        ShowCurrentTitle();

        StartCoroutine("WaitForTrackEnd");
    }
    public void PreviousTrack()
    {
        source.Stop();
        currentTrack--;
        if(currentTrack < 0)
        {
            currentTrack = clips.Length - 1;
        }

        source.clip = clips[currentTrack];
        source.Play();

        ShowCurrentTitle();

        StartCoroutine("WaitForTrackEnd");
    }
    public void MuteTrack()
    {
        source.mute = !source.mute;
    }

    private void ShowCurrentTitle()
    {
        clipTitleText.text = source.clip.name;
        fullLength = (int)source.clip.length;
    }
    private void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;

        clipTimeText.text = minutes + ":" + seconds.ToString("D2") + "/";
    }
}
