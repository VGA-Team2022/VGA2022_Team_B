using UnityEngine;
using System.Collections;

public enum Dogs
{
    Stop, //�ꎞ��~����
    Run //�~�܂炸����
}

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Dogs _dogs = Dogs.Run;

    ///<summary> �G�l�~�[�̃X�s�[�h <summary>
    private float _speed = 2.0f;

    private float _stopTime = 2f;

    private int _startPosX = 0;

    void FixedUpdate()
    {
        switch(_dogs) //�G�l�~�[�̈ړ�
        {
            case Dogs.Run:
                if (_startPosX >= 0)
                {
                    SpriteRenderer children = GetComponentInChildren<SpriteRenderer>();
                    children.flipX = true;
                    transform.position -= new Vector3(Time.deltaTime * _speed, 0);
                }
                else
                {
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                break;

            case Dogs.Stop:
                if (_startPosX >= 0)
                {
                    //�����Ă錢
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                else//Player�̌���������
                {
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);

                    //�v���C���[�̑O��1�������ɓ������Ƃ��̋���
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDogTime(_stopTime));
                    }
                }
                break;
        }

        if (transform.position.x <= 30 || transform.position.x >= -30) 
        {
            Destroy(gameObject);
        }
    }

    /// <summary> �~�܂錢��~�� </summary>
    private IEnumerator StopDogTime(float time)
    {
        _speed = 0;
        yield return new WaitForSeconds(time);
    }
}
