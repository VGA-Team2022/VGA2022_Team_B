using UnityEngine;

/// <summary> ギミックの基底script </summary>
public class GimmickBase : MonoBehaviour
{
    [Tooltip("全体時間に対する出現時間(％)")]
    [Range(5, 80)]
    [SerializeField] private int _timeToAppear = 10;
    [Range(0, 6)]
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
}
