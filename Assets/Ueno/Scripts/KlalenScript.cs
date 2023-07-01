using UnityEngine;

public class KlalenScript : GimmickBase
{
    /// <summary>���E�W�Q�pUI��Canvas�AGameObject</summary>
    private GameObject _klakenCanvas;
    private Animator _anim;

    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }
    private StageMove _stageMove;

    private string _actionAnimName = "SplashTrigger";

    /// <summary>�A�N�V�������������ǂ���</summary>
    private bool _isActionDone = false;

    public Vector3 AppeairPos { get => _appaeirPos; set => _appaeirPos = value; }
    private Vector3 _appaeirPos;
    void Start()
    {
        transform.position = _appaeirPos;

        _klakenCanvas = this.transform.GetChild(0).gameObject;
        //�~�X�������悤�ɍŏ���false�ɂ��Ă���
        _klakenCanvas.SetActive(false);

        _anim = GetComponent<Animator>();
   
        _stageMove = GetComponent<StageMove>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (this.transform.position.x <= 0 && !_isActionDone)
        {
            _anim.SetTrigger(_actionAnimName);
            _isActionDone = true;
        }
    }

    //�ȉ�2�̊֐��́A�I�u�W�F�N�g�o�����ɃI�u�W�F�N�g���J�������ɂ���A
    //�V�[���r���[�ł���\���ɂȂ�Ȃ��ƌĂ΂�Ȃ��A�����ӓ_�L
    private void OnBecameVisible()
    {
        Debug.Log("������");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("��ʊO�Ȃ̂ŕ`����I�����܂�");
        gameObject.SetActive(false);
    }

    /// <summary>�ړ�����</summary>
    private void FixedUpdate()
    {
        this.gameObject.transform.position -= new Vector3(Time.deltaTime * StageMove.MoveSpeed, 0);
    }
}