using UnityEngine;

public class Ui_Sounds_Handler : MonoBehaviour
{
    [SerializeField] private AudioClip _uiClick;

    public void PlayClick()
    {
        AudioHandler.Instance.PlaySfx(_uiClick);
    }
}
