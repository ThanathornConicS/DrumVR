using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TracksManager : MonoBehaviour
{
    [Header("Audio Clip")]
    public string clipDirectory;
    [SerializeField]
    private List<string> filesInDir = new List<string>();
    public AudioClip[] clips;
    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 440;

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

        // Get all track from folder
        string[] files;
        files = Directory.GetFiles(clipDirectory);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".mp3") || files[i].EndsWith(".flac"))
            {
                filesInDir.Add(files[i].Replace(clipDirectory, ""));
            }
        }

        //Debug.Log("filesInDir Count: " + filesInDir.Count);
        clips = new AudioClip[filesInDir.Count];
        for (int i = 0; i < filesInDir.Count; i++)
        {
            clips[i] = AudioClip.Create(filesInDir[i], samplerate * 2, 1, samplerate, true, OnAudioRead);
        }

        // Set default track
        source.clip = clips[currentTrack];

        // Set dropdown list
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

    private void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate);
            position++;
            count++;
        }
    }
}
