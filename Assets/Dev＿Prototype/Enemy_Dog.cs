using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DogType
{
    Outrun,//���蔲����
    Stop//�ˑR�~�܂�
}

public class Enemy_Dog : MonoBehaviour
{
    [Tooltip("���̎��"), SerializeField] private DogType _dogType = DogType.Outrun;

    /// <summary>�G�l�~�[�̃X�s�[�h</summary>
    private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    private StageMove _stageMove;

    /// <summary>�~�܂錢�̎~�܂��Ă��鎞��</summary>
    private int _stopTime = 2;

    /// <summary>�G�l�~�[�̎��ړ�����</summary>
    private float _startPosX = 0;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager_Yasiki").GetComponent<StageMove>();
        Speed = _stageMove.MoveSpeed;
        _startPosX = transform.position.x;
    }

    /// <summary>
    /// �G�l�~�[�̈ړ�
    /// </summary>
    void FixedUpdate()
    {
        switch (_dogType)
        {
            case DogType.Outrun:

                    //�X�^�[�g�ʒu�ɂ���Đi�s������ς���
                    if (_startPosX >= 0)
                    {
                        this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
                    }
                    else
                    {
                        this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
                    }

                break;

            case DogType.Stop:

                //�X�^�[�g�ʒu�ɂ���Đi�s������ς���
                if (_startPosX >= 0)//Player�̐i�s�������炭�鋓��
                {
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

                    //������-1�`1�l�n�_�ɓ�������~�܂�(Player�̏ꏊ)
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDog(_stopTime));
                    }
                    
                }
                else//Player�̌��������炭�鋓��
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

                    //������-1�`1�l�n�_�ɓ�������~�܂�(Player�̏ꏊ)
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDog(_stopTime));
                    }
                }


                break;
        }


        //��ʊO�ɍs�����������
        if (transform.position.x <= -30 || transform.position.x >= 30)
        {
            Destroy(this.gameObject);
        }
    }


    //�~�܂錢
    private IEnumerator StopDog(float time)
    {
        Speed = 0;
        yield return new WaitForSeconds(time);
        Speed = _stageMove.MoveSpeed;
    }


    //�����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("���C�h�u�����������������������v");
        }
    }
}
