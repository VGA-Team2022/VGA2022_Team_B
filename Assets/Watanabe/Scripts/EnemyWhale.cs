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

    private Transform[] _waves = default;
    private StageMove _stage = default;

    private void Start()
    {
        Debug.Log("鯨出現");
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_Voice);

        _waves = new Transform[3];
        for (int i = 0; i < _waves.Length; i++)
        {
            var wave = Instantiate(_wavePrefab);
            wave.transform.SetParent(transform, false);

            _waves[i] = wave.GetComponent<Transform>();

            _waves[i].position = _waveStartPos * (i + 1);
        }

        _stage = FindObjectOfType<StageMove>();
    }

    private void Update()
    {
        //以下テスト
        if (Input.GetKeyDown(KeyCode.Space)) Squirting();
        if (Input.GetKeyDown(KeyCode.Tab)) SquirtingScale();
    }

    /// <summary> 潮吹き妨害(DOMove) </summary>
    public void Squirting()
    {
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_WaterSplash);

        var sequenceOne = DOTween.Sequence();
        var sequenceTwo = DOTween.Sequence();
        var sequenceThree = DOTween.Sequence();

        sequenceOne.Append(_waves[0].DOMove(-_waveStartPos, _moveWaveTime))
                   .AppendCallback(() =>
                   {
                       Debug.Log("妨害終了0");
                       _waves[0].position = _waveStartPos;
                   });
        sequenceTwo.Append(_waves[1].DOMove(-_waveStartPos, _moveWaveTime * 1.5f))
                   .AppendCallback(() =>
                   {
                       Debug.Log("妨害終了1");
                       _waves[1].position = _waveStartPos;
                   });
        sequenceThree.Append(_waves[2].DOMove(-_waveStartPos, _moveWaveTime * 2f))
                     .AppendCallback(() =>
                     {
                         Debug.Log("妨害終了2");
                         _waves[2].position = _waveStartPos;

                         //波妨害が一通り終わったら水溜りギミック開始(違うかも)
                         AppearPuddle();
                     });
    }

    /// <summary> 潮吹き妨害(DOScale) </summary>
    public void SquirtingScale()
    {
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_WaterSplash);

        var sequence = DOTween.Sequence();

        sequence.Append(_waves[0].DOScale(new Vector3(1f, 3f, 1f), _sabotageTime))
                .Join(_waves[0].DOMove(new Vector3(0f, 0f, 0f), _sabotageTime))
                .Append(_waves[0].DOScale(new Vector3(1f, 1f, 1f), _sabotageTime))
                .Join(_waves[0].DOMove(-_waveStartPos, _sabotageTime))
                .AppendCallback(() =>
                {
                    Debug.Log("妨害終了");
                    _waves[0].localScale = new Vector3(1f, 1f, 1f);
                    _waves[0].position = _waveStartPos;
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
                puddle.Init(_stage == null ? 0f : _stage.MoveSpeed);
            }
        }
    }
}
