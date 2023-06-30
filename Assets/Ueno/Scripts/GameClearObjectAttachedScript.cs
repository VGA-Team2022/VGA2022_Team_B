using UnityEngine;

/// <summary>
/// �N���A���� 
/// </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [Tooltip("�ڐG������N���A����ɂ���objects"),SerializeField] private Animator[] _objectPrefabsAnim;
    [SerializeField] private StageMove _stageMove;

    private int _gameStageNum;
    //Animator��Clear�A�j���[�V�����ɑJ�ڂ��邽�߂�Param�̖��O
    private string _animClearPram = "isClear";
    private BoxCollider _col;

    private void Start()
    {
        _gameStageNum = GameManager.GameStageNum; 
        _objectPrefabsAnim[_gameStageNum] = _objectPrefabsAnim[_gameStageNum].GetComponent<Animator>();
        _stageMove = _stageMove.gameObject.GetComponent<StageMove>();
        _col = GetComponent<BoxCollider>();
        _col.enabled = false;
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj)
        {
            _col.enabled = true;
            _objectPrefabsAnim[_gameStageNum].SetBool(_animClearPram,true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            GameManager.IsGameClear = true;
            Debug.Log("GameClear");
        }
    }


}


