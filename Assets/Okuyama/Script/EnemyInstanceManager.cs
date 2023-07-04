using System.Collections;
using UnityEngine;

public class EnemyInstanceManager : MonoBehaviour
{
    [SerializeField] private Player _player = null;

    [Tooltip("���錢�G�l�~�[")]
    [SerializeField] private GameObject[] _runDogEnemyPrefab = default;
    [Tooltip("�~�܂錢�G�l�~�[")]
    [SerializeField] private GameObject[] _stopDogEnemyPrefab = default;

    [Tooltip("�����ʒu")]
    [SerializeField] private Transform[] _enemyspoint = default;
    [Tooltip("�O�������ʒu")]
    [SerializeField] private Transform[] _enemysForwardPoint = default;

    [Tooltip("�����C���^�[�o��")]
    [SerializeField] private float _interval = 2f;
    [Tooltip("�Ăяo���܂ł̎���")]
    [SerializeField] private float spawnDelay = 1;

    private bool _isCreate = false;

    private void Start()
    {
        _isCreate = true;
        StartCoroutine(Create());
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj) _isCreate = false;
    }

    private IEnumerator Create()
    {
        while (_isCreate)
        {
            CreateInstance();
            yield return new WaitForSecondsRealtime(_interval);
        }
    }

    private void CreateInstance()
    {
        var point = Random.Range(0,_enemyspoint.Length);
        var index = Random.Range(0, _runDogEnemyPrefab.Length);

        var dogType = Random.Range(0, 2);//0�����錢�A�P���~�܂錢
        Debug.Log($"dogType={dogType}");

        if (dogType == 1) //���錢
        {
            var obj = Instantiate(_runDogEnemyPrefab[index], _enemyspoint[point].position, Quaternion.identity);
            obj.GetComponent<EnemyDogScript>().EnemyInstanceManager = this;

        }
        else //�~�܂錢
        {
            Instantiate(_stopDogEnemyPrefab[index], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }

    /// <summary> ��납�炭�錢�̎� </summary>
    public void Dog(GameObject dogs)
    {
        Debug.Log("��");
        Instantiate(dogs, _enemysForwardPoint[_player.NowPos].transform.position, Quaternion.identity);
    }
}
