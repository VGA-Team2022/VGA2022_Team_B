using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> UV�X�N���[���Ŕw�i�𓮂��� </summary>
public class BackGroundScroll : MonoBehaviour
{
    [Tooltip("UV�X�N���[���Ώۂ�material")]
    [SerializeField]
    private Material _targetMaterial = default;

    public Material TargetMaterial { get => _targetMaterial; set => _targetMaterial = value; }

    [Tooltip("x�������ɓ�������"),SerializeField]
    private float _scrollX = 1;

    [Tooltip("y�������ɓ�������"), SerializeField]
    private float _scrollY = 0;

    [Tooltip("y�������ɓ������l"), SerializeField]
    private float _moveY = 0;

    [Tooltip("anim�Ԋu"), SerializeField]
    private float _durationTime = 0.5f;

    /// <summary> �C�X�e�[�W���ǂ����̃t���O </summary>
    private bool _isSea = false;

    private Vector2 _offset;

    private float time;
    private bool _isFlipSeaAnim = false;

    [HideInInspector] public float MoveSpeed = 0;
    [HideInInspector] public float SpeedRatio = 5;

    private void Awake()
    {
        if (_targetMaterial)
        {
            _offset = _targetMaterial.mainTextureOffset;
            _offset.y = 0;
            _targetMaterial.mainTextureOffset = _offset;
        }

        if (GameManager.GameStageNum == 1)
        {
            _isSea = true;
        }
        GetComponent<MeshRenderer>().enabled = _isSea;

        if (SceneManager.GetActiveScene().name == Define.SCENENAME_HOME)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }

        //if (_isSea)
        //{
        //    GetComponent<MeshRenderer>().enabled = false;
        //    enabled = false;
        //}
    }

    private void OnEnable()
    {
        //GetComponent<MeshRenderer>().enabled = true;
        //_isSea = true;
        //Debug.Log(GetComponent<MeshRenderer>().enabled);
    }

    private void FixedUpdate()
    {
        if (_isSea)
        {
            time += Time.deltaTime;
            _offset.x +=  MoveSpeed * SpeedRatio * Time.deltaTime;

            if (_durationTime <= time)
            {
                time = 0f;
                _offset.y = (_isFlipSeaAnim) ? _offset.y += _moveY : _offset.y -= _moveY;
            }
            _targetMaterial.mainTextureOffset = _offset;
        }
        else
        {
            DefaultMove();
        }
    }

    private void DefaultMove()
    {
        _offset.x += _scrollX * Time.deltaTime;
        _offset.y += _scrollY * Time.deltaTime;
        _targetMaterial.mainTextureOffset = _offset;
    }
}
