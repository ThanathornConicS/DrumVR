using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TracksManager : MonoBehaviour
{
    [Header("Audio Clip")]
    public AudioClip[] clips;

    [Header("UI Elements")]
    public TMP_Text clipTitleText;
    public TMP_Text clipTimeText;
    public TMP_Dropdown dropdownList;

    [Header("Debugging Field")]
    [SerializeField]
    private int currentTrack;
    [SerializeField]
    private bool isPause;
    private AudioSource source;

    private int fullLength;
    private int playTime;
    private int seconds;
    private int minutes;

    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        currentTrack = 0;
        isPause = false;

        source.clip = clips[currentTrack];

        dropdownList.options.Clear();
        for(int i = 0; i < clips.Length; i++)
        {
            dropdownList.options.Add(new TMP_Dropdown.OptionData() {text = clips[i].name});
        }

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
            StopCoroutine("WaitForTrackEnd");
        }
        else
        {
            source.Play();
            StartCoroutine("WaitForTrackEnd");
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
            //ShowPlayTime();
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
    public void PlaySpecifiedTrack(int idx)
    {
        source.Stop();

        source.clip = clips[idx];
        source.Play();

        ShowCurrentTitle();

        StartCoroutine("WaitForTrackEnd");
    }
    public void MuteTrack()
    {
        source.mute = !source.mute;
    }

    public void OnDropdownValueChanged(TMP_Dropdown change)
    {
        Debug.Log("Indx: " + change.value);

        currentTrack = change.value;
        //PlaySpecifiedTrack(currentTrack);
        if(source.isPlaying)
        {
            StopTrack();
        }
    }

    private void ShowCurrentTitle()
    {
        clipTitleText.text = source.clip.name;
        fullLength = (int)source.clip.length;
    }
}
