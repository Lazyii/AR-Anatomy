
using System.Collections.Generic;
using UnityEngine;

public class SkeletalSystemModelMap : MonoBehaviour
{
    [Header("Model nguồn từ Blender")]
    [SerializeField]
    private Transform skeletalSystemRoot;

    [Header("Danh sách model đã mapping")]
    [SerializeField]
    private List<Transform> mappedModels = new List<Transform>();

    public IReadOnlyList<Transform> MappedModels
    {
        get { return mappedModels; }
    }

    private void Start()
    {
        Build();
        ShowMappedModels();
    }

    [ContextMenu("Build Skeletal System Mapping")]
    public void Build()
    {
        mappedModels.Clear();

        if (skeletalSystemRoot == null)
        {
            Debug.LogError(
                "[SkeletalSystem] Chưa gán model nguồn " +
                "ARAnatomyRoot/SkeletalSystem."
            );
            return;
        }

        Renderer[] renderers =
            skeletalSystemRoot.GetComponentsInChildren<Renderer>(true);

        HashSet<Transform> uniqueModels = new HashSet<Transform>();

        foreach (Renderer renderer in renderers)
        {
            if (renderer == null)
                continue;

            if (!(renderer is MeshRenderer) &&
                !(renderer is SkinnedMeshRenderer))
                continue;

            Transform model = renderer.transform;

            if (uniqueModels.Add(model))
                mappedModels.Add(model);
        }

        Debug.Log(
            "[SkeletalSystem] Mapping hoàn tất. Tổng số model: " +
            mappedModels.Count
        );
    }

    public void ShowMappedModels()
    {
        foreach (Transform model in mappedModels)
        {
            if (model == null)
                continue;

            // Bật object chứa mesh
            model.gameObject.SetActive(true);

            // Bật renderer để model hiển thị
            Renderer[] renderers = model.GetComponents<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                    renderer.enabled = true;
            }
        }

        Debug.Log(
            "[SkeletalSystem] Đã hiển thị các model đã mapping."
        );
    }
}
