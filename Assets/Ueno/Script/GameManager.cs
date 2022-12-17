using Unity.VisualScripting;
using UnityEngine;

using Common;

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
    public static float GameTime = Define.GAME_TIME;

    /// <summary>�Q�[���N���A�̔���</summary>
    public static bool isGameClear = false;
    /// <summary>�Q�[�����J�n���ꂽ���̔���</summary>
    private bool isGameStart = false;
    /// <summary>�Q�[���I�[�o�[�̔���</summary>
    private bool isGameOver = false;

    /// <summary>scenemanager�i�[�p�ϐ�</summary>
    private AttachedSceneController _scenemng;
    /// <summary>���݂̎���</summary>
    private float _currentTime;

    /// <summary>��񂾂�SceneManager��T���ׂ̔���</summary>
   public static bool isFindScenemng;

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
        GameTime = i;
    }

    
    private void Start()
    {
        isGameStart = false;
        isGameOver = false;
        isGameClear = false;
        FindSceneManager();

        isFindScenemng = false;

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
            Debug.Log(_scenemng);
            isGameStart = false;
            isGameOver = false;
            isGameClear = false;
            isFindScenemng = true;
            _currentTime = GameTime;
        }

        if (_scenemng.gameObject.scene.name == Define.SCENENAME_MASTERGAME)
        {
            if (!isFindScenemng)
            {
                isGameStart = true;
                isFindScenemng = true;
                _currentTime = GameTime;
            }
            GemeClearjudge();
        }



    }

    private void GemeClearjudge()
    {
        if (_scenemng)
        {
            isGameOver = Obon._sweetsFall;

            if (isGameOver  && !isGameClear)
            {
                _scenemng.ChangeResultScene();
                isFindScenemng = false;
            }
            else if (!isGameOver && isGameClear)
            {
                _scenemng.ChangeResultScene();
                isFindScenemng = false;
            }
        }

        if (isGameStart)
        {
            _currentTime -= Time.deltaTime;
            Debug.Log(_currentTime);

            if (_currentTime <= 0 && !isGameOver)
            {
                isGameClear = true;
            }
        }
    }
}