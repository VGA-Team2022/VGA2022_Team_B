using UnityEngine;

public class WaterMelon : GimmickBase
{
    [Range(0f, 10f)]
    [Tooltip("スイカが止まる場所")]
    [SerializeField]
    private float _stopRange = 1;
    [SerializeField] private Sprite _suikaImage = null;

    private Animator _animator= null;
    private float _moveSpeed = 1f;
    private bool _isSpawnNegativeX = false;
    private bool _isStop = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _moveSpeed = StageMovement.KeepSpeed;
        SoundManager.InstanceSound.PlayerMoveSE(SoundManager.SE_Type.Enemy_Rooling);

        _isSpawnNegativeX = transform.position.x <= 0;
        //移動方向の初期設定
        if (_isStop && _isSpawnNegativeX)
        {
            _moveSpeed = -StageMovement.MoveSpeed;
        }
        else if (_isStop && !_isSpawnNegativeX)
        {
            _moveSpeed = StageMovement.MoveSpeed;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary> 横移動（CoconutMove.csに準拠） </summary>
    private void Movement()
    {
        if (!_isSpawnNegativeX)
        {
            transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0);

            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * _moveSpeed, 0);

            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }
        }

        if (transform.position.x <= -30f || transform.position.x >= 30f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obon obon))
        {
            _animator.enabled = false;
            transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = _suikaImage;
        }
    }
}
