using UnityEngine;

public class WaterMelon : GimmickBase
{
    [Range(0f, 10f)]
    [Tooltip("スイカが止まる範囲")]
    [SerializeField] private float _stopRange = 1;
    [SerializeField] private Sprite _suikaImage = null;
    [Tooltip("移動速度の調整値")]
    [SerializeField] private float _multiplyValue = 2f;

    private Animator _animator= null;
    private float _moveSpeed = 1f;
    private bool _isSpawnNegativeX = false;
    //private bool _isStop = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        SoundManager.InstanceSound.PlayerMoveSE(SoundManager.SE_Type.Enemy_Rooling);

        _isSpawnNegativeX = transform.position.x <= 0;
        //移動方向の初期設定
        //if (_isStop)
        //{
        //    _moveSpeed = _isSpawnNegativeX ?
        //        -StageMovement.MoveSpeed : StageMovement.MoveSpeed;

        //    _moveSpeed *= _multiplyValue;
        //}
        _moveSpeed = _isSpawnNegativeX ?
                -StageMovement.MoveSpeed : StageMovement.MoveSpeed;

        _moveSpeed *= _multiplyValue;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary> 横移動（CoconutMove.csに準拠） </summary>
    private void Movement()
    {
        //if (!_isSpawnNegativeX)
        //{
        //    transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0);

        //    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
        //    {
        //        _isStop = true;
        //    }
        //}
        //else
        //{
        //    transform.position += new Vector3(Time.deltaTime * _moveSpeed, 0);

        //    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
        //    {
        //        _isStop = true;
        //    }
        //}
        //フラグによって移動方向を変える
        transform.position = _isSpawnNegativeX ?
            transform.position += new Vector3(Time.deltaTime * _moveSpeed, 0) :
            transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0);

        if (transform.position.x <= -30f || transform.position.x >= 30f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.TryGetComponent(out Obon obon))
        {
            //Animation再生を停止
            _animator.enabled = false;
            transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);

            //当たった際の演出を実行し、おぼんを揺らす
            transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = _suikaImage;
            SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Coconut);
            //_isStop = true;
            obon.Hit(transform.position.x);
        }
    }
}
