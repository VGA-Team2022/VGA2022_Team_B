using UnityEngine;

public enum DogType
{
    Outrun,//���蔲����
    Stop//�ˑR�~�܂�
}

public class EnemyDogScript : MonoBehaviour
{
    [Tooltip("���̎��"), SerializeField] private DogType _dogType = DogType.Outrun;

    [Tooltip("�~�܂錢���ǂ��Ŏ~�܂邩�̒l����"), SerializeField, Range(0f, 10f)] private float _stopRange = 1;

    /// <summary>�G�l�~�[�̃X�s�[�h</summary>
    private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    public EnemyInstansManager EnemyInstansManager { get => _enemyInstansManager; set => _enemyInstansManager = value; }

    private StageMove _stageMove;


    /// <summary>�G�l�~�[�̎��ړ�����</summary>
    private float _startPosX = 0;

    /// <summary>Animation</summary>
    private Animator _anim = default;

    /// <summary>�����ʒu��x�������̒l���̔���</summary>
    private bool isSpawnNegativeX = false;
    /// <summary>�~�܂錢�̍��锻��</summary>
    private bool _isStop = false;
    /// <summary>������Manager/// </summary>
    private EnemyInstansManager _enemyInstansManager;
    private bool _forwardFast = false;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _anim= this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>();
        Speed = _stageMove.KeepSpeed;
        _startPosX = transform.position.x;
        _isStop = false;

        if (this.gameObject.transform.position.x <= 0)//�����ʒu���O��菬�����̂�True
        {
            isSpawnNegativeX = true;

        }
        else 
        {
            isSpawnNegativeX = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (this.gameObject.transform.position.z <= -4 && this.gameObject.transform.position.z >= -7)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder= 8;
        }
        else if (this.gameObject.transform.position.z <= -6)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 15;
        }

        if (_dogType == DogType.Outrun)
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_SmallDog_Cry);
        }
        else 
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_BigDog_Cry);
        }

    }

    private void Update()
    {
        if (_isStop && isSpawnNegativeX)//���锻�肪���ɂȂ����犎�i�s�������E(������)�̌��̏ꍇ
        {
            Speed = -_stageMove.MoveSpeed;//�X�e�[�W�Ɠ����X�s�[�h�ɂ���
        }
        else if (_isStop && !isSpawnNegativeX)//���锻�肪���ɂȂ����犎�i�s��������(������)�̌��̏ꍇ
        {
            Speed = _stageMove.MoveSpeed;//�}�C�i�X�������X�e�[�W�Ɠ��������ƃX�s�[�h�ɂ���
        }

    }


    /// <summary>
    /// �G�l�~�[�̈ړ�
    /// </summary>
    void FixedUpdate()
    {

        switch (_dogType)
        {
            case DogType.Outrun:
                InvokeRepeating("SmallDogBreathPlayAudio", 0, 2f);
                //�X�^�[�g�ʒu�ɂ���Đi�s������Sprite�̌�����ς���
                if (!isSpawnNegativeX)//Player�̐i�s����(��ʉE�[���獶�[�Ɍ�����)���炭�鋓��
                {
                        //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                        this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
                }
                else
                {
                  this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
                    if (!_forwardFast && this.gameObject.transform.position.x >= 25)
                    {
                        EnemyInstansManager.Dog(gameObject);
                        _forwardFast = true;
                    }
                    
                }

                break;

            case DogType.Stop:
                InvokeRepeating("BigDogBreathPlayAudio", 0, 2f);
                //�X�^�[�g�ʒu�ɂ���Đi�s������Sprite�̌�����ς���
                if (!isSpawnNegativeX)//Player�̐i�s����(��ʉE�[���獶�[�Ɍ�����)���炭�鋓��
                {
                    //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

                    //������_stopRnge�l�n�_�ɓ�������~�܂�(Player�̏ꏊ)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        _isStop = true;//�A�j���[�V�����̍��锻��𐳂ɂ���
                    }
                    
                }
                else//Player�̌��������炭�鋓��
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

                    //������_stopRange�n�_�ɓ�������~�܂�(Player�̏ꏊ)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        _isStop = true;//�A�j���[�V�����̍��锻��𐳂ɂ���
                    }
                }


                break;
        }


        //��ʊO�ɍs�����������
        if (transform.position.x <= -30 || transform.position.x >= 30)
        {
            Destroy(this.gameObject);
        }

        if (_anim)
        {
            _anim.SetBool("isStop",_isStop);
        }
    }

    private void BigDogBreathPlayAudio()
    {
        Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_BigDog_Breath);
    }
    private void SmallDogBreathPlayAudio()
    {
        Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_SmallDog_Breath);           
    }

    //�����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            _isStop = true;
            if(collision.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(this.transform.position.x);
            }

        }
    }
}
