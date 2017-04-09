using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {
    float speed { get; set; }
    public int lineNum;
    private GameManager _gameManager { get; set; }
    private float m_noteSecond;
    public float noteSecond
    {
        get
        {
            return m_noteSecond;
        }
        set
        {
            m_noteSecond = value;
        }
    }

	// Use this for initialization
	void Start () {
        //speed = -0.5f;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        speed = _gameManager.noteSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(this.transform.position.y < -5.0f)
        {
            _gameManager.NotesTimingFunc(lineNum, JUDGERES.POOR);
            Destroy(this.gameObject);
        }
	}
    /*判定線に入った時に、キーが押されているかを検出
     * 
     */
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Call");
        switch (lineNum)
        {
            case 0:
                CheckInput(KeyCode.D);
                break;
            case 1:
                CheckInput(KeyCode.F);
                break;
            case 2:
                CheckInput(KeyCode.Space);
                break;
            case 3:
                CheckInput(KeyCode.J);
                break;
            case 4:
                CheckInput(KeyCode.K);
                break;
            default:
                break;
        }
    }
    /*入力を検出し、判定を行う
     * 
     */
    void CheckInput(KeyCode key)
    {
        float musicTime = _gameManager.GetMusicTime();
        float frameSecond = _gameManager.m_frameSecond;
        
        if (Input.GetKeyDown(key))
        {
            JUDGERES judge = JUDGERES.POOR;
            //押されるべきタイミングより遅く押されたか、同時の場合
            if (noteSecond >= musicTime) 
            {
                if (noteSecond <= musicTime + (_gameManager.m_frameSecond * _gameManager.GetJudgeDifference(JUDGERES.PERFECT)))    //parfect
                {
                    judge = JUDGERES.PERFECT;
                }
                else if (noteSecond <= musicTime + (_gameManager.m_frameSecond * _gameManager.GetJudgeDifference(JUDGERES.GREAT)))   //great
                {
                    judge = JUDGERES.GREAT;
                }
                else if (noteSecond <= musicTime + (_gameManager.m_frameSecond * _gameManager.GetJudgeDifference(JUDGERES.GOOD)))  //good
                {
                    judge = JUDGERES.GOOD;
                }
                else{
                    judge = JUDGERES.POOR;
                }
            }

            //押されるべきタイミングより早く押された場合
            else
            {
                if (noteSecond >= musicTime - (_gameManager.m_frameSecond * _gameManager.GetJudgeDifference(JUDGERES.PERFECT)))    //parfect
                {
                    judge = JUDGERES.PERFECT;
                }
                else if (noteSecond >= musicTime - (_gameManager.m_frameSecond * _gameManager.GetJudgeDifference(JUDGERES.GREAT)))   //great
                {
                    judge = JUDGERES.GREAT;
                }
                else if (noteSecond >= musicTime - (_gameManager.m_frameSecond * _gameManager.GetJudgeDifference(JUDGERES.GOOD)))  //good
                {
                    judge = JUDGERES.GOOD;
                }
                else
                {
                    judge = JUDGERES.POOR;
                }
            }

            _gameManager.NotesTimingFunc(lineNum,judge);
            Destroy(this.gameObject);
        }
    }
}
