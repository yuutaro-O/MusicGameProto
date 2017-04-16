using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputer : MonoBehaviour {
    Touch m_dummyTouch;
    bool m_isEnableDummyTouch;
    Touch[] m_touchDatas;
    public bool IsEnableDummyTouch {
        get
        {
            return m_isEnableDummyTouch;
        }
    }
    // Use this for initialization
    void Awake() {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                m_isEnableDummyTouch = true;
                m_touchDatas = new Touch[1];
                break;
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                m_isEnableDummyTouch = false;
                break;
            default:
                break;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (IsEnableDummyTouch == true)
        {
            MouseToTouchInput();
            m_touchDatas[0] = m_dummyTouch;
        }
        else
        {
            m_touchDatas = Input.touches;
        }

		foreach(Touch touchData in m_touchDatas)
        {
            Debug.Log("touch");
            Debug.Log("touchPosition = " + touchData.position);
        }
            
	}

    void MouseToTouchInput()
    {
        
        Vector2 t_diffPosition;
        
        if (Input.GetMouseButton(0) == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_dummyTouch.phase = TouchPhase.Began;
            }
            else
            {
                t_diffPosition.x = Input.mousePosition.x;
                t_diffPosition.y = Input.mousePosition.y;

                if (m_dummyTouch.position == t_diffPosition)
                {
                    m_dummyTouch.phase = TouchPhase.Stationary;
                }
                else
                {
                    m_dummyTouch.phase = TouchPhase.Moved;
                }
            }
            
            m_dummyTouch.position = Input.mousePosition;
        }
    }
}
