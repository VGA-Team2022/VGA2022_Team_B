using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary> 挑戦するステージの情報 </summary>
    public static GameState GameState = new(StageType.NONE, StageTime.NONE);
    public static GameResult GameResult = GameResult.NONE;

    /// <summary>ゲームクリアまでの時間</summary>
    public static float GameTimeClearLength = 25;

    /// <summary>現在の時間</summary>
    public static float CurrentTime;

    /// <summary>リザルト演出の終了判定</summary>
    public static bool IsFinishedEffect = false;

    /// <summary>一回だけSceneManagerを探す為の判定</summary>
    public static bool IsFindScenemng = false;
    /// <summary>Playerの進行をストップする為の判定</summary>
    public static bool IsStop = false;

    /// <summary>SceneManager格納用変数</summary>
    private AttachedSceneController _scenemng = default;

    /// <summary>ゲームが開始されたかの判定</summary>
    private bool _isGameStart = false;

    /// <summary>Clear判定用のドアを出現させる判定</summary>
    private static bool _isAppearDoorObj = false;

    /// <summary>クリア判定後に出現するobjectフラグ</summary>
    public static bool IsAppearClearObj => _isAppearDoorObj;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PreferenceStage(int num)
    {
        GameState.Stage = num switch
        {
            0 => StageType.YASHIKI,
            1 => StageType.SEA,
            2 => StageType.GARDEN,
            _ => StageType.NONE,
        };
    }

    public void PreferenceTime(int num)
    {
        GameState.Time = num switch
        {
            0 => StageTime.DAYTIME,
            1 => StageTime.NIGHT,
            _ => StageTime.NONE,
        };
    }

    public void PreferenceGameTime(float i)
    {
        GameTimeClearLength = i;
    }

    private void Start()
    {
        _isGameStart = false;
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

            if (GameResult != GameResult.NONE)
            {
                if (IsFinishedEffect) _scenemng.ChangeResultScene();
            }
        }
        //GameClearになったら扉を呼び出す
        if (_isGameStart && !IsStop)
        {
            if (!IsAppearClearObj)
            {
                CurrentTime += Time.deltaTime;
            }

            if (CurrentTime >= GameTimeClearLength && GameResult != GameResult.FAILED)
            {
                _isAppearDoorObj = true;
            }
        }
    }

    /// <summary> 落下モーションを見る為 </summary>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        GameResult = GameResult.FAILED;
    }
}
