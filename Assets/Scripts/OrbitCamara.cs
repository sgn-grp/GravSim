using UnityEngine;
using UnityEngine.UI;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 50f;
    public float zoomSpeed = 1f;
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float minDistance = -1000f;
    public float maxDistance = 1000f;

    private float x = 0f;
    private float y = 0f;

    private Vector2 lastTouchPos;
    private float lastTouchDistance;

    public float movespeed = 5.0f;
    private bool moveback = false;
    private bool moveforward = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        movespeed = 5.0f;
    }

    public void downheld()
    {
        moveback = true;
    }

    public void downreleased()
    {
        moveback = false;
    }

    public void upheld()
    {
        moveforward = true;
    }
    public void upreleased()
    {
        moveforward = false;
    }

    void LateUpdate()
    {
        if (!target) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        // Desktop: Right Mouse Button for orbit
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
        }

        // Scroll wheel for zoom
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 10f, minDistance, maxDistance);

#else
        // Mobile: One-finger orbit, two-finger pinch to zoom
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;

                x += delta.x * xSpeed * 0.02f * Time.deltaTime;
                y -= delta.y * ySpeed * 0.02f * Time.deltaTime;
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            }
        }

        if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            Vector2 t1Prev = t1.position - t1.deltaPosition;
            Vector2 t2Prev = t2.position - t2.deltaPosition;

            float prevDistance = Vector2.Distance(t1Prev, t2Prev);
            float currentDistance = Vector2.Distance(t1.position, t2.position);
            float delta = prevDistance - currentDistance;

            distance = Mathf.Clamp(distance + delta * zoomSpeed * 0.05f, minDistance, maxDistance);
        }
#endif

        // Apply rotation and position
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;

        if (moveforward)
        {
            target.Translate(movespeed * Time.deltaTime * Vector2.left, Space.Self);
        }

        else if (moveback)
        {
            target.Translate(movespeed * Time.deltaTime * Vector2.right, Space.Self);
        }
    }
}
