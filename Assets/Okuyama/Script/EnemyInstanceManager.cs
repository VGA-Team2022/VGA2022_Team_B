using System.Collections;
using UnityEngine;

public class EnemyInstanceManager : MonoBehaviour
{
    [SerializeField] private Player _player = null;

    [Tooltip("走る犬エネミー")]
    [SerializeField] private GameObject[] _runDogEnemyPrefab = default;
    [Tooltip("止まる犬エネミー")]
    [SerializeField] private GameObject[] _stopDogEnemyPrefab = default;

    [Tooltip("生成位置")]
    [SerializeField] private Transform[] _enemyspoint = default;
    [Tooltip("前方生成位置")]
    [SerializeField] private Transform[] _enemysForwardPoint = default;

    [Tooltip("生成インターバル")]
    [SerializeField] private float _interval = 2f;
    [Tooltip("呼び出すまでの時間")]
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

        var dogType = Random.Range(0, 2);//0→走る犬、１→止まる犬
        Debug.Log($"dogType={dogType}");

        if (dogType == 1) //走る犬
        {
            var obj = Instantiate(_runDogEnemyPrefab[index], _enemyspoint[point].position, Quaternion.identity);
            obj.GetComponent<EnemyDogScript>().EnemyInstanceManager = this;

        }
        else //止まる犬
        {
            Instantiate(_stopDogEnemyPrefab[index], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }

    /// <summary> 後ろからくる犬の時 </summary>
    public void Dog(GameObject dogs)
    {
        Debug.Log("犬");
        Instantiate(dogs, _enemysForwardPoint[_player.NowPos].transform.position, Quaternion.identity);
    }
}
