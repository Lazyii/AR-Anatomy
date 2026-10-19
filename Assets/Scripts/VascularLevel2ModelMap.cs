using System;
using System.Collections.Generic;
using UnityEngine;

public class VascularLevel2ModelMap : MonoBehaviour
{
    [Header("Existing Vascular Level 1 Map")]
    [SerializeField]
    private VascularLevel1ModelMap level1Map;

    private readonly Dictionary<string, List<Transform>> modelMap =
        new Dictionary<string, List<Transform>>();

    private bool built;

    public void Build()
    {
        modelMap.Clear();
        built = false;

        if (level1Map == null)
        {
            Debug.LogError(
                "[Vascular L2] Chưa gán VascularLevel1ModelMap."
            );
            return;
        }

        // =========================================
        // LEVEL 1: HEART
        // =========================================

        AnatomyModelGroup heart = level1Map.GetGroup("Heart");

        if (heart == null)
        {
            Debug.LogError(
                "[Vascular L2] Không tìm thấy group Heart."
            );
            return;
        }

        List<Transform> heartModels = GetValidModels(heart);

        // Giữ nguyên toàn bộ model Heart ở Level 1.
        modelMap["Heart"] = new List<Transform>(heartModels);

        AddByNames("RightAtrium", heartModels,
            "Right atrium",
            "Superior vena cava",
            "Inferior vena cava (thoracic part)",
            "Coronary sinus");

        AddByNames("RightVentricle", heartModels,
            "Right ventricle",
            "Inferior leaflet of right atrioventricular valve",
            "Septal leaflet of right atrioventricular valve",
            "Anterior semilunar leaflet of pulmonary valve",
            "Left semilunar leaflet of pulmonary valve",
            "Right semilunar leaflet of pulmonary valve");

        AddByNames("LeftAtrium", heartModels,
            "Left atrium",
            "Left inferior pulmonary vein",
            "Left superior pulmonary vein",
            "Right inferior pulmonary vein",
            "Right superior pulmonary vein");

        AddByNames("LeftVentricle", heartModels,
            "Left ventricle",
            "Posterior leaflet of left atrioventricular valve",
            "Left coronary leaflet",
            "Right coronary leaflet",
            "Non-coronary leaflet");

        AddByNames("CoronaryArteries", heartModels,
            "Left coronary artery",
            "Right coronary artery");

        AddByNames("CardiacVeins", heartModels,
            "Coronary sinus");

        AddByNames("RightCoronaryArtery", heartModels,
            "Right coronary artery");

        AddByNames("LeftCoronaryArtery", heartModels,
            "Left coronary artery");

        // Các node Heart hiện chưa có mapping model riêng.
        AddEmptyIfMissing(
            "CardiacWall",
            "CardiacSeptum",
            "RightAuricle",
            "TricuspidValve",
            "ChordaeTendineae_Right",
            "PapillaryMuscles_Right",
            "PulmonaryValve",
            "LeftAuricle",
            "PulmonaryVeins",
            "MitralValve",
            "ChordaeTendineae_Left",
            "PapillaryMuscles_Left",
            "AorticValve",
            "Epicardium",
            "Myocardium",
            "Endocardium",
            "InteratrialSeptum",
            "InterventricularSeptum",
            "AnteriorInterventricularArtery",
            "CircumflexArtery",
            "GreatCardiacVein",
            "MiddleCardiacVein",
            "SmallCardiacVein",
            "ConductingSystem",
            "SinoatrialNode",
            "AtrioventricularNode",
            "AtrioventricularBundle",
            "RightBundleBranch",
            "LeftBundleBranch",
            "PurkinjeFibers"
        );

        // =========================================
        // LEVEL 1: HEAD & NECK VASCULAR
        // =========================================

        AnatomyModelGroup headNeck =
            level1Map.GetGroup("HeadNeck_Vascular");

        if (headNeck == null)
        {
            Debug.LogError(
                "[Vascular L2] Không tìm thấy group HeadNeck_Vascular."
            );
        }
        else
        {
            List<Transform> headNeckModels =
                GetValidModels(headNeck);

            // Giữ nguyên toàn bộ model ở Level 1.
            modelMap["HeadNeck_Vascular"] =
                new List<Transform>(headNeckModels);

            // -------------------------------------
            // ARTERIAL HEAD & NECK
            // -------------------------------------

            AddByNames("AnteriorCommunicatingArtery",
                headNeckModels,
                "Anterior communicating artery");

            AddByNames("PosteriorCommunicating_Left",
                headNeckModels,
                "Posterior communicating artery.l");

            AddByNames("PosteriorCommunicating_Right",
                headNeckModels,
                "Posterior communicating artery.r");

            CombineNodes("CerebralArteries",
                "AnteriorCommunicatingArtery",
                "PosteriorCommunicating_Left",
                "PosteriorCommunicating_Right");

            AddByNames("AnteriorSpinal_Left",
                headNeckModels,
                "Anterior spinal artery.l");

            AddByNames("AnteriorSpinal_Right",
                headNeckModels,
                "Anterior spinal artery.r");

            CombineNodes("AnteriorSpinalArteries",
                "AnteriorSpinal_Left",
                "AnteriorSpinal_Right");

            AddByNames("CommonCarotid_Left",
                headNeckModels,
                "Left common carotid artery");

            AddByNames("CommonCarotid_Right",
                headNeckModels,
                "Right common carotid artery");

            CombineNodes("CommonCarotidArteries",
                "CommonCarotid_Left",
                "CommonCarotid_Right");

            CombineNodes("Arterial_HeadNeck",
                "CerebralArteries",
                "AnteriorSpinalArteries",
                "CommonCarotidArteries");

            // -------------------------------------
            // VENOUS HEAD & NECK
            // -------------------------------------

            AddByNames("InternalJugular_Left",
                headNeckModels,
                "Internal jugular vein.l");

            AddByNames("InternalJugular_Right",
                headNeckModels,
                "Internal jugular vein.r");

            CombineNodes("InternalJugularVeins",
                "InternalJugular_Left",
                "InternalJugular_Right");

            AddByNames("SuperiorOphthalmic_Left",
                headNeckModels,
                "Superior ophthalmic vein.l");

            AddByNames("SuperiorOphthalmic_Right",
                headNeckModels,
                "Superior ophthalmic vein.r");

            AddByNames("InferiorOphthalmic_Left",
                headNeckModels,
                "Inferior ophthalmic vein.l");

            AddByNames("InferiorOphthalmic_Right",
                headNeckModels,
                "Inferior ophthalmic vein.r");

            CombineNodes("OphthalmicVeins",
                "SuperiorOphthalmic_Left",
                "SuperiorOphthalmic_Right",
                "InferiorOphthalmic_Left",
                "InferiorOphthalmic_Right");

            // Dural venous sinuses
            AddByNames("InferiorPetrosal_Left",
                headNeckModels,
                "Inferior petrosal sinus.l");

            AddByNames("InferiorPetrosal_Right",
                headNeckModels,
                "Inferior petrosal sinus.r");

            CombineNodes("PetrosalSinus",
                "InferiorPetrosal_Left",
                "InferiorPetrosal_Right");

            AddByNames("SuperiorSagittalSinus",
                headNeckModels,
                "Superior sagittal sinus");

            AddByNames("InferiorSagittalSinus",
                headNeckModels,
                "Inferior sagittal sinus");

            CombineNodes("SagittalSinus",
                "SuperiorSagittalSinus",
                "InferiorSagittalSinus");

            AddByNames("OccipitalSinus",
                headNeckModels,
                "Occipital sinus");

            AddByNames("SigmoidSinus_Left",
                headNeckModels,
                "Sigmoid sinus.l");

            AddByNames("SigmoidSinus_Right",
                headNeckModels,
                "Sigmoid sinus.r");

            CombineNodes("SigmoidSinus",
                "SigmoidSinus_Left",
                "SigmoidSinus_Right");

            AddByNames("StraightSinus",
                headNeckModels,
                "Straight sinus");

            CombineNodes("DuralVenousSinuses",
                "PetrosalSinus",
                "SagittalSinus",
                "OccipitalSinus",
                "SigmoidSinus",
                "StraightSinus");

            CombineNodes("Venous_HeadNeck",
                "InternalJugularVeins",
                "OphthalmicVeins",
                "DuralVenousSinuses");
        }

        // =====================================================
        // THORAX VASCULAR
        // =====================================================
        AnatomyModelGroup thoraxGroup =
            level1Map.GetGroup("Thorax_Vascular");

        List<Transform> thoraxModels = GetValidModels(thoraxGroup);
        // Giữ nguyên toàn bộ model của nhóm Level 1.
        modelMap["Thorax_Vascular"] =
            new List<Transform>(thoraxModels);

        // Cấp 2: Động mạch và tĩnh mạch lồng ngực.
        AddByNames("Arterial_Thorax", thoraxModels,
            "Aortic arch",
            "Ascending aorta",
            "Thoracic aorta");

        AddByNames("Venous_Thorax", thoraxModels,
            "Inferior vena cava (thoracic part)",
            "Superior vena cava");

        // Cấp 3: Các node model riêng biệt.
        AddByNames("AorticArch", thoraxModels,
            "Aortic arch");

        AddByNames("AscendingAorta", thoraxModels,
            "Ascending aorta");

        AddByNames("ThoracicAorta", thoraxModels,
            "Thoracic aorta");

        AddByNames("InferiorVenaCava_Thoracic", thoraxModels,
            "Inferior vena cava (thoracic part)");

        AddByNames("SuperiorVenaCava", thoraxModels,
            "Superior vena cava");

        built = true;

        Debug.Log(
            "[Vascular L2] Mapping hoàn tất. Tổng node: "
            + modelMap.Count
        );

        LogNodeSummary("Heart");
        LogNodeSummary("HeadNeck_Vascular");

        
        // =====================================================
        // ABDOMEN / PELVIS VASCULAR
        // =====================================================

        AnatomyModelGroup abdomenPelvisGroup =
            level1Map.GetGroup("AbdomenPelvis_Vascular");

        List<Transform> abdomenPelvisModels =
            GetValidModels(abdomenPelvisGroup);

        // Giữ nguyên toàn bộ model thuộc Level 1.
        modelMap["AbdomenPelvis_Vascular"] =
            new List<Transform>(abdomenPelvisModels);

        // Cấp 2: Động mạch và tĩnh mạch.
        AddByNames("Arterial_AbdomenPelvis", abdomenPelvisModels,
            "Abdominal aorta",
            "Common iliac artery.l",
            "Common iliac artery.r",
            "Median sacral artery");

        AddByNames("Venous_AbdomenPelvis", abdomenPelvisModels,
            "Hepatic portal vein",
            "Splenic vein",
            "Superior mesenteric vein");

        // Cấp 3: Nhóm động mạch chậu chung.
        AddByNames("CommonIliacArteries", abdomenPelvisModels,
            "Common iliac artery.l",
            "Common iliac artery.r");

        // Cấp 3: Hệ tĩnh mạch cửa.
        AddByNames("PortalVenousSystem", abdomenPelvisModels,
            "Hepatic portal vein",
            "Splenic vein",
            "Superior mesenteric vein");

        // Các node model riêng lẻ.
        AddByNames("AbdominalAorta", abdomenPelvisModels,
            "Abdominal aorta");

        AddByNames("CommonIliacArtery_Left", abdomenPelvisModels,
            "Common iliac artery.l");

        AddByNames("CommonIliacArtery_Right", abdomenPelvisModels,
            "Common iliac artery.r");

        AddByNames("MedianSacralArtery", abdomenPelvisModels,
            "Median sacral artery");

        AddByNames("HepaticPortalVein", abdomenPelvisModels,
            "Hepatic portal vein");

        AddByNames("SplenicVein", abdomenPelvisModels,
            "Splenic vein");

        AddByNames("SuperiorMesentericVein", abdomenPelvisModels,
            "Superior mesenteric vein");

        // =====================================================
        // LeftUpperLimb_Vascular
        // =====================================================
        AnatomyModelGroup leftUpperLimbGroup =
            level1Map.GetGroup("LeftUpperLimb_Vascular");

        List<Transform> leftUpperLimbModels =
            GetValidModels(leftUpperLimbGroup);

        modelMap["LeftUpperLimb_Vascular"] =
            new List<Transform>(leftUpperLimbModels);

        // Arterial group
        AddByNames(
            "Arterial_LeftUpperLimb",
            leftUpperLimbModels,
            "Left subclavian artery"
        );

        // Venous group
        AddByNames(
            "Venous_LeftUpperLimb",
            leftUpperLimbModels,
            "Left subclavian vein"
        );

        // Leaf nodes
        AddByNames(
            "SubclavianArtery_Left",
            leftUpperLimbModels,
            "Left subclavian artery"
        );

        AddByNames(
            "SubclavianVein_Left",
            leftUpperLimbModels,
            "Left subclavian vein"
        );

        // =====================================================
        // RightUpperLimb_Vascular
        // =====================================================
        AnatomyModelGroup rightUpperLimbGroup =
            level1Map.GetGroup("RightUpperLimb_Vascular");

        List<Transform> rightUpperLimbModels =
            GetValidModels(rightUpperLimbGroup);

        modelMap["RightUpperLimb_Vascular"] =
            new List<Transform>(rightUpperLimbModels);

        // Arterial group
        AddByNames(
            "Arterial_RightUpperLimb",
            rightUpperLimbModels,
            "Right subclavian artery"
        );

        // Venous group
        AddByNames(
            "Venous_RightUpperLimb",
            rightUpperLimbModels,
            "Right subclavian vein"
        );

        // Leaf nodes
        AddByNames(
            "SubclavianArtery_Right",
            rightUpperLimbModels,
            "Right subclavian artery"
        );

        AddByNames(
            "SubclavianVein_Right",
            rightUpperLimbModels,
            "Right subclavian vein"
        );

        // =====================================================
        // LeftLowerLimb_Vascular
        // =====================================================
        AnatomyModelGroup leftLowerLimbGroup =
            level1Map.GetGroup("LeftLowerLimb_Vascular");

        List<Transform> leftLowerLimbModels =
            GetValidModels(leftLowerLimbGroup);

        modelMap["LeftLowerLimb_Vascular"] =
            new List<Transform>(leftLowerLimbModels);

        // Arterial group
        AddByNames(
            "Arterial_LeftLowerLimb",
            leftLowerLimbModels,
            "Femoral artery.l"
        );

        // Venous group
        AddByNames(
            "Venous_LeftLowerLimb",
            leftLowerLimbModels,
            "Femoral vein.l"
        );

        // Leaf nodes
        AddByNames(
            "FemoralArtery_Left",
            leftLowerLimbModels,
            "Femoral artery.l"
        );

        AddByNames(
            "FemoralVein_Left",
            leftLowerLimbModels,
            "Femoral vein.l"
        );

        // =====================================================
        // RightLowerLimb_Vascular
        // =====================================================
        AnatomyModelGroup rightLowerLimbGroup =
            level1Map.GetGroup("RightLowerLimb_Vascular");

        List<Transform> rightLowerLimbModels =
            GetValidModels(rightLowerLimbGroup);

        modelMap["RightLowerLimb_Vascular"] =
            new List<Transform>(rightLowerLimbModels);

        // Arterial group
        AddByNames(
            "Arterial_RightLowerLimb",
            rightLowerLimbModels,
            "Femoral artery.r"
        );

        // Venous group
        AddByNames(
            "Venous_RightLowerLimb",
            rightLowerLimbModels,
            "Femoral vein.r"
        );

        // Leaf nodes
        AddByNames(
            "FemoralArtery_Right",
            rightLowerLimbModels,
            "Femoral artery.r"
        );

        AddByNames(
            "FemoralVein_Right",
            rightLowerLimbModels,
            "Femoral vein.r"
        );
    }

