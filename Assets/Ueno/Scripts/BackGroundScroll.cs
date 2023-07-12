using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> UVスクロールで背景を動かす </summary>
public class BackGroundScroll : MonoBehaviour
{
    [Tooltip("UVスクロール対象のmaterial")]
    [SerializeField]
    private Material _targetMaterial = default;

    public Material TargetMaterial { get => _targetMaterial; set => _targetMaterial = value; }

    [Tooltip("x軸方向に動く速さ"), SerializeField]
    private float _scrollX = 1;

    [Tooltip("y軸方向に動く速さ"), SerializeField]
    private float _scrollY = 0;

    [Tooltip("y軸方向に動かす値"), SerializeField]
    private float _moveY = 0;

    [Tooltip("anim間隔"), SerializeField]
    private float _animDuration = 0.5f;

    /// <summary> 海ステージかどうかのフラグ </summary>
    private bool _isSea = false;

    private Vector2 _offset;

    private float _time;
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

        if (GameManager.StageType == StageType.SEA_DAYTIME ||
            GameManager.StageType == StageType.SEA_NIGHT)
        {
            _isSea = true;
        }
        GetComponent<MeshRenderer>().enabled = _isSea;

        if (SceneManager.GetActiveScene().name == Define.SCENENAME_HOME ||
            SceneManager.GetActiveScene().name == Define.SCENENAME_TITLE)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isSea)
        {
            _time += Time.deltaTime;
            _offset.x += MoveSpeed * SpeedRatio * Time.deltaTime;

            if (_animDuration <= _time)
            {
                _time = 0f;
                _offset.y = _isFlipSeaAnim ? _offset.y += _moveY : _offset.y -= _moveY;
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
