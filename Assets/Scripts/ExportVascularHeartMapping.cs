using System.IO;
using System.Text;
using UnityEngine;

public class ExportVascularHeartMapping : MonoBehaviour
{
    [SerializeField]
    private VascularLevel1ModelMap vascularLevel1ModelMap;

    [ContextMenu("Export Heart Mapping To Desktop")]
    public void ExportHeartMapping()
    {
        if (vascularLevel1ModelMap == null)
        {
            Debug.LogError(
                "[Export Heart] Chưa gán VascularLevel1ModelMap!"
            );
            return;
        }

        // Build lại mapping theo danh sách tên model đã khai báo.
        vascularLevel1ModelMap.Build();

        AnatomyModelGroup heartGroup =
            vascularLevel1ModelMap.GetGroup("Heart");

        if (heartGroup == null)
        {
            Debug.LogError(
                "[Export Heart] Không tìm thấy group Heart!"
            );
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("VASCULAR SYSTEM - LEVEL 1 - HEART MAPPING");
        sb.AppendLine("Total models: " + heartGroup.LinkedModels.Count);
        sb.AppendLine("------------------------------------------");

        int index = 1;

        foreach (Transform model in heartGroup.LinkedModels)
        {
            if (model == null)
                continue;

            sb.AppendLine(index + ". " + model.name);
            index++;
        }

        string desktopPath = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Desktop
        );

        string filePath = Path.Combine(
            desktopPath,
            "Vascular_Heart_Level1_Mapping.txt"
        );

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        Debug.Log(
            "[Export Heart] Đã xuất " +
            (index - 1) +
            " model ra file: " + filePath
        );
    }
}
