using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee_cake : MonoBehaviour
{
    //Enemy_Dog用
    //Enemy_DogをEnemyにして犬だけじゃなくてスポナーから生成される
    //Enemyは全部タイプで分けたほうが楽かも
    [SerializeField, Tooltip("積む用ケーキ")] GameObject _cake = null;
    [SerializeField, Tooltip("ケーキ出現場所")] GameObject _cakePos = null;

    //使用人用
    SpringJoint _joint = null;
    void Start()
    {
        _joint = GetComponent<SpringJoint>();
    }
    //ここはEnemy_Dog
    void CackInstans()
    {
        Instantiate(_cake, _cakePos.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cake"))
        {
            _joint.connectedBody = collision.rigidbody;
        }
    }
}
