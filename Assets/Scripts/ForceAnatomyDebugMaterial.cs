using UnityEngine;

public class ForceAnatomyDebugMaterial : MonoBehaviour
{
    [SerializeField] private Transform anatomyModel;

    private Material debugMaterial;

    private void Start()
    {
        if (anatomyModel == null)
        {
            Debug.LogError("CHUA GAN ANATOMY MODEL");
            return;
        }

        Shader shader =
            Shader.Find("Universal Render Pipeline/Unlit");

        if (shader == null)
        {
            Debug.LogError(
                "KHONG TIM THAY URP UNLIT SHADER"
            );

            return;
        }

        debugMaterial =
            new Material(shader);

        debugMaterial.color =
            Color.magenta;

        Renderer[] renderers =
            anatomyModel.GetComponentsInChildren<Renderer>(true);

        Debug.Log(
            "DEBUG MATERIAL - Renderers = "
            + renderers.Length
        );

        foreach (Renderer r in renderers)
        {
            r.enabled = true;
            r.sharedMaterial = debugMaterial;
        }

        Debug.Log(
            "DEBUG MATERIAL APPLIED"
        );
    }
}