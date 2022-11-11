using UnityEngine;
using System.Collections;

public enum Dogs
{
    Stop, //�ꎞ��~����
    Run //�~�܂炸����
}

/// <summary>
///�G�l�~�[�ړ�
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /*[Header("�G�l�~�[")]
    [Tooltip("�G�l�~�[�̃X�s�[�h"), SerializeField]*/

    ///<summary> �G�l�~�[�̃X�s�[�h <summary>
    private float _speed = 2.0f;

    private float _stopTime = 2f;

    private int _startPosX = 0;

    private StageMove _stageMoves;

    [Tooltip("���̎��"), SerializeField] private Dogs _dogs = Dogs.Run;

    private EnemyInstansManager _enemyInstansManager;


    /// <summary>�G�l�~�[�̎��ړ�����</summary>
    private int x = 0;

    /// <summary>
    /// �G�l�~�[�̈ړ�
    /// </summary>
    void FixedUpdate()
    {
        switch(_dogs)
        {
            case Dogs.Run:
                if (_startPosX >= 0)
                {
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * _speed, 0);
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                break;
            case Dogs.Stop:
                if (_startPosX == 0)
                {
                    //�����Ă錢
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                else//Player�̌���������
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * _speed, 0);

                    //�v���C���[�̑O��1�������ɓ������Ƃ��̋���
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDogTime(_stopTime));
                    }
                }
                if(transform.position.x <= 25)
                {
                    //��ʊO�ɍs������v���C���[�̂��郌�[������o���@
                    //�v���C���[�̃��[����Player��NowPos�v���p�e�B�擾���Ă���̂ŁA�Q�Ɖ�
                    _enemyInstansManager.Dog(this.gameObject);
                }
                break;
        }
        if (transform.position.x <= 30 || transform.position.x >= -30) 
        {
            Destroy(this.gameObject);
        }
    }

    //�~�܂錢��~��
    private IEnumerator StopDogTime(float time)
    {
        _speed = 0;
        yield return new WaitForSeconds(time);
        _speed = _stageMoves.MoveSpeed;
    }
}
