using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerLocomotionInput _playerInput;
    [SerializeField] private PlayerIceHandling _playerIceHandling;
    [SerializeField] private GameObject _interactionText;
    [Space(2), Header("Settings")]
    [SerializeField] private float _interactDistance;
    [SerializeField] private Vector3 _interactionBox;

    private void Update()
    {
        DrawInteractionRay();
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
    }

    private void DrawInteractionRay()
    {
        if (Physics.BoxCast(Camera.main.transform.position, 
            _interactionBox, Camera.main.transform.forward, 
            out RaycastHit hit, 
            Quaternion.identity, 
            _interactDistance))
        {
            if (hit.transform.gameObject.TryGetComponent<IInteractable>(out var interacted))
            {
                if (interacted.Interacted != true)
                {
                    ToggleInteractionText(true);
                }
                else { ToggleInteractionText(false); }

                if (_playerInput.InteractionButtonPressed)
                {
                    interacted.OnInteractAction();
                    print("Interactable!");
                }
            }
            else
            {
                ToggleInteractionText(false);
            }
        }
    }

    private void ToggleInteractionText(bool flag)
    {
        if (_interactionText.activeInHierarchy != flag)
        {
            _interactionText.SetActive(flag);
        }
    }
}
