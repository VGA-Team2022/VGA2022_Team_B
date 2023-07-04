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
    public static float GameTimeClearLength = 25;

    /// <summary>���݂̎���</summary>
    public static float CurrentTime;

    /// <summary>�Q�[�����J�n���ꂽ���̔���</summary>
    private bool _isGameStart = false;

    /// <summary>�Q�[���N���A�̔���</summary>
    public static bool IsGameClear = false;
    /// <summary>�Q�[���I�[�o�[�̔���</summary>
    public static bool IsGameOver = false;

    /// <summary>���U���g���o�̏I������</summary>
    public static bool IsFinishedEffect = false;

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
    /// <summary> �X�e�[�W�I�� </summary>
    public void PrefarenceStage(int i)
    {
        GameStageNum = i;
    }

    /// <summary> ���x���I�� </summary>
    public void PrefarenceLevel(int i)
    {
        StageLevelNum = i;
    }

    /// <summary> ���Ԑݒ�(?) </summary>
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
        IsFinishedEffect = false;
        FindSceneManager();

        IsFindScenemng = false;

        CurrentTime = 0f;

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
                IsFinishedEffect = false;
                IsFindScenemng = true;
            }
            else if (SceneManager.GetActiveScene().name != Define.SCENENAME_RESULT &&
                     SceneManager.GetActiveScene().name != Define.SCENENAME_MASTERGAME)
            {
                _isGameStart = false;
                IsGameOver = false;
                IsGameClear = false;
                IsFinishedEffect = false;
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
                IsFinishedEffect = false;
                IsGameOver = false;
                IsGameClear = false;
                IsStop = false;
                _isAppearDoorObj = false;
                CurrentTime = 0;
            }
            GemeClearjudge();
        }
    }

    private void GemeClearjudge()
    {
        if (_scenemng)
        {
            if (Obon.IsSweetsFall == true)
            {
                StartCoroutine(GameOver());
            }

            if (IsGameOver && !IsGameClear) //GameOver
            {
                if (IsFinishedEffect) _scenemng.ChangeResultScene();
            }
            else if (!IsGameOver && IsGameClear)//GameClear
            {
                if (IsFinishedEffect) _scenemng.ChangeResultScene();
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
                CurrentTime += Time.deltaTime;
            }

            if (CurrentTime >= GameTimeClearLength && !IsGameOver)
            {
                _isAppearDoorObj = true;
            }
        }
    }

    /// <summary> �������[�V����������� </summary>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        IsGameOver = Obon.IsSweetsFall;
    }
}
