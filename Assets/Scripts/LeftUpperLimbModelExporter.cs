using System;
using System.IO;
using System.Text;
using UnityEngine;

public class LeftUpperLimbModelExporter : MonoBehaviour
{
[Header("Source Model Group")]
[SerializeField]
private AnatomyModelGroup leftUpperLimbGroup;

[ContextMenu("Export LeftUpperLimb Models to Desktop")]
public void ExportModels()
{
    if (leftUpperLimbGroup == null)
    {
        Debug.LogError(
            "LeftUpperLimbModelExporter: Chua gan LeftUpperLimb Group."
        );
        return;
    }

    string desktopPath =
        Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory
        );

    if (string.IsNullOrEmpty(desktopPath) ||
        !Directory.Exists(desktopPath))
    {
        Debug.LogError(
            "Khong tim thay thu muc Desktop."
        );
        return;
    }

    string filePath = Path.Combine(
        desktopPath,
        "LeftUpperLimb_Models.txt"
    );

    var output = new StringBuilder();

    output.AppendLine("LEFT UPPER LIMB - LINKED MODELS");
    output.AppendLine("Export date: " + DateTime.Now);
    output.AppendLine(
        "Total linked models: "
        + leftUpperLimbGroup.LinkedModels.Count
    );
    output.AppendLine(
        "========================================"
    );

    int index = 0;

    foreach (Transform model in leftUpperLimbGroup.LinkedModels)
    {
        if (model == null)
        {
            output.AppendLine(
                "[NULL MODEL]"
            );
            continue;
        }

        index++;

        output.AppendLine(
            index + ". " + model.name
        );

        output.AppendLine(
            "   Hierarchy: " + GetHierarchyPath(model)
        );
    }

    try
    {
        File.WriteAllText(
            filePath,
            output.ToString(),
            new UTF8Encoding(true)
        );

        Debug.Log(
            "LEFT UPPER LIMB EXPORT SUCCESS"
            + " | Models = " + index
            + " | File = " + filePath
        );
    }
    catch (Exception exception)
    {
        Debug.LogError(
            "Khong the ghi file: "
            + exception.Message
        );
    }
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
