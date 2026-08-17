using UnityEngine;

public class SealBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip _sealClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioHandler.Instance.PlaySfx(_sealClip);
        }
    }
}
