using UnityEngine;

public class SecretRoomDoorsBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _deniedClip;

    private static bool _isDoorActivated;

    private void Awake()
    {
        _isDoorActivated = false;
    }

    public void ActivateDoors()
    {
        _isDoorActivated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameflowManager.CurrentState == GameStates.Active)
        {
            if (_isDoorActivated)
            {
                _animator.SetTrigger("PlayerFound");
                AudioHandler.Instance.PlaySfx(_openClip);
            }
            else
            {
                AudioHandler.Instance.PlaySfx(_deniedClip);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GameflowManager.CurrentState == GameStates.Active)
        {
            if (_isDoorActivated)
            {
                _animator.SetTrigger("PlayerLost");
            }
        }
    }
}
