﻿using UnityEngine;

/// <summary> ギミックの基底script </summary>
public class GimmickBase : MonoBehaviour
{
    [Tooltip("全体時間に対する出現時間(％)")]
    [Range(5, 80)]
    [SerializeField] private int _timeToAppear = 10;
    [Range(0, 5)]
    [Tooltip("ギミックが出現するレーン")]
    [SerializeField] private int _spawnLane = 1;

    private StageMove _stageMovement = default;

    public int TimeToAppear => _timeToAppear;
    public int SpawnLane => _spawnLane;
    public StageMove StageMovement => _stageMovement;

    private void Awake()
    {
        _stageMovement = GameObject.Find("StageManager").GetComponent<StageMove>();
    }

    public void LaneSelect(Transform[] lanes)
    {
        int index = LansSetting(_spawnLane == 5 ? Random.Range(1, 3) : _spawnLane);
        transform.position = lanes[index].position;
    }

    private int LansSetting(int num)
    {
        return num switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => 4,
            _ => -1,
        };
    }
}
