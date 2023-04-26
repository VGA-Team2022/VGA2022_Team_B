using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 牛乳を注ぐ女のギミック
/// </summary>

public class MilkMaid : MonoBehaviour
{

    [SerializeField] private float _fadeDuration;//消える速度、間隔
    
    private Image _milkPanel;
    private Color _fadeColor = default;
    private bool isGimmickAction;

    private StageMove _stageMove;

    private void Awake()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _milkPanel = GameObject.Find("MilkPanel").gameObject.GetComponent<Image>();
        _fadeColor = _milkPanel.gameObject.GetComponent<Image>().color;
        isGimmickAction = false;
    }


    // Update is called once per frame
    void Update()
    {
        GimmickAction();
    }

    /// <summary>
    /// アクション開始のメソッド
    /// </summary>
    private void GimmickAction()
    {
        if (this.gameObject.transform.position.x <= 12 && !isGimmickAction)//x軸が12以下になったら
        {
            AudioManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_kaiga_miruku");
            StartCoroutine(FadeIn());
            isGimmickAction = true;
        }
    }

    /// <summary>
    /// ミルクパネルのフェードイン処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        _milkPanel.enabled = true;//imageをon

        float clearScale = 1f;//α値

        Color currentColor = _fadeColor;

        while (clearScale > 0f)//clearScaleが０になるまで回す
        {
            clearScale -= _fadeDuration * Time.deltaTime;//1秒ごとにα値を下げる

            if (clearScale <= 0f)//値が0未満になることを避けている
            {
                clearScale = 0f;
            }

            currentColor.a = clearScale;//α値をcolorに代入

            _milkPanel.color = currentColor;//colorを実際のImageのcolorに代入
            
            yield return null;
        }
        _milkPanel.enabled = false;//while文が終了したらImageをfalseにする
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void FixedUpdate()
    {
        
         this.gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0); 
    }
}
