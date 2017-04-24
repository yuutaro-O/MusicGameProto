using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputer : MonoBehaviour {
    Touch m_dummyTouch;
    bool m_isEnableDummyTouch;
    Touch[] m_touchDatas;
    Camera ref_MainCamera;
    
    GameObject m_laycasterObject;
    RaycasterObject m_raycasterObjectComponent;
    [SerializeField]
    GameObject Pre_laycasterObject;

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
            //エディタ起動の場合
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
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
        ref_MainCamera = Camera.main;
        m_laycasterObject = Instantiate(Pre_laycasterObject);
        m_raycasterObjectComponent = m_laycasterObject.GetComponent<RaycasterObject>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (IsEnableDummyTouch == true)
        {
            MouseToTouchInput();
            
        }
        else
        {
            m_touchDatas = Input.touches;
        }

		foreach(Touch touchData in m_touchDatas)
        {
            if (touchData.phase == TouchPhase.Began)
            {
                Vector3 t_touchdataPosition;
                RaycastHit t_raycastHit;
                t_touchdataPosition = ref_MainCamera.ScreenToWorldPoint(new Vector3(touchData.position.x, touchData.position.y, 10));
                t_raycastHit = m_raycasterObjectComponent.Raycast(t_touchdataPosition, Vector3.up);
                if (t_raycastHit.collider != null)
                {
                    t_raycastHit.rigidbody.gameObject.GetComponent<Notes>().Judge();
                }
                Debug.Log(t_raycastHit.rigidbody);
                Debug.Log("touch");
                Debug.Log("touchPosition = " + touchData.position);
            }
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
                m_touchDatas = new Touch[1];
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
            m_touchDatas[0] = m_dummyTouch;
        }
        else
        {
            if(m_touchDatas.Length == 1)
            {
                m_touchDatas = new Touch[0];
            }
        }
    }
}
