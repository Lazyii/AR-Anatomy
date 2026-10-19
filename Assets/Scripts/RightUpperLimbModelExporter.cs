using System.IO;
using System.Text;
using UnityEngine;

public class RightUpperLimbModelExporter : MonoBehaviour
{
[Header("Right Upper Limb Model Group")]
[SerializeField]
private AnatomyModelGroup rightUpperLimbGroup;


[ContextMenu("Export Right Upper Limb Models To Desktop")]
public void ExportModels()
{
    if (rightUpperLimbGroup == null)
    {
        Debug.LogError(
            "Chua gan RightUpperLimb AnatomyModelGroup!"
        );
        return;
    }

    StringBuilder sb = new StringBuilder();

    sb.AppendLine("RIGHT UPPER LIMB MODEL LIST");
    sb.AppendLine("===========================");
    sb.AppendLine();

    int count = 0;

    foreach (Transform model in rightUpperLimbGroup.LinkedModels)
    {
        if (model == null)
            continue;

        count++;

        sb.AppendLine(
            count + ". " + model.name
        );

        sb.AppendLine(
            "   Hierarchy: " + GetHierarchyPath(model)
        );

        sb.AppendLine();
    }

    sb.AppendLine("===========================");
    sb.AppendLine("TOTAL MODELS: " + count);

    string desktopPath = System.Environment.GetFolderPath(
        System.Environment.SpecialFolder.Desktop
    );

    string filePath = Path.Combine(
        desktopPath,
        "RightUpperLimb_Models.txt"
    );

    File.WriteAllText(
        filePath,
        sb.ToString(),
        Encoding.UTF8
    );

    Debug.Log(
        "RIGHT UPPER LIMB EXPORT COMPLETE: "
        + count
        + " models\nSaved to: "
        + filePath
    );
}

private string GetHierarchyPath(Transform target)
{
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
