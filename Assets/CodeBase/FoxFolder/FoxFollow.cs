using UnityEngine;

public class FoxFollow : MonoBehaviour
{
    public Transform _foxTarget;
    public float _foxLerpRate;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _foxTarget.position, Time.deltaTime * _foxLerpRate);
    }
}
