using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class AnatomyUIManager : MonoBehaviour
{
[Header("Top Tabs")]
[SerializeField] private Button btnCo;
[SerializeField] private Button btnMachMau;
[SerializeField] private Button btnXuong;

[Header("Level 1 Model Viewer")]
[SerializeField] private MuscularLevel1Viewer muscularLevel1Viewer;
[SerializeField] private VascularLevel1Viewer vascularLevel1Viewer;
[SerializeField] private SkeletalSystemLevel1Viewer skeletalLevel1Viewer;
[Header("Muscular Level 2 Model Mapping")]
[SerializeField]
private MuscularLevel2ModelMap muscularLevel2ModelMap;
[SerializeField] private VascularLevel2ModelMap vascularLevel2ModelMap;
[SerializeField] private VascularLevel1ModelMap vascularLevel1ModelMap;
[SerializeField] private SkeletalSystemLevel1ModelMap skeletalLevel1ModelMap;
[SerializeField]
private SkeletalSystemLevel2ModelMap skeletalLevel2ModelMap;

[Header("System Models")]
[SerializeField] private GameObject muscularSystem;
[SerializeField] private GameObject vascularSystem;
[SerializeField] private GameObject skeletalSystem;

[SerializeField] private GameObject muscularModelRoot;
[SerializeField] private GameObject arAnatomyRoot;

[SerializeField] private Camera arCamera;
[SerializeField] private float vascularDisplayDistance = 1.0f;
private readonly Dictionary<Transform, Vector3> vascularOriginalPositions
    = new Dictionary<Transform, Vector3>();

[Header("Muscular Model Groups")]
[SerializeField] private AnatomyModelGroup headNeckGroup;
[SerializeField] private AnatomyModelGroup trunkGroup;
[SerializeField] private AnatomyModelGroup leftUpperLimbGroup;
[SerializeField] private AnatomyModelGroup rightUpperLimbGroup;
[SerializeField] private AnatomyModelGroup leftLowerLimbGroup;
[SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

[Header("Parts List")]
[SerializeField] private Button btnParts;
[SerializeField] private GameObject partListPanel;
[SerializeField] private Transform partListContent;

[Header("Detail")]
[SerializeField] private GameObject partDetailPanel;
[SerializeField] private Button btnBack;
[SerializeField] private TMP_Text detailTitle;
[SerializeField] private TMP_Text detailDescription;
[SerializeField] private TMP_Text detailFunction;

[SerializeField] private Transform relatedContainer;
[SerializeField] private Button relatedButtonPrefab;

[SerializeField] private Stack<string> navigationStack =
    new Stack<string>();

[SerializeField] private string currentPartName;

[Header("Button Prefab")]
[SerializeField] private Button partButtonPrefab;

[Header("Selected Part")]
[SerializeField] private float selectedScale = 2.0f;


private Dictionary<string, string[]> level1Data =
    new Dictionary<string, string[]>();

private Dictionary<string, string[]> childData =
    new Dictionary<string, string[]>();

private Dictionary<string, string> parentData =
    new Dictionary<string, string>();
private string currentSystem;
private string currentParentKey;
private GameObject currentSelectedPart;
private Vector3 originalSelectedScale;

private List<Transform> currentSelectedModels =
    new List<Transform>();

private Dictionary<Transform, Vector3> originalModelScales =
    new Dictionary<Transform, Vector3>();

Dictionary<string, string[]> relatedData =
new Dictionary<string, string[]>();
private void Start()
{
    SetupData();
    BuildParentData();

    // Build mapping hệ xương: Level 1 trước, Level 2 sau
    if (skeletalLevel1ModelMap != null)
    {
        skeletalLevel1ModelMap.Build();
    }
    else
    {
        Debug.LogError(
            "[AnatomyUIManager] Chưa gán SkeletalSystemLevel1ModelMap!"
        );
    }

    if (skeletalLevel2ModelMap != null)
    {
        skeletalLevel2ModelMap.Build();
    }
    else
    {
        Debug.LogError(
            "[AnatomyUIManager] Chưa gán SkeletalSystemLevel2ModelMap!"
        );
    }

    btnCo.onClick.AddListener(() =>
    {
        ShowSystem("MuscularSystem");
    });

    btnMachMau.onClick.AddListener(() =>
    {
        ShowSystem("VascularSystem");
    });

    btnXuong.onClick.AddListener(() =>
    {
        ShowSystem("SkeletalSystem");
    });

    btnParts.onClick.AddListener(TogglePartList);

    btnBack.onClick.AddListener(ShowPartList);

    ShowSystem("MuscularSystem");

    relatedData["LeftUpperLimb"] = new string[]
    {
        "Biceps Brachii",
        "Triceps Brachii",
        "Brachialis"
    };

    relatedData["RightUpperLimb"] = new string[]
    {
        "Biceps Brachii",
        "Triceps Brachii",
        "Brachialis"
    };

    relatedData["BicepsBrachii"] = new string[]
    {
        "Brachialis"
    };

    relatedData["TricepsBrachii"] = new string[]
    {

    };

    relatedData["Brachialis"] = new string[]
    {

    };
}


private void SetupData()
{
    level1Data["MuscularSystem"] = new string[]
    {
    "HeadNeck",
    "Trunk",
    "LeftUpperLimb",
    "RightUpperLimb",
    "LeftLowerLimb",
    "RightLowerLimb"
    };
        ///headneck----------------------------------------///
    childData["HeadNeck"] = new string[]
    {
    "FacialExpressionMuscles",
    "MasticationMuscles",
    "SuperficialNeckMuscles",
    "SuprahyoidMuscles",
    "InfrahyoidMuscles",
    "ScaleneMuscles",
    "DeepNeckMuscles"
    };

    childData["FacialExpressionMuscles"] = new string[]
    {
    "Frontalis",
    "OrbicularisOculi",
    "OrbicularisOris",
    "Buccinator",
    "ZygomaticusMajor",
    "ZygomaticusMinor",
    "Risorius",
    "DepressorAnguliOris",
    "Mentalis"
    };

    childData["MasticationMuscles"] = new string[]
    {
    "Masseter",
    "Temporalis",
    "MedialPterygoid",
    "LateralPterygoid"
    };

    childData["SuperficialNeckMuscles"] = new string[]
    {
    "Platysma",
    "Sternocleidomastoid_Left",
    "Sternocleidomastoid_Right",
    };

    childData["SuprahyoidMuscles"] = new string[]
    {
    "Digastric",
    "Mylohyoid",
    "Geniohyoid",
    "Stylohyoid"
    };

    childData["InfrahyoidMuscles"] = new string[]
    {
    "Sternohyoid",
    "Sternothyroid",
    "Thyrohyoid",
    "Omohyoid"
    };

    childData["ScaleneMuscles"] = new string[]
    {
    "AnteriorScalene",
    "MiddleScalene",
    "PosteriorScalene"
    };

    childData["DeepNeckMuscles"] = new string[]
    {
    "SpleniusCapitis",
    "LongusCapitis",
    "LongusColli",
    "RectusCapitisAnterior",
    "RectusCapitisLateralis"
    };

    childData["Digastric"] = new string[]
    {
    "AnteriorBelly",
    "PosteriorBelly"
    };

        ///trunk------------------------------------------///
    childData["Trunk"] = new string[]
    {
        "PectoralMuscles",
        "ThoracicWallMuscles",
        "Diaphragm",
        "AnteriorAbdominalWall",
        "PosteriorAbdominalWall",
        "SuperficialBackMuscles",
        "IntermediateBackMuscles",
        "DeepBackMuscles"
    };
    childData["PectoralMuscles"] = new string[]
    {
        "PectoralisMajor_Left",
        "PectoralisMajor_Right",
        "PectoralisMinor_Left",
        "PectoralisMinor_Right",
        "Subclavius_Left",
        "Subclavius_Right"
    };
    childData["ThoracicWallMuscles"] = new string[]
    {
        "SerratusAnterior_Left",
        "SerratusAnterior_Right",
        "ExternalIntercostals_Left",
        "ExternalIntercostals_Right",
        "InternalIntercostals_Left",
        "InternalIntercostals_Right",
        "InnermostIntercostals_Left",
        "InnermostIntercostals_Right",
        "TransversusThoracis_Left",
        "TransversusThoracis_Right"
    };
    childData["AnteriorAbdominalWall"] = new string[]
    {
        "RectusAbdominis_Left",
        "RectusAbdominis_Right",
        "Pyramidalis_Left",
        "Pyramidalis_Right",
        "ExternalOblique_Left",
        "ExternalOblique_Right",
        "InternalOblique_Left",
        "InternalOblique_Right",
        "TransversusAbdominis_Left",
        "TransversusAbdominis_Right"
    };
    childData["PosteriorAbdominalWall"] = new string[]
    {
        "QuadratusLumborum_Left",
        "QuadratusLumborum_Right",
    };
    childData["SuperficialBackMuscles"] = new string[]
    {
        "Trapezius_Left",
        "Trapezius_Right",
        "LatissimusDorsi_Left",
        "LatissimusDorsi_Right",
        "RhomboidMajor_Left",
        "RhomboidMajor_Right",
        "RhomboidMinor_Left",
        "RhomboidMinor_Right",
        "LevatorScapulae_Left",
        "LevatorScapulae_Right"
    };
    childData["IntermediateBackMuscles"] = new string[]
    {
        "SerratusPosteriorSuperior_Left",
        "SerratusPosteriorSuperior_Right",
        "SerratusPosteriorInferior_Left",
        "SerratusPosteriorInferior_Right"
    };
    childData["DeepBackMuscles"] = new string[]
    {
        "ErectorSpinae_Left",
        "ErectorSpinae_Right",
        "Transversospinales_Left",
        "Transversospinales_Right"
    };
    childData["ErectorSpinae_Left"] = new string[]
    {
        "Iliocostalis_Left",
        "Longissimus_Left",
        "Spinalis_Left"
    };

    childData["ErectorSpinae_Right"] = new string[]
    {
        "Iliocostalis_Right",
        "Longissimus_Right",
        "Spinalis_Right"
    };

    childData["Transversospinales_Left"] = new string[]
    {
        "Semispinalis_Left",
        "Multifidus_Left",
        "Rotatores_Left"
    };

    childData["Transversospinales_Right"] = new string[]
    {
        "Semispinalis_Right",
        "Multifidus_Right",
        "Rotatores_Right"
    };

            ///leftupperLimb-------------------------------------------///
    childData["LeftUpperLimb"] = new string[]
    {
        "ShoulderMuscles",
        "AnteriorArmMuscles",
        "PosteriorArmMuscles",
        "AnteriorForearmMuscles",
        "PosteriorForearmMuscles",
        "HandMuscles"
    };

    childData["ShoulderMuscles"] = new string[]
    {
        "Deltoid_Left",
        "Supraspinatus_Left",
        "Infraspinatus_Left",
        "TeresMinor_Left",
        "TeresMajor_Left",
        "Subscapularis_Left"
    };

    childData["AnteriorArmMuscles"] = new string[]
    {
        "BicepsBrachii_Left",
        "Brachialis_Left",
        "Coracobrachialis_Left"
    };

    childData["PosteriorArmMuscles"] = new string[]
    {
        "TricepsBrachii_Left",
        "Anconeus_Left"
    };

    childData["AnteriorForearmMuscles"] = new string[]
    {
        "SuperficialAnteriorForearm_Left",
        "IntermediateAnteriorForearm_Left",
        "DeepAnteriorForearm_Left"
    };

    childData["SuperficialAnteriorForearm_Left"] = new string[]
    {
        "PronatorTeres_Left",
        "FlexorCarpiRadialis_Left",
        "PalmarisLongus_Left",
        "FlexorCarpiUlnaris_Left"
    };

    childData["IntermediateAnteriorForearm_Left"] = new string[]
    {
        "FlexorDigitorumSuperficialis_Left"
    };

    childData["DeepAnteriorForearm_Left"] = new string[]
    {
        "FlexorDigitorumProfundus_Left",
        "FlexorPollicisLongus_Left",
        "PronatorQuadratus_Left"
    };

    childData["PosteriorForearmMuscles"] = new string[]
    {
        "SuperficialPosteriorForearm_Left",
        "DeepPosteriorForearm_Left"
    };

    childData["SuperficialPosteriorForearm_Left"] = new string[]
    {
        "ExtensorCarpiRadialisLongus_Left",
        "ExtensorCarpiRadialisBrevis_Left",
        "ExtensorDigitorum_Left",
        "ExtensorDigitiMinimi_Left",
        "ExtensorCarpiUlnaris_Left",
        "Brachioradialis_Left"
    };

    childData["DeepPosteriorForearm_Left"] = new string[]
    {
        "Supinator_Left",
        "AbductorPollicisLongus_Left",
        "ExtensorPollicisBrevis_Left",
        "ExtensorPollicisLongus_Left",
        "ExtensorIndicis_Left"
    };

    childData["HandMuscles"] = new string[]
    {
        "ThenarMuscles_Left",
        "HypothenarMuscles_Left"
    };

    childData["ThenarMuscles_Left"] = new string[]
    {
        "AbductorPollicisBrevis_Left",
        "FlexorPollicisBrevis_Left",
        "OpponensPollicis_Left",
        "AdductorPollicis_Left"
    };

    childData["HypothenarMuscles_Left"] = new string[]
    {
        "AbductorDigitiMinimi_Left",
        "FlexorDigitiMinimiBrevis_Left",
        "OpponensDigitiMinimi_Left"
    };

            ///RightUpperLimb---------------------------///
    childData["RightUpperLimb"] = new string[]
    {
        "ShoulderMuscles_Right",
        "AnteriorArmMuscles_Right",
        "PosteriorArmMuscles_Right",
        "AnteriorForearmMuscles_Right",
        "PosteriorForearmMuscles_Right",
        "HandMuscles_Right"
    };
    childData["ShoulderMuscles_Right"] = new string[]
    {
        "Deltoid_Right",
        "Supraspinatus_Right",
        "Infraspinatus_Right",
        "TeresMinor_Right",
        "TeresMajor_Right",
        "Subscapularis_Right"
    };
    childData["AnteriorArmMuscles_Right"] = new string[]
    {
        "BicepsBrachii_Right",
        "Brachialis_Right",
        "Coracobrachialis_Right"
    };

    childData["PosteriorArmMuscles_Right"] = new string[]
    {
        "TricepsBrachii_Right",
        "Anconeus_Right"
    };
    childData["AnteriorForearmMuscles_Right"] = new string[]
    {
        "SuperficialAnteriorForearm_Right",
        "IntermediateAnteriorForearm_Right",
        "DeepAnteriorForearm_Right"
    };

    childData["SuperficialAnteriorForearm_Right"] = new string[]
    {
        "PronatorTeres_Right",
        "FlexorCarpiRadialis_Right",
        "PalmarisLongus_Right",
        "FlexorCarpiUlnaris_Right"
    };

    childData["IntermediateAnteriorForearm_Right"] = new string[]
    {
        "FlexorDigitorumSuperficialis_Right"
    };

    childData["DeepAnteriorForearm_Right"] = new string[]
    {
        "FlexorDigitorumProfundus_Right",
        "FlexorPollicisLongus_Right",
        "PronatorQuadratus_Right"
    };
    childData["PosteriorForearmMuscles_Right"] = new string[]
    {
        "SuperficialPosteriorForearm_Right",
        "DeepPosteriorForearm_Right"
    };

    childData["SuperficialPosteriorForearm_Right"] = new string[]
    {
        "ExtensorCarpiRadialisLongus_Right",
        "ExtensorCarpiRadialisBrevis_Right",
        "ExtensorDigitorum_Right",
        "ExtensorDigitiMinimi_Right",
        "ExtensorCarpiUlnaris_Right",
        "Brachioradialis_Right"
    };

    childData["DeepPosteriorForearm_Right"] = new string[]
    {
        "Supinator_Right",
        "AbductorPollicisLongus_Right",
        "ExtensorPollicisBrevis_Right",
        "ExtensorPollicisLongus_Right",
        "ExtensorIndicis_Right"
    };
    childData["HandMuscles_Right"] = new string[]
    {
        "ThenarMuscles_Right",
        "HypothenarMuscles_Right"
    };

    childData["ThenarMuscles_Right"] = new string[]
    {
        "AbductorPollicisBrevis_Right",
        "FlexorPollicisBrevis_Right",
        "OpponensPollicis_Right",
        "AdductorPollicis_Right"
    };

    childData["HypothenarMuscles_Right"] = new string[]
    {
        "AbductorDigitiMinimi_Right",
        "FlexorDigitiMinimiBrevis_Right",
        "OpponensDigitiMinimi_Right"
    };

            ///LeftLowerLimb-------------------------------------///
    childData["LeftLowerLimb"] = new string[]
    {
        "GlutealRegion",
        "ThighRegion",
        "LegRegion",
        "FootRegion"
    };

    childData["GlutealRegion"] = new string[]
    {
        "SuperficialGlutealMuscles",
        "DeepGlutealMuscles"
    };

    childData["SuperficialGlutealMuscles"] = new string[]
    {
        "GluteusMaximus_Left",
        "GluteusMedius_Left",
        "GluteusMinimus_Left"
    };

    childData["DeepGlutealMuscles"] = new string[]
    {
        "Piriformis_Left",
        "SuperiorGemellus_Left",
        "ObturatorInternus_Left",
        "InferiorGemellus_Left",
        "QuadratusFemoris_Left"
    };
    childData["ThighRegion"] = new string[]
    {
        "AnteriorCompartment_Thigh",
        "MedialCompartment_Thigh",
        "PosteriorCompartment_Thigh"
    };

    childData["AnteriorCompartment_Thigh"] = new string[]
    {
        "IliopsoasGroup",
        "Sartorius_Left",
        "QuadricepsFemoris"
    };

    childData["IliopsoasGroup"] = new string[]
    {
        "Iliacus_Left",
        "PsoasMajor_Left"
    };

    childData["QuadricepsFemoris"] = new string[]
    {
        "RectusFemoris_Left",
        "VastusLateralis_Left",
        "VastusMedialis_Left",
        "VastusIntermedius_Left"
    };

    childData["MedialCompartment_Thigh"] = new string[]
    {
        "Pectineus_Left",
        "AdductorLongus_Left",
        "AdductorBrevis_Left",
        "AdductorMagnus_Left",
        "Gracilis_Left",
        "ObturatorExternus_Left"
    };

    childData["PosteriorCompartment_Thigh"] = new string[]
    {
        "BicepsFemoris_Left",
        "Semitendinosus_Left",
        "Semimembranosus_Left"
    };
    childData["LegRegion"] = new string[]
    {
        "AnteriorCompartment_Leg",
        "LateralCompartment_Leg",
        "PosteriorCompartment_Leg"
    };

    childData["AnteriorCompartment_Leg"] = new string[]
    {
        "TibialisAnterior_Left",
        "ExtensorHallucisLongus_Left",
        "ExtensorDigitorumLongus_Left",
        "FibularisTertius_Left"
    };

    childData["LateralCompartment_Leg"] = new string[]
    {
        "FibularisLongus_Left",
        "FibularisBrevis_Left"
    };

    childData["PosteriorCompartment_Leg"] = new string[]
    {
        "SuperficialPosteriorGroup",
        "DeepPosteriorGroup"
    };

    childData["SuperficialPosteriorGroup"] = new string[]
    {
        "Gastrocnemius_Left",
        "Soleus_Left",
        "Plantaris_Left"
    };

    childData["DeepPosteriorGroup"] = new string[]
    {
        "Popliteus_Left",
        "TibialisPosterior_Left",
        "FlexorDigitorumLongus_Left",
        "FlexorHallucisLongus_Left"
    };
    childData["FootRegion"] = new string[]
    {
        "DorsalFootMuscles",
        "PlantarFootMuscles"
    };

    childData["DorsalFootMuscles"] = new string[]
    {
        "ExtensorDigitorumBrevis_Left",
        "ExtensorHallucisBrevis_Left"
    };

    childData["PlantarFootMuscles"] = new string[]
    {
        "Layer1",
        "Layer2",
        "Layer3",
        "Layer4"
    };

    childData["Layer1"] = new string[]
    {
        "AbductorHallucis_Left",
        "FlexorDigitorumBrevis_Left",
        "AbductorDigitiMinimi_Left"
    };

    childData["Layer2"] = new string[]
    {
        "QuadratusPlantae_Left",
        "Lumbrical_Left"
    };

    childData["Layer3"] = new string[]
    {
        "FlexorHallucisBrevis_Left",
        "AdductorHallucis_Left",
        "FlexorDigitiMinimiBrevis_Left",
        "OpponensDigitiMinimi_Left"
    };

    childData["Layer4"] = new string[]
    {
        "PlantarInterosseous_Left",
        "DorsalInterosseous_Left"
    };

            ///rightlowerLimb--------------------------------------///
    childData["RightLowerLimb"] = new string[]
    {
        "GlutealRegion_Right",
        "ThighRegion_Right",
        "LegRegion_Right",
        "FootRegion_Right"
    };

    childData["GlutealRegion_Right"] = new string[]
    {
        "SuperficialGlutealMuscles_Right",
        "DeepGlutealMuscles_Right"
    };

    childData["SuperficialGlutealMuscles_Right"] = new string[]
    {
        "GluteusMaximus_Right",
        "GluteusMedius_Right",
        "GluteusMinimus_Right"
    };

    childData["DeepGlutealMuscles_Right"] = new string[]
    {
        "Piriformis_Right",
        "SuperiorGemellus_Right",
        "ObturatorInternus_Right",
        "InferiorGemellus_Right",
        "QuadratusFemoris_Right"
    };
    childData["ThighRegion_Right"] = new string[]
    {
        "AnteriorCompartment_Thigh_Right",
        "MedialCompartment_Thigh_Right",
        "PosteriorCompartment_Thigh_Right"
    };

    childData["AnteriorCompartment_Thigh_Right"] = new string[]
    {
        "IliopsoasGroup_Right",
        "Sartorius_Right",
        "QuadricepsFemoris_Right"
    };

    childData["IliopsoasGroup_Right"] = new string[]
    {
        "Iliacus_Right",
        "PsoasMajor_Right"
    };

    childData["QuadricepsFemoris_Right"] = new string[]
    {
        "RectusFemoris_Right",
        "VastusLateralis_Right",
        "VastusMedialis_Right",
        "VastusIntermedius_Right"
    };

    childData["MedialCompartment_Thigh_Right"] = new string[]
    {
        "Pectineus_Right",
        "AdductorLongus_Right",
        "AdductorBrevis_Right",
        "AdductorMagnus_Right",
        "Gracilis_Right",
        "ObturatorExternus_Right"
    };

    childData["PosteriorCompartment_Thigh_Right"] = new string[]
    {
        "BicepsFemoris_Right",
        "Semitendinosus_Right",
        "Semimembranosus_Right"
    };
    childData["LegRegion_Right"] = new string[]
    {
        "AnteriorCompartment_Leg_Right",
        "LateralCompartment_Leg_Right",
        "PosteriorCompartment_Leg_Right"
    };

    childData["AnteriorCompartment_Leg_Right"] = new string[]
    {
        "TibialisAnterior_Right",
        "ExtensorHallucisLongus_Right",
        "ExtensorDigitorumLongus_Right",
        "FibularisTertius_Right"
    };

    childData["LateralCompartment_Leg_Right"] = new string[]
    {
        "FibularisLongus_Right",
        "FibularisBrevis_Right"
    };

    childData["PosteriorCompartment_Leg_Right"] = new string[]
    {
        "SuperficialPosteriorGroup_Right",
        "DeepPosteriorGroup_Right"
    };

    childData["SuperficialPosteriorGroup_Right"] = new string[]
    {
        "Gastrocnemius_Right",
        "Soleus_Right",
        "Plantaris_Right"
    };

    childData["DeepPosteriorGroup_Right"] = new string[]
    {
        "Popliteus_Right",
        "TibialisPosterior_Right",
        "FlexorDigitorumLongus_Right",
        "FlexorHallucisLongus_Right"
    };
    childData["FootRegion_Right"] = new string[]
    {
        "DorsalFootMuscles_Right",
        "PlantarFootMuscles_Right"
    };

    childData["DorsalFootMuscles_Right"] = new string[]
    {
        "ExtensorDigitorumBrevis_Right",
        "ExtensorHallucisBrevis_Right"
    };

    childData["PlantarFootMuscles_Right"] = new string[]
    {
        "Layer1_Right",
        "Layer2_Right",
        "Layer3_Right",
        "Layer4_Right"
    };

    childData["Layer1_Right"] = new string[]
    {
        "AbductorHallucis_Right",
        "FlexorDigitorumBrevis_Right",
        "AbductorDigitiMinimi_Right"
    };

    childData["Layer2_Right"] = new string[]
    {
        "QuadratusPlantae_Right",
        "Lumbrical_Right"
    };

    childData["Layer3_Right"] = new string[]
    {
        "FlexorHallucisBrevis_Right",
        "AdductorHallucis_Right",
        "FlexorDigitiMinimiBrevis_Right",
        "OpponensDigitiMinimi_Right"
    };

    childData["Layer4_Right"] = new string[]
    {
        "PlantarInterosseous_Right",
        "DorsalInterosseous_Right"
    };

    level1Data["VascularSystem"] = new string[]
    {
    "Heart",
    "HeadNeck_Vascular",
    "Thorax_Vascular",
    "AbdomenPelvis_Vascular",
    "LeftUpperLimb_Vascular",
    "RightUpperLimb_Vascular",
    "LeftLowerLimb_Vascular",
    "RightLowerLimb_Vascular"
    };

            ///heart--------------------------------------///
    childData["Heart"] = new string[]
    {
        "RightAtrium",
        "RightVentricle",
        "LeftAtrium",
        "LeftVentricle",
        "CoronaryArteries",
        "CardiacVeins"
    };

    childData["CoronaryArteries"] = new string[]
    {
        "RightCoronaryArtery",
        "LeftCoronaryArtery"
    };

            ///HeadNeck----------------------------------------////
    childData["HeadNeck_Vascular"] = new string[]
    {
        "Arterial_HeadNeck",
        "Venous_HeadNeck"
    };

    childData["Arterial_HeadNeck"] = new string[]
    {
        "CerebralArteries",
        "AnteriorSpinalArteries",
        "CommonCarotidArteries"
    };

    childData["CerebralArteries"] = new string[]
    {
        "AnteriorCommunicatingArtery",
        "PosteriorCommunicating_Left",
        "PosteriorCommunicating_Right"
    };

    childData["AnteriorSpinalArteries"] = new string[]
    {
        "AnteriorSpinal_Left",
        "AnteriorSpinal_Right"
    };

    childData["CommonCarotidArteries"] = new string[]
    {
        "CommonCarotid_Left",
        "CommonCarotid_Right"
    };

    childData["Venous_HeadNeck"] = new string[]
    {
        "InternalJugularVeins",
        "OphthalmicVeins",
        "DuralVenousSinuses"
    };

    childData["InternalJugularVeins"] = new string[]
    {
        "InternalJugular_Left",
        "InternalJugular_Right"
    };

    childData["OphthalmicVeins"] = new string[]
    {
        "SuperiorOphthalmic_Left",
        "SuperiorOphthalmic_Right",
        "InferiorOphthalmic_Left",
        "InferiorOphthalmic_Right"
    };

    childData["DuralVenousSinuses"] = new string[]
    {
        "PetrosalSinus",
        "SagittalSinus",
        "OccipitalSinus",
        "SigmoidSinus",
        "StraightSinus"
    };

    childData["PetrosalSinus"] = new string[]
    {
        "InferiorPetrosal_Left",
        "InferiorPetrosal_Right"
    };

    childData["SagittalSinus"] = new string[]
    {
        "SuperiorSagittalSinus",
        "InferiorSagittalSinus"
    };

    childData["SigmoidSinus"] = new string[]
    {
        "SigmoidSinus_Left",
        "SigmoidSinus_Right"
    };

            ///Thorax_Vascular---------------------------///
    childData["Thorax_Vascular"] = new string[]
    {
        "Arterial_Thorax",
        "Venous_Thorax"
    };

    childData["Arterial_Thorax"] = new string[]
    {
        "AorticArch",
        "AscendingAorta",
        "ThoracicAorta"
    };

    childData["Venous_Thorax"] = new string[]
    {
        "InferiorVenaCava_Thoracic",
        "SuperiorVenaCava"
    };

            ///AbdomenPelvis_Vascular-------------------------///
    childData["AbdomenPelvis_Vascular"] = new string[]
    {
        "Arterial_AbdomenPelvis",
        "Venous_AbdomenPelvis"
    };

    childData["Arterial_AbdomenPelvis"] = new string[]
    {
        "AbdominalAorta",
        "CommonIliacArteries",
        "MedianSacralArtery"
    };

    childData["CommonIliacArteries"] = new string[]
    {
        "CommonIliacArtery_Left",
        "CommonIliacArtery_Right"
    };

    childData["Venous_AbdomenPelvis"] = new string[]
    {
        "PortalVenousSystem"
    };

    childData["PortalVenousSystem"] = new string[]
    {
        "HepaticPortalVein",
        "SplenicVein",
        "SuperiorMesentericVein"
    };

      // LeftUpperLimb_Vascular
    childData["LeftUpperLimb_Vascular"] = new string[]
    {
        "Arterial_LeftUpperLimb",
        "Venous_LeftUpperLimb"
    };

    childData["Arterial_LeftUpperLimb"] = new string[]
    {
        "SubclavianArtery_Left"
    };

    childData["Venous_LeftUpperLimb"] = new string[]
    {
        "SubclavianVein_Left"
    };

        ///RightUpperLimb_Vascular--------------------------///
    childData["RightUpperLimb_Vascular"] = new string[]
    {
        "Arterial_RightUpperLimb",
        "Venous_RightUpperLimb"
    };

    childData["Arterial_RightUpperLimb"] = new string[]
    {
        "SubclavianArtery_Right"
    };

    childData["Venous_RightUpperLimb"] = new string[]
    {
        "SubclavianVein_Right"
    };

        // LeftLowerLimb_Vascular
    childData["LeftLowerLimb_Vascular"] = new string[]
    {
        "Arterial_LeftLowerLimb",
        "Venous_LeftLowerLimb"
    };

    childData["Arterial_LeftLowerLimb"] = new string[]
    {
        "FemoralArtery_Left"
    };

    childData["Venous_LeftLowerLimb"] = new string[]
    {
        "FemoralVein_Left"
    };

        ///RightLowerLimb_Vascular--------------------------///
    childData["RightLowerLimb_Vascular"] = new string[]
    {
        "Arterial_RightLowerLimb",
        "Venous_RightLowerLimb"
    };

    childData["Arterial_RightLowerLimb"] = new string[]
    {
        "FemoralArtery_Right"
    };

    childData["Venous_RightLowerLimb"] = new string[]
    {
        "FemoralVein_Right"
    };

    level1Data["SkeletalSystem"] = new string[]
    {
    "Skull",
    "VertebralColumn",
    "ThoracicCage",
    "PectoralGirdle",
    "UpperLimb",
    "PelvicGirdle",
    "LowerLimb",
    };

        ///Skull---------------------------------------------------///
    childData["Skull"] = new string[]
    {
        "CranialBones",
        "FacialBones",
        "AuditoryOssicles",
        "Teeth",
        "NasalAndSinusStructures",
        "Cartilages",
        "HyoidBone"
    };

    childData["CranialBones"] = new string[]
    {
        "FrontalBone",
        "ParietalBones",
        "TemporalBones",
        "OccipitalBone",
        "SphenoidBone",
        "EthmoidBone"
    };

    childData["FacialBones"] = new string[]
    {
        "Mandible",
        "Maxillae",
        "ZygomaticBones",
        "NasalBones",
        "LacrimalBones",
        "PalatineBones",
        "InferiorNasalConchae",
        "Vomer"
    };

    childData["AuditoryOssicles"] = new string[]
    {
        "Malleus",
        "Incus",
        "Stapes"
    };

    childData["Teeth"] = new string[]
    {
        "UpperTeeth",
        "LowerTeeth"
    };

    childData["UpperTeeth"] = new string[]
    {
        "UpperIncisors",
        "UpperCanines",
        "UpperPremolars",
        "UpperMolars"
    };

    childData["LowerTeeth"] = new string[]
    {
        "LowerIncisors",
        "LowerCanines",
        "LowerPremolars",
        "LowerMolars"
    };

    childData["NasalAndSinusStructures"] = new string[]
    {
        "NasalSeptum"
    };


    childData["Cartilages"] = new string[]
    {
        "NasalCartilages",
        "LaryngealCartilages"
    };

    childData["NasalCartilages"] = new string[]
    {
        "MajorAlarCartilages",
        "LateralNasalSeptalProcesses"
    };

    childData["LaryngealCartilages"] = new string[]
    {
        "ThyroidCartilage",
        "CricoidCartilage",
        "ArytenoidCartilages",
        "CorniculateCartilages"
    };

        ///VertebralColumn---------------------------///
    childData["VertebralColumn"] = new string[]
    {
        "CervicalVertebrae",
        "ThoracicVertebrae",
        "LumbarVertebrae",
        "Sacrum",
        "Coccyx"
    };

    // Level 2: Cervical vertebrae
    childData["CervicalVertebrae"] = new string[]
    {
        "UpperCervicalVertebrae",
        "LowerCervicalVertebrae"
    };

    // Level 3–4: Atlas, Axis, C3–C7
    childData["UpperCervicalVertebrae"] = new string[]
    {
        "Atlas",
        "Axis"
    };

    childData["LowerCervicalVertebrae"] = new string[]
    {
        "VertebraC3",
        "VertebraC4",
        "VertebraC5",
        "VertebraC6",
        "VertebraC7"
    };

    // Level 2: Thoracic vertebrae
    childData["ThoracicVertebrae"] = new string[]
    {
        "UpperThoracicVertebrae",
        "LowerThoracicVertebrae"
    };

    // Level 3–4: T1–T6 and T7–T12
    childData["UpperThoracicVertebrae"] = new string[]
    {
        "VertebraT1",
        "VertebraT2",
        "VertebraT3",
        "VertebraT4",
        "VertebraT5",
        "VertebraT6"
    };

    childData["LowerThoracicVertebrae"] = new string[]
    {
        "VertebraT7",
        "VertebraT8",
        "VertebraT9",
        "VertebraT10",
        "VertebraT11",
        "VertebraT12"
    };

    // Level 2: Lumbar vertebrae
    childData["LumbarVertebrae"] = new string[]
    {
        "UpperLumbarVertebrae",
        "LowerLumbarVertebrae"
    };

    // Level 3–4: L1–L2 and L3–L5
    childData["UpperLumbarVertebrae"] = new string[]
    {
        "VertebraL1",
        "VertebraL2"
    };

    childData["LowerLumbarVertebrae"] = new string[]
    {
        "VertebraL3",
        "VertebraL4",
        "VertebraL5"
    };

        ///ThoracicCage-----------------------------///
    childData["ThoracicCage"] = new string[]
    {
        "Sternum",
        "Ribs",
        "CostalCartilages"
    };

    childData["Sternum"] = new string[]
    {
        "Manubrium",
        "BodyOfSternum",
        "XiphoidProcess"
    };

    childData["Ribs"] = new string[]
    {
        "FirstRib",
        "SecondRib",
        "ThirdRib",
        "FourthRib",
        "FifthRib",
        "SixthRib",
        "SeventhRib",
        "EighthRib",
        "NinthRib",
        "TenthRib",
        "EleventhRib",
        "TwelfthRib"
    };

    childData["FirstRib"] = new string[] { "FirstRibLeft", "FirstRibRight" };
    childData["SecondRib"] = new string[] { "SecondRibLeft", "SecondRibRight" };
    childData["ThirdRib"] = new string[] { "ThirdRibLeft", "ThirdRibRight" };
    childData["FourthRib"] = new string[] { "FourthRibLeft", "FourthRibRight" };
    childData["FifthRib"] = new string[] { "FifthRibLeft", "FifthRibRight" };
    childData["SixthRib"] = new string[] { "SixthRibLeft", "SixthRibRight" };
    childData["SeventhRib"] = new string[] { "SeventhRibLeft", "SeventhRibRight" };
    childData["EighthRib"] = new string[] { "EighthRibLeft", "EighthRibRight" };
    childData["NinthRib"] = new string[] { "NinthRibLeft", "NinthRibRight" };
    childData["TenthRib"] = new string[] { "TenthRibLeft", "TenthRibRight" };
    childData["EleventhRib"] = new string[] { "EleventhRibLeft", "EleventhRibRight" };
    childData["TwelfthRib"] = new string[] { "TwelfthRibLeft", "TwelfthRibRight" };

    childData["CostalCartilages"] = new string[]
    {
        "FirstCostalCartilage",
        "SecondCostalCartilage",
        "ThirdCostalCartilage",
        "FourthCostalCartilage",
        "FifthCostalCartilage",
        "SixthCostalCartilage",
        "SeventhCostalCartilage",
        "EighthCostalCartilage",
        "NinthCostalCartilage",
        "TenthCostalCartilage"
    };

    childData["FirstCostalCartilage"] = new string[] { "FirstCostalCartilageLeft", "FirstCostalCartilageRight" };
    childData["SecondCostalCartilage"] = new string[] { "SecondCostalCartilageLeft", "SecondCostalCartilageRight" };
    childData["ThirdCostalCartilage"] = new string[] { "ThirdCostalCartilageLeft", "ThirdCostalCartilageRight" };
    childData["FourthCostalCartilage"] = new string[] { "FourthCostalCartilageLeft", "FourthCostalCartilageRight" };
    childData["FifthCostalCartilage"] = new string[] { "FifthCostalCartilageLeft", "FifthCostalCartilageRight" };
    childData["SixthCostalCartilage"] = new string[] { "SixthCostalCartilageLeft", "SixthCostalCartilageRight" };
    childData["SeventhCostalCartilage"] = new string[] { "SeventhCostalCartilageLeft", "SeventhCostalCartilageRight" };
    childData["EighthCostalCartilage"] = new string[] { "EighthCostalCartilageLeft", "EighthCostalCartilageRight" };
    childData["NinthCostalCartilage"] = new string[] { "NinthCostalCartilageLeft", "NinthCostalCartilageRight" };
    childData["TenthCostalCartilage"] = new string[] { "TenthCostalCartilageLeft", "TenthCostalCartilageRight" };

}

private void BuildParentData()
{
    parentData.Clear();

    foreach (KeyValuePair<string, string[]> pair in childData)
    {
        string parent = pair.Key;

        foreach (string child in pair.Value)
        {
            parentData[child] = parent;
        }
    }

    foreach (KeyValuePair<string, string[]> pair in level1Data)
    {
        string system = pair.Key;

        foreach (string child in pair.Value)
        {
            parentData[child] = system;
        }
    }
}
Dictionary<string, string> displayNames = new Dictionary<string, string>()
{
    { "HeadNeck", "Head & Neck" },
    { "Trunk", "Trunk" },

    { "PectoralGirdle", "Pectoral Girdle" },
    { "UpperLimb", "Upper Limb" },
    { "LowerLimb", "Lower Limb" },
    { "PelvicGirdle", "Pelvic Girdle" },

    { "LeftUpperLimb", "Left Upper Limb" },
    { "RightUpperLimb", "Right Upper Limb" },

    { "LeftLowerLimb", "Left Lower Limb" },
    { "RightLowerLimb", "Right Lower Limb" },

    { "Heart", "Heart" },


    { "Skull", "Skull" },
    { "Pelvis", "Pelvis" },

    { "FacialExpressionMuscles", "Facial Expression Muscles" },
    { "MasticationMuscles", "Mastication Muscles" },
    { "SuperficialNeckMuscles", "Superficial Neck Muscles" },
    { "SuprahyoidMuscles", "Suprahyoid Muscles" },
    { "InfrahyoidMuscles", "Infrahyoid Muscles" },
    { "ScaleneMuscles", "Scalene Muscles" },
    { "DeepNeckMuscles", "Deep Neck Muscles" },
    { "Frontalis", "Frontalis" },
    { "OrbicularisOculi", "Orbicularis Oculi" },
    { "OrbicularisOris", "Orbicularis Oris" },
    { "Buccinator", "Buccinator" },
    { "ZygomaticusMajor", "Zygomaticus Major" },
    { "ZygomaticusMinor", "Zygomaticus Minor" },
    { "Risorius", "Risorius" },
    { "DepressorAnguliOris", "Depressor Anguli Oris" },
    { "Mentalis", "Mentalis" },

    { "Masseter", "Masseter" },
    { "Temporalis", "Temporalis" },
    { "MedialPterygoid", "Medial Pterygoid" },
    { "LateralPterygoid", "Lateral Pterygoid" },

    { "Platysma", "Platysma" },
    { "Sternocleidomastoid_Left", "Sternocleidomastoid Left" },
    { "Sternocleidomastoid_Right", "Sternocleidomastoid Right" },
    { "Trapezius_Left", "Trapezius Left" },
    { "Trapezius_Right", "Trapezius Right" },

    { "Digastric", "Digastric" },
    { "Mylohyoid", "Mylohyoid" },
    { "Geniohyoid", "Geniohyoid" },
    { "Stylohyoid", "Stylohyoid" },

    { "Sternohyoid", "Sternohyoid" },
    { "Sternothyroid", "Sternothyroid" },
    { "Thyrohyoid", "Thyrohyoid" },
    { "Omohyoid", "Omohyoid" },

    { "AnteriorBelly", "Anterior Belly" },
    { "PosteriorBelly", "Posterior Belly" },
    { "SuperiorBelly", "Superior Belly" },
    { "InferiorBelly", "Inferior Belly" },

    { "AnteriorScalene", "Anterior Scalene" },
    { "MiddleScalene", "Middle Scalene" },
    { "PosteriorScalene", "Posterior Scalene" },

    { "SpleniusCapitis", "Splenius Capitis" },
    { "SemispinalisCapitis", "Semispinalis Capitis" },
    { "LongusCapitis", "Longus Capitis" },
    { "LongusColli", "Longus Colli" },
    { "RectusCapitisAnterior", "Rectus Capitis Anterior" },
    { "RectusCapitisLateralis", "Rectus Capitis Lateralis" },

    { "PectoralMuscles", "Pectoral Muscles" },
    { "ThoracicWallMuscles", "Thoracic Wall Muscles" },
    { "Diaphragm", "Diaphragm" },
    { "AnteriorAbdominalWall", "Anterior Abdominal Wall" },
    { "PosteriorAbdominalWall", "Posterior Abdominal Wall" },
    { "SuperficialBackMuscles", "Superficial Back Muscles" },
    { "IntermediateBackMuscles", "Intermediate Back Muscles" },
    { "DeepBackMuscles", "Deep Back Muscles" },

    { "ShoulderMuscles", "Shoulder Muscles" },
    { "AnteriorArmMuscles", "Anterior Arm Muscles" },
    { "PosteriorArmMuscles", "Posterior Arm Muscles" },

    { "AnteriorForearmMuscles", "Anterior Forearm Muscles" },
    { "PosteriorForearmMuscles", "Posterior Forearm Muscles" },

    { "SuperficialAnteriorForearm_Left", "Superficial Anterior Forearm" },
    { "IntermediateAnteriorForearm_Left", "Intermediate Anterior Forearm" },
    { "DeepAnteriorForearm_Left", "Deep Anterior Forearm" },

    { "SuperficialPosteriorForearm_Left", "Superficial Posterior Forearm" },
    { "DeepPosteriorForearm_Left", "Deep Posterior Forearm" },

    { "Brachioradialis_Left", "Brachioradialis"},

    { "HandMuscles", "Hand Muscles" },
    { "ThenarMuscles_Left", "Thenar Muscles" },
    { "HypothenarMuscles_Left", "Hypothenar Muscles" },
    { "Lumbricals_Left", "Lumbricals" },
    { "PalmarInterossei_Left", "Palmar Interossei" },
    { "DorsalInterossei_Left", "Dorsal Interossei" },
    { "ShoulderMuscles_Right", "Shoulder Muscles" },

    { "AnteriorArmMuscles_Right", "Anterior Arm Muscles" },
    { "PosteriorArmMuscles_Right", "Posterior Arm Muscles" },

    { "AnteriorForearmMuscles_Right", "Anterior Forearm Muscles" },
    { "PosteriorForearmMuscles_Right", "Posterior Forearm Muscles" },

    { "SuperficialAnteriorForearm_Right", "Superficial Anterior Forearm" },
    { "IntermediateAnteriorForearm_Right", "Intermediate Anterior Forearm" },
    { "DeepAnteriorForearm_Right", "Deep Anterior Forearm" },

    { "SuperficialPosteriorForearm_Right", "Superficial Posterior Forearm" },
    { "DeepPosteriorForearm_Right", "Deep Posterior Forearm" },

    { "HandMuscles_Right", "Hand Muscles" },

    { "ThenarMuscles_Right", "Thenar Muscles" },
    { "HypothenarMuscles_Right", "Hypothenar Muscles" },

    { "Lumbricals_Right", "Lumbricals" },

    { "PalmarInterossei_Right", "Palmar Interossei" },
    { "DorsalInterossei_Right", "Dorsal Interossei" },
    { "GlutealRegion", "Gluteal Region" },
    { "SuperficialGlutealMuscles", "Superficial Gluteal Muscles" },
    { "DeepGlutealMuscles", "Deep Gluteal Muscles" },

    { "ThighRegion", "Thigh Region" },
    { "AnteriorCompartment_Thigh", "Anterior Compartment" },
    { "MedialCompartment_Thigh", "Medial Compartment" },
    { "PosteriorCompartment_Thigh", "Posterior Compartment" },

    { "IliopsoasGroup", "Iliopsoas Group" },
    { "QuadricepsFemoris", "Quadriceps Femoris" },

    { "LegRegion", "Leg Region" },
    { "AnteriorCompartment_Leg", "Anterior Compartment" },
    { "LateralCompartment_Leg", "Lateral Compartment" },
    { "PosteriorCompartment_Leg", "Posterior Compartment" },

    { "SuperficialPosteriorGroup", "Superficial Posterior Group" },
    { "DeepPosteriorGroup", "Deep Posterior Group" },

    { "FootRegion", "Foot Region" },
    { "DorsalFootMuscles", "Dorsal Foot Muscles" },
    { "PlantarFootMuscles", "Plantar Foot Muscles" },

    { "Layer1", "Layer 1" },
    { "Layer2", "Layer 2" },
    { "Layer3", "Layer 3" },
    { "Layer4", "Layer 4" },

    { "GlutealRegion_Right", "Gluteal Region" },
    { "SuperficialGlutealMuscles_Right", "Superficial Gluteal Muscles" },
    { "DeepGlutealMuscles_Right", "Deep Gluteal Muscles" },

    { "ThighRegion_Right", "Thigh Region" },
    { "AnteriorCompartment_Thigh_Right", "Anterior Compartment" },
    { "MedialCompartment_Thigh_Right", "Medial Compartment" },
    { "PosteriorCompartment_Thigh_Right", "Posterior Compartment" },

    { "IliopsoasGroup_Right", "Iliopsoas Group" },
    { "QuadricepsFemoris_Right", "Quadriceps Femoris" },

    { "LegRegion_Right", "Leg Region" },
    { "AnteriorCompartment_Leg_Right", "Anterior Compartment" },
    { "LateralCompartment_Leg_Right", "Lateral Compartment" },
    { "PosteriorCompartment_Leg_Right", "Posterior Compartment" },

    { "SuperficialPosteriorGroup_Right", "Superficial Posterior Group" },
    { "DeepPosteriorGroup_Right", "Deep Posterior Group" },

    { "FootRegion_Right", "Foot Region" },
    { "DorsalFootMuscles_Right", "Dorsal Foot Muscles" },
    { "PlantarFootMuscles_Right", "Plantar Foot Muscles" },

    { "Layer1_Right", "Layer 1" },
    { "Layer2_Right", "Layer 2" },
    { "Layer3_Right", "Layer 3" },
    { "Layer4_Right", "Layer 4" },

    { "RightAtrium", "Right Atrium" },
    { "RightVentricle", "Right Ventricle" },
    { "LeftAtrium", "Left Atrium" },
    { "LeftVentricle", "Left Ventricle" },

    { "RightAuricle", "Right Auricle" },
    { "LeftAuricle", "Left Auricle" },

    { "PulmonaryVeins", "Pulmonary Veins" },
    { "CoronarySinus", "Coronary Sinus" },

    { "TricuspidValve", "Tricuspid Valve" },
    { "MitralValve", "Mitral Valve" },
    { "PulmonaryValve", "Pulmonary Valve" },
    { "AorticValve", "Aortic Valve" },

    { "ChordaeTendineae_Right", "Chordae Tendineae" },
    { "ChordaeTendineae_Left", "Chordae Tendineae" },

    { "PapillaryMuscles_Right", "Papillary Muscles" },
    { "PapillaryMuscles_Left", "Papillary Muscles" },

    { "CardiacWall", "Cardiac Wall" },
    { "Epicardium", "Epicardium" },
    { "Myocardium", "Myocardium" },
    { "Endocardium", "Endocardium" },

    { "CardiacSeptum", "Cardiac Septum" },
    { "InteratrialSeptum", "Interatrial Septum" },
    { "InterventricularSeptum", "Interventricular Septum" },

    { "CoronaryArteries", "Coronary Arteries" },
    { "RightCoronaryArtery", "Right Coronary Artery" },
    { "LeftCoronaryArtery", "Left Coronary Artery" },
    { "AnteriorInterventricularArtery", "Anterior Interventricular Artery (LAD)" },
    { "CircumflexArtery", "Circumflex Artery" },

    { "CardiacVeins", "Cardiac Veins" },
    { "GreatCardiacVein", "Great Cardiac Vein" },
    { "MiddleCardiacVein", "Middle Cardiac Vein" },
    { "SmallCardiacVein", "Small Cardiac Vein" },

    { "ConductingSystem", "Conducting System" },
    { "SinoatrialNode", "Sinoatrial Node" },
    { "AtrioventricularNode", "Atrioventricular Node" },
    { "AtrioventricularBundle", "Atrioventricular Bundle" },
    { "RightBundleBranch", "Right Bundle Branch" },
    { "LeftBundleBranch", "Left Bundle Branch" },
    { "PurkinjeFibers", "Purkinje Fibers" },
    { "HeadNeck_Vascular", "Head & Neck" },
    { "Arterial_HeadNeck", "Arterial System" },
    { "Venous_HeadNeck", "Venous System" },

    { "CommonCarotid_Left", "Common Carotid (Left)" },
    { "CommonCarotid_Right", "Common Carotid (Right)" },

    { "InternalCarotid_Left", "Internal Carotid (Left)" },
    { "InternalCarotid_Right", "Internal Carotid (Right)" },

    { "ExternalCarotid_Left", "External Carotid (Left)" },
    { "ExternalCarotid_Right", "External Carotid (Right)" },

    { "Vertebral_Left", "Vertebral Artery (Left)" },
    { "Vertebral_Right", "Vertebral Artery (Right)" },

    { "CerebralArteries", "Cerebral Arteries" },

    { "AnteriorCerebral_Left", "Anterior Cerebral Artery (Left)" },
    { "AnteriorCerebral_Right", "Anterior Cerebral Artery (Right)" },

    { "MiddleCerebral_Left", "Middle Cerebral Artery (Left)" },
    { "MiddleCerebral_Right", "Middle Cerebral Artery (Right)" },

    { "PosteriorCerebral_Left", "Posterior Cerebral Artery (Left)" },
    { "PosteriorCerebral_Right", "Posterior Cerebral Artery (Right)" },

    { "InternalJugular_Left", "Internal Jugular Vein (Left)" },
    { "InternalJugular_Right", "Internal Jugular Vein (Right)" },

    { "ExternalJugular_Left", "External Jugular Vein (Left)" },
    { "ExternalJugular_Right", "External Jugular Vein (Right)" },

    { "VertebralVein_Left", "Vertebral Vein (Left)" },
    { "VertebralVein_Right", "Vertebral Vein (Right)" },

    { "DuralVenousSinuses", "Dural Venous Sinuses" },
    { "Brachioradialis_Right", "Brachioradialis"},

    { "SuperiorSagittalSinus", "Superior Sagittal Sinus" },
    { "InferiorSagittalSinus", "Inferior Sagittal Sinus" },
    { "StraightSinus", "Straight Sinus" },

    { "TransverseSinus_Left", "Transverse Sinus (Left)" },
    { "TransverseSinus_Right", "Transverse Sinus (Right)" },

    { "SigmoidSinus_Left", "Sigmoid Sinus (Left)" },
    { "SigmoidSinus_Right", "Sigmoid Sinus (Right)" },

    { "Arterial_Thorax", "Arterial System" },
    { "Venous_Thorax", "Venous System" },

    { "AorticArch", "Aortic Arch" },
    { "ThoracicAorta", "Thoracic Aorta" },
    { "BrachiocephalicTrunk", "Brachiocephalic Trunk" },

    { "IntercostalArteries", "Intercostal Arteries" },
    { "InternalThoracicArteries", "Internal Thoracic Arteries" },

    { "SuperiorVenaCava", "Superior Vena Cava" },
    { "BrachiocephalicVeins", "Brachiocephalic Veins" },

    { "AzygosSystem", "Azygos System" },
    { "AzygosVein", "Azygos Vein" },
    { "HemiazygosVein", "Hemiazygos Vein" },
    { "AccessoryHemiazygosVein", "Accessory Hemiazygos Vein" },

    { "IntercostalVeins", "Intercostal Veins" },

    { "PulmonaryVessels", "Pulmonary Vessels" },
    { "PulmonaryTrunk", "Pulmonary Trunk" },

    { "PulmonaryArtery_Left", "Pulmonary Artery (Left)" },
    { "PulmonaryArtery_Right", "Pulmonary Artery (Right)" },

    { "PulmonaryVeins_Left", "Pulmonary Veins (Left)" },
    { "PulmonaryVeins_Right", "Pulmonary Veins (Right)" },

    { "Arterial_AbdomenPelvis", "Arterial System" },
    { "AbdominalAorta", "Abdominal Aorta" },

    { "CeliacTrunk", "Celiac Trunk" },
    { "LeftGastricArtery", "Left Gastric Artery" },
    { "SplenicArtery", "Splenic Artery" },
    { "CommonHepaticArtery", "Common Hepatic Artery" },

    { "SuperiorMesentericArtery", "Superior Mesenteric Artery" },
    { "InferiorMesentericArtery", "Inferior Mesenteric Artery" },

    { "RenalArteries", "Renal Arteries" },
    { "RenalArtery_Left", "Renal Artery (Left)" },
    { "RenalArtery_Right", "Renal Artery (Right)" },

    { "GonadalArteries", "Gonadal Arteries" },
    { "GonadalArtery_Left", "Gonadal Artery (Left)" },
    { "GonadalArtery_Right", "Gonadal Artery (Right)" },

    { "CommonIliacArteries", "Common Iliac Arteries" },
    { "CommonIliacArtery_Left", "Common Iliac Artery (Left)" },
    { "CommonIliacArtery_Right", "Common Iliac Artery (Right)" },

    { "InternalExternalIliacArteries", "Internal & External Iliac Arteries" },
    { "InternalIliacArtery_Left", "Internal Iliac Artery (Left)" },
    { "InternalIliacArtery_Right", "Internal Iliac Artery (Right)" },
    { "ExternalIliacArtery_Left", "External Iliac Artery (Left)" },
    { "ExternalIliacArtery_Right", "External Iliac Artery (Right)" },


    { "Venous_AbdomenPelvis", "Venous System" },
    { "InferiorVenaCava", "Inferior Vena Cava" },

    { "RenalVeins", "Renal Veins" },
    { "RenalVein_Left", "Renal Vein (Left)" },
    { "RenalVein_Right", "Renal Vein (Right)" },

    { "GonadalVeins", "Gonadal Veins" },
    { "GonadalVein_Left", "Gonadal Vein (Left)" },
    { "GonadalVein_Right", "Gonadal Vein (Right)" },

    { "CommonIliacVeins", "Common Iliac Veins" },
    { "CommonIliacVein_Left", "Common Iliac Vein (Left)" },
    { "CommonIliacVein_Right", "Common Iliac Vein (Right)" },

    { "InternalExternalIliacVeins", "Internal & External Iliac Veins" },
    { "InternalIliacVein_Left", "Internal Iliac Vein (Left)" },
    { "InternalIliacVein_Right", "Internal Iliac Vein (Right)" },
    { "ExternalIliacVein_Left", "External Iliac Vein (Left)" },
    { "ExternalIliacVein_Right", "External Iliac Vein (Right)" },

    { "PortalVenousSystem", "Portal Venous System" },
    { "HepaticPortalVein", "Hepatic Portal Vein" },
    { "SplenicVein", "Splenic Vein" },
    { "SuperiorMesentericVein", "Superior Mesenteric Vein" },

    { "Arterial_LeftUpperLimb", "Arterial System" },

    { "SubclavianArtery_Left", "Subclavian Artery (Left)" },
    { "AxillaryArtery_Left", "Axillary Artery (Left)" },

    { "SuperiorThoracicArtery_Left", "Superior Thoracic Artery (Left)" },
    { "ThoracoacromialArtery_Left", "Thoracoacromial Artery (Left)" },
    { "LateralThoracicArtery_Left", "Lateral Thoracic Artery (Left)" },
    { "SubscapularArtery_Left", "Subscapular Artery (Left)" },
    { "AnteriorCircumflexHumeralArtery_Left", "Anterior Circumflex Humeral Artery (Left)" },
    { "PosteriorCircumflexHumeralArtery_Left", "Posterior Circumflex Humeral Artery (Left)" },

    { "BrachialArtery_Left", "Brachial Artery (Left)" },
    { "DeepBrachialArtery_Left", "Deep Brachial Artery (Left)" },
    { "SuperiorUlnarCollateralArtery_Left", "Superior Ulnar Collateral Artery (Left)" },
    { "InferiorUlnarCollateralArtery_Left", "Inferior Ulnar Collateral Artery (Left)" },

    { "ForearmArteries_Left", "Forearm Arteries" },
    { "RadialArtery_Left", "Radial Artery (Left)" },
    { "RadialRecurrentArtery_Left", "Radial Recurrent Artery (Left)" },

    { "UlnarArtery_Left", "Ulnar Artery (Left)" },
    { "AnteriorUlnarRecurrentArtery_Left", "Anterior Ulnar Recurrent Artery (Left)" },
    { "PosteriorUlnarRecurrentArtery_Left", "Posterior Ulnar Recurrent Artery (Left)" },
    { "CommonInterosseousArtery_Left", "Common Interosseous Artery (Left)" },

    { "InterosseousArteries_Left", "Interosseous Arteries" },
    { "AnteriorInterosseousArtery_Left", "Anterior Interosseous Artery (Left)" },
    { "PosteriorInterosseousArtery_Left", "Posterior Interosseous Artery (Left)" },

    { "HandArteries_Left", "Hand Arteries" },
    { "SuperficialPalmarArch_Left", "Superficial Palmar Arch (Left)" },
    { "DeepPalmarArch_Left", "Deep Palmar Arch (Left)" },
    { "DigitalArteries_Left", "Digital Arteries (Left)" },


    { "Venous_LeftUpperLimb", "Venous System" },

    { "DeepVeins_Left", "Deep Veins" },
    { "RadialVeins_Left", "Radial Veins (Left)" },
    { "UlnarVeins_Left", "Ulnar Veins (Left)" },
    { "BrachialVeins_Left", "Brachial Veins (Left)" },
    { "AxillaryVein_Left", "Axillary Vein (Left)" },

    { "SuperficialVeins_Left", "Superficial Veins" },
    { "CephalicVein_Left", "Cephalic Vein (Left)" },
    { "BasilicVein_Left", "Basilic Vein (Left)" },
    { "MedianCubitalVein_Left", "Median Cubital Vein (Left)" },

    { "SubclavianVein_Left", "Subclavian Vein (Left)" },

    { "Arterial_RightUpperLimb", "Arterial System" },

    { "SubclavianArtery_Right", "Subclavian Artery (Right)" },
    { "AxillaryArtery_Right", "Axillary Artery (Right)" },

    { "SuperiorThoracicArtery_Right", "Superior Thoracic Artery (Right)" },
    { "ThoracoacromialArtery_Right", "Thoracoacromial Artery (Right)" },
    { "LateralThoracicArtery_Right", "Lateral Thoracic Artery (Right)" },
    { "SubscapularArtery_Right", "Subscapular Artery (Right)" },
    { "AnteriorCircumflexHumeralArtery_Right", "Anterior Circumflex Humeral Artery (Right)" },
    { "PosteriorCircumflexHumeralArtery_Right", "Posterior Circumflex Humeral Artery (Right)" },

    { "BrachialArtery_Right", "Brachial Artery (Right)" },
    { "DeepBrachialArtery_Right", "Deep Brachial Artery (Right)" },
    { "SuperiorUlnarCollateralArtery_Right", "Superior Ulnar Collateral Artery (Right)" },
    { "InferiorUlnarCollateralArtery_Right", "Inferior Ulnar Collateral Artery (Right)" },

    { "ForearmArteries_Right", "Forearm Arteries" },
    { "RadialArtery_Right", "Radial Artery (Right)" },
    { "RadialRecurrentArtery_Right", "Radial Recurrent Artery (Right)" },

    { "UlnarArtery_Right", "Ulnar Artery (Right)" },
    { "AnteriorUlnarRecurrentArtery_Right", "Anterior Ulnar Recurrent Artery (Right)" },
    { "PosteriorUlnarRecurrentArtery_Right", "Posterior Ulnar Recurrent Artery (Right)" },
    { "CommonInterosseousArtery_Right", "Common Interosseous Artery (Right)" },

    { "InterosseousArteries_Right", "Interosseous Arteries" },
    { "AnteriorInterosseousArtery_Right", "Anterior Interosseous Artery (Right)" },
    { "PosteriorInterosseousArtery_Right", "Posterior Interosseous Artery (Right)" },

    { "HandArteries_Right", "Hand Arteries" },
    { "SuperficialPalmarArch_Right", "Superficial Palmar Arch (Right)" },
    { "DeepPalmarArch_Right", "Deep Palmar Arch (Right)" },
    { "DigitalArteries_Right", "Digital Arteries (Right)" },


    { "Venous_RightUpperLimb", "Venous System" },

    { "DeepVeins_Right", "Deep Veins" },
    { "RadialVeins_Right", "Radial Veins (Right)" },
    { "UlnarVeins_Right", "Ulnar Veins (Right)" },
    { "BrachialVeins_Right", "Brachial Veins (Right)" },
    { "AxillaryVein_Right", "Axillary Vein (Right)" },

    { "SuperficialVeins_Right", "Superficial Veins" },
    { "CephalicVein_Right", "Cephalic Vein (Right)" },
    { "BasilicVein_Right", "Basilic Vein (Right)" },
    { "MedianCubitalVein_Right", "Median Cubital Vein (Right)" },

    { "SubclavianVein_Right", "Subclavian Vein (Right)" },

    { "Arterial_LeftLowerLimb", "Arterial System" },

    { "FemoralArtery_Left", "Femoral Artery (Left)" },
    { "DeepFemoralArtery_Left", "Deep Femoral Artery (Left)" },

    { "SuperficialEpigastricArtery_Left", "Superficial Epigastric Artery (Left)" },
    { "SuperficialCircumflexIliacArtery_Left", "Superficial Circumflex Iliac Artery (Left)" },
    { "SuperficialExternalPudendalArtery_Left", "Superficial External Pudendal Artery (Left)" },
    { "DeepExternalPudendalArtery_Left", "Deep External Pudendal Artery (Left)" },

    { "MedialCircumflexFemoralArtery_Left", "Medial Circumflex Femoral Artery (Left)" },
    { "LateralCircumflexFemoralArtery_Left", "Lateral Circumflex Femoral Artery (Left)" },
    { "PerforatingArteries_Left", "Perforating Arteries (Left)" },

    { "PoplitealArtery_Left", "Popliteal Artery (Left)" },
    { "GenicularArteries_Left", "Genicular Arteries (Left)" },

    { "AnteriorTibialArtery_Left", "Anterior Tibial Artery (Left)" },
    { "AnteriorMedialMalleolarArtery_Left", "Anterior Medial Malleolar Artery (Left)" },
    { "AnteriorLateralMalleolarArtery_Left", "Anterior Lateral Malleolar Artery (Left)" },

    { "PosteriorTibialArtery_Left", "Posterior Tibial Artery (Left)" },
    { "MedialPlantarArtery_Left", "Medial Plantar Artery (Left)" },
    { "LateralPlantarArtery_Left", "Lateral Plantar Artery (Left)" },

    { "FibularArtery_Left", "Fibular Artery (Left)" },
    { "PerforatingFibularArtery_Left", "Perforating Fibular Artery (Left)" },

    { "FootArteries_Left", "Foot Arteries" },
    { "DorsalisPedisArtery_Left", "Dorsalis Pedis Artery (Left)" },
    { "ArcuateArtery_Left", "Arcuate Artery (Left)" },
    { "DeepPlantarArtery_Left", "Deep Plantar Artery (Left)" },


    { "Venous_LeftLowerLimb", "Venous System" },

    { "DeepVeins_LeftLowerLimb", "Deep Veins" },
    { "AnteriorTibialVeins_Left", "Anterior Tibial Veins (Left)" },
    { "PosteriorTibialVeins_Left", "Posterior Tibial Veins (Left)" },
    { "FibularVeins_Left", "Fibular Veins (Left)" },
    { "PoplitealVein_Left", "Popliteal Vein (Left)" },
    { "FemoralVein_Left", "Femoral Vein (Left)" },
    { "DeepFemoralVein_Left", "Deep Femoral Vein (Left)" },

    { "SuperficialVeins_LeftLowerLimb", "Superficial Veins" },
    { "GreatSaphenousVein_Left", "Great Saphenous Vein (Left)" },
    { "SmallSaphenousVein_Left", "Small Saphenous Vein (Left)" },

    { "Arterial_RightLowerLimb", "Arterial System" },

    { "FemoralArtery_Right", "Femoral Artery (Right)" },
    { "DeepFemoralArtery_Right", "Deep Femoral Artery (Right)" },

    { "SuperficialEpigastricArtery_Right", "Superficial Epigastric Artery (Right)" },
    { "SuperficialCircumflexIliacArtery_Right", "Superficial Circumflex Iliac Artery (Right)" },
    { "SuperficialExternalPudendalArtery_Right", "Superficial External Pudendal Artery (Right)" },
    { "DeepExternalPudendalArtery_Right", "Deep External Pudendal Artery (Right)" },

    { "MedialCircumflexFemoralArtery_Right", "Medial Circumflex Femoral Artery (Right)" },
    { "LateralCircumflexFemoralArtery_Right", "Lateral Circumflex Femoral Artery (Right)" },
    { "PerforatingArteries_Right", "Perforating Arteries (Right)" },

    { "PoplitealArtery_Right", "Popliteal Artery (Right)" },
    { "GenicularArteries_Right", "Genicular Arteries (Right)" },

    { "AnteriorTibialArtery_Right", "Anterior Tibial Artery (Right)" },
    { "AnteriorMedialMalleolarArtery_Right", "Anterior Medial Malleolar Artery (Right)" },
    { "AnteriorLateralMalleolarArtery_Right", "Anterior Lateral Malleolar Artery (Right)" },

    { "PosteriorTibialArtery_Right", "Posterior Tibial Artery (Right)" },
    { "MedialPlantarArtery_Right", "Medial Plantar Artery (Right)" },
    { "LateralPlantarArtery_Right", "Lateral Plantar Artery (Right)" },

    { "FibularArtery_Right", "Fibular Artery (Right)" },
    { "PerforatingFibularArtery_Right", "Perforating Fibular Artery (Right)" },

    { "FootArteries_Right", "Foot Arteries" },
    { "DorsalisPedisArtery_Right", "Dorsalis Pedis Artery (Right)" },
    { "ArcuateArtery_Right", "Arcuate Artery (Right)" },
    { "DeepPlantarArtery_Right", "Deep Plantar Artery (Right)" },

    { "Venous_RightLowerLimb", "Venous System" },

    { "DeepVeins_RightLowerLimb", "Deep Veins" },
    { "AnteriorTibialVeins_Right", "Anterior Tibial Veins (Right)" },
    { "PosteriorTibialVeins_Right", "Posterior Tibial Veins (Right)" },
    { "FibularVeins_Right", "Fibular Veins (Right)" },
    { "PoplitealVein_Right", "Popliteal Vein (Right)" },
    { "FemoralVein_Right", "Femoral Vein (Right)" },
    { "DeepFemoralVein_Right", "Deep Femoral Vein (Right)" },

    { "SuperficialVeins_RightLowerLimb", "Superficial Veins" },
    { "GreatSaphenousVein_Right", "Great Saphenous Vein (Right)" },
    { "SmallSaphenousVein_Right", "Small Saphenous Vein (Right)" },
    { "CranialBones", "Cranial Bones" },
    { "FacialBones", "Facial Bones" },
    { "AuditoryOssicles", "Auditory Ossicles" },
    { "Teeth", "Teeth" },
    { "UpperTeeth", "Upper Teeth" },
    { "LowerTeeth", "Lower Teeth" },

    { "UpperIncisors", "Upper Incisors" },
    { "UpperCanines", "Upper Canines" },
    { "UpperPremolars", "Upper Premolars" },
    { "UpperMolars", "Upper Molars" },

    { "LowerIncisors", "Lower Incisors" },
    { "LowerCanines", "Lower Canines" },
    { "LowerPremolars", "Lower Premolars" },
    { "LowerMolars", "Lower Molars" },

    { "NasalAndSinusStructures", "Nasal & Sinus Structures" },
    { "EthmoidAirCells", "Ethmoid Air Cells" },
    { "AnteriorEthmoidCells", "Anterior Ethmoid Cells" },
    { "MiddleEthmoidCells", "Middle Ethmoid Cells" },
    { "PosteriorEthmoidCells", "Posterior Ethmoid Cells" },
    { "ParanasalSinuses", "Paranasal Sinuses" },
    { "FrontalSinus", "Frontal Sinus" },
    { "SphenoidSinus", "Sphenoid Sinus" },
    { "NasalSeptum", "Nasal Septum" },

    { "Cartilages", "Cartilages" },
    { "NasalCartilages", "Nasal Cartilages" },
    { "MajorAlarCartilages", "Major Alar Cartilages" },
    { "LateralNasalSeptalProcesses", "Lateral Nasal Septal Processes" },
    { "LaryngealCartilages", "Laryngeal Cartilages" },
    { "ThyroidCartilage", "Thyroid Cartilage" },
    { "CricoidCartilage", "Cricoid Cartilage" },
    { "ArytenoidCartilages", "Arytenoid Cartilages" },
    { "CorniculateCartilages", "Corniculate Cartilages" },

    { "HyoidBone", "Hyoid Bone" },

    { "FrontalBone", "Frontal Bone" },
    { "ParietalBones", "Parietal Bones" },
    { "TemporalBones", "Temporal Bones" },
    { "OccipitalBone", "Occipital Bone" },
    { "SphenoidBone", "Sphenoid Bone" },
    { "EthmoidBone", "Ethmoid Bone" },
    { "Mandible", "Mandible" },
    { "Maxillae", "Maxillae" },
    { "ZygomaticBones", "Zygomatic Bones" },
    { "NasalBones", "Nasal Bones" },
    { "LacrimalBones", "Lacrimal Bones" },
    { "PalatineBones", "Palatine Bones" },
    { "InferiorNasalConchae", "Inferior Nasal Conchae" },
    { "VertebralColumn", "Vertebral Column" },

    { "CervicalVertebrae", "Cervical Vertebrae" },
    { "UpperCervicalVertebrae", "Upper Cervical Vertebrae" },
    { "LowerCervicalVertebrae", "Lower Cervical Vertebrae" },
    { "Atlas", "Atlas (C1)" },
    { "Axis", "Axis (C2)" },
    { "VertebraC3", "Vertebra C3" },
    { "VertebraC4", "Vertebra C4" },
    { "VertebraC5", "Vertebra C5" },
    { "VertebraC6", "Vertebra C6" },
    { "VertebraC7", "Vertebra C7" },

    { "ThoracicVertebrae", "Thoracic Vertebrae" },
    { "UpperThoracicVertebrae", "Upper Thoracic Vertebrae" },
    { "LowerThoracicVertebrae", "Lower Thoracic Vertebrae" },
    { "VertebraT1", "Vertebra T1" },
    { "VertebraT2", "Vertebra T2" },
    { "VertebraT3", "Vertebra T3" },
    { "VertebraT4", "Vertebra T4" },
    { "VertebraT5", "Vertebra T5" },
    { "VertebraT6", "Vertebra T6" },
    { "VertebraT7", "Vertebra T7" },
    { "VertebraT8", "Vertebra T8" },
    { "VertebraT9", "Vertebra T9" },
    { "VertebraT10", "Vertebra T10" },
    { "VertebraT11", "Vertebra T11" },
    { "VertebraT12", "Vertebra T12" },

    { "LumbarVertebrae", "Lumbar Vertebrae" },
    { "UpperLumbarVertebrae", "Upper Lumbar Vertebrae" },
    { "LowerLumbarVertebrae", "Lower Lumbar Vertebrae" },
    { "VertebraL1", "Vertebra L1" },
    { "VertebraL2", "Vertebra L2" },
    { "VertebraL3", "Vertebra L3" },
    { "VertebraL4", "Vertebra L4" },
    { "VertebraL5", "Vertebra L5" },
    { "ThoracicCage", "Thoracic Cage" },

    { "Sternum", "Sternum" },
    { "Manubrium", "Manubrium of Sternum" },
    { "BodyOfSternum", "Body of Sternum" },
    { "XiphoidProcess", "Xiphoid Process" },

    { "Ribs", "Ribs" },
    { "FirstRib", "First Rib" },
    { "FirstRibLeft", "First Rib (Left)" },
    { "FirstRibRight", "First Rib (Right)" },
    { "SecondRib", "Second Rib" },
    { "SecondRibLeft", "Second Rib (Left)" },
    { "SecondRibRight", "Second Rib (Right)" },
    { "ThirdRib", "Third Rib" },
    { "ThirdRibLeft", "Third Rib (Left)" },
    { "ThirdRibRight", "Third Rib (Right)" },
    { "FourthRib", "Fourth Rib" },
    { "FourthRibLeft", "Fourth Rib (Left)" },
    { "FourthRibRight", "Fourth Rib (Right)" },
    { "FifthRib", "Fifth Rib" },
    { "FifthRibLeft", "Fifth Rib (Left)" },
    { "FifthRibRight", "Fifth Rib (Right)" },
    { "SixthRib", "Sixth Rib" },
    { "SixthRibLeft", "Sixth Rib (Left)" },
    { "SixthRibRight", "Sixth Rib (Right)" },
    { "SeventhRib", "Seventh Rib" },
    { "SeventhRibLeft", "Seventh Rib (Left)" },
    { "SeventhRibRight", "Seventh Rib (Right)" },
    { "EighthRib", "Eighth Rib" },
    { "EighthRibLeft", "Eighth Rib (Left)" },
    { "EighthRibRight", "Eighth Rib (Right)" },
    { "NinthRib", "Ninth Rib" },
    { "NinthRibLeft", "Ninth Rib (Left)" },
    { "NinthRibRight", "Ninth Rib (Right)" },
    { "TenthRib", "Tenth Rib" },
    { "TenthRibLeft", "Tenth Rib (Left)" },
    { "TenthRibRight", "Tenth Rib (Right)" },
    { "EleventhRib", "Eleventh Rib" },
    { "EleventhRibLeft", "Eleventh Rib (Left)" },
    { "EleventhRibRight", "Eleventh Rib (Right)" },
    { "TwelfthRib", "Twelfth Rib" },
    { "TwelfthRibLeft", "Twelfth Rib (Left)" },
    { "TwelfthRibRight", "Twelfth Rib (Right)" },

    { "CostalCartilages", "Costal Cartilages" },
    { "FirstCostalCartilage", "First Costal Cartilage" },
    { "FirstCostalCartilageLeft", "First Costal Cartilage (Left)" },
    { "FirstCostalCartilageRight", "First Costal Cartilage (Right)" },
    { "SecondCostalCartilage", "Second Costal Cartilage" },
    { "SecondCostalCartilageLeft", "Second Costal Cartilage (Left)" },
    { "SecondCostalCartilageRight", "Second Costal Cartilage (Right)" },
    { "ThirdCostalCartilage", "Third Costal Cartilage" },
    { "ThirdCostalCartilageLeft", "Third Costal Cartilage (Left)" },
    { "ThirdCostalCartilageRight", "Third Costal Cartilage (Right)" },
    { "FourthCostalCartilage", "Fourth Costal Cartilage" },
    { "FourthCostalCartilageLeft", "Fourth Costal Cartilage (Left)" },
    { "FourthCostalCartilageRight", "Fourth Costal Cartilage (Right)" },
    { "FifthCostalCartilage", "Fifth Costal Cartilage" },
    { "FifthCostalCartilageLeft", "Fifth Costal Cartilage (Left)" },
    { "FifthCostalCartilageRight", "Fifth Costal Cartilage (Right)" },
    { "SixthCostalCartilage", "Sixth Costal Cartilage" },
    { "SixthCostalCartilageLeft", "Sixth Costal Cartilage (Left)" },
    { "SixthCostalCartilageRight", "Sixth Costal Cartilage (Right)" },
    { "SeventhCostalCartilage", "Seventh Costal Cartilage" },
    { "SeventhCostalCartilageLeft", "Seventh Costal Cartilage (Left)" },
    { "SeventhCostalCartilageRight", "Seventh Costal Cartilage (Right)" },
    { "EighthCostalCartilage", "Eighth Costal Cartilage" },
    { "EighthCostalCartilageLeft", "Eighth Costal Cartilage (Left)" },
    { "EighthCostalCartilageRight", "Eighth Costal Cartilage (Right)" },
    { "NinthCostalCartilage", "Ninth Costal Cartilage" },
    { "NinthCostalCartilageLeft", "Ninth Costal Cartilage (Left)" },
    { "NinthCostalCartilageRight", "Ninth Costal Cartilage (Right)" },
    { "TenthCostalCartilage", "Tenth Costal Cartilage" },
    { "TenthCostalCartilageLeft", "Tenth Costal Cartilage (Left)" },
    { "TenthCostalCartilageRight", "Tenth Costal Cartilage (Right)" }
};

private string GetDisplayName(string key)
{
    if (displayNames.ContainsKey(key))
    {
        return displayNames[key];
    }

    string result = key;

    bool isLeft = result.EndsWith("_Left");
    bool isRight = result.EndsWith("_Right");

    result = result
        .Replace("_Left", "")
        .Replace("_Right", "");

    result = Regex.Replace(
        result,
        @"([a-z])([A-Z])",
        "$1 $2"
    );

    if (isLeft)
    {
        result += " (Left)";
    }
    else if (isRight)
    {
        result += " (Right)";
    }

    return result;
}





private void ShowSystem(string systemName)
{
    // Khôi phục viewer của hệ thống đang rời khỏi.
    if (skeletalLevel1Viewer != null)
    {
        skeletalLevel1Viewer.ShowFullBody();
    }

    if (vascularLevel1Viewer != null)
    {
        vascularLevel1Viewer.RestorePreviousGroup();
    }

    if (muscularLevel1Viewer != null)
    {
        muscularLevel1Viewer.RestoreFullBody();
    }

    // Khôi phục phần đang chọn theo logic hiện tại.
    RestoreSelectedPart();

    // Xóa lịch sử chọn của hệ cũ để Back không đi nhầm hệ.
    navigationStack.Clear();
    currentPartName = "";
    currentSelectedModels.Clear();

    partListPanel.SetActive(false);
    partDetailPanel.SetActive(false);

    if (muscularSystem != null)
        muscularSystem.SetActive(systemName == "MuscularSystem");

    if (vascularSystem != null)
        vascularSystem.SetActive(systemName == "VascularSystem");

    if (skeletalSystem != null)
        skeletalSystem.SetActive(systemName == "SkeletalSystem");

    currentSystem = systemName;

    if (systemName == "MuscularSystem")
    {
        if (muscularLevel1Viewer != null)
            muscularLevel1Viewer.ShowFullBody();

        ShowAllMuscularModels();
    }
    else if (systemName == "VascularSystem")
    {
        if (vascularLevel1Viewer != null)
            vascularLevel1Viewer.ShowFullBody();
        else if (vascularSystem != null)
            ActivateAllChildren(vascularSystem.transform);
    }
    else if (systemName == "SkeletalSystem")
    {
        if (skeletalLevel1Viewer != null)
            skeletalLevel1Viewer.ShowFullBody();

        if (skeletalSystem != null)
            ActivateAllChildren(skeletalSystem.transform);
    }

    Debug.Log(
        "[System Check] " + systemName
        + " | Muscular active: "
        + (muscularSystem != null && muscularSystem.activeInHierarchy)
        + " | Vascular active: "
        + (vascularSystem != null && vascularSystem.activeInHierarchy)
        + " | Skeletal active: "
        + (skeletalSystem != null && skeletalSystem.activeInHierarchy)
    );

    GeneratePartList(systemName);
}



private void ShowAllMuscularModels()
    {
        if (muscularModelRoot == null)
        {
            Debug.LogError(
                "Muscular Model Root chưa được gán."
            );

            return;
        }

        ActivateAllChildren(
            muscularModelRoot.transform
        );

        List<Transform> allModels =
            new List<Transform>();

        AddGroupModels(
            headNeckGroup,
            allModels
        );

        AddGroupModels(
            trunkGroup,
            allModels
        );

        AddGroupModels(
            leftUpperLimbGroup,
            allModels
        );

        AddGroupModels(
            rightUpperLimbGroup,
            allModels
        );

        AddGroupModels(
            leftLowerLimbGroup,
            allModels
        );

        AddGroupModels(
            rightLowerLimbGroup,
            allModels
        );

        foreach (Transform model in allModels)
        {
            if (model == null)
                continue;

            model.gameObject.SetActive(true);
        }

        currentSelectedModels.Clear();
        originalModelScales.Clear();

        currentSelectedPart = null;

        Debug.Log(
            "ALL MUSCULAR MODELS ACTIVATED: "
            + allModels.Count
        );
    }

private void ActivateAllChildren(
        Transform root)
    {
        if (root == null)
            return;

        root.gameObject.SetActive(true);

        foreach (Transform child in root)
        {
            child.gameObject.SetActive(true);

            ActivateAllChildren(child);
        }
    }

private void TogglePartList()
{
    bool isOpen = partListPanel.activeSelf;

    partListPanel.SetActive(!isOpen);

    if (!isOpen)
    {
        partDetailPanel.SetActive(false);

        navigationStack.Clear();
        currentPartName = null;

        GeneratePartList(currentSystem);
    }
}


private void GeneratePartList(string key)
{
    foreach (Transform child in partListContent)
    {
        Destroy(child.gameObject);
    }

    string[] parts = null;

    // Level 1
    if (level1Data.ContainsKey(key))
    {
        parts = level1Data[key];
    }
    // Level 2, Level 3, Level 4...
    else if (childData.ContainsKey(key))
    {
        parts = childData[key];
    }

    if (parts == null)
        return;

    foreach (string partName in parts)
    {
        Button button = Instantiate(
            partButtonPrefab,
            partListContent
        );

        TMP_Text text =
            button.GetComponentInChildren<TMP_Text>();

        if (text != null)
        {
            text.text =
                GetDisplayName(partName);
        }

        string selectedPart = partName;

        button.onClick.AddListener(() =>
        {
            navigationStack.Clear();
            currentPartName = null;

            SelectPart(selectedPart);
        });
    }
}

private GameObject GetCurrentSystemRoot()
{
    if (currentSystem == "MuscularSystem")
    {
        return muscularSystem;
    }

    if (currentSystem == "VascularSystem")
    {
        return vascularSystem;
    }

    if (currentSystem == "SkeletalSystem")
    {
        return skeletalSystem;
    }

    return null;
}

private void SelectPart(string partName)
{
    if (!string.IsNullOrEmpty(currentPartName))
    {
    navigationStack.Push(currentPartName);
    }

    // Xử lý riêng các nhóm cấp 1 của hệ mạch máu.
    if (currentSystem == "VascularSystem")
    {
        if (vascularLevel2ModelMap != null &&
            vascularLevel2ModelMap.ContainsNode(partName))
        {
            List<Transform> models =
                vascularLevel2ModelMap.GetModels(partName);

            if (models.Count > 0)
            {
                currentPartName = partName;
                currentSelectedModels.Clear();
                currentSelectedModels.AddRange(models);

                vascularLevel1Viewer.ShowModels(models, partName);
                ShowPartDetail(partName);
            }
            else
            {
                Debug.LogWarning(
                    "[Vascular] Node chưa có model phù hợp: " + partName
                );
            }

            return;
        }

        SelectVascularLevel1Group(partName);
        return;
    }

    // SKELETAL LEVEL 1
    
    // SKELETAL SYSTEM LEVEL 1-4
    if (currentSystem == "SkeletalSystem")
    {
        if (skeletalLevel1ModelMap == null ||
            skeletalLevel1Viewer == null)
        {
            Debug.LogError(
                "[SkeletalSystem] Chưa gán Level 1 ModelMap hoặc Viewer."
            );
            return;
        }

        // Ưu tiên node Skull Level 2-4.
        if (skeletalLevel2ModelMap != null &&
            skeletalLevel2ModelMap.ContainsNode(partName))
        {
            List<Transform> models =
                skeletalLevel2ModelMap.GetModels(partName);

            if (models != null && models.Count > 0)
            {
                currentPartName = partName;

                currentSelectedModels.Clear();
                currentSelectedModels.AddRange(models);

                skeletalLevel1Viewer.ShowModels(
                        models,
                        partName,
                        IsSkeletalLevel3Or4(partName)
                );

                ShowPartDetail(partName);
            }
            else
            {
                Debug.LogWarning(
                    "[SkeletalSystem] Node chưa có model: "
                    + partName
                );
            }

            return;
        }

        // Nếu không phải node Level 2-4, kiểm tra group Level 1.
        AnatomyModelGroup skeletalGroup =
            skeletalLevel1ModelMap.GetGroup(partName);

        if (skeletalGroup != null)
        {
            List<Transform> models =
                new List<Transform>(skeletalGroup.LinkedModels);

            if (models.Count > 0)
            {
                currentPartName = partName;

                currentSelectedModels.Clear();
                currentSelectedModels.AddRange(models);

                skeletalLevel1Viewer.ShowModels(
                        models,
                        partName,
                        IsSkeletalLevel3Or4(partName)
                );

                ShowPartDetail(partName);
            }
            else
            {
                Debug.LogWarning(
                    "[SkeletalSystem] Group chưa có model: "
                    + partName
                );
            }

            return;
        }
    }

    AnatomyModelGroup mappedGroup =
        GetMuscularGroup(partName);

    if (mappedGroup != null)
    {
        SelectMappedMuscularGroup(
            partName,
            mappedGroup
        );

        return;
    }


    // =====================================================
    // MUSCULAR LEVEL 2 / LEVEL 3 / LEVEL 4
    // =====================================================

    if (
        currentSystem == "MuscularSystem" &&
        muscularLevel2ModelMap != null &&
        muscularLevel2ModelMap.ContainsNode(partName)
    )
    {
        List<Transform> levelModels =
            muscularLevel2ModelMap.GetModels(
                partName
            );

        if (
            levelModels != null &&
            levelModels.Count > 0
        )
        {
            SelectMappedLevel2Models(
                partName,
                levelModels
            );
        }
        else
        {
            RestoreSelectedPart();

            Debug.LogWarning(
                "Muscular node co mapping nhung khong co model: "
                + partName
            );
        }

        ShowPartDetail(
            partName
        );

        return;
    }


    // =====================================================
    // FALLBACK
    // =====================================================

    GameObject systemRoot =
        GetCurrentSystemRoot();

    if (systemRoot == null)
    {
        Debug.LogWarning(
            "System model chưa được gán."
        );

        return;
    }

    GameObject selectedPart =
        FindChildRecursive(
            systemRoot.transform,
            partName
        );

    if (selectedPart == null)
    {
        Debug.LogWarning(
            "Không tìm thấy model: "
            + partName
        );

        return;
    }

    currentSelectedPart =
        selectedPart;

    originalSelectedScale =
        currentSelectedPart.transform.localScale;

    HideOtherParts(
        systemRoot,
        currentSelectedPart
    );

    currentSelectedPart.transform.localScale =
        originalSelectedScale * selectedScale;

    ShowPartDetail(
        partName
    );

    Debug.Log(
        "Selected: "
        + partName
    );
}



private AnatomyModelGroup GetMuscularGroup(
    string partName)
{
    switch (partName)
    {
        case "HeadNeck":
            return headNeckGroup;

        case "Trunk":
            return trunkGroup;

        case "LeftUpperLimb":
            return leftUpperLimbGroup;

        case "RightUpperLimb":
            return rightUpperLimbGroup;

        case "LeftLowerLimb":
            return leftLowerLimbGroup;

        case "RightLowerLimb":
            return rightLowerLimbGroup;
    }

    return null;
}

private void SelectMappedMuscularGroup(
    string partName,
    AnatomyModelGroup group)
{
    RestoreSelectedPart();

    currentPartName = partName;

    currentSelectedModels.Clear();
    originalModelScales.Clear();

    if (muscularLevel1Viewer != null)
    {
        muscularLevel1Viewer.ShowGroup(
            group,
            partName
        );
    }
    else
    {
        Debug.LogError(
            "MuscularLevel1Viewer CHUA DUOC GAN."
        );
    }

    foreach (Transform model in group.LinkedModels)
    {
        if (model == null)
            continue;

        currentSelectedModels.Add(model);
    }

    ShowPartDetail(partName);

    Debug.Log(
        "Selected muscular group: "
        + partName
        + " | Models: "
        + currentSelectedModels.Count
    );
}

private void AddGroupModels(
    AnatomyModelGroup group,
    List<Transform> result)
{
    if (group == null)
        return;

    foreach (Transform model in group.LinkedModels)
    {
        if (model == null)
            continue;

        if (!result.Contains(model))
        {
            result.Add(model);
        }
    }
}

private void ShowPartDetail(string partName)
{
    currentPartName = partName;
    partListPanel.SetActive(false);
    partDetailPanel.SetActive(true);

    if (detailTitle != null)
    {
        detailTitle.text =
            GetDisplayName(partName);
    }

    if (detailDescription != null)
    {
        detailDescription.text =
            "Description of " + partName;
    }

    if (detailFunction != null)
    {
        detailFunction.text =
            "Function of " + partName;
    }


    // =====================================================
    // CLEAR RELATED NODES
    // =====================================================

    foreach (Transform child in relatedContainer)
    {
        Destroy(child.gameObject);
    }


    // =====================================================
    // CREATE CHILD NODES
    // =====================================================

    string[] children;

    if (
        childData.TryGetValue(
            partName,
            out children
        )
    )
    {
        foreach (string childPart in children)
        {
            if (string.IsNullOrEmpty(childPart))
                continue;

            Button button =
                Instantiate(
                    relatedButtonPrefab,
                    relatedContainer
                );

            TMP_Text txt =
                button.GetComponentInChildren<TMP_Text>();

            if (txt != null)
            {
                txt.text =
                    GetDisplayName(childPart);
            }

            string nextPart =
                childPart;

            button.onClick.AddListener(() =>
            {
                SelectPart(
                    nextPart
                );
            });
        }
    }


    // =====================================================
    // DEBUG
    // =====================================================

    Debug.Log(
        "DETAIL OPENED: "
        + partName
        + " | Children = "
        + (
            childData.ContainsKey(partName)
                ? childData[partName].Length
                : 0
        )
    );

}


private void HideOtherParts(
    GameObject systemRoot,
    GameObject selectedPart)
{
    foreach (Transform child in systemRoot.transform)
    {
        if (child.gameObject == selectedPart)
        {
            child.gameObject.SetActive(true);
        }
        else
        {
            child.gameObject.SetActive(false);
        }
    }
}


private void RestoreSelectedPart()
{
    // Khôi phục các model cơ đã được chuyển
    // vào SelectedGroupRoot về cha ban đầu.
    if (muscularLevel1Viewer != null)
    {
        muscularLevel1Viewer.RestorePreviousGroup();
    }

    // Khôi phục scale của các model đã chọn.
    foreach (
        KeyValuePair<Transform, Vector3> pair
        in originalModelScales)
    {
        Transform model = pair.Key;

        if (model == null)
            continue;

        model.localScale = pair.Value;
    }

    currentSelectedModels.Clear();
    originalModelScales.Clear();

    currentSelectedPart = null;
}


private void SelectVascularLevel1Group(string groupName)
{
    if (vascularLevel1ModelMap == null)
    {
        Debug.LogError(
            "[Vascular] Chưa gán VascularLevel1ModelMap."
        );
        return;
    }

    if (vascularSystem == null)
    {
        Debug.LogError(
            "[Vascular] VascularSystem chưa được gán."
        );
        return;
    }

    AnatomyModelGroup group =
        vascularLevel1ModelMap.GetGroup(groupName);

    if (group == null)
    {
        Debug.LogWarning(
            "[Vascular] Không tìm thấy group: " + groupName
        );
        return;
    }

    List<Transform> selectedModels =
        new List<Transform>();

    foreach (Transform model in group.LinkedModels)
    {
        if (model != null)
            selectedModels.Add(model);
    }

    if (selectedModels.Count == 0)
    {
        Debug.LogWarning(
            "[Vascular] Group chưa có model: " + groupName
            + ". Hãy kiểm tra mapping và Build()."
        );
        return;
    }
    foreach (var item in vascularOriginalPositions)
        {
        if (item.Key != null)
        item.Key.position = item.Value;
        }
    RestoreSelectedPart();

    currentPartName = groupName;
    currentSelectedModels.Clear();
    originalModelScales.Clear();

    // Ẩn các model mạch máu không thuộc nhóm được chọn.
    foreach (Transform model in vascularSystem.transform)
    {
        if (model != null)
            model.gameObject.SetActive(false);
    }

    // Chỉ bật các model thuộc nhóm được chọn.
    foreach (Transform model in selectedModels)
    {
        if (model == null) continue;

        if (!vascularOriginalPositions.ContainsKey(model))
            vascularOriginalPositions.Add(model, model.position);

        model.gameObject.SetActive(true);
    }

    // Tính tâm của toàn bộ nhóm model đang hiển thị.
    if (TryGetModelsCenter(selectedModels, out Vector3 groupCenter))
    {
    Camera cam = arCamera != null ? arCamera : Camera.main;

    if (cam != null)
    {
        // Điểm chính giữa màn hình, cách camera một khoảng tùy chỉnh.
        Vector3 targetCenter = cam.ViewportToWorldPoint(
            new Vector3(0.5f, 0.5f, vascularDisplayDistance)
        );

        Vector3 offset = targetCenter - groupCenter;

        foreach (Transform model in selectedModels)
        {
            if (model != null)
                model.position += offset;
        }
    }
    else


    {
        Debug.LogWarning(
            "[Vascular] Không tìm thấy Camera. Hãy gán AR Camera vào trường Ar Camera."
        );
    }
    }

    ShowPartDetail(groupName);

    Debug.Log(
        "[Vascular] Đã chọn nhóm: " + groupName
        + " | Số model: " + currentSelectedModels.Count
    );
}

private bool TryGetModelsCenter(
    List<Transform> models,
    out Vector3 center)
{
    center = Vector3.zero;
    Bounds bounds = new Bounds();
    bool hasBounds = false;

    foreach (Transform model in models)
    {
        if (model == null)
            continue;

        Renderer[] renderers =
            model.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            if (!hasBounds)
            {
                bounds = renderer.bounds;
                hasBounds = true;
            }
            else
            {
                bounds.Encapsulate(renderer.bounds);
            }
        }
    }

    if (!hasBounds)
        return false;

    center = bounds.center;
    return true;
}


private GameObject FindChildRecursive(
    Transform parent,
    string objectName)
{
    foreach (Transform child in parent)
    {
        if (child.name == objectName)
        {
            return child.gameObject;
        }

        GameObject result =
            FindChildRecursive(child, objectName);

        if (result != null)
        {
            return result;
        }
    }

    return null;
}

private string FindParentKey(string childKey)
{
    if (parentData.ContainsKey(childKey))
    {
        return parentData[childKey];
    }

    return null;
}

private void SelectMappedLevel2Models(
    string partName,
    List<Transform> models
)
{
    currentPartName =
    partName;
    currentSelectedModels.Clear();
    originalModelScales.Clear();

    if (muscularLevel1Viewer != null)
    {
        muscularLevel1Viewer.ShowModels(
            models,
            partName
        );
    }
    else
    {
        Debug.LogError(
            "MuscularLevel1Viewer CHUA DUOC GAN."
        );

        return;
    }

    foreach (Transform model in models)
    {
        if (model == null)
            continue;

        currentSelectedModels.Add(
            model
        );
    }

    ShowPartDetail(
        partName
    );

    Debug.Log(
        "CHILD MODEL SELECTED: "
        + partName
        + " | Models = "
        + models.Count
    );
}



private void ShowPreviousSkeletalPart(string previousPart)
{
    if (skeletalLevel1ModelMap == null ||
        skeletalLevel1Viewer == null)
    {
        Debug.LogError(
            "[SkeletalSystem] Chưa gán Level 1 ModelMap hoặc Viewer."
        );
        return;
    }

    // Quay lại node Skull Level 2-4.
    if (skeletalLevel2ModelMap != null &&
        skeletalLevel2ModelMap.ContainsNode(previousPart))
    {
        List<Transform> models =
            skeletalLevel2ModelMap.GetModels(previousPart);

        if (models != null && models.Count > 0)
        {
            currentPartName = previousPart;

            currentSelectedModels.Clear();
            currentSelectedModels.AddRange(models);

            skeletalLevel1Viewer.ShowModels(
                    models,
                    previousPart,
                    IsSkeletalLevel3Or4(previousPart)
            );

            ShowPartDetail(previousPart);
        }
        else
        {
            Debug.LogWarning(
                "[SkeletalSystem] Node trước không có model: "
                + previousPart
            );
        }

        return;
    }

    // Quay lại group Level 1.
    AnatomyModelGroup group =
        skeletalLevel1ModelMap.GetGroup(previousPart);

    if (group != null)
    {
        List<Transform> models =
            new List<Transform>(group.LinkedModels);

        if (models.Count > 0)
        {
            currentPartName = previousPart;

            currentSelectedModels.Clear();
            currentSelectedModels.AddRange(models);

            skeletalLevel1Viewer.ShowModels(
                models,
                previousPart,
                false
            );

            ShowPartDetail(previousPart);
        }
        else
        {
            Debug.LogWarning(
                "[SkeletalSystem] Group không có model: "
                + previousPart
            );
        }

        return;
    }

    Debug.LogWarning(
        "[SkeletalSystem] Không tìm thấy nhóm trước đó: "
        + previousPart
    );
}

private int GetSkeletalNodeLevel(string targetNode)
{
    foreach (var entry in childData)
    {
        if (entry.Key == "Skull" ||
            entry.Key == "VertebralColumn" ||
            entry.Key == "ThoracicCage" ||
            entry.Key == "PectoralGirdle" ||
            entry.Key == "UpperLimb" ||
            entry.Key == "PelvicGirdle" ||
            entry.Key == "LowerLimb")
        {
            if (FindNodeLevel(entry.Key, targetNode, 1, out int level))
                return level;
        }
    }

    return -1;
}

private bool FindNodeLevel(
    string currentNode,
    string targetNode,
    int level,
    out int foundLevel)
{
    if (currentNode == targetNode)
    {
        foundLevel = level;
        return true;
    }

    if (childData.TryGetValue(currentNode, out string[] children))
    {
        foreach (string child in children)
        {
            if (FindNodeLevel(child, targetNode, level + 1, out foundLevel))
                return true;
        }
    }

    foundLevel = -1;
    return false;
}

private bool IsSkeletalLevel3Or4(string nodeName)
{
    int level = GetSkeletalNodeLevel(nodeName);
    return level == 3 || level == 4;
}

public void ShowPartList()  //back
{
    // VASCULAR SYSTEM
    if (currentSystem == "VascularSystem")
    {
        if (navigationStack.Count > 0)
        {
            string previousPart = navigationStack.Pop();

            if (vascularLevel2ModelMap != null &&
                vascularLevel2ModelMap.ContainsNode(previousPart))
            {
                List<Transform> models =
                    vascularLevel2ModelMap.GetModels(previousPart);

                if (models != null && models.Count > 0)
                {
                    currentPartName = previousPart;

                    currentSelectedModels.Clear();
                    currentSelectedModels.AddRange(models);

                    if (vascularLevel1Viewer != null)
                    {
                        vascularLevel1Viewer.ShowModels(
                            models,
                            previousPart
                        );
                    }

                    ShowPartDetail(previousPart);
                    return;
                }
            }

            SelectVascularLevel1Group(previousPart);
            return;
        }

        if (vascularLevel1Viewer != null)
        {
            vascularLevel1Viewer.ShowFullBody();
        }

        currentPartName = "";
        currentSelectedModels.Clear();

        partDetailPanel.SetActive(false);
        partListPanel.SetActive(true);
        GeneratePartList("VascularSystem");
        return;
    }

    // SKELETAL SYSTEM
    if (currentSystem == "SkeletalSystem")
    {
        if (navigationStack.Count > 0)
        {
            string previousPart = navigationStack.Pop();

            ShowPreviousSkeletalPart(previousPart);
            return;
        }

        // Không còn node trước đó: khôi phục toàn bộ hệ xương.
        if (skeletalLevel1Viewer != null)
        {
            skeletalLevel1Viewer.ShowFullBody();
        }

        currentPartName = "";
        currentSelectedModels.Clear();

        partDetailPanel.SetActive(false);
        partListPanel.SetActive(true);
        GeneratePartList("SkeletalSystem");
        return;
    }

    // MUSCULAR SYSTEM
    if (navigationStack.Count > 0)
    {
        string previousPart = navigationStack.Pop();

        AnatomyModelGroup mappedGroup =
            GetMuscularGroup(previousPart);

        if (mappedGroup != null)
        {
            SelectMappedMuscularGroup(
                previousPart,
                mappedGroup
            );

            return;
        }

        if (muscularLevel2ModelMap != null &&
            muscularLevel2ModelMap.ContainsNode(previousPart))
        {
            List<Transform> models =
                muscularLevel2ModelMap.GetModels(previousPart);

            if (models != null && models.Count > 0)
            {
                SelectMappedLevel2Models(
                    previousPart,
                    models
                );

                return;
            }
        }

        return;
    }

    if (muscularLevel1Viewer != null)
    {
        muscularLevel1Viewer.RestoreFullBody();
    }

    RestoreSelectedPart();

    currentPartName = "";
    currentSelectedModels.Clear();

    partDetailPanel.SetActive(false);
    partListPanel.SetActive(true);
}

}

