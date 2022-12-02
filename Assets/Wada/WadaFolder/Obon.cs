using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obon : MonoBehaviour
{
    [Tooltip("�v���C���[���Q�[���J�n���Ɏ����Ă��邨�َq�̔z��"), SerializeField]
    private GameObject[] _startOkasis;

    [Tooltip("�v���C���[���v���C���Ɏ����Ă��邨�َq�̔z��")]
    private List<GameObject> _okasis = new List<GameObject>();

    [Tooltip("�v���C���[���Q�[���J�n���Ɏ����Ă��邨�َq�̔z��"), SerializeField]
    private int[] _int;

    private float _zure;

    private float _movement;

    [HideInInspector]
    public bool _gameOver = false;


    float h;
    float n = 1;

    public float Zure
    {
        get
        {
            return _zure;
        }
        set
        {
            _zure = value;
        }
    }
    public float Movement
    {
        get
        {
            return _movement;
        }
        set
        {
            _movement = value;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < _startOkasis.Length; i++)
        {
            if(_okasis.Count == 0)
            {
                _okasis.Add(_startOkasis[0]);
                _okasis[0].transform.position = this.transform.position;
                _startOkasis[0].GetComponent<Sweets>().MisalignmentDifference = 0;//�ǉ��������َq�̗h��̍���ύX
            }
            else
            {
                _okasis.Add(_startOkasis[i]);
                _okasis[i].transform.position = _okasis[i - 1].GetComponent<Sweets>().NextPos.position;
                _startOkasis[i].GetComponent<Sweets>().MisalignmentDifference = 1 + (float)i / 10;//�ǉ��������َq�̗h��̍���ύX
            }
        }
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");

        //Zure += ((n += h * 0.00001f) * 100f);

        Zure += (h * 0.00001f) * n;
        n += Mathf.Abs(Zure * 1000);

        if (h == 0)
        {
            n = 1;
        }

        //MisalignmentOfSweetsCausedByMovement();
    }

    public void SweetsAdd(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (_okasis.Count == 0)
            {
                _okasis.Add(gameObjects[0]);
                _okasis[0].transform.position = this.transform.position;//�v�C��
            }
            else
            {
                _okasis.Add(gameObjects[i]);
                _okasis[i].transform.position = _okasis[i - 1].GetComponent<Sweets>().NextPos.position;
            }
        }
    }

    public void MisalignmentOfSweetsCausedByMovement(float stickX)
    {
        Movement += 0.0005f * stickX;////////�ϐ��ɂ��Ă�by�ߋ��̉�
    }

    public void GameOver()
    {
        if(!_gameOver)
        {
            Debug.Log("���˃J�X�@�U�R�@�ċz����Ȏ_�f�����������Ȃ�");
            _gameOver = true;
        }
    }
}
