using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("GameManager"), SerializeField] private GameManager _gameManager;

    private int _appearGimmickNum = 0;
    private float _time = 120;
    private bool isAppearGimmick = false;

    private void Update()
    {
        _time -= Time.deltaTime;
        isAppearGimmick = false;

        //GameManagerの時間が必須
        //if (_gameManager._time >= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        //{
        //    var index = Random.Range(0, _gimmickPrefabs.Length);
        //    GenerateGimmick(index, 3);
        //    if(_appearGimmickNum == _appearTime.Length) { return; }
        //    _appearGimmickNum++;
        //    isAppearGimmick = true;
        //}
        //if(_gameManager._time <= 60)
        //{
        //  //ケーキを積む使用人  
        //    GenerateGimmick(3, 3);
        //}
        if (_time <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            var index = Random.Range(0, 2);
            GenerateGimmick(index, 3);
            if (_appearGimmickNum == _appearTime.Length) { return; }
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum,int laneNum)
    {
        Instantiate(_gimmickPrefabs[gimmickPrefabNum], _appearLane[laneNum]);
        Debug.Log($"{_gimmickPrefabs[gimmickPrefabNum]}が出現した");
    }

}
