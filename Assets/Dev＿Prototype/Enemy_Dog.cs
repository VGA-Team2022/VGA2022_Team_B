using UnityEngine;

public enum DogType
{
    Outrun,//���蔲����
    Stop//�ˑR�~�܂�
}

public class Enemy_Dog : MonoBehaviour
{
    [Tooltip("���̎��"), SerializeField] private DogType _dogType = DogType.Outrun;

    [Tooltip("�~�܂錢���ǂ��Ŏ~�܂邩�̒l����"), SerializeField, Range(0f, 10f)] private float _stopRange = 1;

    /// <summary>�G�l�~�[�̃X�s�[�h</summary>
    private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    private StageMove _stageMove;


    /// <summary>�G�l�~�[�̎��ړ�����</summary>
    private float _startPosX = 0;

    /// <summary>Animation</summary>
    private Animator _anim = default;

    /// <summary>�����ʒu��x�������̒l���̔���</summary>
    private bool isSpawnNegativeX = false;
    /// <summary>�~�܂錢�̍��锻��</summary>
    private bool isStop = false;

   
    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _anim= this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>();
        Speed = _stageMove._keepSpeed;
        _startPosX = transform.position.x;
        isStop = false;

        if (this.gameObject.transform.position.x <= 0)//�����ʒu���O��菬�����̂�True
        {
            isSpawnNegativeX = true;

        }
        else 
        {
            isSpawnNegativeX = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (this.gameObject.transform.position.z <= -6)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder= 15;
        }

    }

    private void Update()
    {
        if (isStop && isSpawnNegativeX)//���锻�肪���ɂȂ����犎�i�s�������E(������)�̌��̏ꍇ
        {
            Speed = -_stageMove.MoveSpeed;//�X�e�[�W�Ɠ����X�s�[�h�ɂ���
        }
        else if (isStop && !isSpawnNegativeX)//���锻�肪���ɂȂ����犎�i�s��������(������)�̌��̏ꍇ
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

                //�X�^�[�g�ʒu�ɂ���Đi�s������Sprite�̌�����ς���
                if (!isSpawnNegativeX)//Player�̐i�s����(��ʉE�[���獶�[�Ɍ�����)���炭�鋓��
                {
                        //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                        this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
                    }
                    else
                    {
                        this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
                    }

                break;

            case DogType.Stop:

                //�X�^�[�g�ʒu�ɂ���Đi�s������Sprite�̌�����ς���
                if (!isSpawnNegativeX)//Player�̐i�s����(��ʉE�[���獶�[�Ɍ�����)���炭�鋓��
                {
                    //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

                    //������_stopRnge�l�n�_�ɓ�������~�܂�(Player�̏ꏊ)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        isStop = true;//�A�j���[�V�����̍��锻��𐳂ɂ���
                    }
                    
                }
                else//Player�̌��������炭�鋓��
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

                    //������_stopRange�n�_�ɓ�������~�܂�(Player�̏ꏊ)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        isStop = true;//�A�j���[�V�����̍��锻��𐳂ɂ���
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
            _anim.SetBool("isStop",isStop);
        }
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
