using Common;
using System;
using UnityEngine;

[Serializable]
class GimmickPrefabs
{
    [Tooltip("�M�~�b�NPrefab"),SerializeField] public GameObject[] _gimmickPrefabs;
}

/// <summary>
/// �M�~�b�N����Manager
/// </summary>
public class Gimmickmanager : MonoBehaviour
{ 
    //[Header("��ԉ��Ɏg�p�l������")]
    
    [Header("Scecn�ԍ��Ɠ���Element�ԍ���Scene�ɍ�����Prefab������\n���~(��)��0�ԂȂ�Element�ԍ�0�Ԃɉ��~(��)�Ɏg���M�~�b�N������\n�P�[�L��ǉ�����g�p�l�͏�Ɉ�ԉ��ɓ����")]
    [SerializeField] private GimmickPrefabs[] _sceneGimmick;
    [Tooltip("�o�����郌�[��"), SerializeField] private Transform _appearLane;
    [Tooltip("�o�����鎞��"), SerializeField] private float[] _appearTime;
    [Tooltip("StageMove"), SerializeField] private StageMove _stageMove;
    [Tooltip("���ڂ�Obj"), SerializeField] private GameObject _obonObj;

    private GameObject[] _gimmickPrefabs;
    private int _appearGimmickNum = 0;
    private bool isAppearGimmick = false;
    private bool employee = false;
    const int TARBANGARL_INDEX_ONE = 1;
    int _nowStarg;

    private void Start()
    {
         _nowStarg = GameManager.GameStageNum;
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
            GenerateGimmick(index);
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum)
    {
        var obj = Instantiate(_gimmickPrefabs[gimmickPrefabNum], _appearLane);
        Debug.Log($"{_gimmickPrefabs[gimmickPrefabNum]}���o������,���݂̃Q�[��������{GameManager.CurrentTime}");
        if (gimmickPrefabNum == TARBANGARL_INDEX_ONE && _nowStarg == 0)//_nowStarg == 0�͉��~(��)�ɍ������ԍ��ɂ���
        {
            obj.GetComponent<TarbanGarl>().StageMove = _stageMove;
        }
        else if (gimmickPrefabNum == _gimmickPrefabs.Length - 1)
        {
            obj.GetComponent<Employee>().StageMove = _stageMove;
            obj.GetComponent<Employee>().Gimmickmanager = this;
            obj.GetComponent<Employee>().ObonObj = _obonObj;
        }
    }

}
