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
public class Gimmickmanager : MonoBehaviour
{
    [Header("ケーキを追加する使用人は常に一番下に入れる")]
    [Tooltip("Scecn番号と同じElement番号にSceneに合ったPrefabを入れる")]
    [SerializeField] private GimmickPrefabs[] _sceneGimmick;
    [Tooltip("出現するレーン")]
    [SerializeField] private Transform _appearLane;
    [Tooltip("StageMove")]
    [SerializeField] private StageMove _stageMove;
    [Tooltip("おぼんObj")]
    [SerializeField] private GameObject _obonObj;
    [Tooltip("第一第二第三レーン")]
    [SerializeField] private Transform[] _lanes;

    /// <summary> 出現する時間 </summary>
    private float[] _appearTimes;
    private GameObject[] _gimmicks = default;
    private int _appearGimmickNum = 0;
    private bool _isAppearGimmick = false;

    public Transform[] Lanes => _lanes;

    private void Start()
    {
        int nowStage = (GameManager.GameStageNum * 2) + GameManager.StageLevelNum;
        Debug.Log($"StageNum : {nowStage}");

        _gimmicks = _sceneGimmick[nowStage].Gimmicks;
        _appearTimes = new float[_gimmicks.Length - 1];
        for (int i = 0; i < _appearTimes.Length; i++)
        {
            _appearTimes[i] =
                GameManager.GameTimeClearLength * (_gimmicks[i].GetComponent<GimmickBase>().TimeToAppear / 100);
        }
        ArraySort();
    }

    private void Update()
    {
        _isAppearGimmick = false;

        if (_appearGimmickNum == _appearTimes.Length) return;

        if (GameManager.CurrentTime <= _appearTimes[_appearGimmickNum] && !_isAppearGimmick)
        {
            var index = UnityEngine.Random.Range(0, _gimmicks.Length -1);

            //if (index == _gimmicks.Length - 1) return;

            GenerateGimmick(index);
            _appearGimmickNum++;
            _isAppearGimmick = true;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum)
    {
        var obj = Instantiate(_gimmicks[gimmickPrefabNum], _appearLane);
        obj.transform.parent = null;

        if (obj.TryGetComponent(out ArtPainting art))
        {
            art.StageMove = _stageMove;
        }
        else if (gimmickPrefabNum == _gimmicks.Length - 1)
        {
            obj.GetComponent<Employee>().Init(_stageMove, _obonObj);
        }
    }

    /// <summary> インサートソートを用いて配列の並び替えを行う(降順) </summary>
    private void ArraySort()
    {
        for (int i = 0; i < _appearTimes.Length - 1; i++)
        {
            int index = i + 1;

            for (int j = i; j >= 0; j--)
            {
                if (_appearTimes[index] >= _appearTimes[j])
                {
                    Swap(ref _appearTimes, index, j);
                    Swap(ref _gimmicks, index, j);

                    index--;
                }
                else break;
            }
        }
    }

    private void Swap<T>(ref T[] array, int a, int b)
    {
        var save = array[a];

        array[a] = array[b];
        array[b] = save;
    }
}
