using UnityEngine;

public class CheckAnatomyBounds : MonoBehaviour
{
    [SerializeField] private Transform testModel;
    [SerializeField] private Camera targetCamera;

    private void Start()
    {
        if (testModel == null)
        {
            Debug.LogError("CHUA GAN TEST MODEL");
            return;
        }

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        Renderer[] renderers =
            testModel.GetComponentsInChildren<Renderer>(true);

        Bounds bounds =
            renderers[0].bounds;

        foreach (Renderer r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }

        Vector3 center =
            bounds.center;

        Vector3 viewport =
            targetCamera.WorldToViewportPoint(center);

        bool visible =
            targetCamera.WorldToScreenPoint(center).z > 0;

        int modelLayer =
            renderers[0].gameObject.layer;

        bool layerVisible =
            (targetCamera.cullingMask
            & (1 << modelLayer)) != 0;

        Debug.Log("=== CAMERA RUNTIME TEST ===");

        Debug.Log(
            "Camera = "
            + targetCamera.name
        );

        Debug.Log(
            "Camera Position = "
            + targetCamera.transform.position
        );

        Debug.Log(
            "Camera Rotation = "
            + targetCamera.transform.rotation.eulerAngles
        );

        Debug.Log(
            "Camera Near = "
            + targetCamera.nearClipPlane
        );

        Debug.Log(
            "Camera Far = "
            + targetCamera.farClipPlane
        );

        Debug.Log(
            "Camera CullingMask = "
            + targetCamera.cullingMask
        );

        Debug.Log(
            "Model Layer = "
            + LayerMask.LayerToName(modelLayer)
        );

        Debug.Log(
            "Layer Visible = "
            + layerVisible
        );

        Debug.Log(
            "Bounds Center = "
            + center
        );

        Debug.Log(
            "Viewport = "
            + viewport
        );

        Debug.Log(
            "IN FRONT OF CAMERA = "
            + visible
        );

        Debug.Log(
            "=== END CAMERA TEST ==="
        );
    }
}