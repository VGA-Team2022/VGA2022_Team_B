using System;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary> 各キャラクターのステージ毎のSpriteをまとめるクラス </summary>
    [Serializable]
    public class CharacterSprites
    {
        [SerializeField] private Sprite[] _princessYashiki = default;
        [SerializeField] private Sprite[] _princessSea = default;
        [SerializeField] private Sprite[] _princessGarden = default;
        [SerializeField] private Sprite[] _maid = default;

        public Sprite[] PrincessYashiki => _princessYashiki;
        public Sprite[] PrincessSea => _princessSea;
        public Sprite[] PrincessGarden => _princessGarden;
        public Sprite[] Maid => _maid;
    }

    /// <summary> 背景のSpriteをまとめるクラス </summary>
    [Serializable]
    public class BackGrounds
    {
        [SerializeField] private Sprite _yashikiDaytime = default;
        [SerializeField] private Sprite _yashikiNight = default;
        [SerializeField] private Sprite _seaStageDaytime = default;
        [SerializeField] private Sprite _seaStageNight = default;
        [SerializeField] private Sprite _gardenDaytime = default;
        [SerializeField] private Sprite _gardenNight = default;

        public Sprite YashikiDaytime => _yashikiDaytime;
        public Sprite YashikiNight => _yashikiNight;
        public Sprite SeaStageDaytime => _seaStageDaytime;
        public Sprite SeaStageNight => _seaStageNight;
        public Sprite GardenDaytime => _gardenDaytime;
        public Sprite GardenNight => _gardenNight;
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