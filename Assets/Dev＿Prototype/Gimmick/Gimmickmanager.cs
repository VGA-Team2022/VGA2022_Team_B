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
    [Tooltip("出現する時間")]
    [SerializeField] private float[] _appearTime;
    [Tooltip("StageMove")]
    [SerializeField] private StageMove _stageMove;
    [Tooltip("おぼんObj")]
    [SerializeField] private GameObject _obonObj;
    [Tooltip("第一第二第三レーン")]
    [SerializeField] private Transform[] _lanes;

    public Transform[] Lanes => _lanes;

    private GameObject[] _gimmicks = default;
    private int _appearGimmickNum = 0;
    private bool isAppearGimmick = false;
    //private bool employee = false;

    private void Start()
    {
        int nowStage = (GameManager.GameStageNum * 2) + GameManager.StageLevelNum;
        Debug.Log($"StageNum : {nowStage}");

        _gimmicks = _sceneGimmick[nowStage].Gimmicks;
    }
    private void Update()
    {
        isAppearGimmick = false;

        //if (GameManager.CurrentTime < GameManager.GameTimeClearLength / 2 && !employee)
        //{
        //    GenerateGimmick(_gimmickPrefabs.Length);
        //    employee = true;
        //}

        if (_appearGimmickNum == _appearTime.Length) return;

        if (GameManager.CurrentTime <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            var index = UnityEngine.Random.Range(0, _gimmicks.Length -1);

            if (index == _gimmicks.Length - 1) return;

            GenerateGimmick(index);
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
        //Debug.Log($"CurrentTime{GameManager.CurrentTime}");
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

}
