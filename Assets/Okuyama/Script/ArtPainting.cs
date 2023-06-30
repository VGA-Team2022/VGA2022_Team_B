using System.Collections;
using UnityEngine;
/// <summary>
/// �G��n
/// </summary>
public class ArtPainting : GimmickBase
{
    [SerializeField, Tooltip("�G��̃X�v���C�g")]
    private Sprite[] _art = default;
    [SerializeField, Tooltip("�^�[�o���{��")]
    private GameObject _tarban = null;
    [SerializeField, Tooltip("�^�[�o����������܂ł̎���")]
    private float _tarbanOff = 3;
    [SerializeField, Tooltip("�A�j���[�V����")]
    private Animator _artAnim = default;

    private StageMove _stageMove;
    private int _artIndex = 0;
    private float _ratio = 0;
    private bool isGimmickAction = false;

    private const float STAGEMOVE_ADJUSTMENT_EIGHT = 8.0f;
    private const int SETACTIV_FALSE = 47, MILK_AMIN_START = 12, MILK_INDEX_ZERO = 0;

    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }

    void Start()
    {
        _ratio = StageMove.SpeedRatio * STAGEMOVE_ADJUSTMENT_EIGHT;
        _artIndex = Random.RandomRange(0, _art.Length);
        GetComponent<SpriteRenderer>().sprite = _art[_artIndex];
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= -SETACTIV_FALSE)
        {
            gameObject.SetActive(false);
            return;
        }
        if (gameObject.transform.position.x <= MILK_AMIN_START && _artIndex == MILK_INDEX_ZERO && isGimmickAction == false)
        {
            _artAnim.Play("Milk");
            //�T�E���h
            isGimmickAction = true;
        }
        gameObject.transform.position -= new Vector3(Time.deltaTime * StageMove.MoveSpeed * _ratio, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")&& _artIndex == 1)
        {
            StartCoroutine(TarbanStart());
            //�T�E���h
        }
        if (_tarban.activeInHierarchy && other.gameObject.CompareTag("Obon"))
        {
            if (other.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(this.transform.position.x);
            }
        }
    }
    IEnumerator TarbanStart()
    {
        _tarban.SetActive(true);
        yield return new WaitForSeconds(_tarbanOff);
        _tarban.SetActive(false);
    }
}
