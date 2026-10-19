
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class ExportSkeletalSystemLevel1Models : MonoBehaviour
{
    [Header("Model nguồn từ Blender")]
    [SerializeField]
    private Transform skeletalSystemRoot;

    [ContextMenu("Export SkeletalSystem Level 1 Models To Desktop")]
    public void ExportModelsToDesktop()
    {
        if (skeletalSystemRoot == null)
        {
            Debug.LogError(
                "[Export SkeletalSystem] Chưa gán " +
                "ARAnatomyRoot/SkeletalSystem."
            );
            return;
        }

        Renderer[] renderers =
            skeletalSystemRoot.GetComponentsInChildren<Renderer>(true);

        HashSet<Transform> uniqueModels = new HashSet<Transform>();
        List<Transform> mappedModels = new List<Transform>();

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

        mappedModels.Sort((a, b) =>
            string.Compare(a.name, b.name, StringComparison.Ordinal));

        StringBuilder content = new StringBuilder();
        content.AppendLine("SkeletalSystem - Level 1 Mapped Models");
        content.AppendLine("Total: " + mappedModels.Count);
        content.AppendLine("-----------------------------------");

        for (int i = 0; i < mappedModels.Count; i++)
        {
            content.AppendLine(
                (i + 1) + ". " + mappedModels[i].name
            );
        }

        string desktopPath = Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory
        );

        if (string.IsNullOrEmpty(desktopPath))
        {
            desktopPath = Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop
            );
        }

        string filePath = Path.Combine(
            desktopPath,
            "SkeletalSystem_Level1_Models.txt"
        );

        try
        {
            File.WriteAllText(
                filePath,
                content.ToString(),
                Encoding.UTF8
            );

            Debug.Log(
                "[Export SkeletalSystem] Đã xuất " +
                mappedModels.Count + " model ra: " + filePath
            );
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "[Export SkeletalSystem] Không thể ghi file: " +
                ex.Message
            );
        }
    }
}
