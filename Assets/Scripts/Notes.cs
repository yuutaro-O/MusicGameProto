using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {
    float speed { get; set; }
    public int lineNum;
    private GameManager _gameManager { get; set; }

	// Use this for initialization
	void Start () {
        speed = -0.5f;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.down * 10 * Time.deltaTime;
        if(this.transform.position.y < -5.0f)
        {
            Debug.Log("false");
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Call");
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

    void CheckInput(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            _gameManager.GoodTimingFunc(lineNum);
            Destroy(this.gameObject);
        }
    }
}
