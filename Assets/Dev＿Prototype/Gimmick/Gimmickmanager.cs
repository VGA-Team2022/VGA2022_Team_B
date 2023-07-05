using System;
using UnityEngine;

[Serializable]
class GimmickPrefabs
{
    [Tooltip("ギミックPrefab")]
    [SerializeField] private GameObject[] _gimmickPrefabs;

    public GameObject[] Gimmicks => _gimmickPrefabs;
}

/// <summary>
/// ギミック生成Manager
/// </summary>
public class GimmickManager : MonoBehaviour
{
    [Header("ケーキを追加する使用人は常に一番下に入れる")]
    [Tooltip("Scecn番号と同じElement番号にSceneに合ったPrefabを入れる")]
    [SerializeField] private GimmickPrefabs[] _sceneGimmick;
    [Tooltip("4レーン目")]
    [SerializeField] private Transform _appearLane4;
    [Tooltip("海レーン")]
    [SerializeField] private Transform _seaLane;
    [Tooltip("StageMove")]
    [SerializeField] private StageMove _stageMove;
    [Tooltip("おぼんObj")]
    [SerializeField] private GameObject _obonObj;
    [Tooltip("移動する各レーン")]
    [SerializeField] private Transform[] _lanes;

    /// <summary> 出現する時間 </summary>
    private float[] _appearTimes;
    private GameObject[] _gimmicks = default;
    private int _appearGimmickNum = 0;

    public Transform[] Lanes => _lanes;

    private void Start()
    {
        int nowStage = (GameManager.GameStageNum * 2) + GameManager.StageLevelNum;
        Debug.Log($"StageNum : {nowStage}");

        _gimmicks = _sceneGimmick[nowStage].Gimmicks;
        _appearTimes = new float[_gimmicks.Length];
        for (int i = 0; i < _appearTimes.Length; i++)
        {
            _appearTimes[i] =
                (float)(GameManager.GameTimeClearLength / 100) * _gimmicks[i].GetComponent<GimmickBase>().TimeToAppear;
        }
        ArraySort();
    }

    private void Update()
    {
        if (_appearGimmickNum == _appearTimes.Length) return;

        if (GameManager.CurrentTime >= _appearTimes[_appearGimmickNum])
        {
            GenerateGimmick(_appearGimmickNum);
            _appearGimmickNum++;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum)
    {
        var obj = Instantiate(_gimmicks[gimmickPrefabNum], _appearLane4);
        obj.transform.parent = null;

        if (obj.TryGetComponent(out Employee employee))
        {
            employee.Init(_obonObj);
        }
        else if (obj.TryGetComponent(out KlalenScript klaken))
        {
            klaken.AppearPos = _seaLane.position;
        }
    }

    /// <summary> インサートソートを用いて配列の並び替えを行う </summary>
    private void ArraySort()
    {
        for (int i = 0; i < _appearTimes.Length - 1; i++)
        {
            int index = i + 1;

            for (int j = i; j >= 0; j--)
            {
                if (_appearTimes[index] < _appearTimes[j])
                {
                    Swap(ref _appearTimes, index, j);
                    Swap(ref _gimmicks, index, j);

                    index--;
                }
                else break;
            }
        }
    }

    private void Swap<T>(ref T[] array, int i, int j)
    {
        (array[i], array[j]) = (array[j], array[i]);
    }
}
