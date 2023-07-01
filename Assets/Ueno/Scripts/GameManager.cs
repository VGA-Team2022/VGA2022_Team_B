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

    /// <summary>�Q�[���N���A�܂ł̎���</summary>
    public static float GameTimeClearLength = 10;

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
    private static bool _isAppearDoorObj = false;

    /// <summary>SceneManager�i�[�p�ϐ�</summary>
    private AttachedSceneController _scenemng = default;

    /// <summary>�N���A�����ɏo������object�t���O</summary>
    public static bool IsAppearClearObj => _isAppearDoorObj;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #region �X�e�[�W�ƃ��x���̑I�𑀍�Ŏg�����\�b�h
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
    #endregion

    private void Start()
    {
        _isGameStart = false;
        IsGameOver = false;
        IsGameClear = false;
        IsGameStaged = false;
        FindSceneManager();

        IsFindScenemng = false;

        CurrentTime = GameTimeClearLength;

        if (SceneManager.GetActiveScene().name == Define.SCENENAME_TITLE)
        {
            SoundManager.InstanceSound.PlayAudioClip(SoundManager.BGM_Type.BGM_Title_Home);
        }
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
                _isAppearDoorObj = false;
                CurrentTime = GameTimeClearLength;
            }
            GemeClearjudge();
        }
    }

    #region �N���A����
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
            if (!IsAppearClearObj)
            {
                CurrentTime -= Time.deltaTime;
            }
            /// Debug.Log(CurrentTime);

            if (CurrentTime <= 0 && !IsGameOver)
            {
                _isAppearDoorObj = true;
            }
        }
    }
    #endregion

    /// <summary> �������[�V����������� </summary>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        IsGameOver = Obon._staticSweetsFall;
    }
}