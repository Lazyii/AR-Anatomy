using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AuditMuscularModelMapping : MonoBehaviour
{
[Header("REAL Z-ANATOMY MODEL ROOT")]
[SerializeField] private Transform modelRoot;
[Header("CURRENT PROJECT MAPPING")]
[SerializeField] private AnatomyModelGroup headNeckGroup;
[SerializeField] private AnatomyModelGroup trunkGroup;
[SerializeField] private AnatomyModelGroup leftUpperLimbGroup;
[SerializeField] private AnatomyModelGroup rightUpperLimbGroup;
[SerializeField] private AnatomyModelGroup leftLowerLimbGroup;
[SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

[Header("Audit")]
[SerializeField] private bool includeObjectsWithoutRenderer = false;

[ContextMenu("AUDIT MUSCULAR MODEL")]
public void AuditMuscularModel()
{
    if (modelRoot == null)
    {
        Debug.LogError(
            "AUDIT FAILED: Model Root chưa được gán."
        );

        return;
    }

    List<Transform> allObjects =
        new List<Transform>();

    List<Transform> renderableObjects =
        new List<Transform>();

    CollectObjects(
        modelRoot,
        allObjects,
        renderableObjects
    );

    HashSet<Transform> mappedObjects =
        new HashSet<Transform>();

    AddGroupToSet(
        headNeckGroup,
        mappedObjects
    );

    AddGroupToSet(
        trunkGroup,
        mappedObjects
    );

    AddGroupToSet(
        leftUpperLimbGroup,
        mappedObjects
    );

    AddGroupToSet(
        rightUpperLimbGroup,
        mappedObjects
    );

    AddGroupToSet(
        leftLowerLimbGroup,
        mappedObjects
    );

    AddGroupToSet(
        rightLowerLimbGroup,
        mappedObjects
    );

    Dictionary<string, int> nameCounts =
        new Dictionary<string, int>(
            StringComparer.Ordinal
        );

    foreach (Transform t in renderableObjects)
    {
        if (!nameCounts.ContainsKey(t.name))
        {
            nameCounts.Add(t.name, 0);
        }

        nameCounts[t.name]++;
    }

    List<Transform> unmapped =
        new List<Transform>();

    foreach (Transform t in renderableObjects)
    {
        if (!mappedObjects.Contains(t))
        {
            unmapped.Add(t);
        }
    }

    StringBuilder report =
        new StringBuilder();

    report.AppendLine(
        "=================================================="
    );

    report.AppendLine(
        "Z-ANATOMY MUSCULAR MODEL AUDIT"
    );

    report.AppendLine(
        "=================================================="
    );

    report.AppendLine();

    report.AppendLine(
        "MODEL ROOT:"
    );

    report.AppendLine(
        GetPath(modelRoot)
    );

    report.AppendLine();

    report.AppendLine(
        "TOTAL OBJECTS:"
    );

    report.AppendLine(
        allObjects.Count.ToString()
    );

    report.AppendLine();

    report.AppendLine(
        "RENDERABLE MODEL OBJECTS:"
    );

    report.AppendLine(
        renderableObjects.Count.ToString()
    );

    report.AppendLine();

    report.AppendLine(
        "CURRENT MAPPING"
    );

    report.AppendLine(
        "--------------------------------------------------"
    );

    AppendGroupCount(
        report,
        "HeadNeck",
        headNeckGroup
    );

    AppendGroupCount(
        report,
        "Trunk",
        trunkGroup
    );

    AppendGroupCount(
        report,
        "LeftUpperLimb",
        leftUpperLimbGroup
    );

    AppendGroupCount(
        report,
        "RightUpperLimb",
        rightUpperLimbGroup
    );

    AppendGroupCount(
        report,
        "LeftLowerLimb",
        leftLowerLimbGroup
    );

    AppendGroupCount(
        report,
        "RightLowerLimb",
        rightLowerLimbGroup
    );

    report.AppendLine();

    report.AppendLine(
        "TOTAL MAPPED TRANSFORMS:"
    );

    report.AppendLine(
        mappedObjects.Count.ToString()
    );

    report.AppendLine();

    report.AppendLine(
        "EXPECTED FINAL LEVEL 1:"
    );

    report.AppendLine(
        "HeadNeck       = 143"
    );

    report.AppendLine(
        "Trunk          = 101"
    );

    report.AppendLine(
        "LeftUpperLimb  = 51"
    );

    report.AppendLine(
        "RightUpperLimb = 51"
    );

    report.AppendLine(
        "LeftLowerLimb  = 54"
    );

    report.AppendLine(
        "RightLowerLimb = 54"
    );

    report.AppendLine(
        "TOTAL          = 454"
    );

    report.AppendLine();

    report.AppendLine(
        "UNMAPPED RENDERABLE MODEL OBJECTS"
    );

    report.AppendLine(
        "--------------------------------------------------"
    );

    report.AppendLine(
        "COUNT = " + unmapped.Count
    );

    report.AppendLine();

    foreach (Transform t in unmapped)
    {
        report.AppendLine(
            t.name
            + "    |    "
            + GetPath(t)
        );
    }

    report.AppendLine();

    report.AppendLine(
        "DUPLICATE MODEL NAMES"
    );

    report.AppendLine(
        "--------------------------------------------------"
    );

    int duplicateCount = 0;

    foreach (
        KeyValuePair<string, int> pair
        in nameCounts
    )
    {
        if (pair.Value <= 1)
            continue;

        duplicateCount++;

        report.AppendLine(
            pair.Key
            + "    x"
            + pair.Value
        );
    }

    if (duplicateCount == 0)
    {
        report.AppendLine(
            "NONE"
        );
    }

    report.AppendLine();

    report.AppendLine(
        "ALL RENDERABLE MODEL OBJECTS"
    );

    report.AppendLine(
        "--------------------------------------------------"
    );

    foreach (Transform t in renderableObjects)
    {
        string status =
            mappedObjects.Contains(t)
                ? "MAPPED"
                : "UNMAPPED";

        report.AppendLine(
            status
            + "    |    "
            + t.name
            + "    |    "
            + GetPath(t)
        );
    }

    report.AppendLine();

    report.AppendLine(
        "=================================================="
    );

    report.AppendLine(
        "END OF AUDIT"
    );

    report.AppendLine(
        "=================================================="
    );

    string desktop =
        Environment.GetFolderPath(
            Environment.SpecialFolder.Desktop
        );

    if (string.IsNullOrEmpty(desktop))
    {
        desktop =
            Application.persistentDataPath;
    }

    string filePath =
        Path.Combine(
            desktop,
            "ZAnatomy_Muscular_Audit.txt"
        );

    try
    {
        File.WriteAllText(
            filePath,
            report.ToString(),
            Encoding.UTF8
        );

        Debug.Log(
            "=================================================="
        );

        Debug.Log(
            "AUDIT COMPLETE"
        );

        Debug.Log(
            "All objects: "
            + allObjects.Count
        );

        Debug.Log(
            "Renderable: "
            + renderableObjects.Count
        );

        Debug.Log(
            "Mapped: "
            + mappedObjects.Count
        );

        Debug.Log(
            "Unmapped: "
            + unmapped.Count
        );

        Debug.Log(
            "REPORT:"
        );

        Debug.Log(
            filePath
        );

        Debug.Log(
            "=================================================="
        );
    }
    catch (Exception e)
    {
        Debug.LogError(
            "Không thể ghi file audit: "
            + e.Message
        );
    }
}

private void CollectObjects(
    Transform root,
    List<Transform> allObjects,
    List<Transform> renderableObjects
)
{
    if (root == null)
        return;

    if (root != modelRoot ||
        includeObjectsWithoutRenderer)
    {
        allObjects.Add(root);
    }

    Renderer renderer =
        root.GetComponent<Renderer>();

    if (renderer != null)
    {
        renderableObjects.Add(root);
    }

    foreach (Transform child in root)
    {
        CollectObjects(
            child,
            allObjects,
            renderableObjects
        );
    }
}

private void AddGroupToSet(
    AnatomyModelGroup group,
    HashSet<Transform> target
)
{
    if (group == null)
        return;

    foreach (Transform model in group.LinkedModels)
    {
        if (model == null)
            continue;

        target.Add(model);
    }
}

private void AppendGroupCount(
    StringBuilder report,
    string groupName,
    AnatomyModelGroup group
)
{
    int count = 0;

    if (group != null)
    {
        count =
            group.LinkedModels.Count;
    }

    report.AppendLine(
        groupName
        + " = "
        + count
    );
}

private string GetPath(
    Transform target
)
{
    if (target == null)
        return "(null)";

    List<string> parts =
        new List<string>();

    Transform current =
        target;

    while (
        current != null &&
        current != transform
    )
    {
        parts.Add(
            current.name
        );

        current =
            current.parent;
    }

    parts.Reverse();

    return string.Join(
        "/",
        parts
    );
}
}