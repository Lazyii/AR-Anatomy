using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ExportLeftLowerLimbVascularModels : MonoBehaviour
{
    [Header("Vascular Level 1 Map")]
    [SerializeField]
    private VascularLevel1ModelMap vascularLevel1ModelMap;

    [ContextMenu("Export LeftLowerLimb_Vascular Models To Desktop")]
    public void ExportModelsToDesktop()
    {
        if (vascularLevel1ModelMap == null)
        {
            Debug.LogError(
                "[Export LeftLowerLimb] Chưa gán VascularLevel1ModelMap."
            );
            return;
        }

        vascularLevel1ModelMap.Build();

        AnatomyModelGroup group =
            vascularLevel1ModelMap.GetGroup("LeftLowerLimb_Vascular");

        if (group == null)
        {
            Debug.LogError(
                "[Export LeftLowerLimb] Không tìm thấy group " +
                "LeftLowerLimb_Vascular. Hãy kiểm tra tên group trong mapping."
            );
            return;
        }

        StringBuilder content = new StringBuilder();
        content.AppendLine(
            "LeftLowerLimb_Vascular - Mapped Model Names"
        );
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
            "LeftLowerLimb_Vascular_Models.txt"
        );

        try
        {
            File.WriteAllText(
                filePath,
                content.ToString(),
                Encoding.UTF8
            );

            Debug.Log(
                "[Export LeftLowerLimb] Đã xuất " + (index - 1) +
                " model ra: " + filePath
            );
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "[Export LeftLowerLimb] Không thể ghi file: " +
                ex.Message
            );
        }
    }
}