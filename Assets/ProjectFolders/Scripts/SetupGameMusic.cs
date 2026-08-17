using System.Collections;
using UnityEngine;

public class SetupGameMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private AudioClip _gameplayMusic;

    private void Awake()
    {

    }

    private IEnumerator PlayMusic()
    {
        while (AudioHandler.Instance == null)
        {
            yield return null;
        }

    }
}
