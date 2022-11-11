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
    //[SerializeField] private int[] _appearLaneArray;
    [Tooltip("出現する時間"), SerializeField] private float[] _appearTime;

    private int _appearGimmickNum = 0;
    private float _time;
    private bool isAppearGimmick = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        _time += Time.deltaTime;
        isAppearGimmick = false;

        if (_time >= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            Debug.Log(_time);
            GenerateGimmick(_appearGimmickNum, _appearGimmickNum);
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
