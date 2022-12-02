using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �P�[�L��ςގg�p�l
/// </summary>
public class Employee : MonoBehaviour
{
    [SerializeField, Tooltip("�P�[�L")] 
    GameObject _cake = null;
    [SerializeField, Tooltip("�P�[�L�o���ꏊ")] 
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
