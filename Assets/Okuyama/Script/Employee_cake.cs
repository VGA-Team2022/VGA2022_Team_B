using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ケーキを積む使用人用ケーキ
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
        _obon.SweetsAdd(gameObject);//ここがうまく行ってない
        StartCoroutine(aa());
        
    }

    IEnumerator aa()//確認用
    {
        yield return new WaitForSeconds(1);
        _event.Invoke();
    }
}
