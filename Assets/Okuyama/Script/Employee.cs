using UnityEngine;

/// <summary> ケーキを積む使用人 </summary>
public class Employee : GimmickBase
{
    [SerializeField] 
    private GameObject _cake = null;

    private GameObject _obonObj;
    private bool _isCakeInstance = false;

    private SpriteRenderer _handingCake = default;

    public void Init(GameObject obon)
    {
        _obonObj = obon;

        var child = transform.GetChild(0).gameObject;
        var grandchild = child.transform.GetChild(0).gameObject;

        _handingCake = grandchild.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(Time.deltaTime * StageMovement.MoveSpeed, 0);

        if (transform.position.x <= 0 && !_isCakeInstance)
        {
            var cake = _obonObj.GetComponent<Obon>().SweetsAdd(_cake, true);
            cake.transform.parent = _obonObj.transform;
            _handingCake.enabled = false;
            _isCakeInstance = true;
        }

        if (transform.position.x <= -47)
        {
            gameObject.SetActive(false);
        }
    }
}
