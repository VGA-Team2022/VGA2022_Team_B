using Unity.VisualScripting;
using UnityEngine;

using Common;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    /// <summary>ステージの値</summary>
    public static int GameStageNum = 0;
    /// <summary>ステージレベルの値</summary>
    public static int StageLevelNum =  0;

    /// <summary>BGMのボリューム</summary>
    public static float GameBGMVolume = 50;
    /// <summary>SEのボリューム</summary>
    public static float GameSEVolume = 50;
    
    /// <summary>ゲームクリアまでの時間</summary>
    public static float GameTime = Define.GAME_TIME;

    /// <summary>ゲームクリアの判定</summary>
    public static bool isGameClear = false;
    /// <summary>ゲームが開始されたかの判定</summary>
    private bool isGameStart = false;
    /// <summary>ゲームオーバーの判定</summary>
    private bool isGameOver = false;

    /// <summary>scenemanager格納用変数</summary>
    private AttachedSceneController _scenemng;
    /// <summary>現在の時間</summary>
    private float _currentTime;

    bool _ks;

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
        isGameOver = false;
        isGameClear = false;
        _scenemng = GameObject.Find("SceneManager").GetComponent<AttachedSceneController>();
        Debug.Log(_scenemng);

        _ks = false;

    }

    private void Update()
    {
        

        if (_scenemng.gameObject.scene.name == Define.SCENENAME_MASTERGAME && !_ks)
        {
            Debug.Log(_scenemng);
            isGameStart  = true;
            isGameOver = false;
            isGameClear = false;
            _ks = true;
            _currentTime = GameTime;
        }

        GemeClearjudge();

    }

    private void GemeClearjudge()
    {
        if (_scenemng)
        {
            isGameOver = Obon._gameOver;

            if (isGameOver  && !isGameClear)
            {
                _scenemng.ChangeResultScene();
            }
            else if (!isGameOver && isGameClear)
            {
                _scenemng.ChangeResultScene();
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