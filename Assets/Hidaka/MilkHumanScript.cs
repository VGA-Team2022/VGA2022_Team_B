using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilkHumanScript : MonoBehaviour
{

    Image _milk = null; //敵オブジェクトの攻撃用イメージ

    Color _fadeMilk; //水滴の不透明度を変化するための
    bool _isMilkAttack;

    private void Awake()
    {
        _milk = GameObject.Find("MilkPanel").gameObject.GetComponent<Image>();
        _fadeMilk = _milk.gameObject.GetComponent<Image>().color;
        _isMilkAttack = false;
    }
    private void Update()
    {
        MilkAttack();
    }

    void MilkAttack()
    {
        if (this.gameObject.transform.position.x == 12 && _isMilkAttack == false) //x軸12になったときに発動
        {
            StartCoroutine(FadeInMilk());
            _isMilkAttack = true;
        }
    }

    private IEnumerator FadeInMilk()
    {
        _milk.enabled = true;
        float _milkAlpha = 1f;
        Color _milkColor = _fadeMilk;

        while(_milkAlpha > 0f)
        {
            _fadeMilk.a -= Time.deltaTime * 1.5f;//1.5秒かけてフェードアウト
            if (_milkAlpha <= 0f) 
            {
                _milkAlpha = 0f;
            }
            _milk.color = _milkColor;

            yield return null;
        }
        _milk.enabled = false;
    }
}
