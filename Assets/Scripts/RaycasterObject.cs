using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterObject : MonoBehaviour {
    [SerializeField]
    float m_RaycastDistance;
    [SerializeField]
    LayerMask m_LayerMask;
    LineRenderer m_LineRenderer;
    GameManager ref_gameManager;

    RaycastHit t_hit;

    // Use this for initialization
    void Start () {
        m_LineRenderer = GetComponent<LineRenderer>();
        ref_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
	}
    /*RaycastFromThis()
     * 戻り値     ：RaycastHit レイキャスト結果
     * 動作       ：レイキャストをこのオブジェクトの座標から行う。
     *            ：射出方向を前方ベクトルに固定。
     */
    public RaycastHit RaycastFromThis()
    {
        Physics.Raycast(transform.position, Vector3.forward,out t_hit,m_RaycastDistance,m_LayerMask);
        RaycastLineUpdate(transform.position, transform.position + (Vector3.up * m_RaycastDistance));
        return t_hit;
    }
    /*Raycast(Vector3 rotation)
     * 引数       ：Vector3 レイキャストを飛ばしたい方向の方向ベクトル
     * 戻り値     ：RaycastHit レイキャスト結果
     * 動作       ：レイキャストをこのオブジェクトの座標から行う。
     *            ：方向指定可能
     */
    public RaycastHit Raycast(Vector3 rotation)
    {
        Physics.Raycast(transform.position, rotation, out t_hit, m_RaycastDistance, m_LayerMask);
        RaycastLineUpdate(transform.position, transform.position + (m_RaycastDistance * rotation));
        return t_hit;
    }
    /*Raycast(Vector3 position,Vector3 rotation,bool isCustomPosition = true)
     * 引数       ：Vector3 レイキャストを飛ばす始点
     *              Vector3 レイキャストを飛ばしたい方向の方向ベクトル
     *              bool y座標の位置のみ、このオブジェクトの設置位置から射出するか？
     * 戻り値     ：RaycastHit レイキャスト結果
     * 動作       ：レイキャストを指定座標と方向から行う。
     */
    public RaycastHit Raycast(Vector3 position,Vector3 rotation,bool isCustomPosition = true)
    {
        
        if(isCustomPosition == true)
        {
            position.y = transform.position.y;
        }
        Physics.Raycast(position, rotation, out t_hit, m_RaycastDistance, m_LayerMask);
        RaycastLineUpdate(position,position  + (m_RaycastDistance * rotation));
        return t_hit;
    }
    /*void RaycastLineUpdate(Vector3 startPosition,Vector3 endPosition)
     * 引数       ：Vector3 レイキャスト始点
     *              Vector3 レイキャスト終点
     * 戻り値     ：なし
     * 動作       ：レイキャストを可視化したものの位置を更新
     */
    void RaycastLineUpdate(Vector3 startPosition,Vector3 endPosition)
    {
        Vector3[] t_Positions = { startPosition, endPosition };
        
        m_LineRenderer.SetPositions(t_Positions);
    }

}
