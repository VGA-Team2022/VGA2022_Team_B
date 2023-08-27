using Common;
using System.Collections.Generic;
using UnityEngine;

/// <summary> リザルトでの入力、Printerの初期設定を行うクラス </summary>
public class Sequencer : MonoBehaviour
{
    [Tooltip("セリフのデータ")]
    [SerializeField] private TextAsset _textData = default;
    [Tooltip("リザルトの出力関連")]
    [SerializeField] private Printer _printer = new();

    private List<string[]> _resultTexts = new();

    private void OnEnable()
    {
        TextLoad();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _printer.ShowText();
        }
    }

    /// <summary> TextAssetのセリフを配列に格納する </summary>
    private void TextLoad()
    {
        var messages = _textData.text.Split(",,");

        for (int i = 0; i <  messages.Length; i++)
        {
            _resultTexts.Add(messages[i].Split("\n"));
        }
    }

    /// <summary> ResultSceneに表示するTextを設定する処理 </summary>
    public void SetDialogue()
    {
        string[] dialogue = _resultTexts[Result()];
        Stage stage = ChallengedStage();

        _printer.Init(dialogue, stage);
        _printer.SetBackGround();
    }

    private Stage ChallengedStage()
    {
        return GameManager.GameState.Stage switch
        {
            StageType.YASHIKI => Stage.Yashiki,
            StageType.SEA => Stage.Sea,
            StageType.GARDEN => Stage.Garden,
            _ => Stage.None,
        };
    }

    private int Result()
    {
        if (GameManager.GameResult == GameResult.CLEAR)
        {
            switch (GameManager.GameState)
            {
                case GameState { Stage: StageType.YASHIKI, Time: StageTime.DAYTIME }: { return 0; }
                case GameState { Stage: StageType.YASHIKI, Time: StageTime.NIGHT }:   { return 1; }
                case GameState { Stage: StageType.SEA, Time: StageTime.DAYTIME }:     { return 3; }
                case GameState { Stage: StageType.SEA, Time: StageTime.NIGHT }:       { return 4; }
                //ここは分からない
                case GameState { Stage: StageType.GARDEN, Time: StageTime.DAYTIME }:  { return 6; }
                case GameState { Stage: StageType.GARDEN, Time: StageTime.NIGHT }:    { return 7; }
            }
        }
        else if (GameManager.GameResult == GameResult.FAILED)
        {
            switch (GameManager.GameState.Stage)
            {
                case StageType.YASHIKI: { return 2; }
                case StageType.SEA:     { return 5; }
                //ここは分からない
                case StageType.GARDEN:  { return 8; }
            }
        }
        return -1;
    }
}

public enum Stage
{
    None,
    Yashiki,
    Sea,
    Garden,
}
