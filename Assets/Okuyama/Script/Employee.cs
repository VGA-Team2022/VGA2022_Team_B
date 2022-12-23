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
    Gimmickmanager _gimmickmanager;
    GameObject _obonObj;
    bool _cakeInstance = false;

    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }
    public Gimmickmanager Gimmickmanager { get => _gimmickmanager; set => _gimmickmanager = value; }
    public GameObject ObonObj { get => _obonObj; set => _obonObj = value; }

    void Start()
    {
        _cakePos = ObonObj.gameObject.transform.GetChild(5).gameObject;
    }
    private void FixedUpdate()
    {
        gameObject.transform.position -= new Vector3(Time.deltaTime * StageMove.MoveSpeed, 0);
        if(this.gameObject.transform.position.x <= 0 && _cakeInstance == false)
        {
            var cake = Instantiate(_cake, _cakePos.transform);
            cake.transform.parent = ObonObj.transform;
            _cakeInstance = true;
        }
        if(gameObject.transform.position.x <= -47)
        {
            gameObject.SetActive(false);
        }
    }
}
