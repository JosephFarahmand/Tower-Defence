using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = -cam.transform.forward;
    }
}
