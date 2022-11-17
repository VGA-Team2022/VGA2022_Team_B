using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilkHumanScript : MonoBehaviour
{
    Color color; //水滴の不透明度を変化するための

    [Tooltip("牛乳を注ぐ女の牛乳で視界が覆われる"), SerializeField] Image _milk = null;
    private void Start()
    {
        _milk.gameObject.SetActive(false);
        color = _milk.color;
        color.a = 0.0f;
    }
    private void Update()
    {
        if (this.gameObject.transform.position.x == 12)
        {
            _milk.gameObject.SetActive(true);
            color.a = 100.0f;
            StartCoroutine(FadeInMilk());
        }
        //5秒かけてフェードアウト
        if(this.gameObject.transform.position.x == -20)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FadeInMilk()
    {
        if (color.a > 1.6f) //1.5秒かけてフェードアウト
        {
            color.a -= Time.deltaTime * 1.5f;
        }
        _milk.gameObject.SetActive(false);
        yield return null;
    }
}
