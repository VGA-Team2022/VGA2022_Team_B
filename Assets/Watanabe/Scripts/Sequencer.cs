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
    public void SetDialogue(GameResult result)
    {
        string[] dialogue = default;
        Stage stage = Stage.Yashiki;

        switch (result)
        {
            //屋敷ステージ
            case GameResult.YashikiStage_Daytime_Clear:
                dialogue = _resultTexts[0];
                stage = Stage.Yashiki;
                break;
            case GameResult.YashikiStage_Night_Clear:
                dialogue = _resultTexts[1];
                stage = Stage.Yashiki;
                break;
            case GameResult.YashikiStage_Daytime_Failed:
            case GameResult.YashikiStage_Night_Failed:
                dialogue = _resultTexts[2];
                stage = Stage.Yashiki;
                break;
            //海ステージ
            case GameResult.SeaStage_Daytime_Clear:
                dialogue = _resultTexts[3];
                stage = Stage.Sea;
                break;
            case GameResult.SeaStage_Night_Clear:
                dialogue = _resultTexts[4];
                stage = Stage.Sea;
                break;
            case GameResult.SeaStage_Daytime_Failed:
            case GameResult.SeaStage_Night_Failed:
                dialogue = _resultTexts[5];
                stage = Stage.Sea;
                break;
        }
        _printer.Init(dialogue, stage);
        _printer.SetBackGround(result);
    }
}

public enum Stage
{
    Yashiki,
    Sea,
}
