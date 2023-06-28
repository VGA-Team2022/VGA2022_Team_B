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
       // public const float GAME_TIME = 90;
    }

    public enum SceneNames
    {
        TITLE_SCENE,
        HOME_SCENE,
        GAME_SCENE,
        RESULT_SCENE,
    }
}