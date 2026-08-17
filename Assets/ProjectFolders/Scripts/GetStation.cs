using UnityEngine;

public class GetStation : MonoBehaviour
{
    [SerializeField] private CubeBehaviour _cube;
    [SerializeField] private AudioClip _activateClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _cube.WasCarried || _cube.gameObject.activeInHierarchy == false)
        {
            _cube.SetupNewCube();
            AudioHandler.Instance.PlaySfx(_activateClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _cube.Interacted == true)
        {
            _cube.GiveChest.SetTrigger("CubeTaken");
        }
    }
}
