using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee_cake : MonoBehaviour
{
    //Enemy_Dog�p
    //Enemy_Dog��Enemy�ɂ��Č���������Ȃ��ăX�|�i�[���琶�������
    //Enemy�͑S���^�C�v�ŕ������ق����y����
    [SerializeField, Tooltip("�ςޗp�P�[�L")] GameObject _cake = null;
    [SerializeField, Tooltip("�P�[�L�o���ꏊ")] GameObject _cakePos = null;

    //�g�p�l�p
    SpringJoint _joint = null;
    void Start()
    {
        _joint = GetComponent<SpringJoint>();
    }
    //������Enemy_Dog
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
