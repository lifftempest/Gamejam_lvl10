using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-3)]
public class GameflowManager : MonoBehaviour
{
    [SerializeField] private PlayerLocomotionInput _playerInput;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private GameObject _finishIndicator;

    public byte DeliveryCount { get; private set; }
    public static GameStates CurrentState { get; private set; }
    public static GameflowManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
        PauseGame(false);
    }

    private void Update()
    {
        print(CurrentState);
    }

    public void PauseGame(bool flag)
    {
        CurrentState = GameStates.Paused;
        UI_WindowManager.Instance.OpenPauseWindow(flag);
        Time.timeScale = 0;
        ShowCursor(true);
        _playerInput.enabled = false;
    }

    public void StopGame()
    {
        StartCoroutine(ExecutePlayerDeath());
    }

    public void StartGame()
    {
        _playerInput.enabled = true;
        _playerController.enabled = true;
        CurrentState = GameStates.Active;
        Time.timeScale = 1;
        ShowCursor(false);
    }

    private void ShowCursor(bool flag)
    {
        if (flag)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private IEnumerator ExecutePlayerDeath()
    {
        _playerController.enabled = false;
        CurrentState = GameStates.Stopped;
        _playerInput.enabled = false;
        _playerAnimator.SetTrigger("Dead");
        var time = _playerAnimator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(time + 0.5f);
        UI_WindowManager.Instance.OpenStoppedWindow();
        ShowCursor(true);
    }

    public void AddDeliveryCount()
    {
        if (DeliveryCount < 5)
        {
            DeliveryCount++;
        }
        else
        {
            _finishIndicator.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

public enum GameStates
{
    Active, Paused, Stopped 
}