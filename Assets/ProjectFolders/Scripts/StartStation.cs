using System.Collections;
using UnityEngine;

public class StartStation : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _startNote;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private PlayerLocomotionInput _playerInput;
    [SerializeField] private AudioClip _activationClip;
    private bool _interacted;

    public bool Interacted => _interacted;

    public void OnInteractAction()
    {
        AudioHandler.Instance.PlaySfx(_activationClip);
        if (GameflowManager.Instance.DeliveryCount == 5)
        {
            HandleWin();
        }
        else
        {
            StartCoroutine(InteractAction());
        }
    }

    public void CloseNote()
    {
        GameflowManager.Instance.StartGame();
        _startNote.SetActive(false);
        _interacted = false;
    }

    private IEnumerator InteractAction()
    {
        _startNote.SetActive(true);
        _interacted = true;
        yield return new WaitForSeconds(0.1f);
        GameflowManager.Instance.PauseGame(false);
    }

    private void HandleWin()
    {
        GameflowManager.Instance.PauseGame(false);
        _winPanel.SetActive(true);
    }
}
