using UnityEngine;

public class DoorsSignBehabiour : MonoBehaviour
{
    [SerializeField] private Material _greenOff;
    [SerializeField] private Material _greenOn;
    [SerializeField] private Material _redOff;
    [SerializeField] private Material _redOn;
    [Space(2)]
    [SerializeField] private MeshRenderer _greenBulb;
    [SerializeField] private MeshRenderer _redBulb;
    [SerializeField] private AudioClip _enableClip;

    private void Awake()
    {
        _greenBulb.material = _greenOff;
        _redBulb.material = _redOn;
    }

    public void SwitchLights()
    {
        _greenBulb.material = _greenOn;
        _redBulb.material = _redOff;
        AudioHandler.Instance.PlaySfx(_enableClip);
    }
}
