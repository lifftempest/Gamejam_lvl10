using UnityEngine;

public class PlayerIceHandling : MonoBehaviour
{
    [SerializeField] private AudioClip _squeakClip;
    [SerializeField] private AudioClip _boneCrackClip;

    public bool IsCarryingIce { get; private set; }

    private void Awake()
    {
        IsCarryingIce = false;
    }

    public void TakeIce()
    {
        if(GameflowManager.CurrentState == GameStates.Active && IsCarryingIce != true)
        IsCarryingIce = true;
    }
    public void DeleteIce()
    {
        if (GameflowManager.CurrentState == GameStates.Active && IsCarryingIce == true)
            IsCarryingIce = false;
    }

    public void PlaySqueak()
    {
        AudioHandler.Instance.PlaySfx(_squeakClip);
    }

    public void PlayBoneCrack()
    {
        AudioHandler.Instance.PlaySfx(_boneCrackClip);
    }
}
