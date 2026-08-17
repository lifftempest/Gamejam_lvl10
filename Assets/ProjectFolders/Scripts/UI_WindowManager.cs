using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-4)]
public class UI_WindowManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _stoppedWindow;

    private GameObject _currentWindow;
    private WaitForSeconds hold = new(0.1f);

    public static UI_WindowManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }

        _currentWindow = _mainWindow;
        _currentWindow.SetActive(true);
    }

    public void ChangeWindow(GameObject newWindow)
    {
        _currentWindow.SetActive(false);
        _currentWindow = newWindow;
        _currentWindow.SetActive(true);
    }

    public void OpenPauseWindow(bool flag)
    {
        if(flag)
        ChangeWindow(_settingsWindow);
    }

    public void OpenStoppedWindow()
    {
        ChangeWindow(_stoppedWindow);
    }

    private IEnumerator ExecuteWindowChange(GameObject newWindow)
    {
        yield return hold;
        _currentWindow.SetActive(false);
        _currentWindow = newWindow;
        _currentWindow.SetActive(true);
    }

    public void ExitGame()
    {
        StartCoroutine(ExecuteExitGame());
    }

    private IEnumerator ExecuteExitGame()
    {
        yield return hold;
        GameflowManager.Instance.ExitGame();
    }
}
