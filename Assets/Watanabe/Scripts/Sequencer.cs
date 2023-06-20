using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    [SerializeField] private TextAsset _textData = default;
    [SerializeField] private Printer _printer = new();

    private List<string[]> _resultTexts = new();

    private void OnEnable()
    {
        TextLoad();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    public void SetDialogue(GameResult result)
    {
        //以下ResultSceneに表示するTextを設定する処理
        string[] dialogue = default;
        switch (result)
        {
            //屋敷ステージ
            case GameResult.YashikiStage_Daytime_Clear:
                dialogue = _resultTexts[0];
                break;
            case GameResult.YashikiStage_Night_Clear:
                dialogue = _resultTexts[1];
                break;
            case GameResult.YashikiStage_Daytime_Failed:
            case GameResult.YashikiStage_Night_Failed:
                dialogue = _resultTexts[2];
                break;
            //海ステージ
            case GameResult.SeaStage_Daytime_Clear:
                dialogue = _resultTexts[3];
                break;
            case GameResult.SeaStage_Night_Clear:
                dialogue = _resultTexts[4];
                break;
            case GameResult.SeaStage_Daytime_Failed:
            case GameResult.SeaStage_Night_Failed:
                dialogue = _resultTexts[5];
                break;
        }
        _printer.Init(dialogue);
        _printer.SetBackGround(result);
    }
}
