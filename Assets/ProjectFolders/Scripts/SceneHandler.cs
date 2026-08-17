using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private AudioClip _gameplayMusic;

    private WaitForSeconds hold = new(0.1f);

    private void Awake()
    {
        StartCoroutine(PlayMusic());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);

        if (sceneIndex == 0)
        {
            GameflowManager.Instance.StopGame();
        }
    }

    private IEnumerator ExecuteSceneChange(int sceneIndex)
    {
        yield return hold;
        SceneManager.LoadScene(sceneIndex);

        if (sceneIndex == 0)
        {
            GameflowManager.Instance.StopGame();
        }
    }

    private IEnumerator ExecuteSceneReload()
    {
        yield return hold;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator PlayMusic()
    {
        while (AudioHandler.Instance == null)
        {
            yield return null;
        }
        AudioClip clip = null;

        var index = SceneManager.GetActiveScene().buildIndex;
        switch (index) 
        {
            case 0:
                clip = _mainMenuMusic;
                break;
            case 1:
                clip = _gameplayMusic;
                break;
            default: break;
        }
            

        AudioHandler.Instance.PlayMusic(clip);
    }
}
