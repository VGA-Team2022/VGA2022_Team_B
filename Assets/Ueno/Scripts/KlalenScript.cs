using UnityEngine;

/// <summary>
/// �C�X�e�[�W���E�W�Q�M�~�b�N
/// </summary>
public class KlalenScript : GimmickBase
{
    /// <summary>���E�W�Q�pUI��Canvas�AGameObject</summary>
    private GameObject _klakenCanvas;
    private Animator _anim;

    private string _actionAnimName = "SplashTrigger";

    /// <summary>�A�N�V�������������ǂ���</summary>
    private bool _isActionDone = false;

    private Vector3 _appearPos;
    public Vector3 AppearPos { get => _appearPos; set => _appearPos = value; }

    private void Start()
    {
        transform.position = _appearPos;

        _klakenCanvas = transform.GetChild(0).gameObject;
        //�~�X�������悤�ɍŏ���false�ɂ��Ă���
        _klakenCanvas.SetActive(false);

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
        /*Todo:�Ƃ肪�[��߂�
 */       if (this.transform.position.x <= 0 && !_isActionDone)
        {
            _anim.SetTrigger(_actionAnimName);
            SoundManager.InstanceSound.PlayerMoveSE(SoundManager.SE_Type.Enemy_ArtPaint_Milk);
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
        transform.position -= new Vector3(Time.deltaTime * StageMovement.MoveSpeed, 0);
    }
}
