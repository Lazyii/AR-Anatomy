using System.Collections.Generic;
using UnityEngine;

public class MuscularLevel2ModelMap : MonoBehaviour
{
[Header("Level 1 HeadNeck Group")]
[SerializeField]
private AnatomyModelGroup headNeckGroup;

[SerializeField]
private AnatomyModelGroup trunkGroup;

[Header("Level 1 Left Upper Limb Group")]
[SerializeField]
private AnatomyModelGroup leftUpperLimbGroup;

[Header("Level 1 Right Upper Limb Group")]
[SerializeField]
private AnatomyModelGroup rightUpperLimbGroup;

[Header("Level 1 Left Lower Limb Group")]
[SerializeField] private AnatomyModelGroup leftLowerLimbGroup;

[Header("Level 1 Right Lower Limb Group")]
[SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

private readonly Dictionary<string, List<Transform>> modelMap =
    new Dictionary<string, List<Transform>>();

private bool built;


public void Build()
{
    modelMap.Clear();
    built = false;

    // =====================================
    // HEAD NECK SOURCE
    // =====================================

    if (headNeckGroup == null)
    {
        Debug.LogError(
            "MuscularLevel2ModelMap: Head Neck Group chưa được gán."
        );

        return;
    }

    List<Transform> headNeckModels =
        new List<Transform>();

    foreach (Transform model in headNeckGroup.LinkedModels)
    {
        if (model == null)
            continue;

        if (!headNeckModels.Contains(model))
        {
            headNeckModels.Add(model);
        }
    }

    Debug.Log(
        "HEAD NECK LEVEL 2 SOURCE MODELS = "
        + headNeckModels.Count
    );

    BuildHeadNeckMap(headNeckModels);


    // =====================================
    // TRUNK SOURCE
    // =====================================

    if (trunkGroup != null)
    {
        List<Transform> trunkModels =
            new List<Transform>();

        foreach (Transform model in trunkGroup.LinkedModels)
        {
            if (model == null)
                continue;

            if (!trunkModels.Contains(model))
            {
                trunkModels.Add(model);
            }
        }

        Debug.Log(
            "TRUNK LEVEL 2 SOURCE MODELS = "
            + trunkModels.Count
        );

        BuildTrunkMap(trunkModels);
    }
    else
    {
        Debug.LogError(
            "MuscularLevel2ModelMap: Trunk Group chưa được gán trong Inspector."
        );
    }

    // LEFT UPPER LIMB SOURCE
    if (leftUpperLimbGroup != null)
    {
        List<Transform> leftUpperLimbModels =
            new List<Transform>();

        foreach (Transform model in leftUpperLimbGroup.LinkedModels)
        {
            if (model == null)
                continue;

            if (!leftUpperLimbModels.Contains(model))
                leftUpperLimbModels.Add(model);
        }

        Debug.Log(
            "LEFT UPPER LIMB LEVEL 2 SOURCE MODELS = "
            + leftUpperLimbModels.Count
        );

        BuildLeftUpperLimbMap(leftUpperLimbModels);
    }
    else
    {
        Debug.LogError(
            "MuscularLevel2ModelMap: Left Upper Limb Group chưa được gán trong Inspector."
        );
    }

    // RIGHT UPPER LIMB SOURCE
    if (rightUpperLimbGroup != null)
    {
        List<Transform> rightUpperLimbModels =
            new List<Transform>();

        foreach (Transform model in rightUpperLimbGroup.LinkedModels)
        {
            if (model == null)
                continue;

            if (!rightUpperLimbModels.Contains(model))
                rightUpperLimbModels.Add(model);
        }

        Debug.Log(
            "RIGHT UPPER LIMB LEVEL 2 SOURCE MODELS = "
            + rightUpperLimbModels.Count
        );

        BuildRightUpperLimbMap(rightUpperLimbModels);
    }
    else
    {
        Debug.LogError(
            "MuscularLevel2ModelMap: Right Upper Limb Group chưa được gán trong Inspector."
        );
    }

    // Build LeftLowerLimb map
    if (leftLowerLimbGroup != null)
    {
        List<Transform> leftLowerLimbModels = new List<Transform>();

        foreach (Transform model in leftLowerLimbGroup.LinkedModels)
        {
            if (model != null && !leftLowerLimbModels.Contains(model))
            {
                leftLowerLimbModels.Add(model);
            }
        }

        BuildLeftLowerLimbMap(leftLowerLimbModels);
    }
    else
    {
        Debug.LogWarning(
            "LeftLowerLimbGroup chưa được gán trong Inspector.",
            this
        );
    }

        // Build RightLowerLimb map
    if (rightLowerLimbGroup != null)
    {
        List<Transform> rightLowerLimbModels = new List<Transform>();

        foreach (Transform model in rightLowerLimbGroup.LinkedModels)
        {
            if (model != null && !rightLowerLimbModels.Contains(model))
            {
                rightLowerLimbModels.Add(model);
            }
        }

        BuildRightLowerLimbMap(rightLowerLimbModels);
    }
    else
    {
        Debug.LogWarning(
            "RightLowerLimbGroup chưa được gán trong Inspector.",
            this
        );
    }

    // =====================================
    // VALIDATE BOTH SYSTEMS
    // =====================================

    built = true;

    Debug.Log(
        "MUSCULAR LEVEL 2 MODEL MAP BUILT | Nodes = "
        + modelMap.Count
    );

    ValidateMap();

}



private void BuildHeadNeckMap(
    List<Transform> models)
{
    AddByNames(
        "Frontalis",
        models,
        "Frontalis muscle"
    );

    AddByNames(
        "OrbicularisOculi",
        models,
        "Orbital part of orbicularis oculi",
        "Palpebral part of orbicularis oculi"
    );

    AddByNames(
        "OrbicularisOris",
        models,
        "Orbicularis oris muscle"
    );

    AddByNames(
        "Buccinator",
        models,
        "Bucinator"
    );

    AddByNames(
        "ZygomaticusMajor",
        models,
        "Zygomaticus major muscle"
    );

    AddByNames(
        "ZygomaticusMinor",
        models,
        "Zygomaticus minor muscle"
    );

    AddByNames(
        "Risorius",
        models,
        "Risorius muscle"
    );

    AddByNames(
        "DepressorAnguliOris",
        models,
        "Depressor anguli oris"
    );

    AddByNames(
        "Mentalis",
        models,
        "Mentalis muscle"
    );


    AddByNames(
        "Masseter",
        models,
        "Superficial part of masseter",
        "Deep part of masseter"
    );

    AddByNames(
        "Temporalis",
        models,
        "Temporalis muscle"
    );

    AddByNames(
        "MedialPterygoid",
        models,
        "Medial pterygoid muscle"
    );

    AddByNames(
        "LateralPterygoid",
        models,
        "Superior head of lateral pterygoid muscle",
        "Inferior head of lateral pterygoid muscle"
    );


    AddByNames(
        "Platysma",
        models,
        "Platysma"
    );

    AddByNames(
        "Sternocleidomastoid_Left",
        models,
        "Sternocleidomastoid muscle.l"
    );

    AddByNames(
        "Sternocleidomastoid_Right",
        models,
        "Sternocleidomastoid muscle.r"
    );


    AddByNames(
        "Trapezius_Left",
        models,
        "Ascending part of trapezius muscle.l",
        "Descending part of trapezius muscle.l",
        "Transverse part of trapezius muscle.l"
    );

    AddByNames(
        "Trapezius_Right",
        models,
        "Ascending part of trapezius muscle.r",
        "Descending part of trapezius muscle.r",
        "Transverse part of trapezius muscle.r"
    );


    AddByNames(
        "AnteriorBelly",
        models,
        "Anterior belly of digastric muscle"
    );

    AddByNames(
        "PosteriorBelly",
        models,
        "Posterior belly of digastric muscle"
    );

    AddByNames(
        "Mylohyoid",
        models,
        "Mylohyoid muscle"
    );

    AddByNames(
        "Geniohyoid",
        models,
        "Geniohyoid muscle"
    );

    AddByNames(
        "Stylohyoid",
        models,
        "Stylohyoid muscle"
    );


    AddByNames(
        "Sternohyoid",
        models,
        "Sternohyoid muscle"
    );

    AddByNames(
        "Sternothyroid",
        models,
        "Sternothyroid muscle"
    );

    AddByNames(
        "Thyrohyoid",
        models,
        "Thyrohyoid muscle"
    );

    AddByNames(
        "Omohyoid",
        models,
        "Omohyoid muscle"
    );


    AddByNames(
        "AnteriorScalene",
        models,
        "Scalenus anterior muscle"
    );

    AddByNames(
        "MiddleScalene",
        models,
        "Scalenus medius muscle"
    );

    AddByNames(
        "PosteriorScalene",
        models,
        "Scalenus posterior muscle"
    );


    AddByNames(
        "SpleniusCapitis",
        models,
        "Splenius capitis muscle"
    );

    AddByNames(
        "LongusCapitis",
        models,
        "Longus capitis muscle"
    );

    AddByNames(
        "LongusColli",
        models,
        "Longus colli muscle"
    );

    AddByNames(
        "RectusCapitisAnterior",
        models,
        "Rectus anterior capitis muscle"
    );

    AddByNames(
        "RectusCapitisLateralis",
        models,
        "Rectus lateralis capitis muscle"
    );

    // =====================================================
    // BUILD GROUP MODELS FROM THEIR CHILD NODES
    // =====================================================

    CombineNodes(
        "FacialExpressionMuscles",
        "Frontalis",
        "OrbicularisOculi",
        "OrbicularisOris",
        "Buccinator",
        "ZygomaticusMajor",
        "ZygomaticusMinor",
        "Risorius",
        "DepressorAnguliOris",
        "Mentalis"
    );

    CombineNodes(
        "MasticationMuscles",
        "Masseter",
        "Temporalis",
        "MedialPterygoid",
        "LateralPterygoid"
    );

    CombineNodes(
        "SuperficialNeckMuscles",
        "Platysma",
        "Sternocleidomastoid_Left",
        "Sternocleidomastoid_Right"
    );

    CombineNodes(
        "SuprahyoidMuscles",
        "AnteriorBelly",
        "PosteriorBelly",
        "Mylohyoid",
        "Geniohyoid",
        "Stylohyoid"
    );

    CombineNodes(
        "InfrahyoidMuscles",
        "Sternohyoid",
        "Sternothyroid",
        "Thyrohyoid",
        "Omohyoid"
    );

    CombineNodes(
        "ScaleneMuscles",
        "AnteriorScalene",
        "MiddleScalene",
        "PosteriorScalene"
    );

    CombineNodes(
        "DeepNeckMuscles",
        "SpleniusCapitis",
        "LongusCapitis",
        "LongusColli",
        "RectusCapitisAnterior",
        "RectusCapitisLateralis"
    );

}

private void BuildTrunkMap(
    List<Transform> models
)
{
    // =========================================
    // PECTORAL MUSCLES
    // =========================================
    AddByNames(
        "PectoralisMajor_Left",
        models,
        "(Abdominal part of pectoralis major muscle).l",
        "Clavicular head of pectoralis major muscle.l",
        "Sternocostal head of pectoralis major muscle.l"
    );

    AddByNames(
        "PectoralisMajor_Right",
        models,
        "(Abdominal part of pectoralis major muscle).r",
        "Clavicular head of pectoralis major muscle.r",
        "Sternocostal head of pectoralis major muscle.r"
    );

    AddByNames(
        "PectoralisMinor_Left",
        models,
        "Pectoralis minor muscle.l"
    );

    AddByNames(
        "PectoralisMinor_Right",
        models,
        "Pectoralis minor muscle.r"
    );

    AddByNames(
        "Subclavius_Left",
        models,
        "Subclavius muscle.l"
    );

    AddByNames(
        "Subclavius_Right",
        models,
        "Subclavius muscle.r"
    );

    CombineNodes(
        "PectoralMuscles",
        "PectoralisMajor_Left",
        "PectoralisMajor_Right",
        "PectoralisMinor_Left",
        "PectoralisMinor_Right",
        "Subclavius_Left",
        "Subclavius_Right"
    );


    // =========================================
    // THORACIC WALL MUSCLES
    // =========================================

    AddByNames(
        "SerratusAnterior_Left",
        models,
        "Serratus anterior muscle.l"
    );

    AddByNames(
        "SerratusAnterior_Right",
        models,
        "Serratus anterior muscle.r"
    );

    AddByNames(
        "ExternalIntercostals_Left",
        models,
        "External intercostal muscles.l"
    );

    AddByNames(
        "ExternalIntercostals_Right",
        models,
        "External intercostal muscles.r"
    );

    AddByNames(
        "InternalIntercostals_Left",
        models,
        "Internal intercostal muscles.l"
    );

    AddByNames(
        "InternalIntercostals_Right",
        models,
        "Internal intercostal muscles.r"
    );

    AddByNames(
        "InnermostIntercostals_Left",
        models,
        "Innermost intercostal muscles.l"
    );

    AddByNames(
        "InnermostIntercostals_Right",
        models,
        "Innermost intercostal muscles.r"
    );

    AddByNames(
        "TransversusThoracis_Left",
        models,
        "Transversus thoracis muscle.l"
    );

    AddByNames(
        "TransversusThoracis_Right",
        models,
        "Transversus thoracis muscle.r"
    );

    CombineNodes(
        "ThoracicWallMuscles",
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
    );


    // =========================================
    // DIAPHRAGM
    // =========================================

    AddByNames(
        "Diaphragm",
        models,
        "Diaphragm"
    );


    // =========================================
    // ANTERIOR ABDOMINAL WALL
    // =========================================

    AddByNames(
        "RectusAbdominis_Left",
        models,
        "Rectus abdominis muscle.l"
    );

    AddByNames(
        "RectusAbdominis_Right",
        models,
        "Rectus abdominis muscle.r"
    );

    AddByNames(
        "Pyramidalis_Left",
        models,
        "Pyramidalis muscle.l"
    );

    AddByNames(
        "Pyramidalis_Right",
        models,
        "Pyramidalis muscle.r"
    );

    AddByNames(
        "ExternalOblique_Left",
        models,
        "External abdominal oblique muscle.l"
    );

    AddByNames(
        "ExternalOblique_Right",
        models,
        "External abdominal oblique muscle.r"
    );

    AddByNames(
        "InternalOblique_Left",
        models,
        "Internal abdominal oblique muscle.l"
    );

    AddByNames(
        "InternalOblique_Right",
        models,
        "Internal abdominal oblique muscle.r"
    );

    AddByNames(
        "TransversusAbdominis_Left",
        models,
        "Transversus abdominis muscle.l"
    );

    AddByNames(
        "TransversusAbdominis_Right",
        models,
        "Transversus abdominis muscle.r"
    );

    CombineNodes(
        "AnteriorAbdominalWall",
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
    );


    // =========================================
    // POSTERIOR ABDOMINAL WALL
    // =========================================

    AddByNames(
        "QuadratusLumborum_Left",
        models,
        "Quadratus lumborum muscle.l"
    );

    AddByNames(
        "QuadratusLumborum_Right",
        models,
        "Quadratus lumborum muscle.r"
    );

    CombineNodes(
        "PosteriorAbdominalWall",
        "QuadratusLumborum_Left",
        "QuadratusLumborum_Right"
    );


    // =========================================
    // SUPERFICIAL BACK
    // =========================================

    AddByNames(
        "Trapezius_Left",
        models,
        "Descending part of trapezius muscle.l",
        "Transverse part of trapezius muscle.l",
        "Ascending part of trapezius muscle.l"
    );

    AddByNames(
        "Trapezius_Right",
        models,
        "Descending part of trapezius muscle.r",
        "Transverse part of trapezius muscle.r",
        "Ascending part of trapezius muscle.r"
    );

    AddByNames(
        "LatissimusDorsi_Left",
        models,
        "Latissimus dorsi muscle.l"
    );

    AddByNames(
        "LatissimusDorsi_Right",
        models,
        "Latissimus dorsi muscle.r"
    );

    AddByNames(
        "RhomboidMajor_Left",
        models,
        "Rhomboid major muscle.l"
    );

    AddByNames(
        "RhomboidMajor_Right",
        models,
        "Rhomboid major muscle.r"
    );

    AddByNames(
        "RhomboidMinor_Left",
        models,
        "Rhomboid minor muscle.l"
    );

    AddByNames(
        "RhomboidMinor_Right",
        models,
        "Rhomboid minor muscle.r"
    );

    AddByNames(
        "LevatorScapulae_Left",
        models,
        "Levator scapulae.l"
    );

    AddByNames(
        "LevatorScapulae_Right",
        models,
        "Levator scapulae.r"
    );

    CombineNodes(
        "SuperficialBackMuscles",
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
    );


    // =========================================
    // INTERMEDIATE BACK
    // =========================================

    AddByNames(
        "SerratusPosteriorSuperior_Left",
        models,
        "Serratus posterior superior muscle.l"
    );

    AddByNames(
        "SerratusPosteriorSuperior_Right",
        models,
        "Serratus posterior superior muscle.r"
    );

    AddByNames(
        "SerratusPosteriorInferior_Left",
        models,
        "Serratus posterior inferior muscle.l"
    );

    AddByNames(
        "SerratusPosteriorInferior_Right",
        models,
        "Serratus posterior inferior muscle.r"
    );

    CombineNodes(
        "IntermediateBackMuscles",
        "SerratusPosteriorSuperior_Left",
        "SerratusPosteriorSuperior_Right",
        "SerratusPosteriorInferior_Left",
        "SerratusPosteriorInferior_Right"
    );


    // =========================================
    // ERECTOR SPINAE
    // =========================================

    AddByNames(
        "Iliocostalis_Left",
        models,
        "Iliocostalis colli muscle",
        "Iliocostalis lumborum muscle.l",
        "Iliocostalis thoracis muscle.l"
    );

    AddByNames(
        "Iliocostalis_Right",
        models,
        "Iliocostalis colli muscle.r",
        "Iliocostalis lumborum muscle.r",
        "Iliocostalis thoracis muscle.r"
    );

    AddByNames(
        "Longissimus_Left",
        models,
        "Longissimus capitis muscle.l",
        "Longissimus colli muscle.l",
        "Longissimus thoracis muscle.l"
    );

    AddByNames(
        "Longissimus_Right",
        models,
        "Longissimus capitis muscle.r",
        "Longissimus colli muscle.r",
        "Longissimus thoracis muscle.r"
    );

    AddByNames(
        "Spinalis_Left",
        models,
        "Spinalis capitis muscle.l",
        "Spinalis colli muscle.l",
        "Spinalis thoracis muscle.l"
    );

    AddByNames(
        "Spinalis_Right",
        models,
        "Spinalis capitis muscle.r",
        "Spinalis colli muscle.r",
        "Spinalis thoracis muscle.r"
    );

    CombineNodes(
        "ErectorSpinae_Left",
        "Iliocostalis_Left",
        "Longissimus_Left",
        "Spinalis_Left"
    );

    CombineNodes(
        "ErectorSpinae_Right",
        "Iliocostalis_Right",
        "Longissimus_Right",
        "Spinalis_Right"
    );


    // =========================================
    // TRANSVERSOSPINALES
    // =========================================

    AddByNames(
        "Semispinalis_Left",
        models,
        "Semispinalis colli muscle.l",
        "Semispinalis thoracis muscle.l"
    );

    AddByNames(
        "Semispinalis_Right",
        models,
        "Semispinalis colli muscle.r",
        "Semispinalis thoracis muscle.r"
    );

    AddByNames(
        "Multifidus_Left",
        models,
        "Multifidus colli muscle.l",
        "Multifidus lumborum muscle.l",
        "Multifidus thoracis muscle.l"
    );

    AddByNames(
        "Multifidus_Right",
        models,
        "Multifidus colli muscle.r",
        "Multifidus lumborum muscle.r",
        "Multifidus thoracis muscle.r"
    );

    AddByNames(
        "Rotatores_Left",
        models,
        "Rotatores.l"
    );

    AddByNames(
        "Rotatores_Right",
        models,
        "Rotatores.r"
    );

    CombineNodes(
        "Transversospinales_Left",
        "Semispinalis_Left",
        "Multifidus_Left",
        "Rotatores_Left"
    );

    CombineNodes(
        "Transversospinales_Right",
        "Semispinalis_Right",
        "Multifidus_Right",
        "Rotatores_Right"
    );


    // =========================================
    // DEEP BACK
    // =========================================

    CombineNodes(
        "DeepBackMuscles",
        "ErectorSpinae_Left",
        "ErectorSpinae_Right",
        "Transversospinales_Left",
        "Transversospinales_Right"
    );
}

private void BuildLeftUpperLimbMap(List<Transform> models)
{
    // SHOULDER
    AddByNames(
    "Deltoid_Left", models,
    "Acromial part of deltoid muscle.l",
    "Clavicular part of deltoid muscle.l",
    "Scapular spinal part of deltoid muscle.l"
    );

    AddByNames(
        "Supraspinatus_Left", models,
        "Supraspinatus muscle.l"
    );

    AddByNames(
        "Infraspinatus_Left", models,
        "Infraspinatus muscle.l"
    );

    AddByNames(
        "TeresMinor_Left", models,
        "Teres minor muscle.l"
    );

    AddByNames(
        "TeresMajor_Left", models,
        "Teres major muscle.l"
    );

    AddByNames(
        "Subscapularis_Left", models,
        "Subscapularis muscle.l"
    );

    // ANTERIOR ARM
    AddByNames(
        "BicepsBrachii_Left", models,
        "Long head of biceps brachii.l",
        "Short head of biceps brachii.l"
    );

    AddByNames(
        "Brachialis_Left", models,
        "Brachialis muscle.l"
    );

    AddByNames(
        "Coracobrachialis_Left", models,
        "Coracobrachialis muscle.l"
    );

    // POSTERIOR ARM
    AddByNames(
        "TricepsBrachii_Left", models,
        "Long head of triceps brachii.l",
        "Lateral head of triceps brachii.l",
        "Medial head of triceps brachii.l"
    );

    AddByNames(
        "Anconeus_Left", models,
        "Anconeus muscle.l"
    );

    // SUPERFICIAL ANTERIOR FOREARM
    AddByNames(
        "PronatorTeres_Left", models,
        "Superficial head of pronator teres.l",
        "Deep head of pronator teres.l"
    );

    AddByNames(
        "FlexorCarpiRadialis_Left", models,
        "Flexor carpi radialis.l"
    );

    AddByNames(
        "PalmarisLongus_Left", models,
        "Palmaris longus muscle.l"
    );

    AddByNames(
        "FlexorCarpiUlnaris_Left", models,
        "Humeral head of flexor carpi ulnaris.l",
        "Ulnar head of flexor carpi ulnaris.l"
    );

    // INTERMEDIATE ANTERIOR FOREARM
    AddByNames(
        "FlexorDigitorumSuperficialis_Left", models,
        "Humero-ulnar head of flexor digitorum superficialis.l",
        "Radial head of flexor digitorum superficialis.l"
    );

    // DEEP ANTERIOR FOREARM
    AddByNames(
        "FlexorDigitorumProfundus_Left", models,
        "Flexor digitorum profundus.l"
    );

    AddByNames(
        "FlexorPollicisLongus_Left", models,
        "Flexor pollicis longus.l"
    );

    AddByNames(
        "PronatorQuadratus_Left", models,
        "Pronator quadratus.l"
    );

    // SUPERFICIAL POSTERIOR FOREARM
    AddByNames(
        "Brachioradialis_Left", models,
        "Brachioradialis muscle.l"
    );

    AddByNames(
        "ExtensorCarpiRadialisLongus_Left", models,
        "Extensor carpi radialis longus.l"
    );

    AddByNames(
        "ExtensorCarpiRadialisBrevis_Left", models,
        "Extensor carpi radialis brevis.l"
    );

    AddByNames(
        "ExtensorDigitorum_Left", models,
        "Extensor digitorum.l"
    );

    AddByNames(
        "ExtensorDigitiMinimi_Left", models,
        "Extensor digiti minimi.l"
    );

    AddByNames(
        "ExtensorCarpiUlnaris_Left", models,
        "Humeral head of extensor carpi ulnaris.l",
        "Ulnar head of extensor carpi ulnaris.l"
    );

    // DEEP POSTERIOR FOREARM
    AddByNames(
        "Supinator_Left", models,
        "Supinator.l"
    );

    AddByNames(
        "AbductorPollicisLongus_Left", models,
        "Abductor pollicis longus.l"
    );

    AddByNames(
        "ExtensorPollicisBrevis_Left", models,
        "Extensor pollicis brevis.l"
    );

    AddByNames(
        "ExtensorPollicisLongus_Left", models,
        "Extensor pollicis longus.l"
    );

    AddByNames(
        "ExtensorIndicis_Left", models,
        "Extensor indicis.l"
    );

    // THENAR MUSCLES
    AddByNames(
        "AbductorPollicisBrevis_Left", models,
        "Abductor pollicis brevis.l"
    );

    AddByNames(
        "FlexorPollicisBrevis_Left", models,
        "Superficial head of flexor pollicis brevis.l",
        "Deep head of flexor pollicis brevis.l"
    );

    AddByNames(
        "OpponensPollicis_Left", models,
        "Opponens pollicis muscle.l"
    );

    AddByNames(
        "AdductorPollicis_Left", models,
        "Oblique head of adductor pollicis.l",
        "Transverse head of adductor pollicis.l"
    );

    // HYPOTHENAR MUSCLES
    AddByNames(
        "AbductorDigitiMinimi_Left", models,
        "Abductor digiti minimi of hand.l"
    );

    AddByNames(
        "FlexorDigitiMinimiBrevis_Left", models,
        "Flexor digiti minimi of hand.l"
    );

    AddByNames(
        "OpponensDigitiMinimi_Left", models,
        "Opponens digiti minimi muscle of hand.l"
    );

    // HAND MUSCLE GROUPS: ONE MODEL PER GROUP
    AddByNames(
        "Lumbricals_Left", models,
        "Lumbrical muscles of hand.l"
    );

    AddByNames(
        "PalmarInterossei_Left", models,
        "Palmar interossei muscles.l"
    );

    AddByNames(
        "DorsalInterossei_Left", models,
        "Dorsal interossei muscles of hand.l"
    );

    // COMBINE SHOULDER
    CombineNodes(
        "ShoulderMuscles",
        "Deltoid_Left",
        "Supraspinatus_Left",
        "Infraspinatus_Left",
        "TeresMinor_Left",
        "TeresMajor_Left",
        "Subscapularis_Left"
    );

    // COMBINE ANTERIOR ARM
    CombineNodes(
        "AnteriorArmMuscles",
        "BicepsBrachii_Left",
        "Brachialis_Left",
        "Coracobrachialis_Left"
    );

    // COMBINE POSTERIOR ARM
    CombineNodes(
        "PosteriorArmMuscles",
        "TricepsBrachii_Left",
        "Anconeus_Left"
    );

    // COMBINE ANTERIOR FOREARM SUBGROUPS
    CombineNodes(
        "SuperficialAnteriorForearm_Left",
        "PronatorTeres_Left",
        "FlexorCarpiRadialis_Left",
        "PalmarisLongus_Left",
        "FlexorCarpiUlnaris_Left"
    );

    CombineNodes(
        "IntermediateAnteriorForearm_Left",
        "FlexorDigitorumSuperficialis_Left"
    );

    CombineNodes(
        "DeepAnteriorForearm_Left",
        "FlexorDigitorumProfundus_Left",
        "FlexorPollicisLongus_Left",
        "PronatorQuadratus_Left"
    );

    CombineNodes(
        "AnteriorForearmMuscles",
        "SuperficialAnteriorForearm_Left",
        "IntermediateAnteriorForearm_Left",
        "DeepAnteriorForearm_Left"
    );

    // COMBINE POSTERIOR FOREARM SUBGROUPS
    CombineNodes(
        "SuperficialPosteriorForearm_Left",
        "Brachioradialis_Left",
        "ExtensorCarpiRadialisLongus_Left",
        "ExtensorCarpiRadialisBrevis_Left",
        "ExtensorDigitorum_Left",
        "ExtensorDigitiMinimi_Left",
        "ExtensorCarpiUlnaris_Left"
    );

    CombineNodes(
        "DeepPosteriorForearm_Left",
        "Supinator_Left",
        "AbductorPollicisLongus_Left",
        "ExtensorPollicisBrevis_Left",
        "ExtensorPollicisLongus_Left",
        "ExtensorIndicis_Left"
    );

    CombineNodes(
        "PosteriorForearmMuscles",
        "SuperficialPosteriorForearm_Left",
        "DeepPosteriorForearm_Left"
    );

    // COMBINE HAND GROUPS
    CombineNodes(
        "ThenarMuscles_Left",
        "AbductorPollicisBrevis_Left",
        "FlexorPollicisBrevis_Left",
        "OpponensPollicis_Left",
        "AdductorPollicis_Left"
    );

    CombineNodes(
        "HypothenarMuscles_Left",
        "AbductorDigitiMinimi_Left",
        "FlexorDigitiMinimiBrevis_Left",
        "OpponensDigitiMinimi_Left"
    );

    CombineNodes(
        "HandMuscles",
        "ThenarMuscles_Left",
        "HypothenarMuscles_Left",
        "Lumbricals_Left",
        "PalmarInterossei_Left",
        "DorsalInterossei_Left"
    );

    Debug.Log(
        "LEFT UPPER LIMB MAP BUILT."
    );

}

private void BuildRightUpperLimbMap(List<Transform> models)
{
    // SHOULDER
    AddByNames(
    "Deltoid_Right", models,
    "Acromial part of deltoid muscle.r",
    "Clavicular part of deltoid muscle.r",
    "Scapular spinal part of deltoid muscle.r"
    );

    AddByNames("Supraspinatus_Right", models,
        "Supraspinatus muscle.r");

    AddByNames("Infraspinatus_Right", models,
        "Infraspinatus muscle.r");

    AddByNames("TeresMinor_Right", models,
        "Teres minor muscle.r");

    AddByNames("TeresMajor_Right", models,
        "Teres major muscle.r");

    AddByNames("Subscapularis_Right", models,
        "Subscapularis muscle.r");

    // ANTERIOR ARM
    AddByNames(
        "BicepsBrachii_Right", models,
        "Long head of biceps brachii.r",
        "Short head of biceps brachii.r"
    );

    AddByNames("Brachialis_Right", models,
        "Brachialis muscle.r");

    AddByNames("Coracobrachialis_Right", models,
        "Coracobrachialis muscle.r");

    // POSTERIOR ARM
    AddByNames(
        "TricepsBrachii_Right", models,
        "Long head of triceps brachii.r",
        "Lateral head of triceps brachii.r",
        "Medial head of triceps brachii.r"
    );

    AddByNames("Anconeus_Right", models,
        "Anconeus muscle.r");

    // SUPERFICIAL ANTERIOR FOREARM
    AddByNames(
        "PronatorTeres_Right", models,
        "Superficial head of pronator teres.r",
        "Deep head of pronator teres.r"
    );

    AddByNames("FlexorCarpiRadialis_Right", models,
        "Flexor carpi radialis.r");

    AddByNames("PalmarisLongus_Right", models,
        "Palmaris longus muscle.r");

    AddByNames(
        "FlexorCarpiUlnaris_Right", models,
        "Humeral head of flexor carpi ulnaris.r",
        "Ulnar head of flexor carpi ulnaris.r"
    );

    // INTERMEDIATE ANTERIOR FOREARM
    AddByNames(
        "FlexorDigitorumSuperficialis_Right", models,
        "Humero-ulnar head of flexor digitorum superficialis.r",
        "Radial head of flexor digitorum superficialis.r"
    );

    // DEEP ANTERIOR FOREARM
    AddByNames("FlexorDigitorumProfundus_Right", models,
        "Flexor digitorum profundus.r");

    AddByNames("FlexorPollicisLongus_Right", models,
        "Flexor pollicis longus.r");

    AddByNames("PronatorQuadratus_Right", models,
        "Pronator quadratus.r");

    // SUPERFICIAL POSTERIOR FOREARM
    AddByNames("Brachioradialis_Right", models,
        "Brachioradialis muscle.r");

    AddByNames("ExtensorCarpiRadialisLongus_Right", models,
        "Extensor carpi radialis longus.r");

    AddByNames("ExtensorCarpiRadialisBrevis_Right", models,
        "Extensor carpi radialis brevis.r");

    AddByNames("ExtensorDigitorum_Right", models,
        "Extensor digitorum.r");

    AddByNames("ExtensorDigitiMinimi_Right", models,
        "Extensor digiti minimi.r");

    AddByNames(
        "ExtensorCarpiUlnaris_Right", models,
        "Humeral head of extensor carpi ulnaris.r",
        "Ulnar head of extensor carpi ulnaris.r"
    );

    // DEEP POSTERIOR FOREARM
    AddByNames("Supinator_Right", models,
        "Supinator.r");

    AddByNames("AbductorPollicisLongus_Right", models,
        "Abductor pollicis longus.r");

    AddByNames("ExtensorPollicisBrevis_Right", models,
        "Extensor pollicis brevis.r");

    AddByNames("ExtensorPollicisLongus_Right", models,
        "Extensor pollicis longus.r");

    AddByNames("ExtensorIndicis_Right", models,
        "Extensor indicis.r");

    // THENAR
    AddByNames("AbductorPollicisBrevis_Right", models,
        "Abductor pollicis brevis.r");

    AddByNames(
        "FlexorPollicisBrevis_Right", models,
        "Superficial head of flexor pollicis brevis.r",
        "Deep head of flexor pollicis brevis.r"
    );

    AddByNames("OpponensPollicis_Right", models,
        "Opponens pollicis muscle.r");

    AddByNames(
        "AdductorPollicis_Right", models,
        "Oblique head of adductor pollicis.r",
        "Transverse head of adductor pollicis.r"
    );

    // HYPOTHENAR
    AddByNames("AbductorDigitiMinimi_Right", models,
        "Abductor digiti minimi of hand.r");

    AddByNames("FlexorDigitiMinimiBrevis_Right", models,
        "Flexor digiti minimi of hand.r");

    AddByNames("OpponensDigitiMinimi_Right", models,
        "Opponens digiti minimi muscle of hand.r");

    // HAND GROUPS: ONE MESH PER GROUP
    AddByNames("Lumbricals_Right", models,
        "Lumbrical muscles of hand.r");

    AddByNames("PalmarInterossei_Right", models,
        "Palmar interossei muscles.r");

    AddByNames("DorsalInterossei_Right", models,
        "Dorsal interossei muscles of hand.r");

    // COMBINE SHOULDER
    CombineNodes(
        "ShoulderMuscles_Right",
        "Deltoid_Right",
        "Supraspinatus_Right",
        "Infraspinatus_Right",
        "TeresMinor_Right",
        "TeresMajor_Right",
        "Subscapularis_Right"
    );

    // COMBINE ARMS
    CombineNodes(
        "AnteriorArmMuscles_Right",
        "BicepsBrachii_Right",
        "Brachialis_Right",
        "Coracobrachialis_Right"
    );

    CombineNodes(
        "PosteriorArmMuscles_Right",
        "TricepsBrachii_Right",
        "Anconeus_Right"
    );

    // COMBINE ANTERIOR FOREARM
    CombineNodes(
        "SuperficialAnteriorForearm_Right",
        "PronatorTeres_Right",
        "FlexorCarpiRadialis_Right",
        "PalmarisLongus_Right",
        "FlexorCarpiUlnaris_Right"
    );

    CombineNodes(
        "IntermediateAnteriorForearm_Right",
        "FlexorDigitorumSuperficialis_Right"
    );

    CombineNodes(
        "DeepAnteriorForearm_Right",
        "FlexorDigitorumProfundus_Right",
        "FlexorPollicisLongus_Right",
        "PronatorQuadratus_Right"
    );

    CombineNodes(
        "AnteriorForearmMuscles_Right",
        "SuperficialAnteriorForearm_Right",
        "IntermediateAnteriorForearm_Right",
        "DeepAnteriorForearm_Right"
    );

    // COMBINE POSTERIOR FOREARM
    CombineNodes(
        "SuperficialPosteriorForearm_Right",
        "Brachioradialis_Right",
        "ExtensorCarpiRadialisLongus_Right",
        "ExtensorCarpiRadialisBrevis_Right",
        "ExtensorDigitorum_Right",
        "ExtensorDigitiMinimi_Right",
        "ExtensorCarpiUlnaris_Right"
    );

    CombineNodes(
        "DeepPosteriorForearm_Right",
        "Supinator_Right",
        "AbductorPollicisLongus_Right",
        "ExtensorPollicisBrevis_Right",
        "ExtensorPollicisLongus_Right",
        "ExtensorIndicis_Right"
    );

    CombineNodes(
        "PosteriorForearmMuscles_Right",
        "SuperficialPosteriorForearm_Right",
        "DeepPosteriorForearm_Right"
    );

    // COMBINE HAND
    CombineNodes(
        "ThenarMuscles_Right",
        "AbductorPollicisBrevis_Right",
        "FlexorPollicisBrevis_Right",
        "OpponensPollicis_Right",
        "AdductorPollicis_Right"
    );

    CombineNodes(
        "HypothenarMuscles_Right",
        "AbductorDigitiMinimi_Right",
        "FlexorDigitiMinimiBrevis_Right",
        "OpponensDigitiMinimi_Right"
    );

    CombineNodes(
        "HandMuscles_Right",
        "ThenarMuscles_Right",
        "HypothenarMuscles_Right",
        "Lumbricals_Right",
        "PalmarInterossei_Right",
        "DorsalInterossei_Right"
    );

    Debug.Log("RIGHT UPPER LIMB MAP BUILT.");

}

private void BuildLeftLowerLimbMap(List<Transform> models)
{
    // =========================================
    // GLUTEAL REGION
    // =========================================

    AddByNames("GluteusMaximus_Left", models,
        "Gluteus maximus muscle.l");

    AddByNames("GluteusMedius_Left", models,
        "Gluteus medius muscle.l");

    AddByNames("GluteusMinimus_Left", models,
        "Gluteus minimus muscle.l");

    AddByNames("Piriformis_Left", models,
        "Piriformis muscle.l");

    AddByNames("SuperiorGemellus_Left", models,
        "Superior gemellus muscle.l");

    AddByNames("ObturatorInternus_Left", models,
        "Obturator internus.l");

    AddByNames("InferiorGemellus_Left", models,
        "Inferior gemellus muscle.l");

    AddByNames("QuadratusFemoris_Left", models,
        "Quadratus femoris muscle.l");


    // =========================================
    // ANTERIOR THIGH / ILIOPSOAS
    // =========================================

    AddByNames("Iliacus_Left", models,
        "Iliacus muscle.l");

    AddByNames("PsoasMajor_Left", models,
        "Psoas major.l");

    AddByNames("Sartorius_Left", models,
        "Sartorius muscle.l");

    AddByNames("RectusFemoris_Left", models,
        "Rectus femoris muscle.l");

    AddByNames("VastusLateralis_Left", models,
        "Vastus lateralis muscle.l");

    AddByNames("VastusMedialis_Left", models,
        "Vastus medialis muscle.l");

    AddByNames("VastusIntermedius_Left", models,
        "Vastus intermedius muscle.l");


    // =========================================
    // MEDIAL THIGH
    // =========================================

    AddByNames("Pectineus_Left", models,
        "Pectineus muscle.l");

    AddByNames("AdductorLongus_Left", models,
        "Adductor longus.l");

    AddByNames("AdductorBrevis_Left", models,
        "Adductor brevis.l");

    // Adductor minimus là transform con của Adductor magnus.
    // Chỉ map transform cha để giữ nguyên cấu trúc cha-con.
    AddByNames("AdductorMagnus_Left", models,
        "Adductor magnus.l");

    AddByNames("Gracilis_Left", models,
        "Gracilis muscle.l");

    AddByNames("ObturatorExternus_Left", models,
        "Obturator externus.l");


    // =========================================
    // POSTERIOR THIGH
    // =========================================

    AddByNames("BicepsFemoris_Left", models,
        "Long head of biceps femoris.l",
        "Short head of biceps femoris.l");

    AddByNames("Semitendinosus_Left", models,
        "Semitendinosus muscle.l");

    AddByNames("Semimembranosus_Left", models,
        "Semimembranosus muscle.l");


    // =========================================
    // ANTERIOR LEG
    // =========================================

    AddByNames("TibialisAnterior_Left", models,
        "Tibialis anterior muscle.l");

    AddByNames("ExtensorHallucisLongus_Left", models,
        "Extensor hallucis longus.l");

    AddByNames("ExtensorDigitorumLongus_Left", models,
        "Extensor digitorum longus.l");

    AddByNames("FibularisTertius_Left", models,
        "Fibularis tertius muscle.l");


    // =========================================
    // LATERAL LEG
    // =========================================

    AddByNames("FibularisLongus_Left", models,
        "Fibularis longus muscle.l");

    AddByNames("FibularisBrevis_Left", models,
        "Fibularis brevis muscle.l");


    // =========================================
    // SUPERFICIAL POSTERIOR LEG
    // =========================================

    AddByNames("Gastrocnemius_Left", models,
        "Lateral head of gastrocnemius.l",
        "Medial head of gastrocnemius.l");

    AddByNames("Soleus_Left", models,
        "Soleus muscle.l");

    AddByNames("Plantaris_Left", models,
        "Plantaris muscle.l");


    // =========================================
    // DEEP POSTERIOR LEG
    // =========================================

    AddByNames("Popliteus_Left", models,
        "Popliteus muscle.l");

    AddByNames("TibialisPosterior_Left", models,
        "Tibialis posterior muscle.l");

    AddByNames("FlexorDigitorumLongus_Left", models,
        "Flexor digitorum longus.l");

    AddByNames("FlexorHallucisLongus_Left", models,
        "Flexor hallucis longus.l");


    // =========================================
    // DORSAL FOOT
    // =========================================

    AddByNames("ExtensorDigitorumBrevis_Left", models,
        "Extensor digitorum brevis.l");

    AddByNames("ExtensorHallucisBrevis_Left", models,
        "Extensor hallucis brevis.l");


    // =========================================
    // PLANTAR FOOT - LAYER 1
    // =========================================

    AddByNames("AbductorHallucis_Left", models,
        "Abductor hallucis.l");

    AddByNames("FlexorDigitorumBrevis_Left", models,
        "Flexor digitorum brevis.l");

    AddByNames("AbductorDigitiMinimi_Left", models,
        "Abductor digiti minimi of foot.l");


    // =========================================
    // PLANTAR FOOT - LAYER 2
    // =========================================

    AddByNames("QuadratusPlantae_Left", models,
        "Quadratus plantae muscle.l");

    AddByNames("Lumbricals_Left", models,
        "Lumbrical muscles of foot.l");


    // =========================================
    // PLANTAR FOOT - LAYER 3
    // =========================================

    AddByNames("FlexorHallucisBrevis_Left", models,
        "Lateral head of flexor hallucis brevis.l",
        "Medial head of flexor hallucis brevis.l");

    AddByNames("AdductorHallucis_Left", models,
        "Oblique head of adductor hallucis.l",
        "Transverse head of adductor hallucis.l");

    AddByNames("FlexorDigitiMinimiBrevis_Left", models,
        "Flexor digiti minimi of foot.l");

    AddByNames("OpponensDigitiMinimi_Left", models,
        "(Opponens digiti minimi muscle of foot).l");


    // =========================================
    // PLANTAR FOOT - LAYER 4
    // =========================================

    AddByNames("PlantarInterossei_Left", models,
        "Plantar interossei muscles.l");

    AddByNames("DorsalInterossei_Left", models,
        "Dorsal interossei muscles of foot.l");


    // =========================================
    // COMBINE PARENT NODES
    // =========================================

    CombineNodes("SuperficialGlutealMuscles",
        "GluteusMaximus_Left",
        "GluteusMedius_Left",
        "GluteusMinimus_Left");

    CombineNodes("DeepGlutealMuscles",
        "Piriformis_Left",
        "SuperiorGemellus_Left",
        "ObturatorInternus_Left",
        "InferiorGemellus_Left",
        "QuadratusFemoris_Left");

    CombineNodes("GlutealRegion",
        "SuperficialGlutealMuscles",
        "DeepGlutealMuscles");

    CombineNodes("IliopsoasGroup",
        "Iliacus_Left",
        "PsoasMajor_Left");

    CombineNodes("QuadricepsFemoris",
        "RectusFemoris_Left",
        "VastusLateralis_Left",
        "VastusMedialis_Left",
        "VastusIntermedius_Left");

    CombineNodes("AnteriorCompartment_Thigh",
        "IliopsoasGroup",
        "Sartorius_Left",
        "QuadricepsFemoris");

    CombineNodes("MedialCompartment_Thigh",
        "Pectineus_Left",
        "AdductorLongus_Left",
        "AdductorBrevis_Left",
        "AdductorMagnus_Left",
        "Gracilis_Left",
        "ObturatorExternus_Left");

    CombineNodes("PosteriorCompartment_Thigh",
        "BicepsFemoris_Left",
        "Semitendinosus_Left",
        "Semimembranosus_Left");

    CombineNodes("ThighRegion",
        "AnteriorCompartment_Thigh",
        "MedialCompartment_Thigh",
        "PosteriorCompartment_Thigh");

    CombineNodes("AnteriorCompartment_Leg",
        "TibialisAnterior_Left",
        "ExtensorHallucisLongus_Left",
        "ExtensorDigitorumLongus_Left",
        "FibularisTertius_Left");

    CombineNodes("LateralCompartment_Leg",
        "FibularisLongus_Left",
        "FibularisBrevis_Left");

    CombineNodes("SuperficialPosteriorGroup",
        "Gastrocnemius_Left",
        "Soleus_Left",
        "Plantaris_Left");

    CombineNodes("DeepPosteriorGroup",
        "Popliteus_Left",
        "TibialisPosterior_Left",
        "FlexorDigitorumLongus_Left",
        "FlexorHallucisLongus_Left");

    CombineNodes("PosteriorCompartment_Leg",
        "SuperficialPosteriorGroup",
        "DeepPosteriorGroup");

    CombineNodes("LegRegion",
        "AnteriorCompartment_Leg",
        "LateralCompartment_Leg",
        "PosteriorCompartment_Leg");

    CombineNodes("DorsalFootMuscles",
        "ExtensorDigitorumBrevis_Left",
        "ExtensorHallucisBrevis_Left");

    CombineNodes("Layer1",
        "AbductorHallucis_Left",
        "FlexorDigitorumBrevis_Left",
        "AbductorDigitiMinimi_Left");

    CombineNodes("Layer2",
        "QuadratusPlantae_Left",
        "Lumbricals_Left");

    CombineNodes("Layer3",
        "FlexorHallucisBrevis_Left",
        "AdductorHallucis_Left",
        "FlexorDigitiMinimiBrevis_Left",
        "OpponensDigitiMinimi_Left");

    CombineNodes("Layer4",
        "PlantarInterossei_Left",
        "DorsalInterossei_Left");

    CombineNodes("PlantarFootMuscles",
        "Layer1",
        "Layer2",
        "Layer3",
        "Layer4");

    CombineNodes("FootRegion",
        "DorsalFootMuscles",
        "PlantarFootMuscles");

    CombineNodes("LeftLowerLimb",
        "GlutealRegion",
        "ThighRegion",
        "LegRegion",
        "FootRegion");
}


private void BuildRightLowerLimbMap(List<Transform> models)
{
    // GLUTEAL REGION
    AddByNames("GluteusMaximus_Right", models,
        "Gluteus maximus muscle.r");
    AddByNames("GluteusMedius_Right", models,
        "Gluteus medius muscle.r");
    AddByNames("GluteusMinimus_Right", models,
        "Gluteus minimus muscle.r");
    AddByNames("Piriformis_Right", models,
        "Piriformis muscle.r");
    AddByNames("SuperiorGemellus_Right", models,
        "Superior gemellus muscle.r");
    AddByNames("ObturatorInternus_Right", models,
        "Obturator internus.r");
    AddByNames("InferiorGemellus_Right", models,
        "Inferior gemellus muscle.r");
    AddByNames("QuadratusFemoris_Right", models,
        "Quadratus femoris muscle.r");

    // ANTERIOR THIGH / ILIOPSOAS
    AddByNames("Iliacus_Right", models,
        "Iliacus muscle.r");
    AddByNames("PsoasMajor_Right", models,
        "Psoas major.r");
    AddByNames("Sartorius_Right", models,
        "Sartorius muscle.r");
    AddByNames("RectusFemoris_Right", models,
        "Rectus femoris muscle.r");
    AddByNames("VastusLateralis_Right", models,
        "Vastus lateralis muscle.r");
    AddByNames("VastusMedialis_Right", models,
        "Vastus medialis muscle.r");
    AddByNames("VastusIntermedius_Right", models,
        "Vastus intermedius muscle.r");

    // MEDIAL THIGH
    AddByNames("Pectineus_Right", models,
        "Pectineus muscle.r");
    AddByNames("AdductorLongus_Right", models,
        "Adductor longus.r");
    AddByNames("AdductorBrevis_Right", models,
        "Adductor brevis.r");

    // Keep Adductor minimus nested under Adductor magnus.
    AddByNames("AdductorMagnus_Right", models,
        "Adductor magnus.r");

    AddByNames("Gracilis_Right", models,
        "Gracilis muscle.r");
    AddByNames("ObturatorExternus_Right", models,
        "Obturator externus.r");

    // POSTERIOR THIGH
    AddByNames("BicepsFemoris_Right", models,
        "Long head of biceps femoris.r",
        "Short head of biceps femoris.r");
    AddByNames("Semitendinosus_Right", models,
        "Semitendinosus muscle.r");
    AddByNames("Semimembranosus_Right", models,
        "Semimembranosus muscle.r");

    // ANTERIOR LEG
    AddByNames("TibialisAnterior_Right", models,
        "Tibialis anterior muscle.r");
    AddByNames("ExtensorHallucisLongus_Right", models,
        "Extensor hallucis longus.r");
    AddByNames("ExtensorDigitorumLongus_Right", models,
        "Extensor digitorum longus.r");
    AddByNames("FibularisTertius_Right", models,
        "Fibularis tertius muscle.r");

    // LATERAL LEG
    AddByNames("FibularisLongus_Right", models,
        "Fibularis longus muscle.r");
    AddByNames("FibularisBrevis_Right", models,
        "Fibularis brevis muscle.r");

    // SUPERFICIAL POSTERIOR LEG
    AddByNames("Gastrocnemius_Right", models,
        "Lateral head of gastrocnemius.r",
        "Medial head of gastrocnemius.r");
    AddByNames("Soleus_Right", models,
        "Soleus muscle.r");
    AddByNames("Plantaris_Right", models,
        "Plantaris muscle.r");

    // DEEP POSTERIOR LEG
    AddByNames("Popliteus_Right", models,
        "Popliteus muscle.r");
    AddByNames("TibialisPosterior_Right", models,
        "Tibialis posterior muscle.r");
    AddByNames("FlexorDigitorumLongus_Right", models,
        "Flexor digitorum longus.r");
    AddByNames("FlexorHallucisLongus_Right", models,
        "Flexor hallucis longus.r");

    // DORSAL FOOT
    AddByNames("ExtensorDigitorumBrevis_Right", models,
        "Extensor digitorum brevis.r");
    AddByNames("ExtensorHallucisBrevis_Right", models,
        "Extensor hallucis brevis.r");

    // PLANTAR FOOT - LAYER 1
    AddByNames("AbductorHallucis_Right", models,
        "Abductor hallucis.r");
    AddByNames("FlexorDigitorumBrevis_Right", models,
        "Flexor digitorum brevis.r");
    AddByNames("AbductorDigitiMinimi_Right", models,
        "Abductor digiti minimi of foot.r");

    // PLANTAR FOOT - LAYER 2
    AddByNames("QuadratusPlantae_Right", models,
        "Quadratus plantae muscle.r");
    AddByNames("Lumbricals_Right", models,
        "Lumbrical muscles of foot.r");

    // PLANTAR FOOT - LAYER 3
    AddByNames("FlexorHallucisBrevis_Right", models,
        "Lateral head of flexor hallucis brevis.r",
        "Medial head of flexor hallucis brevis.r");
    AddByNames("AdductorHallucis_Right", models,
        "Oblique head of adductor hallucis.r",
        "Transverse head of adductor hallucis.r");
    AddByNames("FlexorDigitiMinimiBrevis_Right", models,
        "Flexor digiti minimi of foot.r");
    AddByNames("OpponensDigitiMinimi_Right", models,
        "(Opponens digiti minimi muscle of foot).r");

    // PLANTAR FOOT - LAYER 4
    AddByNames("PlantarInterossei_Right", models,
        "Plantar interossei muscles.r");
    AddByNames("DorsalInterossei_Right", models,
        "Dorsal interossei muscles of foot.r");

    // COMBINE PARENT NODES
    CombineNodes("SuperficialGlutealMuscles_Right",
        "GluteusMaximus_Right",
        "GluteusMedius_Right",
        "GluteusMinimus_Right");

    CombineNodes("DeepGlutealMuscles_Right",
        "Piriformis_Right",
        "SuperiorGemellus_Right",
        "ObturatorInternus_Right",
        "InferiorGemellus_Right",
        "QuadratusFemoris_Right");

    CombineNodes("GlutealRegion_Right",
        "SuperficialGlutealMuscles_Right",
        "DeepGlutealMuscles_Right");

    CombineNodes("IliopsoasGroup_Right",
        "Iliacus_Right",
        "PsoasMajor_Right");

    CombineNodes("QuadricepsFemoris_Right",
        "RectusFemoris_Right",
        "VastusLateralis_Right",
        "VastusMedialis_Right",
        "VastusIntermedius_Right");

    CombineNodes("AnteriorCompartment_Thigh_Right",
        "IliopsoasGroup_Right",
        "Sartorius_Right",
        "QuadricepsFemoris_Right");

    CombineNodes("MedialCompartment_Thigh_Right",
        "Pectineus_Right",
        "AdductorLongus_Right",
        "AdductorBrevis_Right",
        "AdductorMagnus_Right",
        "Gracilis_Right",
        "ObturatorExternus_Right");

    CombineNodes("PosteriorCompartment_Thigh_Right",
        "BicepsFemoris_Right",
        "Semitendinosus_Right",
        "Semimembranosus_Right");

    CombineNodes("ThighRegion_Right",
        "AnteriorCompartment_Thigh_Right",
        "MedialCompartment_Thigh_Right",
        "PosteriorCompartment_Thigh_Right");

    CombineNodes("AnteriorCompartment_Leg_Right",
        "TibialisAnterior_Right",
        "ExtensorHallucisLongus_Right",
        "ExtensorDigitorumLongus_Right",
        "FibularisTertius_Right");

    CombineNodes("LateralCompartment_Leg_Right",
        "FibularisLongus_Right",
        "FibularisBrevis_Right");

    CombineNodes("SuperficialPosteriorGroup_Right",
        "Gastrocnemius_Right",
        "Soleus_Right",
        "Plantaris_Right");

    CombineNodes("DeepPosteriorGroup_Right",
        "Popliteus_Right",
        "TibialisPosterior_Right",
        "FlexorDigitorumLongus_Right",
        "FlexorHallucisLongus_Right");

    CombineNodes("PosteriorCompartment_Leg_Right",
        "SuperficialPosteriorGroup_Right",
        "DeepPosteriorGroup_Right");

    CombineNodes("LegRegion_Right",
        "AnteriorCompartment_Leg_Right",
        "LateralCompartment_Leg_Right",
        "PosteriorCompartment_Leg_Right");

    CombineNodes("DorsalFootMuscles_Right",
        "ExtensorDigitorumBrevis_Right",
        "ExtensorHallucisBrevis_Right");

    CombineNodes("Layer1_Right",
        "AbductorHallucis_Right",
        "FlexorDigitorumBrevis_Right",
        "AbductorDigitiMinimi_Right");

    CombineNodes("Layer2_Right",
        "QuadratusPlantae_Right",
        "Lumbricals_Right");

    CombineNodes("Layer3_Right",
        "FlexorHallucisBrevis_Right",
        "AdductorHallucis_Right",
        "FlexorDigitiMinimiBrevis_Right",
        "OpponensDigitiMinimi_Right");

    CombineNodes("Layer4_Right",
        "PlantarInterossei_Right",
        "DorsalInterossei_Right");

    CombineNodes("PlantarFootMuscles_Right",
        "Layer1_Right",
        "Layer2_Right",
        "Layer3_Right",
        "Layer4_Right");

    CombineNodes("FootRegion_Right",
        "DorsalFootMuscles_Right",
        "PlantarFootMuscles_Right");

    CombineNodes("RightLowerLimb",
        "GlutealRegion_Right",
        "ThighRegion_Right",
        "LegRegion_Right",
        "FootRegion_Right");
}


private void AddByNames(
    string uiName,
    List<Transform> sourceModels,
    params string[] baseNames)
{
    List<Transform> result =
        new List<Transform>();

    foreach (string baseName in baseNames)
    {
        if (string.IsNullOrEmpty(baseName))
            continue;

        foreach (Transform model in sourceModels)
        {
            if (model == null)
                continue;

            if (
                MatchesModelName(
                    model.name,
                    baseName
                )
            )
            {
                if (!result.Contains(model))
                {
                    result.Add(model);
                }
            }
        }
    }

    modelMap[uiName] = result;
}


private bool MatchesModelName(
    string actualName,
    string expectedBaseName)
{
    if (string.IsNullOrEmpty(actualName))
        return false;

    if (string.IsNullOrEmpty(expectedBaseName))
        return false;

    string actual =
        NormalizeName(actualName);

    string expected =
        NormalizeName(expectedBaseName);

    if (actual == expected)
        return true;

    if (actual == expected + ".l")
        return true;

    if (actual == expected + ".r")
        return true;

    return false;
}


private string NormalizeName(
    string value)
{
    if (string.IsNullOrEmpty(value))
        return string.Empty;

    value = value.Trim();

    value = value.Replace(
        "(Clone)",
        ""
    );

    value = value.Trim();

    while (value.Contains("  "))
    {
        value = value.Replace(
            "  ",
            " "
        );
    }

    return value.ToLowerInvariant();
}


private void ValidateMap()
{
    foreach (
        KeyValuePair<
            string,
            List<Transform>
        > pair
        in modelMap)
    {
        if (
            pair.Value == null ||
            pair.Value.Count == 0
        )
        {
            Debug.LogWarning(
                "LEVEL 2 EMPTY NODE: "
                + pair.Key
            );
        }
        else
        {
            Debug.Log(
                "LEVEL 2 OK: "
                + pair.Key
                + " | Models = "
                + pair.Value.Count
            );
        }
    }
}


public List<Transform> GetModels(
    string uiNodeName)
{
    if (!built)
    {
        Build();
    }

    List<Transform> models;

    if (
        modelMap.TryGetValue(
            uiNodeName,
            out models
        )
    )
    {
        return models;
    }

    return null;
}


public bool ContainsNode(
    string uiNodeName)
{
    if (!built)
    {
        Build();
    }

    return modelMap.ContainsKey(
        uiNodeName
    );
}

private void CombineNodes(
    string groupName,
    params string[] childNames)
{
    List<Transform> combined =
    new List<Transform>();

    foreach (string childName in childNames)
    {
        List<Transform> childModels;

        if (
            !modelMap.TryGetValue(
                childName,
                out childModels
            )
        )
        {
            continue;
        }

        if (childModels == null)
            continue;

        foreach (Transform model in childModels)
        {
            if (model == null)
                continue;

            if (!combined.Contains(model))
            {
                combined.Add(model);
            }
        }
    }

    modelMap[groupName] =
        combined;

    Debug.Log(
        "LEVEL 2 GROUP OK: "
        + groupName
        + " | Models = "
        + combined.Count
    );

}


}
