using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//using UnityEngine.InputSystem.Switch;
using UnityEngine.UI;


public enum Judge
{
    Clear,
    GameOver,
}
public class StagingJudgeScript : MonoBehaviour
{

    [SerializeField] private Judge _stagingJudgeType= Judge.Clear;

    [SerializeField] private Image _clearImage;
    [SerializeField] private Image _gameOverImage;
    [SerializeField] private Sprite _gameOver2;
    [SerializeField] private Sprite _gameOverText2;

    [SerializeField] private bool isDebug;

    private bool isMigrateToResult;
    // Start is called before the first frame update
    void Start()
    {
        isMigrateToResult = false;

        if (isDebug)
        {
            switch (_stagingJudgeType)
            {
                case Judge.Clear:
                    StartCoroutine(Clear());
                    break;

                case Judge.GameOver:
                    StartCoroutine(GameOver());
                    break;

            }

           // GameManager.isGameStaged = true;
        }

    }

    private void Update()
    {
        if (GameManager.isGameClear && !GameManager.isGameStaged && !isMigrateToResult)
        {
             
            UnityEngine.Debug.Log("CLEARRRRRRRRRRRRRRRRRRRRRRRRRR");
            isMigrateToResult = true;
            StartCoroutine(Clear());
        }
        else if (GameManager.isGameOver && !GameManager.isGameStaged && !isMigrateToResult)
        {
            isMigrateToResult = true;
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(2f);
        _clearImage.enabled = true;
        AudioManager.Instance.CriAtomBGMPlay("jingle_success");
        yield return new WaitForSeconds(2f);
        _clearImage.enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.isGameStaged = true;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        var gameoverTex = _gameOverImage.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Image>();
        _gameOverImage.enabled = true;
        yield return new WaitForSeconds(2f);
        _gameOverImage.sprite = _gameOver2;
        AudioManager.Instance.CriAtomBGMPlay("jingle_failure");
        yield return new WaitForSeconds(3f);
        gameoverTex.enabled = true;
        yield return new WaitForSeconds(2f);
        gameoverTex.sprite = _gameOverText2;
        yield return new WaitForSeconds(2f);
        GameManager.isGameStaged = true;

    }
}
