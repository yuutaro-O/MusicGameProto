using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterObject : MonoBehaviour {
    [SerializeField]
    float m_RaycastDistance;
    [SerializeField]
    LayerMask m_LayerMask;
    LineRenderer m_LineRenderer;

    RaycastHit t_hit;

    // Use this for initialization
    void Start () {
        m_LineRenderer = GetComponent<LineRenderer>();
	}

    public RaycastHit RaycastFromThis()
    {
        Physics.Raycast(transform.position, Vector3.up,out t_hit,m_RaycastDistance,m_LayerMask);
        RaycastLineUpdate(transform.position, transform.position + (Vector3.up * m_RaycastDistance));
        return t_hit;
    }

    public RaycastHit Raycast(Vector3 rotation)
    {
        Physics.Raycast(transform.position, rotation, out t_hit, m_RaycastDistance, m_LayerMask);
        RaycastLineUpdate(transform.position, transform.position + (m_RaycastDistance * rotation));
        return t_hit;
    }

    public RaycastHit Raycast(Vector3 position,Vector3 rotation)
    {
        Physics.Raycast(position, rotation, out t_hit, m_RaycastDistance, m_LayerMask);
        RaycastLineUpdate(position,position  + (m_RaycastDistance * rotation));
        return t_hit;
    }

    void RaycastLineUpdate(Vector3 startPosition,Vector3 endPosition)
    {
        Vector3[] t_Positions = { startPosition, endPosition };
        
        m_LineRenderer.SetPositions(t_Positions);
    }

}
