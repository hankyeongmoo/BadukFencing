using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("카메라 컴포넌트")]
    public Camera cam;

    [Header("카메라 이동")]
    public float moveSpeed = 5f;

    [Header("카메라 줌")]
    public float zoomSpeed = 5f;
    public float minZoom = 2.5f;
    public float maxZoom = 5f;

    void Update()
    {
        // 카메라 위치 조정
        float midPoint = (float)(Manager_Move.movePoint[0] + Manager_Move.movePoint[1]) / 2f;
        float newX = Mathf.Lerp(transform.position.x, (midPoint - 6f) * 1.5f, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(newX, 0f, -10f);
        
        // 카메라 줌 조정
        int distanceTwoPlayer = Mathf.Abs(Manager_Move.movePoint[1] - Manager_Move.movePoint[0]);
        float camSize = minZoom + (distanceTwoPlayer * 0.25f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(camSize, minZoom, maxZoom), Time.deltaTime * zoomSpeed);
    }
}
