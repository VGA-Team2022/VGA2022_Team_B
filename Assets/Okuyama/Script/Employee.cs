using UnityEngine;

/// <summary> ケーキを積む使用人 </summary>
public class Employee : GimmickBase
{
    [SerializeField] 
    private GameObject _cake = null;
    [SerializeField]
    private float _moveSpeed = 5;

    private GameObject _obonObj;
    private bool _isCakeInstance = false;

    private SpriteRenderer _handingCake = default;

    public void Init(GameObject obon)
    {
        _obonObj = obon;

        _handingCake = _cake.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0);

        if (transform.position.x <= 0 && !_isCakeInstance)
        {
            var cake = _obonObj.GetComponent<Obon>().SweetsAdd(_cake, true);
            cake.transform.parent = _obonObj.transform;
            _handingCake.enabled = false;
            _isCakeInstance = true;
        }
    }

    /// <summary>
    /// カメラ外に出たら描画終了
    /// </summary>
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
