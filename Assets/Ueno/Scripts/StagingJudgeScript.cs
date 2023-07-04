using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum Judge
{
    Clear,
    GameOver,
}

public class StagingJudgeScript : MonoBehaviour
{
    [SerializeField] private Image _clearImage = default;
    [SerializeField] private Image _gameOverImage = default;
    [SerializeField] private Sprite _letterOpenImage = default;
    [SerializeField] private Sprite _crackGameOver = default;

    [SerializeField] private RectTransform _unMask = default;
    [Tooltip("GameOverの演出を実行する時間")]
    [SerializeField] private float _duration = 1f;

    [Header("Debug用")]
    [SerializeField] private Judge _stagingJudgeType = Judge.Clear;
    [SerializeField] private bool isDebug = false;

    private bool _isMigrateToResult = false;

    private void Start()
    {
        _gameOverImage.raycastTarget = false;

        _isMigrateToResult = false;

        if (isDebug)
        {
            switch (_stagingJudgeType)
            {
                case Judge.Clear:
                    GameClearEffect();
                    break;

                case Judge.GameOver:
                    GameOverEffect();
                    break;
            }
           // GameManager.isGameStaged = true;
        }
    }

    private void Update()
    {
        if (GameManager.IsGameClear && !GameManager.IsFinishedEffect && !_isMigrateToResult)
        {
            Debug.Log("CLEAR");
            _isMigrateToResult = true;
            GameClearEffect();
        }
        else if (GameManager.IsGameOver && !GameManager.IsFinishedEffect && !_isMigrateToResult)
        {
            _isMigrateToResult = true;
            GameOverEffect();
        }
    }

    private void GameClearEffect()
    {
        var sequence = DOTween.Sequence();

        sequence
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                _clearImage.enabled = true;
                SoundManager.InstanceSound.PlayAudioClip(SoundManager.BGM_Type.Jingle_Clear);
            })
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                _clearImage.enabled = false;
                GameManager.IsFinishedEffect = true;
            });
    }

    private void GameOverEffect()
    {
        var sequence = DOTween.Sequence();
        var gameOverTextImage
            = _gameOverImage.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Image>();

        _gameOverImage.raycastTarget = true;

        sequence
            .Append(_unMask.DOScale(0f, _duration).SetEase(Ease.Linear))
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                _gameOverImage.sprite = _letterOpenImage;
                SoundManager.InstanceSound.PlayAudioClip(SoundManager.BGM_Type.Jingle_Faild);
            })
            .AppendInterval(3f)
            .AppendCallback(() =>
            {
                gameOverTextImage.enabled = true;
                gameOverTextImage.sprite = _crackGameOver;
                GameManager.IsFinishedEffect = true;
            });
    }
}