    // =============================================
    // HELPER METHODS
    // =============================================

    private List<Transform> GetValidModels(
        AnatomyModelGroup group)
    {
        List<Transform> result = new List<Transform>();

        if (group == null)
            return result;

        foreach (Transform model in group.LinkedModels)
        {
            if (model != null && !result.Contains(model))
                result.Add(model);
        }

        return result;
    }

    private void AddByNames(
        string nodeName,
        List<Transform> source,
        params string[] modelNames)
    {
        List<Transform> result = new List<Transform>();

        foreach (string expectedName in modelNames)
        {
            foreach (Transform model in source)
            {
                if (model == null)
                    continue;

                if (string.Equals(
                    Normalize(model.name),
                    Normalize(expectedName),
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (!result.Contains(model))
                        result.Add(model);
                }
            }
        }

        modelMap[nodeName] = result;
    }

    private void CombineNodes(
        string nodeName,
        params string[] childNames)
    {
        List<Transform> result = new List<Transform>();

        foreach (string childName in childNames)
        {
            if (!modelMap.TryGetValue(
                childName,
                out List<Transform> childModels))
            {
                continue;
            }

            foreach (Transform model in childModels)
            {
                if (model != null && !result.Contains(model))
                    result.Add(model);
            }
        }

        modelMap[nodeName] = result;
    }

    private void AddEmptyIfMissing(
        params string[] nodeNames)
    {
        foreach (string nodeName in nodeNames)
        {
            if (!modelMap.ContainsKey(nodeName))
                modelMap[nodeName] = new List<Transform>();
        }
    }

    private string Normalize(string value)
    {
        return string.IsNullOrEmpty(value)
            ? string.Empty
            : value.Replace("(Clone)", "").Trim();
    }

    private void LogNodeSummary(string nodeName)
    {
        if (modelMap.TryGetValue(
            nodeName,
            out List<Transform> models))
        {
            Debug.Log(
                "[Vascular L2] "
                + nodeName
                + " = "
                + models.Count
                + " model(s)"
            );
        }
    }

    public List<Transform> GetModels(string nodeName)
    {
        if (!built)
            Build();

        return modelMap.TryGetValue(
            nodeName,
            out List<Transform> models)
            ? new List<Transform>(models)
            : new List<Transform>();
    }

    public bool ContainsNode(string nodeName)
    {
        if (!built)
            Build();

        return modelMap.ContainsKey(nodeName);
    }
}
