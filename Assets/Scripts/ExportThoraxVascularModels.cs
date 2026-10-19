
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ExportThoraxVascularModels : MonoBehaviour
{
    [Header("Vascular Level 1 Map")]
    [SerializeField]
    private VascularLevel1ModelMap vascularLevel1ModelMap;

    [ContextMenu("Export Thorax_Vascular Models To Desktop")]
    public void ExportThoraxVascularModelsToDesktop()
    {
        if (vascularLevel1ModelMap == null)
        {
            Debug.LogError(
                "[Export Thorax] Chưa gán VascularLevel1ModelMap."
            );
            return;
        }

        // Build mapping trước khi đọc dữ liệu.
        vascularLevel1ModelMap.Build();

        AnatomyModelGroup group =
            vascularLevel1ModelMap.GetGroup("Thorax_Vascular");

        if (group == null)
        {
            Debug.LogError(
                "[Export Thorax] Không tìm thấy group Thorax_Vascular. " +
                "Kiểm tra key mà GetGroup() hỗ trợ."
            );
            return;
        }

        StringBuilder content = new StringBuilder();
        content.AppendLine("Thorax_Vascular - Mapped Model Names");
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
            Environment.SpecialFolder.Desktop
        );

        string filePath = Path.Combine(
            desktopPath,
            "Thorax_Vascular_Models.txt"
        );

        File.WriteAllText(
            filePath,
            content.ToString(),
            Encoding.UTF8
        );

        Debug.Log(
            "[Export Thorax] Đã xuất " + (index - 1) +
            " model ra: " + filePath
        );
    }
}
