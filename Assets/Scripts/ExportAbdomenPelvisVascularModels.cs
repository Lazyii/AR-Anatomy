
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ExportAbdomenPelvisVascularModels : MonoBehaviour
{
    [Header("Vascular Level 1 Map")]
    [SerializeField]
    private VascularLevel1ModelMap vascularLevel1ModelMap;

    [ContextMenu("Export AbdomenPelvis_Vascular Models To Desktop")]
    public void ExportModelsToDesktop()
    {
        if (vascularLevel1ModelMap == null)
        {
            Debug.LogError(
                "[Export AbdomenPelvis] Chưa gán VascularLevel1ModelMap."
            );
            return;
        }

        vascularLevel1ModelMap.Build();

        AnatomyModelGroup group =
            vascularLevel1ModelMap.GetGroup("AbdomenPelvis_Vascular");

        if (group == null)
        {
            Debug.LogError(
                "[Export AbdomenPelvis] Không tìm thấy group " +
                "AbdomenPelvis_Vascular. Kiểm tra GetGroup() trong mapping."
            );
            return;
        }

        StringBuilder content = new StringBuilder();
        content.AppendLine("AbdomenPelvis_Vascular - Mapped Model Names");
        content.AppendLine("Total: " + group.LinkedModels.Count);
        content.AppendLine("-----------------------------------");

        int index = 1;

        foreach (Transform model in group.LinkedModels)
        {
            if (model == null)
                continue;

            content.AppendLine(index + ". " + model.name);
            index++;
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
            "AbdomenPelvis_Vascular_Models.txt"
        );

        try
        {
            File.WriteAllText(
                filePath,
                content.ToString(),
                Encoding.UTF8
            );

            Debug.Log(
                "[Export AbdomenPelvis] Đã xuất " + (index - 1) +
                " model ra: " + filePath
            );
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "[Export AbdomenPelvis] Không thể ghi file: " + ex.Message
            );
        }
    }
}
