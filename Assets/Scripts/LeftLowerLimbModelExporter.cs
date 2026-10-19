using System.IO;
using System.Text;
using UnityEngine;

public class LeftLowerLimbModelExporter : MonoBehaviour
{
[Header("Left Lower Limb Model Group")]
[SerializeField]
private AnatomyModelGroup leftLowerLimbGroup;

[ContextMenu("Export Left Lower Limb Models To Desktop")]
public void ExportModels()
{
    if (leftLowerLimbGroup == null)
    {
        Debug.LogError(
            "Chua gan LeftLowerLimb AnatomyModelGroup!"
        );
        return;
    }

    StringBuilder sb = new StringBuilder();

    sb.AppendLine("LEFT LOWER LIMB MODEL LIST");
    sb.AppendLine("==========================");
    sb.AppendLine();

    int count = 0;

    foreach (Transform model in leftLowerLimbGroup.LinkedModels)
    {
        if (model == null)
            continue;

        count++;

        sb.AppendLine(count + ". " + model.name);
        sb.AppendLine("   Hierarchy: " + GetHierarchyPath(model));
        sb.AppendLine();
    }

    sb.AppendLine("==========================");
    sb.AppendLine("TOTAL MODELS: " + count);

    string desktopPath = System.Environment.GetFolderPath(
        System.Environment.SpecialFolder.Desktop
    );

    string filePath = Path.Combine(
        desktopPath,
        "LeftLowerLimb_Models.txt"
    );

    File.WriteAllText(
        filePath,
        sb.ToString(),
        Encoding.UTF8
    );

    Debug.Log(
        "LEFT LOWER LIMB EXPORT COMPLETE: "
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
