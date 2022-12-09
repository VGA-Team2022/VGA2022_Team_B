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
    [Tooltip("�P�[�L�o���ꏊ")] 
    GameObject _cakePos = null;

    StageMove _stageMove;
    bool _cakeInstance = false;
    GameObject _obonObj;

    void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        var gimmickManaget = GameObject.Find("GimmickManager").GetComponent<Gimmickmanager>();
        _cakePos = gimmickManaget.gameObject.transform.GetChild(1).gameObject;
        _obonObj = GameObject.Find("ObonPos");
    }
    private void FixedUpdate()
    {
        gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
        if(this.gameObject.transform.position.x <= 0 && _cakeInstance == false)
        {
            var cake = Instantiate(_cake, _cakePos.transform);
            cake.transform.parent = _obonObj.transform;
            _cakeInstance = true;
        }
        if(gameObject.transform.position.x <= -47)
        {
            gameObject.SetActive(false);
        }
    }
}
