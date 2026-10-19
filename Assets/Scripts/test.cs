using UnityEngine;

public class CameraRuntimeTracker : MonoBehaviour
{
    private Camera cam;

    private float timer;

    private void Start()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("KHONG TIM THAY MAIN CAMERA");
            return;
        }

        Debug.Log("=== CAMERA TRACK START ===");
    }

    private void Update()
    {
        if (cam == null)
            return;

        timer += Time.deltaTime;

        if (timer < 0.5f)
            return;

        timer = 0f;

        Debug.Log(
            "CAM POS = "
            + cam.transform.position
            + " | ROT = "
            + cam.transform.rotation.eulerAngles
        );
    }
}