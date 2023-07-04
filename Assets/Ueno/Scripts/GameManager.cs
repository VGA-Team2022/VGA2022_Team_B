using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>ステージの値</summary>
    public static int GameStageNum = 0;
    /// <summary>ステージレベルの値</summary>
    public static int StageLevelNum = 0;

    /// <summary>ゲームクリアまでの時間</summary>
    public static float GameTimeClearLength = 25;

    /// <summary>現在の時間</summary>
    public static float CurrentTime;

    /// <summary>ゲームが開始されたかの判定</summary>
    private bool _isGameStart = false;

    /// <summary>ゲームクリアの判定</summary>
    public static bool IsGameClear = false;
    /// <summary>ゲームオーバーの判定</summary>
    public static bool IsGameOver = false;

    /// <summary>リザルト演出の終了判定</summary>
    public static bool IsFinishedEffect = false;

    /// <summary>一回だけSceneManagerを探す為の判定</summary>
    public static bool IsFindScenemng = false;
    /// <summary>Playerの進行をストップする為の判定</summary>
    public static bool IsStop = false;

    /// <summary>Clear判定用のドアを出現させる判定</summary>
    private static bool _isAppearDoorObj = false;

    /// <summary>SceneManager格納用変数</summary>
    private AttachedSceneController _scenemng = default;

    /// <summary>クリア判定後に出現するobjectフラグ</summary>
    public static bool IsAppearClearObj => _isAppearDoorObj;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #region ステージとレベルの選択操作で使うメソッド
    /// <summary> ステージ選択 </summary>
    public void PrefarenceStage(int i)
    {
        GameStageNum = i;
    }

    /// <summary> レベル選択 </summary>
    public void PrefarenceLevel(int i)
    {
        StageLevelNum = i;
    }

    /// <summary> 時間設定(?) </summary>
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
            else if (IsGameOver && IsGameClear)//もし両方クリア判定になったら
            {
                _scenemng.ChangeResultScene();
            }
        }
        //GameClearになったら扉を呼び出す
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

    /// <summary> 落下モーションを見る為 </summary>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        IsGameOver = Obon.IsSweetsFall;
    }
}
