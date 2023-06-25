using Common;
using System;
using UnityEngine;

[Serializable]
class GimmickPrefabs
{
    [Tooltip("ギミックPrefab"),SerializeField] public GameObject[] _gimmickPrefabs;
}

/// <summary>
/// ギミック生成Manager
/// </summary>
public class Gimmickmanager : MonoBehaviour
{ 
    //[Header("一番下に使用人を入れる")]
    
    [Header("Scecn番号と同じElement番号にSceneに合ったPrefabを入れる\n屋敷(昼)が0番ならElement番号0番に屋敷(昼)に使うギミックを入れる\nケーキを追加する使用人は常に一番下に入れる")]
    [SerializeField] private GimmickPrefabs[] _sceneGimmick;
    [Tooltip("出現するレーン"), SerializeField] private Transform _appearLane;
    [Tooltip("出現する時間"), SerializeField] private float[] _appearTime;
    [Tooltip("StageMove"), SerializeField] private StageMove _stageMove;
    [Tooltip("おぼんObj"), SerializeField] private GameObject _obonObj;
    [Tooltip("第一第二第三レーン")] public Transform[] _lanes;

    private GameObject[] _gimmickPrefabs;
    private int _appearGimmickNum = 0;
    private bool isAppearGimmick = false;
    private bool employee = false;
    int _nowStarg;

    private void Start()
    {
         _nowStarg = GameManager.StageLevelNum;
        _gimmickPrefabs = _sceneGimmick[_nowStarg]._gimmickPrefabs;
    }
    private void Update()
    {

        isAppearGimmick = false;

        if (GameManager.CurrentTime < GameManager.GameTimeClearLength/ 2 && !employee)
        {
            GenerateGimmick(_gimmickPrefabs.Length -1);
            employee = true;
        }

        if (_appearGimmickNum == _appearTime.Length) { return; }
        if (GameManager.CurrentTime <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            var index = UnityEngine.Random.Range(0, _gimmickPrefabs.Length -1);
            if(index == _gimmickPrefabs.Length - 1) { return; }
            GenerateGimmick(index);
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
        //Debug.Log($"CurrentTime{GameManager.CurrentTime}");
    }

    private void GenerateGimmick(int gimmickPrefabNum)
    {
        var obj = Instantiate(_gimmickPrefabs[gimmickPrefabNum], _appearLane);
        obj.transform.parent = null;
        if (obj.GetComponent<ArtPainting>() != null /*&& _nowStarg == 0*/)//_nowStarg == 0は屋敷(昼)に合った番号にする
        {
            obj.GetComponent<ArtPainting>().StageMove = _stageMove;
        }
        else if (gimmickPrefabNum == _gimmickPrefabs.Length - 1)
        {
            obj.GetComponent<Employee>().StageMove = _stageMove;
            obj.GetComponent<Employee>().Gimmickmanager = this;
            obj.GetComponent<Employee>().ObonObj = _obonObj;
        }
    }

}
