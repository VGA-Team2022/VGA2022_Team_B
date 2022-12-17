using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obon : MonoBehaviour
{
    [Tooltip("�v���C���[���Q�[���J�n���Ɏ����Ă��邨�َq�̔z��"), SerializeField]
    private GameObject[] _startOkasis;

    [Tooltip("�v���C���[�̃A�j���[�V�����Ǘ��N���X")]
    public PlayerAnimControl _playerAnim;

    [Tooltip("�v���C���[���v���C���Ɏ����Ă��邨�َq�̔z��")]
    private List<GameObject> _okasis = new List<GameObject>();

    [Tooltip("�v���C���[���Q�[���J�n���Ɏ����Ă��邨�َq�̔z��"), SerializeField]
    private int[] _int;


    private float _zure;

    private float _movement;

    [HideInInspector]
    public bool _sweetsFall = false;

    float h;
    float n = 1;

    //////////////���u��/////////////////
    private static List<GameObject> _staticOkasis;
    private static bool _staticSweetsFall;

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
            if (_okasis.Count == 0)
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

        _staticOkasis = _okasis;
        _staticSweetsFall = _sweetsFall;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("tamarinoKinntamari");
            Hit(this.transform.position.x - 1);
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
    public void SweetsAdd(GameObject gameObjects)
    {
        if (_okasis.Count == 0)
        {
            _okasis.Add(gameObjects);
            _okasis[0].transform.position = this.transform.position;//�v�C��
        }
        else
        {
            _okasis.Add(gameObjects);
            _okasis[_okasis.Count - 1].transform.position = _okasis[_okasis.Count - 2].GetComponent<Sweets>().NextPos.position;
        }
    }

    public void MisalignmentOfSweetsCausedByMovement(float stickX)
    {
        Movement += 0.0005f * stickX;////////�ϐ��ɂ��Ă�by�ߋ��̉�
    }

    public void Hit(float hitPos)
    {
        if(this.transform.position.x > hitPos)
        {
            Zure += 0.1f;
        }
        else if(this.transform.position.x < hitPos)
        {
            Zure -= 0.1f;
        }


        if (!_sweetsFall && !_staticSweetsFall)//�܂��Q�[���I�[�o�[���ĂȂ��Ƃ�
        {
            foreach (GameObject okasis in _okasis)//�h�炷
            {
                if (okasis.TryGetComponent(out Sweets sweets))/////////////����Q�b�g�R���|�[�l���g����̂��邢����ŏ�����Sweets�^��List�ɂ���
                {
                    sweets.SwayAnim();
                }
            }
        }
    }

    public void GameOver()
    {
        if (!_sweetsFall && !_staticSweetsFall)
        {

            foreach (GameObject okasis in _okasis)
            {
                if(okasis.TryGetComponent(out Sweets sweets))
                {
                    sweets.Boom(100);//�}�W�b�N�i���o�[�łԂׂ�
                }
            }
            _sweetsFall = true;
        }
    }

    public static void OutSideGameOver()
    {
        if (!_staticSweetsFall)
        {

            foreach (GameObject okasis in _staticOkasis)
            {
                if (okasis.TryGetComponent(out Sweets sweets))
                {
                    sweets.Boom(50);//�}�W�b�N�i���o�[�łԂׂ�
                }
            }
            _staticSweetsFall = true;
        }
    }
}
