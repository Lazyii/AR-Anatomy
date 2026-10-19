using System.IO;
using System.Text;
using UnityEngine;

public class RightLowerLimbModelExporter : MonoBehaviour
{
    [Header("Right Lower Limb Model Group")]
    [SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

    [ContextMenu("Export Right Lower Limb Models")]
    public void ExportRightLowerLimbModels()
    {
        if (rightLowerLimbGroup == null)
        {
            Debug.LogError(
                "Chưa gán Right Lower Limb Group!",
                this
            );
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("RIGHT LOWER LIMB MODELS");
        sb.AppendLine("=======================");
        sb.AppendLine();

        int index = 1;

        foreach (Transform model in rightLowerLimbGroup.LinkedModels)
        {
            if (model == null)
                continue;

            sb.AppendLine(index + ". " + model.name);

            sb.AppendLine(
                "   Hierarchy: " + GetHierarchyPath(model)
            );

            sb.AppendLine();
            index++;
        }

        string desktopPath = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Desktop
        );

        string filePath = Path.Combine(
            desktopPath,
            "RightLowerLimb_Models.txt"
        );

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        Debug.Log(
            "Đã xuất " + (index - 1) +
            " model RightLowerLimb ra file: " + filePath,
            this
        );
    }

    private string GetHierarchyPath(Transform target)
    {
        if (target == null)
            return string.Empty;

        string path = target.name;
        Transform parent = target.parent;

        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }

        return path;
    }
}