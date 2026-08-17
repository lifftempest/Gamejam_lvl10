using UnityEngine;

public interface IInteractable
{
    public bool Interacted { get; }
    public void OnInteractAction();
}
