using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {
    float speed { get; set; }

	// Use this for initialization
	void Start () {
        speed = -0.5f;
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
}
