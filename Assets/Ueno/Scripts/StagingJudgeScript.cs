using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum Judge
{
    Clear,
    GameOver,
}
public class StagingJudgeScript : MonoBehaviour
{

    [SerializeField] private Judge _stagingJudgeType = Judge.Clear;

    [SerializeField] private Image _clearImage;
    [SerializeField] private Image _gameOverImage;
    [SerializeField] private Sprite _gameOver2;
    [SerializeField] private Sprite _gameOverText2;

    [SerializeField] private bool isDebug;

    private bool _isMigrateToResult;

    private void Start()
    {
        _isMigrateToResult = false;

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
        if (GameManager.IsGameClear && !GameManager.IsGameStaged && !_isMigrateToResult)
        {   
            Debug.Log("CLEAR");
            _isMigrateToResult = true;
            StartCoroutine(Clear());
        }
        else if (GameManager.IsGameOver && !GameManager.IsGameStaged && !_isMigrateToResult)
        {
            _isMigrateToResult = true;
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(2f);
        _clearImage.enabled = true;
        Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.BGM_Type.Jingle_Clear);
        yield return new WaitForSeconds(2f);
        _clearImage.enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.IsGameStaged = true;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        var gameoverTex = _gameOverImage.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Image>();
        _gameOverImage.enabled = true;
        yield return new WaitForSeconds(2f);
        _gameOverImage.sprite = _gameOver2;
        Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.BGM_Type.Jingle_Faild);
        yield return new WaitForSeconds(3f);
        gameoverTex.enabled = true;
        yield return new WaitForSeconds(2f);
        gameoverTex.sprite = _gameOverText2;
        yield return new WaitForSeconds(2f);
        GameManager.IsGameStaged = true;

    }
}
