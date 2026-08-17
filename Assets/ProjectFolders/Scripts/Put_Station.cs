using UnityEngine;

public class Put_Station : MonoBehaviour
{
    [SerializeField] private CubeBehaviour _cube;
    [SerializeField] private AudioClip _takeClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _cube.transform.parent != _cube.PutCrate && _cube.transform.parent != _cube.GetChest)
        {
            _cube.TakeCubeFromPlayer();
            AudioHandler.Instance.PlaySfx(_takeClip);
        }
    }
}
