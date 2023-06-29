using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweets : MonoBehaviour
{
    [SerializeField]
    Transform _nextPos;

    

    [Tooltip("����ȏ㉡�ɂ͂ݏo�����痎����"), SerializeField]
    float _deadWidth;

    public GameObject _prevObj;

    private bool _tanma = true;

    private float _misalignmentDifference;//����̍���ɍs���΍s���قǑ傫��������̕ϐ�

    Obon obon;

    Rigidbody _rb;

    Animator _anim;

    public Transform NextPos
    {
        get
        {
            return _nextPos;
        }
        set
        {

        }
    }

    public float MisalignmentDifference
    {
        get
        {
            return _misalignmentDifference;
        }
        set
        {
            _misalignmentDifference = value;
        }
    }

    public bool Tanma
    {
        get
        {
            return _tanma;
        }
        set
        {
            _tanma = value;
        }
    }

    private void Start()
    {
        obon = GameObject.FindGameObjectWithTag("Obon").GetComponent<Obon>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if(!obon._sweetsFall && _tanma)
        {
            if (this.transform.position.x >= _prevObj.transform.position.x + _deadWidth || this.transform.position.x <= _prevObj.transform.position.x - _deadWidth)
            {
                //_rb.AddForce(Vector2.up * 10);//AddForce����ƕ���Ȃ�����AddForce�B���o�ɂ��g���\��;
                obon.GameOver();//Obon�N���X�̃Q�[���I�[�o�[�֐��̌Ăяo��
                obon._sweetsFall = true;
                Obon._staticSweetsFall = true;
            }
            else
            {
                this.transform.position = new Vector3(_prevObj.transform.position.x + (obon.Zure * _misalignmentDifference) - obon.Movement, this.transform.position.y, this.transform.position.z);
            }
        }


        //Debug.Log(_prevObj.transform.position.x - _deadWidth);

        //if (this.transform.position.x >= _prevObj.transform.position.x + _deadWidth / 2 
        //    || this.transform.position.x <= _prevObj.transform.position.x - _deadWidth / 2)
        //{
        //    obon._playerAnim.Abunaaaaaaai();
        //}

        try
        {
            if (this.transform.position.x >= _prevObj.transform.position.x + _deadWidth / 2
                || this.transform.position.x <= _prevObj.transform.position.x - _deadWidth / 2)
            {
                obon._playerAnim.Abunaaaaaaai();
            }
        }
        catch (NullReferenceException nullException)
        {
            obon._playerAnim.Abunaaaaaaai();
        }
    }




    public void PutOnSweets(GameObject gameObj)
    {
        gameObj.transform.position = _nextPos.position;
    }

    public void Boom(int power)
    {
        _prevObj = null;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;//Rigidbody�̃��b�N������
        _anim.enabled = false;
        this.transform.eulerAngles = new Vector3(0, 0,UnityEngine.Random.Range(-30,30));//���َq�̌����������_���ɕς���
        //transform.rotation = Quaternion.Euler(0, 0, 90);
        //this.transform.eulerAngles = new Vector3(90, 90, 90);//���َq�̌����������_���ɕς���
        _rb.AddForce(new Vector3(UnityEngine.Random.Range(-1, 1), 1,0) * power);//AddForce����ƕ���Ȃ�����AddForce�B���o�ɂ��g���\��;
    }

    public void SwayAnim()
    {
        _anim.Play("GuraGura");
    }
}
