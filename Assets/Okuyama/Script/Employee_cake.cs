using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// �P�[�L��ςގg�p�l�p�P�[�L
/// </summary>
public class Employee_cake : MonoBehaviour
{
    [SerializeField]UnityEvent _event;
    Sweets _sweets;
    GameObject _obonPos;
    Obon _obon;

    private void Start()
    {
        _sweets = GetComponent<Sweets>();
        _obonPos = GameObject.Find("ObonPos");
        _sweets._prevObj = _obonPos;
        _obon = _obonPos.GetComponent<Obon>();
        _obon.SweetsAdd(gameObject);//���������܂��s���ĂȂ�
        StartCoroutine(aa());
        
    }

    IEnumerator aa()//�m�F�p
    {
        yield return new WaitForSeconds(1);
        _event.Invoke();
    }
}
