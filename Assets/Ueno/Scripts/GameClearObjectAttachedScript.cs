using UnityEngine;

/// <summary>
/// �N���A���� 
/// </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [Tooltip("�ڐG������N���A����ɂ���objects"),SerializeField] private Animator[] _objectPrefabsAnim;

    private int _gameStageNum;
    //Animator��Clear�A�j���[�V�����ɑJ�ڂ��邽�߂�Param�̖��O
    private string _animClearPram = "isClear";
    private bool _isClear;


    private void Start()
    {
        _gameStageNum = GameManager.GameStageNum; 
        _objectPrefabsAnim[_gameStageNum] = _objectPrefabsAnim[_gameStageNum].GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj&& !_isClear)
        {
            _objectPrefabsAnim[_gameStageNum].SetBool(_animClearPram,true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            GameManager.IsGameClear = true;
            _isClear = true;
            Debug.Log("GameClear");
        }
    }


}


