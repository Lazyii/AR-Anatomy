
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ExportLeftUpperLimbVascularModels : MonoBehaviour
{
    [Header("Vascular Level 1 Map")]
    [SerializeField]
    private VascularLevel1ModelMap vascularLevel1ModelMap;

    [ContextMenu("Export LeftUpperLimb_Vascular Models To Desktop")]
    public void ExportModelsToDesktop()
    {
        if (vascularLevel1ModelMap == null)
        {
            Debug.LogError(
                "[Export LeftUpperLimb] Chưa gán VascularLevel1ModelMap."
            );
            return;
        }

        vascularLevel1ModelMap.Build();

        AnatomyModelGroup group =
            vascularLevel1ModelMap.GetGroup("LeftUpperLimb_Vascular");

        if (group == null)
        {
            Debug.LogError(
                "[Export LeftUpperLimb] Không tìm thấy group " +
                "LeftUpperLimb_Vascular. Kiểm tra GetGroup() trong mapping."
            );
            return;
        }

        StringBuilder content = new StringBuilder();
        content.AppendLine("LeftUpperLimb_Vascular - Mapped Model Names");
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
            "LeftUpperLimb_Vascular_Models.txt"
        );

        try
        {
            File.WriteAllText(
                filePath,
                content.ToString(),
                Encoding.UTF8
            );

            Debug.Log(
                "[Export LeftUpperLimb] Đã xuất " + (index - 1) +
                " model ra: " + filePath
            );
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "[Export LeftUpperLimb] Không thể ghi file: " + ex.Message
            );
        }
    }
}
