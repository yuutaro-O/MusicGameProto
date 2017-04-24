using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTimingMaker : MonoBehaviour {
    private AudioSource _audioSource;
    private float _startTime = 0;
    private CSVWriter _CSVWriter;

    private bool _isPlaying = false;
    public GameObject startButton;
    private GameManager ref_GameManager;

	// Use this for initialization
	void Start () {

        _audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        _CSVWriter = GameObject.Find("CSVWriter").GetComponent<CSVWriter>();
        ref_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}

    public void StartMusic()
    {
        startButton.SetActive(false);
        _audioSource.Play();
        _startTime = ref_GameManager.startTime;
        _isPlaying = true;
    }

    public void DetectKeys()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            WriteNotesTiming(0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            WriteNotesTiming(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteNotesTiming(2);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            WriteNotesTiming(3);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            WriteNotesTiming(4);
        }
    }
    void WriteNotesTiming(int num)
    {
        Debug.Log(GetTiming());
        _CSVWriter.WriteCSV(GetTiming().ToString() + "," + num.ToString());
    }

    float GetTiming()
    {
        return Time.time - _startTime;
    }

    public void ChangeCSVFilepass(string filePass)
    {
        _CSVWriter.ChangeFilename(filePass);
    }
}
