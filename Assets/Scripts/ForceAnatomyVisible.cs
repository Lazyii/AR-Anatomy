using UnityEngine;

public class ForceAnatomyVisible : MonoBehaviour
{
    [SerializeField] private Transform anatomyModel;
    [SerializeField] private Camera targetCamera;

    private void Start()
    {
        if (anatomyModel == null)
        {
            Debug.LogError("CHUA GAN ANATOMY MODEL");
            return;
        }

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        Renderer[] renderers =
            anatomyModel.GetComponentsInChildren<Renderer>(true);

        Debug.Log(
            "FORCE TEST - Renderers: "
            + renderers.Length
        );

        foreach (Renderer r in renderers)
        {
            r.enabled = true;

            r.gameObject.layer =
                targetCamera.gameObject.layer;
        }

        anatomyModel.gameObject.SetActive(true);

        Debug.Log(
            "FORCE TEST - Model active: "
            + anatomyModel.gameObject.activeInHierarchy
        );
    }
}