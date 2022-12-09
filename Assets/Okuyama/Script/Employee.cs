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
    [Tooltip("ケーキ出現場所")] 
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
