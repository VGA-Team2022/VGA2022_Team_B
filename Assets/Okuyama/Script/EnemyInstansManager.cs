using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// エネミー生成
/// </summary>
public class EnemyInstansManager : MonoBehaviour
{
    [SerializeField, Tooltip("Player")] Player _player = null;
    [SerializeField, Tooltip("エネミー")] GameObject[] _enemys = default;
    [SerializeField, Tooltip("生成位置")] GameObject[] _enemyspoint = default;
    [SerializeField, Tooltip("前方生成位置")] GameObject[] _enemysForwardPoint = default;
    [SerializeField, Tooltip("生成インターバル")] float _interval = 2f;
    [SerializeField, Tooltip("呼び出すまでの時間")] float spawnDelay = 1;

    void Start()
    {
        InvokeRepeating("Instans", spawnDelay, _interval);
    }

    private void Update()
    {
        if (GameManager.isAppearDoorObj)
        {
            CancelInvoke("Instans");
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Instans()
    {
        //Debug.Log($"NowPos{_player.NowPos}"); 
        var point = Random.Range(1, _enemyspoint.Length);
        var index = Random.Range(0, _enemys.Length);
        if(index == 0) 
        {
            var obj = Instantiate(_enemys[index], _enemyspoint[3].transform.position, Quaternion.identity);
            obj.GetComponent<Enemy_Dog>().EnemyInstansManager = this;
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_big dog_cry");
        }
        else 
        {
            Instantiate(_enemys[index], _enemyspoint[_player.NowPos].transform.position, Quaternion.identity);
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_big dog_cry");
        }
    }

    /// <summary>
    /// 後ろからくる犬の時
    /// </summary>
    public void Dog(GameObject dogs)
    {
        //Player players = new Player();　//Playerスクリプトからインスタンスを精製 Playerの現在のNowPosを使用
        Debug.Log("犬");
        Instantiate(dogs, _enemysForwardPoint[_player.NowPos].transform.position, Quaternion.identity);
    }

}
