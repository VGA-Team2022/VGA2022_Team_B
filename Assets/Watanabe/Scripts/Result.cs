﻿using System;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private GameResult _storyJudge = GameResult.YashikiStage_Daytime_Clear;
    [SerializeField] private Sequencer _sequencer = default;

    private static Patterns[] _resultPatterns = default;

    private void Awake()
    {
        //挑戦結果の該当し得るパターンを列挙（ゲーム一周目のみ初期化を行う）
        _resultPatterns ??= new Patterns[]
            { new Patterns(0, 0, true), new Patterns(0, 0, false),
              new Patterns(0, 1, true), new Patterns(0, 1, false),
              new Patterns(1, 0, true), new Patterns(1, 0, false),
              new Patterns(1, 1, true), new Patterns(1, 1, false) };
    }

    private void Start()
    {
        //挑戦結果がリザルトパターンのどれに該当するか
        var index = Array.IndexOf(
                    _resultPatterns,
                    new Patterns(GameManager.GameStageNum, GameManager.StageLevelNum, GameManager.IsGameClear));

        if (index % 2 == 0)
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.BGM_Type.BGM_Result_GameClear);
        }
        else
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.BGM_Type.BGM_Result_GameOver);
        }

        _storyJudge = (GameResult)index;

        _sequencer.SetDialogue(_storyJudge);
    }

    public struct Patterns
    {
        public int Stage;
        public int Level;
        public bool IsClear;

        public Patterns(int stage, int level, bool isClear)
        {
            Stage = stage;
            Level = level;
            IsClear = isClear;
        }
    }
}

public enum GameResult
{
    YashikiStage_Daytime_Clear,
    YashikiStage_Daytime_Failed,
    YashikiStage_Night_Clear,
    YashikiStage_Night_Failed,

    SeaStage_Daytime_Clear,
    SeaStage_Daytime_Failed,
    SeaStage_Night_Clear,
    SeaStage_Night_Failed,
}
