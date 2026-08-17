using UnityEngine;

public class IceTilesBeh : MonoBehaviour
{
    [SerializeField] private MeshRenderer _iceDecal;
    [SerializeField] private AudioClip _appearClip;

    private void OnTriggerEnter(Collider other)
    {
        if (_iceDecal.enabled && other.CompareTag("Player"))
        {
            GameflowManager.Instance.StopGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerIceHandling>(out var player))
            {
                if (player.IsCarryingIce)
                {
                    _iceDecal.enabled = true;
                    AudioHandler.Instance.PlaySfx(_appearClip);
                }
            }
        }
    }
}
