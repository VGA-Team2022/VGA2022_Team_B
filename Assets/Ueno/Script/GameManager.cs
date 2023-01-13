using Unity.VisualScripting;
using UnityEngine;

using Common;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    /// <summary>�X�e�[�W�̒l</summary>
    public static int GameStageNum = 0;
    /// <summary>�X�e�[�W���x���̒l</summary>
    public static int StageLevelNum =  0;

    /// <summary>BGM�̃{�����[��</summary>
    public static float GameBGMVolume = 50;
    /// <summary>SE�̃{�����[��</summary>
    public static float GameSEVolume = 50;

    /// <summary>�Q�[���N���A�܂ł̎���</summary>
    public static float GameTimeClearLength = 90;

    /// <summary>���݂̎���</summary>
    public static float CurrentTime;

    /// <summary>�Q�[�����J�n���ꂽ���̔���</summary>
    private bool isGameStart = false;

    /// <summary>�Q�[���N���A�̔���</summary>
    public static bool isGameClear = false;
    /// <summary>�Q�[���I�[�o�[�̔���</summary>
    public static bool isGameOver = false;

    /// <summary>���U���g���o�̏I������</summary>
    public static bool isGameStaged = false;

    /// <summary>��񂾂�SceneManager��T���ׂ̔���</summary>
   public static bool isFindScenemng;
    /// <summary>Player�̐i�s���X�g�b�v����ׂ̔���</summary>
    public static bool isStop;

    /// <summary>Clear����p�̃h�A���o�������锻��</summary>
    public static bool isAppearDoorObj;

    /// <summary>scenemanager�i�[�p�ϐ�</summary>
    private AttachedSceneController _scenemng = default;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public void PrefarenceStage(int i)
    {
        GameStageNum = i;
    }
    public void PrefarenceLevel(int i)
    {
        StageLevelNum = i;
    }
    public void PrefarenceTime(float i)
    {
        GameTimeClearLength = i;
    }

    
    private void Start()
    {
        isGameStart = false;
        isGameOver = false;
        isGameClear = false;
        isGameStaged = false;
        FindSceneManager();

        isFindScenemng = false;

        CurrentTime = GameTimeClearLength;

    }

    private void FindSceneManager()
    {
        _scenemng = GameObject.Find("SceneManager").GetComponent<AttachedSceneController>();
        Debug.Log(_scenemng);
    }

    private void Update()
    {

        if (!_scenemng && !isFindScenemng)
        {
            FindSceneManager();

            if (SceneManager.GetActiveScene().name == Define.SCENENAME_RESULT)
            {
                isGameStart = false;
                isGameStaged = false;
                isFindScenemng = true;

            }

            else if (SceneManager.GetActiveScene().name != Define.SCENENAME_RESULT && SceneManager.GetActiveScene().name != Define.SCENENAME_MASTERGAME)
            {
                Debug.Log(_scenemng);
                isGameStart = false;
                isGameOver = false;
                isGameClear = false;
                isGameStaged = false;
                isFindScenemng = true;
                isStop = false;
                CurrentTime = GameTimeClearLength;
            }
        }

         if (SceneManager.GetActiveScene().name == Define.SCENENAME_MASTERGAME)
        {
            if (!isFindScenemng)
            {
                Debug.Log(_scenemng.gameObject.scene.name);
                isGameStart = true;
                isFindScenemng = true;
                isGameStaged = false;
                isGameOver = false;
                isGameClear = false;
                isStop = false;
                isAppearDoorObj = false;
                CurrentTime = GameTimeClearLength;
            }
            GemeClearjudge();

        }

    }

    private void GemeClearjudge()
    {
        if (_scenemng)
        {
            if (Obon._staticSweetsFall == true)
            {
                StartCoroutine(GameOver());
            }

            if (isGameOver && !isGameClear)//GameOver
            {
                if (isGameStaged)//���o���I�������
                {
                    _scenemng.ChangeResultScene();
                    //isFindScenemng = false;
                }
            }
            else if (!isGameOver && isGameClear)//GameClear
            {
                if (isGameStaged)
                {
                    _scenemng.ChangeResultScene();
                    //isFindScenemng = false;
                }
            }
        }
        //GameClear�ɂȂ���������Ăяo��
        if (isGameStart && !isStop)
        {
            if (!isAppearDoorObj) 
            {
                CurrentTime -= Time.deltaTime;
            }
           /// Debug.Log(CurrentTime);

            if (CurrentTime <= 0 && !isGameOver)
            {
                isAppearDoorObj = true;
            }
        }
    }
    /// <summary>
    /// �������[�V�����������
    /// </summary>
    /// <returns></returns>
   private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        isGameOver = Obon._staticSweetsFall;
    }

}