﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject[] notes;      //生成させるノーツを格納
    private float[] _timing;        //生成タイミングを格納
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
    GameObject t_note;
    public float m_frameSecond;
    float noteSpeedSecond = 2.0f;   //何秒で判定ラインに到達する？
    float[] m_judgeDifference = new float[4];
    public float GetJudgeDifference(JUDGERES judgeRes)
    {
        return m_judgeDifference[(int)judgeRes];
    }
    //判定結果を表示するスプライト
    [SerializeField]
    Sprite[] ref_judgeSprite;
    //判定結果表示用オブジェクト
    [SerializeField]
    GameObject ref_judgeResultSprite;

	// Use this for initialization
	void Start () {
        //オブジェクトの参照処理
        _audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        judgeLine = GameObject.FindWithTag("JudgeLine");
        //csvからノーツデータの読み込みを行う
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
        //次のシーンへの遷移処理
        startButton.SetActive(false);
        //時間関連の初期化
        m_frameSecond = Time.deltaTime;
        SetJudgeDifference();
        //判定線までの距離を測定、動く速度の設定
        DistanceSpawnToJudge = Mathf.Sqrt((spawnLine - judgeLine.transform.position.y) * (spawnLine - judgeLine.transform.position.y));
        noteSpeed = DistanceSpawnToJudge / noteSpeedSecond;
        //音楽を再生
        _audioSource.Play();
        _startTime = Time.time;
        _isPlaying = true;
    }
    /*次のノーツをスポーンさせるかを判定する関数
     */
    void CheckNextNotes()
    {
        //次のノーツをスポーンさせる時間になっていた場合、ノーツをスポーンさせる
        while(_timing [_notesCount] + timeOffset - noteSpeedSecond < GetMusicTime() && _timing[_notesCount] != 0)
        {
            SpawnNotes(_lineNum[_notesCount],_timing[_notesCount]);
            _notesCount++;
        }
    }
    /* ノーツを生成する関数
     * 
     */
    void SpawnNotes(int num,float noteTime)
    {
        t_note = Instantiate(notes[num],
            new Vector3(-4.0f + (2.0f * num), spawnLine, 0),
            Quaternion.identity);
        t_note.GetComponent<Notes>().noteSecond = noteTime;
        
    }
    /*csvを読み込み、それらを変数として格納*/
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
    /*音楽の時間を表示*/
    public float GetMusicTime()
    {
        return Time.time - _startTime;
    }
    /*
     * 判定結果を基に処理する関数
     */
    public void NotesTimingFunc(int num,JUDGERES judge)
    {
        GameObject t_JudgeResult;   //判定結果を表示するためのゲームオブジェクト
        SpriteRenderer t_JudgeResultSprite;     //t_JudgeResultのSpriteRendererコンポーネント
        t_JudgeResult = Instantiate(ref_judgeResultSprite, 
                                    new Vector3(-4.0f + (2.0f * num), -1.0f, 0), Quaternion.identity);
        t_JudgeResultSprite = t_JudgeResult.GetComponent<SpriteRenderer>();
        //判定ごとの処理
        switch (judge)
        {
            case JUDGERES.PERFECT:
                t_JudgeResultSprite.sprite = ref_judgeSprite[(int)JUDGERES.PERFECT];
                _score += 3;
                break;
            case JUDGERES.GREAT:
                t_JudgeResultSprite.sprite = ref_judgeSprite[(int)JUDGERES.GREAT];
                _score += 2;
                break;
            case JUDGERES.GOOD:
                t_JudgeResultSprite.sprite = ref_judgeSprite[(int)JUDGERES.GOOD];
                _score++;
                break;
            case JUDGERES.POOR:
                t_JudgeResultSprite.sprite = ref_judgeSprite[(int)JUDGERES.POOR];
                break;
            default:
                break;
        }
        Debug.Log("Line:" + num + judge.ToString());
    }
    /*
     判定基準となるフレーム数をセットする
    */
    void SetJudgeDifference()
    {
        m_judgeDifference[(int)JUDGERES.PERFECT] = 2f;
        m_judgeDifference[(int)JUDGERES.GREAT] = 4f;
        m_judgeDifference[(int)JUDGERES.GOOD] = 6f;
        m_judgeDifference[(int)JUDGERES.POOR] = 8f;
    }
}
