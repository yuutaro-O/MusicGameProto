using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject[] notes;
    private float[] _timing;
    private int[] _lineNum;

    public string filePass;
    private int _notesCount = 0;
    private int _score;

    private AudioSource _audioSource;
    private float _startTime = 0;

    public float timeOffset = -1;

    private bool _isPlaying = false;
    public GameObject startButton;
    public Text scoreText;

    public float spawnLine = 7.0f;
    GameObject judgeLine;
    float DistanceSpawnToJudge;
    public float noteSpeed;

	// Use this for initialization
	void Start () {
        _audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        judgeLine = GameObject.FindWithTag("JudgeLine");
        _timing = new float[1024];
        _lineNum = new int[1024];
        LoadCSV();
	}
    // Update is called once per frame
    private void Update()
    {
        if (_isPlaying)
        {
            CheckNextNotes();
            scoreText.text = _score.ToString();
        }
    }

    public void StartGame()
    {
        
        startButton.SetActive(false);
        DistanceSpawnToJudge = Mathf.Sqrt((spawnLine - judgeLine.transform.position.y) * (spawnLine - judgeLine.transform.position.y));
        noteSpeed = DistanceSpawnToJudge / 2.0f;
        _startTime = Time.time;
        _audioSource.Play();
        _isPlaying = true;
    }

    void CheckNextNotes()
    {
        while(_timing [_notesCount] + timeOffset - 2.0f < GetMusicTime() && _timing[_notesCount] != 0)
        {
            SpawnNotes(_lineNum[_notesCount]);
            _notesCount++;
        }
    }

    void SpawnNotes(int num)
    {
        Instantiate(notes[num],
            new Vector3(-4.0f + (2.0f * num), spawnLine, 0),
            Quaternion.identity);
    }

    void LoadCSV()
    {
        int i = 0, j;
        TextAsset csv = Resources.Load(filePass) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            for(j = 0; j < values.Length; j++)
            {
                _timing[i] = float.Parse(values[0]);
                _lineNum[i] = int.Parse(values[1]);
            }
            i++;
        }
    }

    float GetMusicTime()
    {
        return Time.time - _startTime;
    }

    public void GoodTimingFunc(int num)
    {
        Debug.Log("Line:" + num + "good!");
        Debug.Log(GetMusicTime());
        _score++;
    }
}
