using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// エネミー生成
/// </summary>
public class EnemyInstansManager : MonoBehaviour
{
    [SerializeField, Tooltip("エネミー")] GameObject[] _enemys = default;
    [SerializeField, Tooltip("生成位置")] GameObject[] _enemyspoint = default;
    [SerializeField, Tooltip("生成インターバル")] float _interval = 2f;
    [SerializeField, Tooltip("呼び出すまでの時間")] float spawnDelay = 1;

    void Start()
    {
        InvokeRepeating("Instans", spawnDelay, _interval);
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Instans()
    {
        var point = Random.Range(1, _enemyspoint.Length);
        var index = Random.Range(0, _enemys.Length);
        if(index == 0) 
        {
            Instantiate(_enemys[index], _enemyspoint[0].transform.position, Quaternion.identity); 
        }
        else {Instantiate(_enemys[index], _enemyspoint[point].transform.position, Quaternion.identity); }
    }

    /// <summary>
    /// 後ろからくる犬の時
    /// </summary>
    public void Dog(GameObject dogs)
    {
        Player players = new Player();
        Debug.Log("犬");
        Instantiate(dogs, _enemyspoint[players.NowPos].transform.position, Quaternion.identity);
    }

}
