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
        return GameManager.StageType switch
        {
            StageType.YASHIKI_DAYTIME => Stage.Yashiki,
            StageType.YASHIKI_NIGHT => Stage.Yashiki,
            StageType.SEA_DAYTIME => Stage.Sea,
            StageType.SEA_NIGHT => Stage.Sea,
            StageType.GARDEN_DAYTIME => Stage.Garden,
            StageType.GARDEN_NIGHT => Stage.Garden,
            _ => Stage.None,
        };
    }

    private int Result()
    {
        if (GameManager.GameResult == GameResult.CLEAR)
        {
            switch (GameManager.StageType)
            {
                case StageType.YASHIKI_DAYTIME:
                    return 0;
                case StageType.YASHIKI_NIGHT:
                    return 1;
                case StageType.SEA_DAYTIME:
                    return 3;
                case StageType.SEA_NIGHT:
                    return 4;
                //ここは分からない
                case StageType.GARDEN_DAYTIME:
                    return 6;
                case StageType.GARDEN_NIGHT:
                    return 7;
            }
        }
        else if (GameManager.GameResult == GameResult.FAILED)
        {
            switch (GameManager.StageType)
            {
                case StageType.YASHIKI_DAYTIME:
                case StageType.YASHIKI_NIGHT:
                    return 2;
                case StageType.SEA_DAYTIME:
                case StageType.SEA_NIGHT:
                    return 5;
                //ここは分からない
                case StageType.GARDEN_DAYTIME:
                case StageType.GARDEN_NIGHT:
                    return 8;
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
