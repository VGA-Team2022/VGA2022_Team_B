using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ケーキを積む使用人
/// </summary>
public class Employee : MonoBehaviour
{
    [SerializeField, Tooltip("ケーキ")] 
    GameObject _cake = null;
    [SerializeField, Tooltip("ケーキ出現場所")] 
    GameObject _cakePos = null;
    StageMove _stageMove;

    void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
    }
    private void FixedUpdate()
    {
        gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
        if(this.gameObject.transform.position.x < 0)
        {
            Instantiate(_cake, _cakePos.transform);
        }
    }
}
