using UnityEngine;

/// <summary> ケーキを積む使用人 </summary>
public class Employee : MonoBehaviour
{
    [SerializeField] 
    private GameObject _cake = null;

    private StageMove _stageMove;
    private GameObject _obonObj;
    private bool _cakeInstance = false;

    public void Init(StageMove stageMove, GameObject obon)
    {
        _stageMove = stageMove;
        _obonObj = obon;
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);

        if (transform.position.x <= 0 && !_cakeInstance)
        {
            Debug.Log(1234567898654323456);

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
