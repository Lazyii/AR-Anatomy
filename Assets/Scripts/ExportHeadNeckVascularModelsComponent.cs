using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ExportHeadNeckVascularModelsComponent : MonoBehaviour
{
    [SerializeField] private VascularLevel1ModelMap vascularLevel1ModelMap;
    [SerializeField] private string groupName = "HeadNeck_Vascular";
    [SerializeField] private string outputFileName = "HeadNeck_Vascular_MappedModels.txt";

    [ContextMenu("Export HeadNeck_Vascular Models to Desktop")]
    public void ExportToDesktop()
    {
        if (vascularLevel1ModelMap == null)
            vascularLevel1ModelMap = FindObjectOfType<VascularLevel1ModelMap>();

        if (vascularLevel1ModelMap == null)
        {
            Debug.LogError("[Export HeadNeck] Không tìm thấy VascularLevel1ModelMap. Hãy gán reference trong Inspector.", this);
            return;
        }

        vascularLevel1ModelMap.Build();
        AnatomyModelGroup group = vascularLevel1ModelMap.GetGroup(groupName);
        if (group == null)
        {
            Debug.LogError("[Export HeadNeck] GetGroup(\"" + groupName + "\") trả về null. Kiểm tra key trong VascularLevel1ModelMap.GetGroup().", this);
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(groupName + " - mapped model names");
        sb.AppendLine("Export time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.AppendLine("Model count: " + group.LinkedModels.Count);
        sb.AppendLine(new string('-', 60));

        int index = 1;
        foreach (Transform model in group.LinkedModels)
        {
            if (model == null)
            {
                sb.AppendLine(index + ". [NULL MODEL]");
            }
            else
            {
                sb.AppendLine(index + ". " + model.name);
                sb.AppendLine("   Hierarchy path: " + GetHierarchyPath(model));
            }
            index++;
        }

        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        if (string.IsNullOrEmpty(desktop))
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        string path = Path.Combine(desktop, outputFileName);
        try
        {
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
            Debug.Log("[Export HeadNeck] Đã xuất " + group.LinkedModels.Count + " model ra: " + path, this);
        }
        catch (Exception ex)
        {
            Debug.LogError("[Export HeadNeck] Không ghi được file: " + ex.Message, this);
        }
    }

    private static string GetHierarchyPath(Transform item)
    {
        string path = item.name;
        Transform parent = item.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }
}
