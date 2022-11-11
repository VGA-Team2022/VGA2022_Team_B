using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�l�~�[����
/// </summary>
public class EnemyInstansManager : MonoBehaviour
{
    [SerializeField, Tooltip("�G�l�~�[")] GameObject[] _enemys = default;
    [SerializeField, Tooltip("�����ʒu")] GameObject[] _enemyspoint = default;
    [SerializeField, Tooltip("�����C���^�[�o��")] float _interval = 2f;
    [SerializeField, Tooltip("�Ăяo���܂ł̎���")] float spawnDelay = 1;

    void Start()
    {
        InvokeRepeating("Instans", spawnDelay, _interval);
    }

    /// <summary>
    /// ����
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
    /// ��납�炭�錢�̎�
    /// </summary>
    public void Dog(GameObject dogs)
    {
        Player players = new Player();
        Debug.Log("��");
        Instantiate(dogs, _enemyspoint[players.NowPos].transform.position, Quaternion.identity);
    }

}
