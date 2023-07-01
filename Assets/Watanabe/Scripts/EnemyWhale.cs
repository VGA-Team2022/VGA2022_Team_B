using DG.Tweening;
using UnityEngine;

/// <summary> 鯨Enemy </summary>
public class EnemyWhale : GimmickBase
{
    [Tooltip("波のPrefab")]
    [SerializeField] private GameObject _wavePrefab = default;
    [Tooltip("水溜りのPrefab")]
    [SerializeField] private GameObject _puddlePrefab = default;

    [Header("波妨害ギミックの調整値")]
    [SerializeField] private Vector3 _waveStartPos = Vector3.zero;
    [Min(1f)]
    [Tooltip("何秒かけて波が降りきるか")]
    [SerializeField] private float _moveWaveTime = 1f;
    [Min(1f)]
    [Tooltip("何秒妨害するか")]
    [SerializeField] private float _sabotageTime = 1f;

    [Header("水溜りギミックの調整値")]
    [Min(1)]
    [Tooltip("発生させる水溜りの数")]
    [SerializeField] private int _puddleNum = 1;

    private Transform _wave = default;
    private Vector3 _startPos = default;
    private StageMove _stage = default;
    private WhaleAnimType _animType = WhaleAnimType.None;
    private Animator _animator = default;

    private bool _isMove = false;

    private void Start()
    {
        _startPos = transform.position;
        _animator = GetComponent<Animator>();

        _isMove = true;

        Debug.Log("鯨出現");
        _animType = WhaleAnimType.Idle;
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_Voice);

        var wave = Instantiate(_wavePrefab);
        wave.transform.SetParent(transform, false);

        _wave = wave.GetComponent<Transform>();
        _wave.position = _waveStartPos;

        _stage = FindObjectOfType<StageMove>();
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            transform.position = _startPos - new Vector3(Time.deltaTime, 0f);

            if (transform.position.x <= 0f)
            {
                ChangeAnim(WhaleAnimType.Splash);
                _isMove = false;
            }
        }
    }

    /// <summary> 潮吹き妨害(DOMove) </summary>
    public void Squirting()
    {
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_WaterSplash);

        var sequence = DOTween.Sequence();

        sequence.Append(_wave.DOMove(-_waveStartPos, _moveWaveTime))
                .AppendCallback(() =>
                {
                    Debug.Log("妨害終了");
                    _wave.position = _waveStartPos;

                    //波妨害が一通り終わったら水溜りギミック開始(違うかも)
                    AppearPuddle();
                    _isMove = true;
                    gameObject.SetActive(false);
                });
    }

    /// <summary> 水溜りを発生させる </summary>
    public void AppearPuddle()
    {
        for (int i = 0; i < _puddleNum; i++)
        {
            var go = Instantiate(_puddlePrefab);
            if (go.TryGetComponent(out WaterPuddle puddle))
            {
                puddle.Init(_stage);
            }
        }
    }

    private void ChangeAnim(WhaleAnimType next)
    {
        if (_animType == next) return;

        if (next == WhaleAnimType.Splash)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isSplash", true);
        }
        _animType = next;
    }
}

public enum WhaleAnimType
{
    None,
    Idle,
    Splash,
}
