using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary> 鯨Enemy </summary>
public class EnemyWhale : MonoBehaviour
{
    [Tooltip("UIのlocalPosなので注意")]
    [SerializeField] private Vector3 _waveStartPos = Vector3.zero;

    [Min(1f)]
    [Tooltip("何秒かけて波が降りきるか")]
    [SerializeField] private float _moveWaveTime = 1f;
    [Min(1f)]
    [Tooltip("何秒妨害するか")]
    [SerializeField] private float _sabotageTime = 1f;

    [SerializeField] private RectTransform[] _waves = new RectTransform[3];

    private void Start()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].localPosition = _waveStartPos * (i + 1);
        }
    }

    private void Update()
    {
        //以下テスト
        if (Input.GetKeyDown(KeyCode.Space)) Squirting();
    }

    /// <summary> 潮吹き妨害 </summary>
    private void Squirting()
    {
        var sequenceOne = DOTween.Sequence();
        var sequenceTwo = DOTween.Sequence();
        var sequenceThree = DOTween.Sequence();

        //強制終了（仮）
        //StartCoroutine(KillSequence(sequenceOne, sequenceTwo, sequenceThree));

        for (int i = 0; i < 2; i++)
        {
            sequenceOne.Append(_waves[0].DOAnchorPos(-_waveStartPos, _moveWaveTime))
                       .AppendCallback(() =>
                       {
                           Debug.Log("妨害終了0");
                           _waves[0].localPosition = _waveStartPos;
                       });
            sequenceTwo.Append(_waves[1].DOAnchorPos(-_waveStartPos, _moveWaveTime * 1.5f))
                       .AppendCallback(() =>
                       {
                           Debug.Log("妨害終了1");
                           _waves[1].localPosition = _waveStartPos;
                       });
            sequenceThree.Append(_waves[2].DOAnchorPos(-_waveStartPos, _moveWaveTime * 2f))
                         .AppendCallback(() =>
                         {
                             Debug.Log("妨害終了2");
                             _waves[2].localPosition = _waveStartPos;
                         });
        }
    }

    /// <summary> 一定時間経ったら強制終了 </summary>
    private IEnumerator KillSequence(Tween tween1, Tween tween2, Tween tween3)
    {
        yield return new WaitForSeconds(_sabotageTime);

        Debug.Log("終了");
        tween1.Kill();
        tween2.Kill();
        tween3.Kill();
    }

    /// <summary> 水溜り </summary>
    private void WaterPuddle()
    {

    }
}
