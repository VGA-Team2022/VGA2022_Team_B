using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>�X�e�[�W�̒l</summary>
    public static int GameStageNum = 0;
    /// <summary>�X�e�[�W���x���̒l</summary>
    public static int StageLevelNum = 0;

    /// <summary>BGM�̃{�����[��</summary>
    public static float GameBGMVolume = 50;
    /// <summary>SE�̃{�����[��</summary>
    public static float GameSEVolume = 50;

    /// <summary>�Q�[���N���A�܂ł̎���</summary>
    public static float GameTimeClearLength = 30;

    /// <summary>���݂̎���</summary>
    public static float CurrentTime;

    /// <summary>�Q�[�����J�n���ꂽ���̔���</summary>
    private bool _isGameStart = false;

    /// <summary>�Q�[���N���A�̔���</summary>
    public static bool IsGameClear = false;
    /// <summary>�Q�[���I�[�o�[�̔���</summary>
    public static bool IsGameOver = false;

    /// <summary>���U���g���o�̏I������</summary>
    public static bool IsGameStaged = false;

    /// <summary>��񂾂�SceneManager��T���ׂ̔���</summary>
    public static bool IsFindScenemng = false;
    /// <summary>Player�̐i�s���X�g�b�v����ׂ̔���</summary>
    public static bool IsStop = false;

    /// <summary>Clear����p�̃h�A���o�������锻��</summary>
    private static bool isAppearDoorObj = false;

    /// <summary>SceneManager�i�[�p�ϐ�</summary>
    private AttachedSceneController _scenemng = default;

    public static bool IsAppearDoorObj { get => isAppearDoorObj; set => isAppearDoorObj = value; }

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
        _isGameStart = false;
        IsGameOver = false;
        IsGameClear = false;
        IsGameStaged = false;
        FindSceneManager();

        IsFindScenemng = false;

        CurrentTime = GameTimeClearLength;
    }

    private void FindSceneManager()
    {
        _scenemng = GameObject.Find("SceneManager").GetComponent<AttachedSceneController>();
        Debug.Log(_scenemng);
    }

    private void Update()
    {

        if (!_scenemng && !IsFindScenemng)
        {
            FindSceneManager();

            if (SceneManager.GetActiveScene().name == Define.SCENENAME_RESULT)
            {
                _isGameStart = false;
                IsGameStaged = false;
                IsFindScenemng = true;

            }

            else if (SceneManager.GetActiveScene().name != Define.SCENENAME_RESULT &&
                     SceneManager.GetActiveScene().name != Define.SCENENAME_MASTERGAME)
            {
                Debug.Log(_scenemng);
                _isGameStart = false;
                IsGameOver = false;
                IsGameClear = false;
                IsGameStaged = false;
                IsFindScenemng = true;
                IsStop = false;
                CurrentTime = GameTimeClearLength;
            }
        }

        if (SceneManager.GetActiveScene().name == Define.SCENENAME_MASTERGAME)
        {
            if (!IsFindScenemng)
            {
                Debug.Log(_scenemng.gameObject.scene.name);
                _isGameStart = true;
                IsFindScenemng = true;
                IsGameStaged = false;
                IsGameOver = false;
                IsGameClear = false;
                IsStop = false;
                IsAppearDoorObj = false;
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

            if (IsGameOver && !IsGameClear) //GameOver
            {
                if (IsGameStaged)//���o���I�������
                {
                    _scenemng.ChangeResultScene();
                    //isFindScenemng = false;
                }
            }
            else if (!IsGameOver && IsGameClear)//GameClear
            {
                if (IsGameStaged)
                {
                    _scenemng.ChangeResultScene();
                    //isFindScenemng = false;
                }
            }
            else if (IsGameOver && IsGameClear)//���������N���A����ɂȂ�����
            {
                _scenemng.ChangeResultScene();
            }
        }
        //GameClear�ɂȂ���������Ăяo��
        if (_isGameStart && !IsStop)
        {
            if (!IsAppearDoorObj)
            {
                CurrentTime -= Time.deltaTime;
            }
            /// Debug.Log(CurrentTime);

            if (CurrentTime <= 0 && !IsGameOver)
            {
                IsAppearDoorObj = true;
            }
        }
    }
    /// <summary> �������[�V����������� </summary>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        IsGameOver = Obon._staticSweetsFall;
    }
}