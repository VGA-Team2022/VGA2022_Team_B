using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�l�~�[����
/// </summary>
public class EnemyInstansManager : MonoBehaviour
{
    [SerializeField, Tooltip("Player")] Player _player = null;

    [Header("prefab")]
    [SerializeField, Tooltip("���錢�G�l�~�[")] GameObject[] _runDogEnemyPrefab = default;
    [SerializeField, Tooltip("�~�܂錢�G�l�~�[")] GameObject[] _stopDogEnemyPrefab = default;

    [Header("�ʒu")]
    [SerializeField, Tooltip("�����ʒu")] Transform[] _enemyspoint = default;
    [SerializeField, Tooltip("�O�������ʒu")] Transform[] _enemysForwardPoint = default;

    [Header("����")]
    [SerializeField, Tooltip("�����C���^�[�o��")] float _interval = 2f;
    [SerializeField, Tooltip("�Ăяo���܂ł̎���")] float spawnDelay = 1;

    void Start()
    {
        InvokeRepeating("Instans", spawnDelay, _interval);
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj)
        {
            CancelInvoke("Instans");
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    void Instans()
    { 
        var point = Random.Range(0,_enemyspoint.Length);
        var index = Random.Range(0, _runDogEnemyPrefab.Length);

        var dogType = Random.Range(0, 2);//0�����錢�A�P���~�܂錢
        Debug.Log($"dogType={dogType}");

        if (dogType == 1) //���錢
        {
            var obj = Instantiate(_runDogEnemyPrefab[index], _enemyspoint[point].position, Quaternion.identity);
            obj.GetComponent<EnemyDogScript>().EnemyInstansManager = this;

        }
        else //�~�܂錢
        {
            Instantiate(_stopDogEnemyPrefab[index], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }

    /// <summary>
    /// ��납�炭�錢�̎�
    /// </summary>
    public void Dog(GameObject dogs)
    {
        //Player players = new Player();�@//Player�X�N���v�g����C���X�^���X�𐸐� Player�̌��݂�NowPos���g�p
        Debug.Log("��");
        Instantiate(dogs, _enemysForwardPoint[_player.NowPos].transform.position, Quaternion.identity);
    }

}
