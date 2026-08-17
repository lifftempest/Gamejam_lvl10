using UnityEngine;

public class SecretRoomButtonBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private SecretRoomDoorsBehaviour _doors;
    [SerializeField] private DoorsSignBehabiour _signBehabiour;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _punchClip;

    private bool _interacted;

    public bool Interacted => _interacted;

    public void OnInteractAction()
    {
        if (Interacted != true)
        {
            AudioHandler.Instance.PlaySfx(_punchClip);
            _doors.ActivateDoors();
            _animator.SetTrigger("Activated");
            _interacted = true;
            _signBehabiour.SwitchLights();
        }
    }
}
