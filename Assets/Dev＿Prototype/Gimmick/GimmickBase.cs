using UnityEngine;

/// <summary> �M�~�b�N�̊��script </summary>
public class GimmickBase : MonoBehaviour
{
    [Tooltip("�S�̎��Ԃɑ΂���o������(��)")]
    [Range(5, 80)]
    [SerializeField] private int _timeToAppear = 10;

    public int TimeToAppear => _timeToAppear;
}
