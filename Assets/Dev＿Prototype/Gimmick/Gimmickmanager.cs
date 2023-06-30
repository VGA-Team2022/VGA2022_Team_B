using System;
using UnityEngine;

[Serializable]
class GimmickPrefabs
{
    [Tooltip("�M�~�b�NPrefab")]
    [SerializeField] private GameObject[] _gimmickPrefabs;

    public GameObject[] Gimmicks => _gimmickPrefabs;
}

/// <summary>
/// �M�~�b�N����Manager
/// </summary>
public class Gimmickmanager : MonoBehaviour
{
    [Header("�P�[�L��ǉ�����g�p�l�͏�Ɉ�ԉ��ɓ����")]
    [Tooltip("Scecn�ԍ��Ɠ���Element�ԍ���Scene�ɍ�����Prefab������")]
    [SerializeField] private GimmickPrefabs[] _sceneGimmick;
    [Tooltip("�o�����郌�[��")]
    [SerializeField] private Transform _appearLane;
    [Tooltip("�o�����鎞��")]
    [SerializeField] private float[] _appearTime;
    [Tooltip("StageMove")]
    [SerializeField] private StageMove _stageMove;
    [Tooltip("���ڂ�Obj")]
    [SerializeField] private GameObject _obonObj;
    [Tooltip("������O���[��")]
    [SerializeField] private Transform[] _lanes;

    public Transform[] Lanes => _lanes;

    private GameObject[] _gimmicks = default;
    private int _appearGimmickNum = 0;
    private bool isAppearGimmick = false;
    //private bool employee = false;

    private void Start()
    {
        int nowStage = (GameManager.GameStageNum * 2) + GameManager.StageLevelNum;
        Debug.Log($"StageNum : {nowStage}");

        _gimmicks = _sceneGimmick[nowStage].Gimmicks;
    }
    private void Update()
    {
        isAppearGimmick = false;

        //if (GameManager.CurrentTime < GameManager.GameTimeClearLength / 2 && !employee)
        //{
        //    GenerateGimmick(_gimmickPrefabs.Length);
        //    employee = true;
        //}

        if (_appearGimmickNum == _appearTime.Length) return;

        if (GameManager.CurrentTime <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            var index = UnityEngine.Random.Range(0, _gimmicks.Length -1);

            if (index == _gimmicks.Length - 1) return;

            GenerateGimmick(index);
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
        //Debug.Log($"CurrentTime{GameManager.CurrentTime}");
    }

    private void GenerateGimmick(int gimmickPrefabNum)
    {
        var obj = Instantiate(_gimmicks[gimmickPrefabNum], _appearLane);
        obj.transform.parent = null;

        if (obj.TryGetComponent(out ArtPainting art))
        {
            art.StageMove = _stageMove;
        }
        else if (gimmickPrefabNum == _gimmicks.Length - 1)
        {
            obj.GetComponent<Employee>().Init(_stageMove, _obonObj);
        }
    }

}
