using Common;
using UnityEngine;

/// <summary>
/// �M�~�b�N����Manager
/// </summary>
public class Gimmickmanager : MonoBehaviour
{ 
    [Header("��ԉ��Ɏg�p�l������")]
    [Tooltip("�M�~�b�NPrefab"),SerializeField] private GameObject[] _gimmickPrefabs;
    [Tooltip("�o�����郌�[��"), SerializeField] private Transform _appearLane;
    [Tooltip("�o�����鎞��"), SerializeField] private float[] _appearTime;
    [Tooltip("StageMove"), SerializeField] private StageMove _stageMove;
    [Tooltip("���ڂ�Obj"), SerializeField] private GameObject _obonObj;

    private int _appearGimmickNum = 0;
    private bool isAppearGimmick = false;
    private bool employee = false;
    const int TARBANGARL_INDEX_ONE = 1;
    int _nowStarg;

    private void Start()
    {
         _nowStarg = GameManager.GameStageNum;
        switch (_nowStarg)//�V�[���ԍ�
        {
            case 0:
                Debug.Log("0");
                break;
            case 1:
                Debug.Log("1");
                break;
            case 2:
                Debug.Log("2");
                break;
        }
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
            var index = Random.Range(0, _gimmickPrefabs.Length -1);
            GenerateGimmick(index);
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum)
    {
        var obj = Instantiate(_gimmickPrefabs[gimmickPrefabNum], _appearLane);
        Debug.Log($"{_gimmickPrefabs[gimmickPrefabNum]}���o������,���݂̃Q�[��������{GameManager.CurrentTime}");
        if (gimmickPrefabNum == TARBANGARL_INDEX_ONE)
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
