using UnityEngine;

/// <summary> �P�[�L��ςގg�p�l </summary>
public class Employee : GimmickBase
{
    [SerializeField] 
    private GameObject _cake = null;

    private GameObject _obonObj;
    private bool _cakeInstance = false;

    public void Init(GameObject obon)
    {
        _obonObj = obon;
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(Time.deltaTime * StageMovement.MoveSpeed, 0);

        if (transform.position.x <= 0 && !_cakeInstance)
        {
            var cake = _obonObj.GetComponent<Obon>().SweetsAdd(_cake, true);
            cake.transform.parent = _obonObj.transform;
            _cakeInstance = true;
        }

        if (transform.position.x <= -47)
        {
            gameObject.SetActive(false);
        }
    }
}
