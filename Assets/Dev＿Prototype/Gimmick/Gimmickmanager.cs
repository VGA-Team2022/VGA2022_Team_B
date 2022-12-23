using Common;
using UnityEngine;

/// <summary>
/// ギミック生成Manager
/// </summary>
public class Gimmickmanager : MonoBehaviour
{ 
    [Header("出現順に設定")]
    [Tooltip("ギミックPrefab"),SerializeField] private GameObject[] _gimmickPrefabs;
    [Tooltip("出現するレーン"), SerializeField] private Transform[] _appearLane;
    [Tooltip("出現する時間"), SerializeField] private float[] _appearTime;
    //  [Tooltip("GameManager"), SerializeField] private GameManager _gameManager;
    [Tooltip("StageMove"), SerializeField] private StageMove _stageMove;
    [Tooltip("おぼんObj"), SerializeField] private GameObject _obonObj;

    private int _appearGimmickNum = 0;
    private float _time = Define.GAME_TIME;
    private bool isAppearGimmick = false;
    private bool employee = false;
    const int TARBANGARL_INDEX_ONE = 1, EMPLOYEE_INDEX_TWO = 2, LANE_THREE = 3;


    private void Update()
    {
        _time -= Time.deltaTime;
        isAppearGimmick = false;

        if (_time <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            var index = Random.Range(0, _gimmickPrefabs.Length -1);
            GenerateGimmick(index, LANE_THREE);
            if (_appearGimmickNum == _appearTime.Length) { return; }
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
        if (_time < Define.GAME_TIME/2 && !employee)
        {
            GenerateGimmick(EMPLOYEE_INDEX_TWO, LANE_THREE);
            employee = true;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum,int laneNum)
    {
        var obj = Instantiate(_gimmickPrefabs[gimmickPrefabNum], _appearLane[laneNum]);
        Debug.Log($"{_gimmickPrefabs[gimmickPrefabNum]}が出現した");
        if (gimmickPrefabNum == TARBANGARL_INDEX_ONE)
        {
            obj.GetComponent<TarbanGarl>().StageMove = _stageMove;
        }
        else if (gimmickPrefabNum == EMPLOYEE_INDEX_TWO)
        {
            obj.GetComponent<Employee>().StageMove = _stageMove;
            obj.GetComponent<Employee>().Gimmickmanager = this;
            obj.GetComponent<Employee>().ObonObj = _obonObj;
        }
    }

}
