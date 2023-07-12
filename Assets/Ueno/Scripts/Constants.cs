using System.Collections.Generic;

namespace Common
{
    public static class Define
    {
        public static readonly Dictionary<SceneNames, string> Scenes = new()
        {
            [SceneNames.TITLE_SCENE] = "TitleScene",
            [SceneNames.HOME_SCENE] = "HomeScene",
            [SceneNames.GAME_SCENE] = "GameScene_Master",
            [SceneNames.RESULT_SCENE] = "ResultScene",
        };

        public const string SCENENAME_TITLE = "TitleScene";
        public const string SCENENAME_HOME = "HomeScene";
        public const string SCENENAME_MASTERGAME = "GameScene_Master";
        public const string SCENENAME_RESULT = "ResultScene";
    }

    /// <summary> 挑戦するステージを保存する </summary>
    public struct GameState
    {
        public StageType Stage;
        public StageTime Time;

        public GameState(StageType stage, StageTime time)
        {
            Stage = stage;
            Time = time;
        }
    }

    public enum SceneNames
    {
        TITLE_SCENE,
        HOME_SCENE,
        GAME_SCENE,
        RESULT_SCENE,
    }

    /// <summary> 各ステージの種類 </summary>
    public enum StageType
    {
        NONE,
        YASHIKI,
        SEA,
        GARDEN,
    }

    public enum StageTime
    {
        NONE,
        DAYTIME,
        NIGHT,
    }

    /// <summary> ゲームの結果 </summary>
    public enum GameResult
    {
        NONE,
        CLEAR,
        FAILED,
    }
}