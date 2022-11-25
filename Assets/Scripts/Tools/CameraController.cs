using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool _doMovement = true;

    [Header("Rotate")]
    public float rotateSpeed = 5f;
    Vector3 lastMousePosition;
    Vector3 mouseDelta;

    [Header("Pan")]
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    [Header("Scroll")]
    public float scrollSpeed = 5f;
    public float minScroll = 10f;
    public float maxScroll = 80f;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _doMovement = !_doMovement;
        }

        if (!_doMovement) return;

        Pan();
        Scroll();
        Rotate();
    }

    private void Pan()
    {
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.forward, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.back, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.right, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.left, Space.World);
        }
    }

    private void Scroll()
    {
        var scroll = Input.mouseScrollDelta;
        Vector3 pos = transform.position;

        pos.y -= scroll.y * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minScroll, maxScroll);

        transform.position = pos;
    }

    private void Rotate()
    {
        mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        if (Input.GetKey(KeyCode.Q) || (Input.GetMouseButton(1) && mouseDelta.x < 0))
        {
            transform.RotateAround(transform.position, Vector3.up, 20 * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E) || (Input.GetMouseButton(1) && mouseDelta.x > 0))
        {
            transform.RotateAround(transform.position, Vector3.up, -20 * rotateSpeed * Time.deltaTime);
        }
    }
}
