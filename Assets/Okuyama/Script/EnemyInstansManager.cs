using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�l�~�[����
/// </summary>
public class EnemyInstansManager : MonoBehaviour
{
    [SerializeField, Tooltip("Player")] Player _player = null;
    [SerializeField, Tooltip("�G�l�~�[")] GameObject[] _enemys = default;
    [SerializeField, Tooltip("�����ʒu")] GameObject[] _enemyspoint = default;
    [SerializeField, Tooltip("�O�������ʒu")] GameObject[] _enemysForwardPoint = default;
    [SerializeField, Tooltip("�����C���^�[�o��")] float _interval = 2f;
    [SerializeField, Tooltip("�Ăяo���܂ł̎���")] float spawnDelay = 1;

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
    /// ����
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
    /// ��납�炭�錢�̎�
    /// </summary>
    public void Dog(GameObject dogs)
    {
        //Player players = new Player();�@//Player�X�N���v�g����C���X�^���X�𐸐� Player�̌��݂�NowPos���g�p
        Debug.Log("��");
        Instantiate(dogs, _enemysForwardPoint[_player.NowPos].transform.position, Quaternion.identity);
    }

}
