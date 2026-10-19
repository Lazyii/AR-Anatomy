using System;
using System.Collections.Generic;
using UnityEngine;

public class VascularLevel1ModelMap : MonoBehaviour
{
[Header("Root: ARAnatomyRoot/VascularSystem")]
[SerializeField] private Transform vascularSystem;

[Header("Existing Level 1 groups - do not rename")]
[SerializeField] private AnatomyModelGroup heartGroup;
[SerializeField] private AnatomyModelGroup headNeckGroup;
[SerializeField] private AnatomyModelGroup thoraxGroup;
[SerializeField] private AnatomyModelGroup abdomenPelvisGroup;
[SerializeField] private AnatomyModelGroup leftUpperLimbGroup;
[SerializeField] private AnatomyModelGroup rightUpperLimbGroup;
[SerializeField] private AnatomyModelGroup leftLowerLimbGroup;
[SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

private readonly Dictionary<string, AnatomyModelGroup> groupByTag =
    new Dictionary<string, AnatomyModelGroup>();

// Mỗi tên model cấp 1 được khai báo đúng một lần.
private static readonly string[] HeartModels =
{
    "Left atrium",
    "Left ventricle",
    "Right atrium",
    "Right ventricle",
    "Anterior semilunar leaflet of pulmonary valve",
    "Left semilunar leaflet of pulmonary valve",
    "Right semilunar leaflet of pulmonary valve",
    "Left coronary leaflet",
    "Right coronary leaflet",
    "Non-coronary leaflet",
    "Inferior leaflet of right atrioventricular valve",
    "Septal leaflet of right atrioventricular valve",
    "Posterior leaflet of left atrioventricular valve",
    "Left coronary artery",
    "Right coronary artery",
    "Coronary sinus",
    "Pulmonary trunk",
    "Left pulmonary artery",
    "Right pulmonary artery",
    "Left inferior pulmonary vein",
    "Left superior pulmonary vein",
    "Right inferior pulmonary vein",
    "Right superior pulmonary vein",
    "Anterior basal segmental artery of left lung",
    "Anterior segmental artery of left lung",
    "Apical segmental artery of left lung",
    "Inferior lingular artery of left lung",
    "Lateral basal segmental artery of left lung",
    "Medial basal segmental artery of left lung",
    "Posterior basal segmental artery of left lung",
    "Posterior segmental artery of left lung",
    "Superior lingular artery of left lung",
    "Superior segmental artery of left lung"
};

private static readonly string[] HeadNeckModels =
{
    "Anterior communicating artery",
    "Anterior spinal artery.l",
    "Anterior spinal artery.r",
    "Inferior ophthalmic vein.l",
    "Inferior ophthalmic vein.r",
    "Inferior petrosal sinus.l",
    "Inferior petrosal sinus.r",
    "Inferior sagittal sinus",
    "Internal jugular vein.l",
    "Internal jugular vein.r",
    "Occipital sinus",
    "Posterior communicating artery.l",
    "Posterior communicating artery.r",
    "Sigmoid sinus.l",
    "Sigmoid sinus.r",
    "Straight sinus",
    "Superior ophthalmic vein.l",
    "Superior ophthalmic vein.r",
    "Superior sagittal sinus",
    "Right common carotid artery",
    "Left common carotid artery"
};

private static readonly string[] ThoraxModels =
{
    "Aortic arch",
    "Ascending aorta",
    "Thoracic aorta",
    "Inferior vena cava (thoracic part)",
    "Superior vena cava"
};

private static readonly string[] AbdomenPelvisModels =
{
    "Abdominal aorta",
    "Common iliac artery.l",
    "Common iliac artery.r",
    "Hepatic portal vein",
    "Median sacral artery",
    "Splenic vein",
    "Superior mesenteric vein"
};

private static readonly string[] LeftUpperLimbModels =
{
    "Left subclavian artery",
    "Left subclavian vein"
};

private static readonly string[] RightUpperLimbModels =
{
    "Right subclavian artery",
    "Right subclavian vein"
};

private static readonly string[] LeftLowerLimbModels =
{
    "Femoral artery.l",
    "Femoral vein.l"
};

private static readonly string[] RightLowerLimbModels =
{
    "Femoral artery.r",
    "Femoral vein.r"
};

[ContextMenu("Build Vascular Level 1 Map")]
public void Build()
{
    if (vascularSystem == null)
    {
        Debug.LogError(
            "[Vascular] Chưa gán ARAnatomyRoot/VascularSystem."
        );
        return;
    }

    groupByTag.Clear();

    RegisterGroup("Heart", heartGroup);
    RegisterGroup("HeadNeck_Vascular", headNeckGroup);
    RegisterGroup("Thorax_Vascular", thoraxGroup);
    RegisterGroup("AbdomenPelvis_Vascular", abdomenPelvisGroup);
    RegisterGroup("LeftUpperLimb_Vascular", leftUpperLimbGroup);
    RegisterGroup("RightUpperLimb_Vascular", rightUpperLimbGroup);
    RegisterGroup("LeftLowerLimb_Vascular", leftLowerLimbGroup);
    RegisterGroup("RightLowerLimb_Vascular", rightLowerLimbGroup);

    foreach (var group in groupByTag.Values)
    {
        if (group != null)
            group.ClearModels();
    }

    Dictionary<string, Transform> modelsByName =
        new Dictionary<string, Transform>(StringComparer.Ordinal);

    foreach (Transform model in vascularSystem)
    {
        if (modelsByName.ContainsKey(model.name))
        {
            Debug.LogError(
                "[Vascular] Trùng tên object cấp 1: " + model.name
            );
            continue;
        }

        modelsByName.Add(model.name, model);
    }

    HashSet<string> assignedNames = new HashSet<string>(
        StringComparer.Ordinal
    );

    AssignNames("Heart", HeartModels, modelsByName, assignedNames);
    AssignNames("HeadNeck_Vascular", HeadNeckModels, modelsByName, assignedNames);
    AssignNames("Thorax_Vascular", ThoraxModels, modelsByName, assignedNames);
    AssignNames("AbdomenPelvis_Vascular", AbdomenPelvisModels, modelsByName, assignedNames);
    AssignNames("LeftUpperLimb_Vascular", LeftUpperLimbModels, modelsByName, assignedNames);
    AssignNames("RightUpperLimb_Vascular", RightUpperLimbModels, modelsByName, assignedNames);
    AssignNames("LeftLowerLimb_Vascular", LeftLowerLimbModels, modelsByName, assignedNames);
    AssignNames("RightLowerLimb_Vascular", RightLowerLimbModels, modelsByName, assignedNames);

    foreach (var entry in modelsByName)
    {
        if (!assignedNames.Contains(entry.Key))
        {
            Debug.LogError(
                "[Vascular] CHƯA MAPPING: " + entry.Key
            );
        }
    }

    Debug.Log(
        $"[Vascular] Tổng model cấp 1: {modelsByName.Count}; " +
        $"đã mapping: {assignedNames.Count}; " +
        $"chưa mapping: {modelsByName.Count - assignedNames.Count}."
    );
}

private void RegisterGroup(string tag, AnatomyModelGroup group)
{
    if (group == null)
    {
        Debug.LogError(
            "[Vascular] Chưa gán AnatomyModelGroup cho tag: " + tag
        );
        return;
    }

    groupByTag[tag] = group;
}

private void AssignNames(
    string tag,
    string[] names,
    Dictionary<string, Transform> modelsByName,
    HashSet<string> assignedNames)
{
    if (!groupByTag.TryGetValue(tag, out AnatomyModelGroup group))
    {
        Debug.LogError(
            "[Vascular] Không có group được gán cho tag: " + tag
        );
        return;
    }

    foreach (string name in names)
    {
        if (!modelsByName.TryGetValue(name, out Transform model))
        {
            Debug.LogError(
                "[Vascular] Không tìm thấy model: " + name +
                " (tag: " + tag + ")"
            );
            continue;
        }

        if (!assignedNames.Add(name))
        {
            Debug.LogError(
                "[Vascular] Model bị khai báo mapping nhiều lần: " + name
            );
            continue;
        }

        group.AddModel(model);
    }
}


public AnatomyModelGroup GetGroup(string tag)
{
    switch (tag)
    {
        case "Heart":
            return heartGroup;

        case "HeadNeck_Vascular":
            return headNeckGroup;

        case "Thorax_Vascular":
            return thoraxGroup;

        case "AbdomenPelvis_Vascular":
            return abdomenPelvisGroup;

        case "LeftUpperLimb_Vascular":
            return leftUpperLimbGroup;

        case "RightUpperLimb_Vascular":
            return rightUpperLimbGroup;

        case "LeftLowerLimb_Vascular":
            return leftLowerLimbGroup;

        case "RightLowerLimb_Vascular":
            return rightLowerLimbGroup;

        default:
            return null;
    }
}

}
