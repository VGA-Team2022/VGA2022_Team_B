using UnityEngine;

/// <summary> �M�~�b�N�̊��script </summary>
public class GimmickBase : MonoBehaviour
{
    [Tooltip("�S�̎��Ԃɑ΂���o������(��)")]
    [Range(5, 80)]
    [SerializeField] private int _timeToAppear = 10;

    private StageMove _stageMovement = default;

    public int TimeToAppear => _timeToAppear;
    public StageMove StageMovement => _stageMovement;

    private void Awake()
    {
        _stageMovement = GameObject.Find("StageManager").GetComponent<StageMove>();
    }
}
