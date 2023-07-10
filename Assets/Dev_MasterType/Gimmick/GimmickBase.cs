using UnityEngine;

/// <summary> ギミックの基底script </summary>
public class GimmickBase : MonoBehaviour
{
    [Tooltip("全体時間に対する出現時間(％)")]
    [Range(5, 80)]
    [SerializeField] private int _timeToAppear = 10;
    [Tooltip("ギミックが出現するレーン")]
    [SerializeField] private LaneType _spawnLane = LaneType.None;

    private StageMove _stageMovement = default;

    public int TimeToAppear => _timeToAppear;
    public StageMove StageMovement => _stageMovement;

    private void Awake()
    {
        _stageMovement = GameObject.Find("StageManager").GetComponent<StageMove>();
    }

    public void LaneSelect(Transform[] lanes)
    {
        int index = _spawnLane switch
        {
            LaneType.Random => Random.Range(0, 3),
            LaneType.Sea => 4,
            LaneType.Other => 3,
            _ => -1,
        };

        //int index = _spawnLane == 4 ? _spawnLane : Random.Range(1, 3);
        Debug.Log(index);
        var pos = lanes[index].position;
        pos.y += 3f;
        transform.position = pos;
    }

    public enum LaneType
    {
        None,
        Random,
        Sea,
        Other,
    }
}
