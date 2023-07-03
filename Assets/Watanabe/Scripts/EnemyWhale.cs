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

    [Header("Debug")]
    [SerializeField] private bool _isMove = false;

    private WhaleAnimType _currentAnimType = WhaleAnimType.None;
    private Animator _animator = default;
    private GameObject _wave = default;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _isMove = true;

        Debug.Log($"鯨出現 {transform.position}");
        _currentAnimType = WhaleAnimType.Idle;
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_Voice);

        _wave = Instantiate(_wavePrefab, _waveStartPos, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            transform.position -= new Vector3(Time.deltaTime * 2, 0);

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

        sequence.Append(_wave.transform.DOMove(-_waveStartPos, _moveWaveTime))
                .AppendCallback(() =>
                {
                    Debug.Log("妨害終了");
                    _wave.transform.position = _waveStartPos;

                    //波妨害が一通り終わったら水溜りギミック開始(違うかも)
                    AppearPuddle();
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
                puddle.Init(StageMovement);
            }
        }
    }

    private void ChangeAnim(WhaleAnimType next)
    {
        if (_currentAnimType == next) return;

        if (next == WhaleAnimType.Splash)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isSplash", true);
        }
        _currentAnimType = next;
    }
}

public enum WhaleAnimType
{
    None,
    Idle,
    Splash,
}
