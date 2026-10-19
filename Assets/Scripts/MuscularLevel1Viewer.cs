using System.Collections.Generic;
using UnityEngine;

public class MuscularLevel1Viewer : MonoBehaviour
{
    [Header("Full Body")]
    [SerializeField] private GameObject fullBodyRoot;

    [Header("Level 1 Groups")]
    [SerializeField] private AnatomyModelGroup headNeckGroup;
    [SerializeField] private AnatomyModelGroup trunkGroup;
    [SerializeField] private AnatomyModelGroup leftUpperLimbGroup;
    [SerializeField] private AnatomyModelGroup rightUpperLimbGroup;
    [SerializeField] private AnatomyModelGroup leftLowerLimbGroup;
    [SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

    [SerializeField] private float level1Scale = 0.65f;

    [Header("Viewing")]
    [SerializeField] private Camera arCamera;

    [SerializeField] private float distanceFromCamera = 1.5f;

    [SerializeField] private float targetHeight = -0.1f;

    [SerializeField] private float zoomScale = 2f;

    private Transform selectedRoot;

    private readonly List<Transform> movedModels =
        new List<Transform>();

    private readonly Dictionary<Transform, Transform> originalParents =
        new Dictionary<Transform, Transform>();

    private readonly Dictionary<Transform, Vector3> originalPositions =
        new Dictionary<Transform, Vector3>();

    private readonly Dictionary<Transform, Quaternion> originalRotations =
        new Dictionary<Transform, Quaternion>();

    private readonly Dictionary<Transform, Vector3> originalScales =
        new Dictionary<Transform, Vector3>();

    [SerializeField]
    private float level1ModelScale = 1f;

    [SerializeField]
    private float childModelScale = 2f;

    [SerializeField] private float trunkChildModelScale = 2f;

    [SerializeField] private float leftUpperLimbChildModelScale = 2f;

    [SerializeField] private float rightUpperLimbChildModelScale = 2f;

    [Header("Left Lower Limb Scale")]
    [SerializeField] private float leftLowerLimbChildModelScale = 2f;
    [SerializeField] private float leftLowerLimbLevel4Scale = 4f;

    [Header("Right Lower Limb Scale")]
    [SerializeField] private float rightLowerLimbChildModelScale = 2f;
    [SerializeField] private float rightLowerLimbLevel4Scale = 4f;

    private void Awake()
    {
        if (arCamera == null)
        {
            arCamera = Camera.main;
        }

        CreateSelectedRoot();
    }

    private void CreateSelectedRoot()
    {
        if (selectedRoot != null)
            return;

        GameObject root = new GameObject("SelectedGroupRoot");

        selectedRoot = root.transform;

        selectedRoot.SetParent(transform);

        selectedRoot.localPosition = Vector3.zero;
        selectedRoot.localRotation = Quaternion.identity;
        selectedRoot.localScale = Vector3.one;
    }


    public void ShowGroup(
        AnatomyModelGroup group,
        string groupName)
    {
        if (group == null)
        {
            Debug.LogWarning(
                "ShowGroup: group null."
            );

            return;
        }

        List<Transform> models =
            new List<Transform>();

        foreach (
            Transform model
            in group.LinkedModels)
        {
            if (model == null)
                continue;

            models.Add(model);
        }

        ShowModels(
            models,
            groupName
        );
    }

    private List<Transform> GetTopLevelModels(
        AnatomyModelGroup group
    )
    {
        List<Transform> result =
            new List<Transform>();

        HashSet<Transform> all =
            new HashSet<Transform>(
                group.LinkedModels
            );

        foreach (Transform model in group.LinkedModels)
        {
            if (model == null)
                continue;

            bool hasSelectedAncestor = false;

            Transform parent = model.parent;

            while (parent != null)
            {
                if (all.Contains(parent))
                {
                    hasSelectedAncestor = true;
                    break;
                }

                parent = parent.parent;
            }

            if (!hasSelectedAncestor)
            {
                result.Add(model);
            }
        }

        return result;
    }

    private void SaveOriginalState(
        Transform model
    )
    {
        if (originalParents.ContainsKey(model))
            return;

        originalParents.Add(
            model,
            model.parent
        );

        originalPositions.Add(
            model,
            model.position
        );

        originalRotations.Add(
            model,
            model.rotation
        );

        originalScales.Add(
            model,
            model.localScale
        );
    }

    private void HideFullBody()
    {
        if (fullBodyRoot == null)
        {
            Debug.LogError(
                "FULL BODY ROOT CHUA GAN."
            );

            return;
        }

        Renderer[] renderers =
            fullBodyRoot.GetComponentsInChildren<Renderer>(
                true
            );

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }



public void ShowFullBody()
{
    if (fullBodyRoot == null)
    {
        Debug.LogError("FULL BODY ROOT CHUA GAN.");
        return;
    }

    Renderer[] renderers =
        fullBodyRoot.GetComponentsInChildren<Renderer>(true);

    foreach (Renderer renderer in renderers)
    {
        renderer.enabled = true;
    }
}


private void FrameSelectedGroup(
    string groupName
    )
{
    if (movedModels.Count == 0)
        return;

    if (arCamera == null)
        return;

    // =========================
    // SCALE TOÀN BỘ GROUP
    // =========================

    float modelScale;

    bool isLevel1 =
    groupName == "HeadNeck" ||
    groupName == "Trunk" ||
    groupName == "LeftUpperLimb" ||
    groupName == "RightUpperLimb" ||
    groupName == "LeftLowerLimb" ||
    groupName == "RightLowerLimb";

    bool isTrunkChild =
    groupName == "PectoralMuscles" ||
    groupName == "ThoracicWallMuscles" ||
    groupName == "Diaphragm" ||
    groupName == "AnteriorAbdominalWall" ||
    groupName == "PosteriorAbdominalWall" ||
    groupName == "SuperficialBackMuscles" ||
    groupName == "IntermediateBackMuscles" ||
    groupName == "DeepBackMuscles" ||
    groupName == "PectoralisMajor_Left" ||
    groupName == "PectoralisMajor_Right" ||
    groupName == "PectoralisMinor_Left" ||
    groupName == "PectoralisMinor_Right" ||
    groupName == "Subclavius_Left" ||
    groupName == "Subclavius_Right" ||
    groupName == "SerratusAnterior_Left" ||
    groupName == "SerratusAnterior_Right" ||
    groupName == "ExternalIntercostals_Left" ||
    groupName == "ExternalIntercostals_Right" ||
    groupName == "InternalIntercostals_Left" ||
    groupName == "InternalIntercostals_Right" ||
    groupName == "InnermostIntercostals_Left" ||
    groupName == "InnermostIntercostals_Right" ||
    groupName == "TransversusThoracis_Left" ||
    groupName == "TransversusThoracis_Right" ||
    groupName == "RectusAbdominis_Left" ||
    groupName == "RectusAbdominis_Right" ||
    groupName == "Pyramidalis_Left" ||
    groupName == "Pyramidalis_Right" ||
    groupName == "ExternalOblique_Left" ||
    groupName == "ExternalOblique_Right" ||
    groupName == "InternalOblique_Left" ||
    groupName == "InternalOblique_Right" ||
    groupName == "TransversusAbdominis_Left" ||
    groupName == "TransversusAbdominis_Right" ||
    groupName == "QuadratusLumborum_Left" ||
    groupName == "QuadratusLumborum_Right" ||
    groupName == "Trapezius_Left" ||
    groupName == "Trapezius_Right" ||
    groupName == "LatissimusDorsi_Left" ||
    groupName == "LatissimusDorsi_Right" ||
    groupName == "RhomboidMajor_Left" ||
    groupName == "RhomboidMajor_Right" ||
    groupName == "RhomboidMinor_Left" ||
    groupName == "RhomboidMinor_Right" ||
    groupName == "LevatorScapulae_Left" ||
    groupName == "LevatorScapulae_Right" ||
    groupName == "SerratusPosteriorSuperior_Left" ||
    groupName == "SerratusPosteriorSuperior_Right" ||
    groupName == "SerratusPosteriorInferior_Left" ||
    groupName == "SerratusPosteriorInferior_Right" ||
    groupName == "ErectorSpinae_Left" ||
    groupName == "ErectorSpinae_Right" ||
    groupName == "Iliocostalis_Left" ||
    groupName == "Iliocostalis_Right" ||
    groupName == "Longissimus_Left" ||
    groupName == "Longissimus_Right" ||
    groupName == "Spinalis_Left" ||
    groupName == "Spinalis_Right" ||
    groupName == "Transversospinales_Left" ||
    groupName == "Transversospinales_Right" ||
    groupName == "Semispinalis_Left" ||
    groupName == "Semispinalis_Right" ||
    groupName == "Multifidus_Left" ||
    groupName == "Multifidus_Right" ||
    groupName == "Rotatores_Left" ||
    groupName == "Rotatores_Right";

    bool isLeftUpperLimbChild =
    groupName == "ShoulderMuscles" ||
    groupName == "AnteriorArmMuscles" ||
    groupName == "PosteriorArmMuscles" ||
    groupName == "AnteriorForearmMuscles" ||
    groupName == "PosteriorForearmMuscles" ||
    groupName == "HandMuscles" ||
    groupName.EndsWith("_Left");

    bool isRightUpperLimbChild =
    groupName.EndsWith("_Right") ||
    groupName == "ShoulderMuscles_Right" ||
    groupName == "AnteriorArmMuscles_Right" ||
    groupName == "PosteriorArmMuscles_Right" ||
    groupName == "AnteriorForearmMuscles_Right" ||
    groupName == "PosteriorForearmMuscles_Right" ||
    groupName == "HandMuscles_Right";

    bool isLeftLowerLimbLevel4 =
    groupName == "GluteusMaximus_Left" ||
    groupName == "GluteusMedius_Left" ||
    groupName == "GluteusMinimus_Left" ||
    groupName == "Piriformis_Left" ||
    groupName == "SuperiorGemellus_Left" ||
    groupName == "ObturatorInternus_Left" ||
    groupName == "InferiorGemellus_Left" ||
    groupName == "QuadratusFemoris_Left" ||
    groupName == "Iliacus_Left" ||
    groupName == "PsoasMajor_Left" ||
    groupName == "Sartorius_Left" ||
    groupName == "RectusFemoris_Left" ||
    groupName == "VastusLateralis_Left" ||
    groupName == "VastusMedialis_Left" ||
    groupName == "VastusIntermedius_Left" ||
    groupName == "Pectineus_Left" ||
    groupName == "AdductorLongus_Left" ||
    groupName == "AdductorBrevis_Left" ||
    groupName == "AdductorMagnus_Left" ||
    groupName == "Gracilis_Left" ||
    groupName == "ObturatorExternus_Left" ||
    groupName == "BicepsFemoris_Left" ||
    groupName == "Semitendinosus_Left" ||
    groupName == "Semimembranosus_Left" ||
    groupName == "TibialisAnterior_Left" ||
    groupName == "ExtensorHallucisLongus_Left" ||
    groupName == "ExtensorDigitorumLongus_Left" ||
    groupName == "FibularisTertius_Left" ||
    groupName == "FibularisLongus_Left" ||
    groupName == "FibularisBrevis_Left" ||
    groupName == "Gastrocnemius_Left" ||
    groupName == "Soleus_Left" ||
    groupName == "Plantaris_Left" ||
    groupName == "Popliteus_Left" ||
    groupName == "TibialisPosterior_Left" ||
    groupName == "FlexorDigitorumLongus_Left" ||
    groupName == "FlexorHallucisLongus_Left" ||
    groupName == "ExtensorDigitorumBrevis_Left" ||
    groupName == "ExtensorHallucisBrevis_Left" ||
    groupName == "AbductorHallucis_Left" ||
    groupName == "FlexorDigitorumBrevis_Left" ||
    groupName == "AbductorDigitiMinimi_Left" ||
    groupName == "QuadratusPlantae_Left" ||
    groupName == "Lumbricals_Left" ||
    groupName == "FlexorHallucisBrevis_Left" ||
    groupName == "AdductorHallucis_Left" ||
    groupName == "FlexorDigitiMinimiBrevis_Left" ||
    groupName == "OpponensDigitiMinimi_Left" ||
    groupName == "PlantarInterossei_Left" ||
    groupName == "DorsalInterossei_Left";


    bool isLeftLowerLimbChild =
    groupName == "GlutealRegion" ||
    groupName == "SuperficialGlutealMuscles" ||
    groupName == "DeepGlutealMuscles" ||
    groupName == "ThighRegion" ||
    groupName == "AnteriorCompartment_Thigh" ||
    groupName == "MedialCompartment_Thigh" ||
    groupName == "PosteriorCompartment_Thigh" ||
    groupName == "IliopsoasGroup" ||
    groupName == "QuadricepsFemoris" ||
    groupName == "LegRegion" ||
    groupName == "AnteriorCompartment_Leg" ||
    groupName == "LateralCompartment_Leg" ||
    groupName == "PosteriorCompartment_Leg" ||
    groupName == "SuperficialPosteriorGroup" ||
    groupName == "DeepPosteriorGroup" ||
    groupName == "FootRegion" ||
    groupName == "DorsalFootMuscles" ||
    groupName == "PlantarFootMuscles" ||
    groupName == "Layer1" ||
    groupName == "Layer2" ||
    groupName == "Layer3" ||
    groupName == "Layer4";

bool isRightLowerLimbLevel4 =
    groupName == "GluteusMaximus_Right" ||
    groupName == "GluteusMedius_Right" ||
    groupName == "GluteusMinimus_Right" ||
    groupName == "Piriformis_Right" ||
    groupName == "SuperiorGemellus_Right" ||
    groupName == "ObturatorInternus_Right" ||
    groupName == "InferiorGemellus_Right" ||
    groupName == "QuadratusFemoris_Right" ||
    groupName == "Iliacus_Right" ||
    groupName == "PsoasMajor_Right" ||
    groupName == "Sartorius_Right" ||
    groupName == "RectusFemoris_Right" ||
    groupName == "VastusLateralis_Right" ||
    groupName == "VastusMedialis_Right" ||
    groupName == "VastusIntermedius_Right" ||
    groupName == "Pectineus_Right" ||
    groupName == "AdductorLongus_Right" ||
    groupName == "AdductorBrevis_Right" ||
    groupName == "AdductorMagnus_Right" ||
    groupName == "Gracilis_Right" ||
    groupName == "ObturatorExternus_Right" ||
    groupName == "BicepsFemoris_Right" ||
    groupName == "Semitendinosus_Right" ||
    groupName == "Semimembranosus_Right" ||
    groupName == "TibialisAnterior_Right" ||
    groupName == "ExtensorHallucisLongus_Right" ||
    groupName == "ExtensorDigitorumLongus_Right" ||
    groupName == "FibularisTertius_Right" ||
    groupName == "FibularisLongus_Right" ||
    groupName == "FibularisBrevis_Right" ||
    groupName == "Gastrocnemius_Right" ||
    groupName == "Soleus_Right" ||
    groupName == "Plantaris_Right" ||
    groupName == "Popliteus_Right" ||
    groupName == "TibialisPosterior_Right" ||
    groupName == "FlexorDigitorumLongus_Right" ||
    groupName == "FlexorHallucisLongus_Right" ||
    groupName == "ExtensorDigitorumBrevis_Right" ||
    groupName == "ExtensorHallucisBrevis_Right" ||
    groupName == "AbductorHallucis_Right" ||
    groupName == "FlexorDigitorumBrevis_Right" ||
    groupName == "AbductorDigitiMinimi_Right" ||
    groupName == "QuadratusPlantae_Right" ||
    groupName == "Lumbricals_Right" ||
    groupName == "FlexorHallucisBrevis_Right" ||
    groupName == "AdductorHallucis_Right" ||
    groupName == "FlexorDigitiMinimiBrevis_Right" ||
    groupName == "OpponensDigitiMinimi_Right" ||
    groupName == "PlantarInterossei_Right" ||
    groupName == "DorsalInterossei_Right";

bool isRightLowerLimbChild =
    groupName == "GlutealRegion_Right" ||
    groupName == "SuperficialGlutealMuscles_Right" ||
    groupName == "DeepGlutealMuscles_Right" ||
    groupName == "ThighRegion_Right" ||
    groupName == "AnteriorCompartment_Thigh_Right" ||
    groupName == "MedialCompartment_Thigh_Right" ||
    groupName == "PosteriorCompartment_Thigh_Right" ||
    groupName == "IliopsoasGroup_Right" ||
    groupName == "QuadricepsFemoris_Right" ||
    groupName == "LegRegion_Right" ||
    groupName == "AnteriorCompartment_Leg_Right" ||
    groupName == "LateralCompartment_Leg_Right" ||
    groupName == "PosteriorCompartment_Leg_Right" ||
    groupName == "SuperficialPosteriorGroup_Right" ||
    groupName == "DeepPosteriorGroup_Right" ||
    groupName == "FootRegion_Right" ||
    groupName == "DorsalFootMuscles_Right" ||
    groupName == "PlantarFootMuscles_Right" ||
    groupName == "Layer1" ||
    groupName == "Layer2" ||
    groupName == "Layer3" ||
    groupName == "Layer4";

    if (isLevel1)
    {
    modelScale = level1ModelScale;
    }
    else if (isTrunkChild)
    {
    modelScale = trunkChildModelScale;
    }
    else if (isLeftUpperLimbChild)
    {
    modelScale = leftUpperLimbChildModelScale;
    }
    else if (isRightUpperLimbChild)
    {
    modelScale = rightUpperLimbChildModelScale;
    }
    else if (isRightLowerLimbLevel4) 
    { 
    modelScale = rightLowerLimbLevel4Scale; 
    }
    else if (isLeftLowerLimbLevel4) 
    { 
    modelScale = leftLowerLimbLevel4Scale; 
    } 
    else if (isRightLowerLimbChild) 
    { 
    modelScale = rightLowerLimbChildModelScale; 
    }
    else if (isLeftLowerLimbChild) 
    { 
    modelScale = leftLowerLimbChildModelScale;
    }
    else
    {
    modelScale = childModelScale;
    }

    selectedRoot.localScale =
        Vector3.one * modelScale;

    // =========================
    // TÍNH BOUNDS SAU KHI SCALE
    // =========================

    Bounds bounds =
        CalculateBounds();

    // =========================
    // ĐƯA GROUP VỀ TRƯỚC CAMERA
    // =========================

    Vector3 targetPosition =
        arCamera.transform.position
        + arCamera.transform.forward
        * distanceFromCamera;

    targetPosition +=
        Vector3.up * targetHeight;

    Vector3 offset =
        targetPosition - bounds.center;

    selectedRoot.position += offset;

    Debug.Log(
    "MODEL FRAME: "
    + groupName
    + " | SCALE = "
    + modelScale
    + " | CENTER = "
    + bounds.center
    + " | SIZE = "
    + bounds.size
    + " | ROOT = "
    + selectedRoot.position
    );
}

public void ShowModels(
    List<Transform> models,
    string groupName)
{
    RestorePreviousGroup();
    if (models == null ||
        models.Count == 0)
    {
        Debug.LogWarning(
            "ShowModels: Khong co model cho "
            + groupName
        );

        return;
    }

    CreateSelectedRoot();

    HideFullBody();

    movedModels.Clear();

    foreach (Transform model in models)
    {
        if (model == null)
            continue;

        if (model == selectedRoot.transform)
            continue;

        if (!originalParents.ContainsKey(model))
        {
            originalParents.Add(
                model,
                model.parent
            );

            originalPositions.Add(
                model,
                model.position
            );

            originalRotations.Add(
                model,
                model.rotation
            );

            originalScales.Add(
                model,
                model.localScale
            );
        }

        model.SetParent(
            selectedRoot.transform,
            true
        );

        Renderer[] renderers =
            model.GetComponentsInChildren<Renderer>(
                true
            );

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        movedModels.Add(model);
    }

    if (movedModels.Count == 0)
    {
        Debug.LogWarning(
            "ShowModels: Khong co model hop le cho "
            + groupName
        );

        return;
    }

    FrameSelectedGroup(groupName);

    Debug.Log(
        "ShowModels: "
        + groupName
        + " | Models: "
        + movedModels.Count
    );
}

    private Bounds CalculateBounds()
    {
        Bounds bounds =
            new Bounds();

        bool initialized = false;

        foreach (Transform model in movedModels)
        {
            if (model == null)
                continue;

            Renderer[] renderers =
                model.GetComponentsInChildren<Renderer>(
                    true
                );

            foreach (Renderer renderer in renderers)
            {
                if (!initialized)
                {
                    bounds = renderer.bounds;
                    initialized = true;
                }
                else
                {
                    bounds.Encapsulate(
                        renderer.bounds
                    );
                }
            }
        }

        return bounds;
    }

    public void RestoreFullBody()
    {
        RestorePreviousGroup();

        ShowFullBody();

        if (selectedRoot != null)
        {
            selectedRoot.position = Vector3.zero;
            selectedRoot.rotation = Quaternion.identity;
            selectedRoot.localScale = Vector3.one;
        }

        Debug.Log(
            "FULL BODY RESTORED"
        );
    }

    public void RestorePreviousGroup()
    {
        if (movedModels.Count == 0)
            return;

        foreach (Transform model in movedModels)
        {
            if (model == null)
                continue;

            if (originalParents.ContainsKey(model))
            {
                model.SetParent(
                    originalParents[model],
                    false
                );

                model.position =
                    originalPositions[model];

                model.rotation =
                    originalRotations[model];

                model.localScale =
                    originalScales[model];

                Renderer[] renderers =
                    model.GetComponentsInChildren<Renderer>(
                        true
                    );

                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
        }

        movedModels.Clear();

        originalParents.Clear();
        originalPositions.Clear();
        originalRotations.Clear();
        originalScales.Clear();

        if (selectedRoot != null)
        {
            selectedRoot.position = Vector3.zero;
            selectedRoot.rotation =
                Quaternion.identity;

            selectedRoot.localScale =
                Vector3.one;
        }
    }
}