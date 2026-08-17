using System.Collections;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerIceHandling _player;
    [SerializeField] private GameObject _getPlacePoint;
    [SerializeField] private GameObject _putPlacePoint;
    [SerializeField] private GameObject _playerPlacePoint;
    [Space(4)]
    [SerializeField] private Animator _giveChestAnimator;
    [SerializeField] private Animator _putCrateAnimator;

    private bool _interacted;

    public bool Interacted => _interacted;
    public bool WasCarried { get; private set; }
    public Animator GiveChest => _giveChestAnimator;
    public Transform PutCrate => _putPlacePoint.transform;
    public Transform GetChest => _getPlacePoint.transform;

    private void Awake()
    {
        WasCarried = true;
    }

    public void OnInteractAction()
    {
        if (Interacted != true)
        {
            GivePlayerCube();
        }
    }

    public void GivePlayerCube()
    {
        print("this shit've interacted");
        _player.TakeIce();
        _interacted = true;
        this.transform.parent = _playerPlacePoint.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    }

    public void TakeCubeFromPlayer()
    {
        if (GameflowManager.Instance.DeliveryCount < 5)
        {
            StartCoroutine(ExecuteTakeCube());
        }
    }

    public void SetupNewCube()
    {
        if (GameflowManager.Instance.DeliveryCount < 5)
        {
            StartCoroutine(ActivateCube());
        }
    }
    
    private IEnumerator ActivateCube()
    {
        if (this.transform.parent == _putPlacePoint.transform)
        {
            _putCrateAnimator.SetTrigger("CubeGetted");
        }

        this.transform.parent = _getPlacePoint.transform;
        this.transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
        _giveChestAnimator.SetTrigger("GiveCube");
        var hold = _giveChestAnimator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(hold);
        WasCarried = false;
        _interacted = false;
    }

    private IEnumerator ExecuteTakeCube()
    {
        this.transform.parent = _putPlacePoint.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.rotation *= Quaternion.identity;
        _putCrateAnimator.SetTrigger("CubePut");
        var hold = _giveChestAnimator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(hold);
        WasCarried = true;
        _player.DeleteIce();
        GameflowManager.Instance.AddDeliveryCount();
        //gameObject.SetActive(false);
    }
}
