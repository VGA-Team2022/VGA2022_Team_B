using UnityEngine;
using UnityEngine.InputSystem;

public class StageMove : MonoBehaviour
{
    [Header("Pram")]
    [Tooltip("移動速度")]
    [SerializeField] private float _moveSpeed = 5f;
    /// <summary>UVスクロール速度が速いので調整の為</summary>
    [Tooltip("速度調整")]
    [SerializeField] private float _speedRatio = 0.1f;
    [SerializeField] private Obon _obon;
    [SerializeField] float _gyroSpeed = 1.2f;
    [SerializeField] private BackGroundScroll _waveObjectScroll;

    /// <summary>停止する時にSpeedの値を取っておく</summary>
    [HideInInspector] public float KeepSpeed;

    private Material _targetMaterial;
    private StageTypeChange _stageTypeChange;

    private Vector2 _offset;

    private bool _isSetMaterial = false;

    public float MoveSpeed => _moveSpeed;
    public float SpeedRatio => _speedRatio;

    private void Start()
    {
        _stageTypeChange = GetComponent<StageTypeChange>();
        _waveObjectScroll = _waveObjectScroll.gameObject.GetComponent<BackGroundScroll>();
        
        KeepSpeed = _moveSpeed;
        _moveSpeed = 0;
        
        _obon = _obon.gameObject.GetComponent<Obon>();
        _isSetMaterial = false;
    }

    private void Update()
    {
        if (!_isSetMaterial)
        {
            _targetMaterial = _stageTypeChange.CurrentMaterial;
            _offset = _targetMaterial.mainTextureOffset;
            _isSetMaterial = true;
        }

        if (!GameManager.IsAppearClearObj)
        {
            StickMove();

            _offset.x +=(GameManager.GameStageNum != 1)? _moveSpeed * SpeedRatio * Time.deltaTime : 0;
            _waveObjectScroll.SpeedRatio = SpeedRatio;
            _waveObjectScroll.MoveSpeed = _moveSpeed;
           
            _targetMaterial.mainTextureOffset = _offset;
        }
        else
        {
            _moveSpeed = 0;
        }
    }

    private void StickMove()
    {
        // 現在のゲームパッド情報
        var current = Gamepad.current;

        // ゲームパッド接続チェック
        if (current == null)
            return;

        // 左スティック入力取得
        var leftStickValue = current.leftStick.ReadValue();

        //左には動かない
        if (leftStickValue.x < 0)
        {
            return;
        }
        else if (leftStickValue.x > 0f)
        {
            GameManager.IsStop = false;
            //transform.Translate(-(leftStickValue.x * MoveSpeed * Time.deltaTime), 0, 0);
            _moveSpeed = KeepSpeed;

        }
        else if (leftStickValue.x == 0)
        {
            GameManager.IsStop = true;
            //AudioManager.Instance.CriAtomPlay(CueSheet.SE, "SE_player_footsteps1");
            _moveSpeed = 0;
        }

        //歩いたら傾いてる方に傾くようにした
        if(_obon.Movement >= 0)
        {
            _obon.MisalignmentOfSweetsCausedByMovement(leftStickValue.x * _gyroSpeed);
        }
        else if(_obon.Movement < 0)
        {
            _obon.MisalignmentOfSweetsCausedByMovement(-leftStickValue.x * _gyroSpeed);
        }
    }

    public void OnTapAdvance()
    {
        _moveSpeed = KeepSpeed;
    }
    public void ExitTapAdvance()
    {
        _moveSpeed = 0;
    }
}
