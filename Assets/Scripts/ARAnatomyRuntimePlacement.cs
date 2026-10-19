using UnityEngine;

public class ARAnatomyRuntimePlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera arCamera;

    [Header("Placement")]
    [SerializeField] private float distanceFromCamera = 1.5f;
    [SerializeField] private float heightOffset = -0.5f;

    private void Start()
    {
        if (arCamera == null)
        {
            arCamera = Camera.main;
        }

        if (arCamera == null)
        {
            Debug.LogError("KHONG TIM THAY MAIN CAMERA");
            return;
        }

        Invoke(nameof(PlaceInFrontOfCamera), 1.0f);
    }

    private void LateUpdate()
    {
        if (arCamera == null)
            return;

        // Chỉ đặt model lại nếu đang ở rất xa camera.
        float distance =
            Vector3.Distance(
                transform.position,
                arCamera.transform.position
            );

        if (distance > 5f)
        {
            PlaceInFrontOfCamera();
        }
    }

    [ContextMenu("PLACE ANATOMY IN FRONT OF CAMERA")]
    public void PlaceInFrontOfCamera()
    {
        if (arCamera == null)
            return;

        Vector3 forward =
            arCamera.transform.forward;

        Vector3 position =
            arCamera.transform.position
            + forward * distanceFromCamera;

        // Hạ model theo hướng xuống của camera
        position +=
            arCamera.transform.up * heightOffset;

        transform.position =
            position;

        transform.rotation =
            Quaternion.LookRotation(
                forward,
                Vector3.up
            );

        Debug.Log(
            "ANATOMY PLACED"
            + " | POS = "
            + transform.position
            + " | CAMERA = "
            + arCamera.transform.position
            + " | HEIGHT OFFSET = "
            + heightOffset
        );
    }
}