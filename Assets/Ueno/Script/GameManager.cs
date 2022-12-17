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

    /// <summary>一回だけSceneManagerを探す為の判定</summary>
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