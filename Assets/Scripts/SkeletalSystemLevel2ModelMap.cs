
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkeletalSystemLevel2ModelMap : MonoBehaviour
{
    [Header("Level 1 Mapping")]
    [SerializeField]
    private SkeletalSystemLevel1ModelMap level1ModelMap;

    private readonly Dictionary<string, List<Transform>> modelMap =
        new Dictionary<string, List<Transform>>();

    private bool built;

    public void Build()
    {
        modelMap.Clear();
        built = false;

        if (level1ModelMap == null)
        {
            Debug.LogError(
                "[Skeletal L2] Chưa gán SkeletalSystemLevel1ModelMap."
            );
            return;
        }

        // =============================================
        // VERTEBRAL COLUMN
        // =============================================

        AnatomyModelGroup vertebralGroup =
            level1ModelMap.GetGroup("VertebralColumn");

        List<Transform> vertebralModels =
            GetValidModels(vertebralGroup);

        if (vertebralModels.Count == 0)
        {
            Debug.LogWarning(
                "[Skeletal L2] Group VertebralColumn chưa có model."
            );
        }

        // Cervical vertebrae
        AddByNames("Atlas", vertebralModels, "Atlas (C1)");
        AddByNames("Axis", vertebralModels, "Axis (C2)");

        AddByNames("VertebraC3", vertebralModels, "Vertebra C3");
        AddByNames("VertebraC4", vertebralModels, "Vertebra C4");
        AddByNames("VertebraC5", vertebralModels, "Vertebra C5");
        AddByNames("VertebraC6", vertebralModels, "Vertebra C6");
        AddByNames("VertebraC7", vertebralModels, "Vertebra C7");

        CombineNodes(
            "UpperCervicalVertebrae",
            "Atlas", "Axis"
        );

        CombineNodes(
            "LowerCervicalVertebrae",
            "VertebraC3", "VertebraC4", "VertebraC5",
            "VertebraC6", "VertebraC7"
        );

        CombineNodes(
            "CervicalVertebrae",
            "UpperCervicalVertebrae",
            "LowerCervicalVertebrae"
        );

        // Thoracic vertebrae
        AddByNames("VertebraT1", vertebralModels, "Vertebra T1");
        AddByNames("VertebraT2", vertebralModels, "Vertebra T2");
        AddByNames("VertebraT3", vertebralModels, "Vertebra T3");
        AddByNames("VertebraT4", vertebralModels, "Vertebra T4");
        AddByNames("VertebraT5", vertebralModels, "Vertebra T5");
        AddByNames("VertebraT6", vertebralModels, "Vertebra T6");
        AddByNames("VertebraT7", vertebralModels, "Vertebra T7");
        AddByNames("VertebraT8", vertebralModels, "Vertebra T8");
        AddByNames("VertebraT9", vertebralModels, "Vertebra T9");
        AddByNames("VertebraT10", vertebralModels, "Vertebra T10");
        AddByNames("VertebraT11", vertebralModels, "Vertebra T11");
        AddByNames("VertebraT12", vertebralModels, "Vertebra T12");

        CombineNodes(
            "UpperThoracicVertebrae",
            "VertebraT1", "VertebraT2", "VertebraT3",
            "VertebraT4", "VertebraT5", "VertebraT6"
        );

        CombineNodes(
            "LowerThoracicVertebrae",
            "VertebraT7", "VertebraT8", "VertebraT9",
            "VertebraT10", "VertebraT11", "VertebraT12"
        );

        CombineNodes(
            "ThoracicVertebrae",
            "UpperThoracicVertebrae",
            "LowerThoracicVertebrae"
        );

        // Lumbar vertebrae
        AddByNames("VertebraL1", vertebralModels, "Vertebra L1");
        AddByNames("VertebraL2", vertebralModels, "Vertebra L2");
        AddByNames("VertebraL3", vertebralModels, "Vertebra L3");
        AddByNames("VertebraL4", vertebralModels, "Vertebra L4");
        AddByNames("VertebraL5", vertebralModels, "Vertebra L5");

        CombineNodes(
            "UpperLumbarVertebrae",
            "VertebraL1", "VertebraL2"
        );

        CombineNodes(
            "LowerLumbarVertebrae",
            "VertebraL3", "VertebraL4", "VertebraL5"
        );

        CombineNodes(
            "LumbarVertebrae",
            "UpperLumbarVertebrae",
            "LowerLumbarVertebrae"
        );

        // Sacrum and coccyx
        AddByNames("Sacrum", vertebralModels, "Sacrum");
        AddByNames("Coccyx", vertebralModels, "Coccyx");

        // Root node
        CombineNodes(
            "VertebralColumn",
            "CervicalVertebrae",
            "ThoracicVertebrae",
            "LumbarVertebrae",
            "Sacrum",
            "Coccyx"
        );

        // ==================== THORACIC CAGE ====================
        AnatomyModelGroup thoracicCageGroup = level1ModelMap.GetGroup("ThoracicCage");
        List<Transform> thoracicCageModels = GetValidModels(thoracicCageGroup);

        if (thoracicCageModels.Count == 0)
        {
            Debug.LogWarning("[SkeletalSystemLevel2ModelMap] ThoracicCage không có model.");
        }
        else
        {
            // Sternum
            AddByNames("Manubrium", thoracicCageModels, "Manubrium of sternum");
            AddByNames("BodyOfSternum", thoracicCageModels, "Body of sternum");
            AddByNames("XiphoidProcess", thoracicCageModels, "Xiphoid process");

            CombineNodes(
                "Sternum",
                "Manubrium",
                "BodyOfSternum",
                "XiphoidProcess"
            );

            // Ribs: mỗi node trái/phải có model riêng
            AddByNames("FirstRibLeft", thoracicCageModels, "First rib.l");
            AddByNames("FirstRibRight", thoracicCageModels, "First rib.r");
            AddByNames("SecondRibLeft", thoracicCageModels, "Second rib.l");
            AddByNames("SecondRibRight", thoracicCageModels, "Second rib.r");
            AddByNames("ThirdRibLeft", thoracicCageModels, "Third rib.l");
            AddByNames("ThirdRibRight", thoracicCageModels, "Third rib.r");
            AddByNames("FourthRibLeft", thoracicCageModels, "Fourth rib.l");
            AddByNames("FourthRibRight", thoracicCageModels, "Fourth rib.r");
            AddByNames("FifthRibLeft", thoracicCageModels, "Fifth rib.l");
            AddByNames("FifthRibRight", thoracicCageModels, "Fifth rib.r");
            AddByNames("SixthRibLeft", thoracicCageModels, "Sixth rib.l");
            AddByNames("SixthRibRight", thoracicCageModels, "Sixth rib.r");
            AddByNames("SeventhRibLeft", thoracicCageModels, "Seventh rib.l");
            AddByNames("SeventhRibRight", thoracicCageModels, "Seventh rib.r");
            AddByNames("EighthRibLeft", thoracicCageModels, "Eighth rib.l");
            AddByNames("EighthRibRight", thoracicCageModels, "Eighth rib.r");
            AddByNames("NinthRibLeft", thoracicCageModels, "Ninth rib.l");
            AddByNames("NinthRibRight", thoracicCageModels, "Ninth rib.r");
            AddByNames("TenthRibLeft", thoracicCageModels, "Tenth rib.l");
            AddByNames("TenthRibRight", thoracicCageModels, "Tenth rib.r");
            AddByNames("EleventhRibLeft", thoracicCageModels, "Eleventh rib.l");
            AddByNames("EleventhRibRight", thoracicCageModels, "Eleventh rib.r");
            AddByNames("TwelfthRibLeft", thoracicCageModels, "Twelfth rib.l");
            AddByNames("TwelfthRibRight", thoracicCageModels, "Twelfth rib.r");

            CombineNodes("FirstRib", "FirstRibLeft", "FirstRibRight");
            CombineNodes("SecondRib", "SecondRibLeft", "SecondRibRight");
            CombineNodes("ThirdRib", "ThirdRibLeft", "ThirdRibRight");
            CombineNodes("FourthRib", "FourthRibLeft", "FourthRibRight");
            CombineNodes("FifthRib", "FifthRibLeft", "FifthRibRight");
            CombineNodes("SixthRib", "SixthRibLeft", "SixthRibRight");
            CombineNodes("SeventhRib", "SeventhRibLeft", "SeventhRibRight");
            CombineNodes("EighthRib", "EighthRibLeft", "EighthRibRight");
            CombineNodes("NinthRib", "NinthRibLeft", "NinthRibRight");
            CombineNodes("TenthRib", "TenthRibLeft", "TenthRibRight");
            CombineNodes("EleventhRib", "EleventhRibLeft", "EleventhRibRight");
            CombineNodes("TwelfthRib", "TwelfthRibLeft", "TwelfthRibRight");

            // Costal cartilage: mỗi node trái/phải có model riêng
            AddByNames("FirstCostalCartilageLeft", thoracicCageModels, "Costal cartilage of first rib.l");
            AddByNames("FirstCostalCartilageRight", thoracicCageModels, "Costal cartilage of first rib.r");
            AddByNames("SecondCostalCartilageLeft", thoracicCageModels, "Costal cartilage of second rib.l");
            AddByNames("SecondCostalCartilageRight", thoracicCageModels, "Costal cartilage of second rib.r");
            AddByNames("ThirdCostalCartilageLeft", thoracicCageModels, "Costal cartilage of third rib.l");
            AddByNames("ThirdCostalCartilageRight", thoracicCageModels, "Costal cartilage of third rib.r");
            AddByNames("FourthCostalCartilageLeft", thoracicCageModels, "Costal cartilage of fourth rib.l");
            AddByNames("FourthCostalCartilageRight", thoracicCageModels, "Costal cartilage of fourth rib.r");
            AddByNames("FifthCostalCartilageLeft", thoracicCageModels, "Costal cartilage of fifth rib.l");
            AddByNames("FifthCostalCartilageRight", thoracicCageModels, "Costal cartilage of fifth rib.r");
            AddByNames("SixthCostalCartilageLeft", thoracicCageModels, "Costal cartilage of sixth rib.l");
            AddByNames("SixthCostalCartilageRight", thoracicCageModels, "Costal cartilage of sixth rib.r");
            AddByNames("SeventhCostalCartilageLeft", thoracicCageModels, "Costal cartilage of seventh rib.l");
            AddByNames("SeventhCostalCartilageRight", thoracicCageModels, "Costal cartilage of seventh rib.r");
            AddByNames("EighthCostalCartilageLeft", thoracicCageModels, "Costal cartilage of eighth rib.l");
            AddByNames("EighthCostalCartilageRight", thoracicCageModels, "Costal cartilage of eighth rib.r");
            AddByNames("NinthCostalCartilageLeft", thoracicCageModels, "Costal cartilage of ninth rib.l");
            AddByNames("NinthCostalCartilageRight", thoracicCageModels, "Costal cartilage of ninth rib.r");
            AddByNames("TenthCostalCartilageLeft", thoracicCageModels, "Costal cartilage of tenth rib.l");
            AddByNames("TenthCostalCartilageRight", thoracicCageModels, "Costal cartilage of tenth rib.r");

            CombineNodes("FirstCostalCartilage", "FirstCostalCartilageLeft", "FirstCostalCartilageRight");
            CombineNodes("SecondCostalCartilage", "SecondCostalCartilageLeft", "SecondCostalCartilageRight");
            CombineNodes("ThirdCostalCartilage", "ThirdCostalCartilageLeft", "ThirdCostalCartilageRight");
            CombineNodes("FourthCostalCartilage", "FourthCostalCartilageLeft", "FourthCostalCartilageRight");
            CombineNodes("FifthCostalCartilage", "FifthCostalCartilageLeft", "FifthCostalCartilageRight");
            CombineNodes("SixthCostalCartilage", "SixthCostalCartilageLeft", "SixthCostalCartilageRight");
            CombineNodes("SeventhCostalCartilage", "SeventhCostalCartilageLeft", "SeventhCostalCartilageRight");
            CombineNodes("EighthCostalCartilage", "EighthCostalCartilageLeft", "EighthCostalCartilageRight");
            CombineNodes("NinthCostalCartilage", "NinthCostalCartilageLeft", "NinthCostalCartilageRight");
            CombineNodes("TenthCostalCartilage", "TenthCostalCartilageLeft", "TenthCostalCartilageRight");

            // Nhóm sụn sườn
            CombineNodes(
                "CostalCartilages",
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
            );

            // Nhóm xương sườn
            CombineNodes(
                "Ribs",
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
            );

            CombineNodes("ThoracicCage", "Sternum", "Ribs", "CostalCartilages");
        }

        // ==================== PECTORAL GIRDLE ====================
        AnatomyModelGroup pectoralGirdleGroup = level1ModelMap.GetGroup("PectoralGirdle");
        List<Transform> pectoralGirdleModels = GetValidModels(pectoralGirdleGroup);

        AddByNames("ClavicleLeft", pectoralGirdleModels, "Clavicle.l");
        AddByNames("ClavicleRight", pectoralGirdleModels, "Clavicle.r");
        CombineNodes("Clavicle", "ClavicleLeft", "ClavicleRight");

        AddByNames("ScapulaLeft", pectoralGirdleModels, "Scapula.l");
        AddByNames("ScapulaRight", pectoralGirdleModels, "Scapula.r");
        CombineNodes("Scapula", "ScapulaLeft", "ScapulaRight");

        CombineNodes("PectoralGirdle", "Clavicle", "Scapula");

        // ==================== UPPER LIMB ====================
        AnatomyModelGroup upperLimbGroup = level1ModelMap.GetGroup("UpperLimb");
        List<Transform> upperLimbModels = GetValidModels(upperLimbGroup);

        // Humerus
        AddByNames("HumerusLeft", upperLimbModels, "Humerus.l");
        AddByNames("HumerusRight", upperLimbModels, "Humerus.r");
        CombineNodes("Humerus", "HumerusLeft", "HumerusRight");

        // Forearm
        AddByNames("RadiusLeft", upperLimbModels, "Radius.l");
        AddByNames("RadiusRight", upperLimbModels, "Radius.r");
        CombineNodes("Radius", "RadiusLeft", "RadiusRight");

        AddByNames("UlnaLeft", upperLimbModels, "Ulna.l");
        AddByNames("UlnaRight", upperLimbModels, "Ulna.r");
        CombineNodes("Ulna", "UlnaLeft", "UlnaRight");
        CombineNodes("Forearm", "Radius", "Ulna");

        // Carpal bones
        AddByNames("CapitateLeft", upperLimbModels, "Capitate bone.l");
        AddByNames("CapitateRight", upperLimbModels, "Capitate bone.r");
        CombineNodes("Capitate", "CapitateLeft", "CapitateRight");

        AddByNames("HamateLeft", upperLimbModels, "Hamate bone.l");
        AddByNames("HamateRight", upperLimbModels, "Hamate bone.r");
        CombineNodes("Hamate", "HamateLeft", "HamateRight");

        AddByNames("LunateLeft", upperLimbModels, "Lunate bone.l");
        AddByNames("LunateRight", upperLimbModels, "Lunate bone.r");
        CombineNodes("Lunate", "LunateLeft", "LunateRight");

        AddByNames("PisiformLeft", upperLimbModels, "Pisiform bone.l");
        AddByNames("PisiformRight", upperLimbModels, "Pisiform bone.r");
        CombineNodes("Pisiform", "PisiformLeft", "PisiformRight");

        AddByNames("ScaphoidLeft", upperLimbModels, "Scaphoid bone.l");
        AddByNames("ScaphoidRight", upperLimbModels, "Scaphoid bone.r");
        CombineNodes("Scaphoid", "ScaphoidLeft", "ScaphoidRight");

        AddByNames("TrapeziumLeft", upperLimbModels, "Trapezium bone.l");
        AddByNames("TrapeziumRight", upperLimbModels, "Trapezium bone.r");
        CombineNodes("Trapezium", "TrapeziumLeft", "TrapeziumRight");

        AddByNames("TrapezoidLeft", upperLimbModels, "Trapezoid bone.l");
        AddByNames("TrapezoidRight", upperLimbModels, "Trapezoid bone.r");
        CombineNodes("Trapezoid", "TrapezoidLeft", "TrapezoidRight");

        AddByNames("TriquetrumLeft", upperLimbModels, "Triquetrum bone.l");
        AddByNames("TriquetrumRight", upperLimbModels, "Triquetrum bone.r");
        CombineNodes("Triquetrum", "TriquetrumLeft", "TriquetrumRight");

        CombineNodes(
            "CarpalBones",
            "Scaphoid",
            "Lunate",
            "Triquetrum",
            "Pisiform",
            "Trapezium",
            "Trapezoid",
            "Capitate",
            "Hamate"
        );

        // Metacarpal bones
        AddByNames("FirstMetacarpalLeft", upperLimbModels, "First metacarpal bone.l");
        AddByNames("FirstMetacarpalRight", upperLimbModels, "First metacarpal bone.r");
        CombineNodes("FirstMetacarpal", "FirstMetacarpalLeft", "FirstMetacarpalRight");

        AddByNames("SecondMetacarpalLeft", upperLimbModels, "Second metacarpal bone.l");
        AddByNames("SecondMetacarpalRight", upperLimbModels, "Second metacarpal bone.r");
        CombineNodes("SecondMetacarpal", "SecondMetacarpalLeft", "SecondMetacarpalRight");

        AddByNames("ThirdMetacarpalLeft", upperLimbModels, "Third metacarpal bone.l");
        AddByNames("ThirdMetacarpalRight", upperLimbModels, "Third metacarpal bone.r");
        CombineNodes("ThirdMetacarpal", "ThirdMetacarpalLeft", "ThirdMetacarpalRight");

        AddByNames("FourthMetacarpalLeft", upperLimbModels, "Fourth metacarpal bone.l");
        AddByNames("FourthMetacarpalRight", upperLimbModels, "Fourth metacarpal bone.r");
        CombineNodes("FourthMetacarpal", "FourthMetacarpalLeft", "FourthMetacarpalRight");

        AddByNames("FifthMetacarpalLeft", upperLimbModels, "Fifth metacarpal bone.l");
        AddByNames("FifthMetacarpalRight", upperLimbModels, "Fifth metacarpal bone.r");
        CombineNodes("FifthMetacarpal", "FifthMetacarpalLeft", "FifthMetacarpalRight");

        CombineNodes(
            "MetacarpalBones",
            "FirstMetacarpal",
            "SecondMetacarpal",
            "ThirdMetacarpal",
            "FourthMetacarpal",
            "FifthMetacarpal"
        );

        // Phalanges: thumb
        AddByNames("ThumbProximalPhalanxLeft", upperLimbModels, "Proximal phalanx of first finger of hand.l");
        AddByNames("ThumbProximalPhalanxRight", upperLimbModels, "Proximal phalanx of first finger of hand.r");
        CombineNodes("ThumbProximalPhalanx", "ThumbProximalPhalanxLeft", "ThumbProximalPhalanxRight");

        AddByNames("ThumbDistalPhalanxLeft", upperLimbModels, "Distal phalanx of first finger of hand.l");
        AddByNames("ThumbDistalPhalanxRight", upperLimbModels, "Distal phalanx of first finger of hand.r");
        CombineNodes("ThumbDistalPhalanx", "ThumbDistalPhalanxLeft", "ThumbDistalPhalanxRight");

        // Index finger: second finger
        AddByNames("IndexProximalPhalanxLeft", upperLimbModels, "Proximal phalanx of second finger of hand.l");
        AddByNames("IndexProximalPhalanxRight", upperLimbModels, "Proximal phalanx of second finger of hand.r");
        CombineNodes("IndexProximalPhalanx", "IndexProximalPhalanxLeft", "IndexProximalPhalanxRight");

        AddByNames("IndexMiddlePhalanxLeft", upperLimbModels, "Middle phalanx of second finger of hand.l");
        AddByNames("IndexMiddlePhalanxRight", upperLimbModels, "Middle phalanx of second finger of hand.r");
        CombineNodes("IndexMiddlePhalanx", "IndexMiddlePhalanxLeft", "IndexMiddlePhalanxRight");

        AddByNames("IndexDistalPhalanxLeft", upperLimbModels, "Distal phalanx of second finger of hand.l");
        AddByNames("IndexDistalPhalanxRight", upperLimbModels, "Distal phalanx of second finger of hand.r");
        CombineNodes("IndexDistalPhalanx", "IndexDistalPhalanxLeft", "IndexDistalPhalanxRight");

        // Middle finger: third finger
        AddByNames("MiddleProximalPhalanxLeft", upperLimbModels, "Proximal phalanx of third finger of hand.l");
        AddByNames("MiddleProximalPhalanxRight", upperLimbModels, "Proximal phalanx of third finger of hand.r");
        CombineNodes("MiddleProximalPhalanx", "MiddleProximalPhalanxLeft", "MiddleProximalPhalanxRight");

        AddByNames("MiddleMiddlePhalanxLeft", upperLimbModels, "Middle phalanx of third finger of hand.l");
        AddByNames("MiddleMiddlePhalanxRight", upperLimbModels, "Middle phalanx of third finger of hand.r");
        CombineNodes("MiddleMiddlePhalanx", "MiddleMiddlePhalanxLeft", "MiddleMiddlePhalanxRight");

        AddByNames("MiddleDistalPhalanxLeft", upperLimbModels, "Distal phalanx of third finger of hand.l");
        AddByNames("MiddleDistalPhalanxRight", upperLimbModels, "Distal phalanx of third finger of hand.r");
        CombineNodes("MiddleDistalPhalanx", "MiddleDistalPhalanxLeft", "MiddleDistalPhalanxRight");

        // Ring finger: fourth finger
        AddByNames("RingProximalPhalanxLeft", upperLimbModels, "Proximal phalanx of fourth finger of hand.l");
        AddByNames("RingProximalPhalanxRight", upperLimbModels, "Proximal phalanx of fourth finger of hand.r");
        CombineNodes("RingProximalPhalanx", "RingProximalPhalanxLeft", "RingProximalPhalanxRight");

        AddByNames("RingMiddlePhalanxLeft", upperLimbModels, "Middle phalanx of fourth finger of hand.l");
        AddByNames("RingMiddlePhalanxRight", upperLimbModels, "Middle phalanx of fourth finger of hand.r");
        CombineNodes("RingMiddlePhalanx", "RingMiddlePhalanxLeft", "RingMiddlePhalanxRight");

        AddByNames("RingDistalPhalanxLeft", upperLimbModels, "Distal phalanx of fourth finger of hand.l");
        AddByNames("RingDistalPhalanxRight", upperLimbModels, "Distal phalanx of fourth finger of hand.r");
        CombineNodes("RingDistalPhalanx", "RingDistalPhalanxLeft", "RingDistalPhalanxRight");

        // Little finger: fifth finger
        AddByNames("LittleProximalPhalanxLeft", upperLimbModels, "Proximal phalanx of fifth finger of hand.l");
        AddByNames("LittleProximalPhalanxRight", upperLimbModels, "Proximal phalanx of fifth finger of hand.r");
        CombineNodes("LittleProximalPhalanx", "LittleProximalPhalanxLeft", "LittleProximalPhalanxRight");

        AddByNames("LittleMiddlePhalanxLeft", upperLimbModels, "Middle phalanx of fifth finger of hand.l");
        AddByNames("LittleMiddlePhalanxRight", upperLimbModels, "Middle phalanx of fifth finger of hand.r");
        CombineNodes("LittleMiddlePhalanx", "LittleMiddlePhalanxLeft", "LittleMiddlePhalanxRight");

        AddByNames("LittleDistalPhalanxLeft", upperLimbModels, "Distal phalanx of fifth finger of hand.l");
        AddByNames("LittleDistalPhalanxRight", upperLimbModels, "Distal phalanx of fifth finger of hand.r");
        CombineNodes("LittleDistalPhalanx", "LittleDistalPhalanxLeft", "LittleDistalPhalanxRight");

        // Gộp nhóm đốt ngón tay
        CombineNodes(
            "Phalanges",
            "ThumbProximalPhalanx",
            "ThumbDistalPhalanx",
            "IndexProximalPhalanx",
            "IndexMiddlePhalanx",
            "IndexDistalPhalanx",
            "MiddleProximalPhalanx",
            "MiddleMiddlePhalanx",
            "MiddleDistalPhalanx",
            "RingProximalPhalanx",
            "RingMiddlePhalanx",
            "RingDistalPhalanx",
            "LittleProximalPhalanx",
            "LittleMiddlePhalanx",
            "LittleDistalPhalanx"
        );

        // Gộp toàn bộ bàn tay và chi trên
        CombineNodes("Hand", "CarpalBones", "MetacarpalBones", "Phalanges");
        CombineNodes("UpperLimb", "Humerus", "Forearm", "Hand");

        // ==================== PelvicGirdle ====================
        AnatomyModelGroup pelvicGirdleGroup = level1ModelMap.GetGroup("PelvicGirdle");
        List<Transform> pelvicGirdleModels = GetValidModels(pelvicGirdleGroup);

        AddByNames("HipBoneLeft", pelvicGirdleModels, "Hip bone.l");
        AddByNames("HipBoneRight", pelvicGirdleModels, "Hip bone.r");

        CombineNodes("HipBone", "HipBoneLeft", "HipBoneRight");
        CombineNodes("PelvicGirdle", "HipBone");

        // ==================== Lower Limb ====================
        AnatomyModelGroup lowerLimbGroup = level1ModelMap.GetGroup("LowerLimb");
        List<Transform> lowerLimbModels = GetValidModels(lowerLimbGroup);

        // Femur
        AddByNames("FemurLeft", lowerLimbModels, "Femur.l");
        AddByNames("FemurRight", lowerLimbModels, "Femur.r");
        CombineNodes("Femur", "FemurLeft", "FemurRight");

        // Patella
        AddByNames("PatellaLeft", lowerLimbModels, "Patella.l");
        AddByNames("PatellaRight", lowerLimbModels, "Patella.r");
        CombineNodes("Patella", "PatellaLeft", "PatellaRight");

        // Tibia
        AddByNames("TibiaLeft", lowerLimbModels, "Tibia.l");
        AddByNames("TibiaRight", lowerLimbModels, "Tibia.r");
        CombineNodes("Tibia", "TibiaLeft", "TibiaRight");

        // Fibula
        AddByNames("FibulaLeft", lowerLimbModels, "Fibula.l");
        AddByNames("FibulaRight", lowerLimbModels, "Fibula.r");
        CombineNodes("Fibula", "FibulaLeft", "FibulaRight");

        CombineNodes("Crus", "Tibia", "Fibula");

        // Tarsal bones: each node combines left and right
        AddByNames("__CalcaneusLeft", lowerLimbModels, "Calcaneus.l");
        AddByNames("__CalcaneusRight", lowerLimbModels, "Calcaneus.r");
        CombineNodes("Calcaneus", "__CalcaneusLeft", "__CalcaneusRight");

        AddByNames("__TalusLeft", lowerLimbModels, "Talus.l");
        AddByNames("__TalusRight", lowerLimbModels, "Talus.r");
        CombineNodes("Talus", "__TalusLeft", "__TalusRight");

        AddByNames("__NavicularLeft", lowerLimbModels, "Navicular bone.l");
        AddByNames("__NavicularRight", lowerLimbModels, "Navicular bone.r");
        CombineNodes("Navicular", "__NavicularLeft", "__NavicularRight");

        AddByNames("__CuboidLeft", lowerLimbModels, "Cuboid bone.l");
        AddByNames("__CuboidRight", lowerLimbModels, "Cuboid bone.r");
        CombineNodes("Cuboid", "__CuboidLeft", "__CuboidRight");

        AddByNames("__MedialCuneiformLeft", lowerLimbModels, "Medial cuneiform bone.l");
        AddByNames("__MedialCuneiformRight", lowerLimbModels, "Medial cuneiform bone.r");
        CombineNodes("MedialCuneiform", "__MedialCuneiformLeft", "__MedialCuneiformRight");

        AddByNames("__IntermediateCuneiformLeft", lowerLimbModels, "Intermediate cuneiform bone.l");
        AddByNames("__IntermediateCuneiformRight", lowerLimbModels, "Intermediate cuneiform bone.r");
        CombineNodes("IntermediateCuneiform", "__IntermediateCuneiformLeft", "__IntermediateCuneiformRight");

        AddByNames("__LateralCuneiformLeft", lowerLimbModels, "Lateral cuneiform bone.l");
        AddByNames("__LateralCuneiformRight", lowerLimbModels, "Lateral cuneiform bone.r");
        CombineNodes("LateralCuneiform", "__LateralCuneiformLeft", "__LateralCuneiformRight");

        CombineNodes(
            "TarsalBones",
            "Calcaneus",
            "Talus",
            "Navicular",
            "Cuboid",
            "MedialCuneiform",
            "IntermediateCuneiform",
            "LateralCuneiform"
        );

        // Metatarsal bones
        AddByNames("__FirstMetatarsalLeft", lowerLimbModels, "First metatarsal bone.l");
        AddByNames("__FirstMetatarsalRight", lowerLimbModels, "First metatarsal bone.r");
        CombineNodes("FirstMetatarsal", "__FirstMetatarsalLeft", "__FirstMetatarsalRight");

        AddByNames("__SecondMetatarsalLeft", lowerLimbModels, "Second metatarsal bone.l");
        AddByNames("__SecondMetatarsalRight", lowerLimbModels, "Second metatarsal bone.r");
        CombineNodes("SecondMetatarsal", "__SecondMetatarsalLeft", "__SecondMetatarsalRight");

        AddByNames("__ThirdMetatarsalLeft", lowerLimbModels, "Third metatarsal bone.l");
        AddByNames("__ThirdMetatarsalRight", lowerLimbModels, "Third metatarsal bone.r");
        CombineNodes("ThirdMetatarsal", "__ThirdMetatarsalLeft", "__ThirdMetatarsalRight");

        AddByNames("__FourthMetatarsalLeft", lowerLimbModels, "Fourth metatarsal bone.l");
        AddByNames("__FourthMetatarsalRight", lowerLimbModels, "Fourth metatarsal bone.r");
        CombineNodes("FourthMetatarsal", "__FourthMetatarsalLeft", "__FourthMetatarsalRight");

        AddByNames("__FifthMetatarsalLeft", lowerLimbModels, "Fifth metatarsal bone.l");
        AddByNames("__FifthMetatarsalRight", lowerLimbModels, "Fifth metatarsal bone.r");
        CombineNodes("FifthMetatarsal", "__FifthMetatarsalLeft", "__FifthMetatarsalRight");

        CombineNodes(
            "MetatarsalBones",
            "FirstMetatarsal",
            "SecondMetatarsal",
            "ThirdMetatarsal",
            "FourthMetatarsal",
            "FifthMetatarsal"
        );

        // Toe phalanges
        AddByNames("__BigToeProximalLeft", lowerLimbModels, "Proximal phalanx of first finger of foot.l");
        AddByNames("__BigToeProximalRight", lowerLimbModels, "Proximal phalanx of first finger of foot.r");
        CombineNodes("BigToeProximalPhalanx", "__BigToeProximalLeft", "__BigToeProximalRight");

        AddByNames("__BigToeDistalLeft", lowerLimbModels, "Distal phalanx of first finger of foot.l");
        AddByNames("__BigToeDistalRight", lowerLimbModels, "Distal phalanx of first finger of foot.r");
        CombineNodes("BigToeDistalPhalanx", "__BigToeDistalLeft", "__BigToeDistalRight");

        // Second toe
        AddByNames("__SecondToeProximalLeft", lowerLimbModels, "Proximal phalanx of second finger of foot.l");
        AddByNames("__SecondToeProximalRight", lowerLimbModels, "Proximal phalanx of second finger of foot.r");
        CombineNodes("SecondToeProximalPhalanx", "__SecondToeProximalLeft", "__SecondToeProximalRight");

        AddByNames("__SecondToeMiddleLeft", lowerLimbModels, "Middle phalanx of second finger of foot.l");
        AddByNames("__SecondToeMiddleRight", lowerLimbModels, "Middle phalanx of second finger of foot.r");
        CombineNodes("SecondToeMiddlePhalanx", "__SecondToeMiddleLeft", "__SecondToeMiddleRight");

        AddByNames("__SecondToeDistalLeft", lowerLimbModels, "Distal phalanx of second finger of foot.l");
        AddByNames("__SecondToeDistalRight", lowerLimbModels, "Distal phalanx of second finger of foot.r");
        CombineNodes("SecondToeDistalPhalanx", "__SecondToeDistalLeft", "__SecondToeDistalRight");

        // Third toe
        AddByNames("__ThirdToeProximalLeft", lowerLimbModels, "Proximal phalanx of third finger of foot.l");
        AddByNames("__ThirdToeProximalRight", lowerLimbModels, "Proximal phalanx of third finger of foot.r");
        CombineNodes("ThirdToeProximalPhalanx", "__ThirdToeProximalLeft", "__ThirdToeProximalRight");

        AddByNames("__ThirdToeMiddleLeft", lowerLimbModels, "Middle phalanx of third finger of foot.l");
        AddByNames("__ThirdToeMiddleRight", lowerLimbModels, "Middle phalanx of third finger of foot.r");
        CombineNodes("ThirdToeMiddlePhalanx", "__ThirdToeMiddleLeft", "__ThirdToeMiddleRight");

        AddByNames("__ThirdToeDistalLeft", lowerLimbModels, "Distal phalanx of third finger of foot.l");
        AddByNames("__ThirdToeDistalRight", lowerLimbModels, "Distal phalanx of third finger of foot.r");
        CombineNodes("ThirdToeDistalPhalanx", "__ThirdToeDistalLeft", "__ThirdToeDistalRight");

        // Fourth toe
        AddByNames("__FourthToeProximalLeft", lowerLimbModels, "Proximal phalanx of fourth finger of foot.l");
        AddByNames("__FourthToeProximalRight", lowerLimbModels, "Proximal phalanx of fourth finger of foot.r");
        CombineNodes("FourthToeProximalPhalanx", "__FourthToeProximalLeft", "__FourthToeProximalRight");

        AddByNames("__FourthToeMiddleLeft", lowerLimbModels, "Middle phalanx of fourth finger of foot.l");
        AddByNames("__FourthToeMiddleRight", lowerLimbModels, "Middle phalanx of fourth finger of foot.r");
        CombineNodes("FourthToeMiddlePhalanx", "__FourthToeMiddleLeft", "__FourthToeMiddleRight");

        AddByNames("__FourthToeDistalLeft", lowerLimbModels, "Distal phalanx of fourth finger of foot.l");
        AddByNames("__FourthToeDistalRight", lowerLimbModels, "Distal phalanx of fourth finger of foot.r");
        CombineNodes("FourthToeDistalPhalanx", "__FourthToeDistalLeft", "__FourthToeDistalRight");

        // Fifth toe
        AddByNames("__FifthToeProximalLeft", lowerLimbModels, "Proximal phalanx of fifth finger of foot.l");
        AddByNames("__FifthToeProximalRight", lowerLimbModels, "Proximal phalanx of fifth finger of foot.r");
        CombineNodes("FifthToeProximalPhalanx", "__FifthToeProximalLeft", "__FifthToeProximalRight");

        AddByNames("__FifthToeMiddleLeft", lowerLimbModels, "Middle phalanx of fifth finger of foot.l");
        AddByNames("__FifthToeMiddleRight", lowerLimbModels, "Middle phalanx of fifth finger of foot.r");
        CombineNodes("FifthToeMiddlePhalanx", "__FifthToeMiddleLeft", "__FifthToeMiddleRight");

        AddByNames("__FifthToeDistalLeft", lowerLimbModels, "Distal phalanx of fifth finger of foot.l");
        AddByNames("__FifthToeDistalRight", lowerLimbModels, "Distal phalanx of fifth finger of foot.r");
        CombineNodes("FifthToeDistalPhalanx", "__FifthToeDistalLeft", "__FifthToeDistalRight");

        CombineNodes(
            "FootPhalanges",
            "BigToeProximalPhalanx",
            "BigToeDistalPhalanx",
            "SecondToeProximalPhalanx",
            "SecondToeMiddlePhalanx",
            "SecondToeDistalPhalanx",
            "ThirdToeProximalPhalanx",
            "ThirdToeMiddlePhalanx",
            "ThirdToeDistalPhalanx",
            "FourthToeProximalPhalanx",
            "FourthToeMiddlePhalanx",
            "FourthToeDistalPhalanx",
            "FifthToeProximalPhalanx",
            "FifthToeMiddlePhalanx",
            "FifthToeDistalPhalanx"
        );

        // Sesamoid bones
        AddByNames("__SesamoidLeft", lowerLimbModels, "Sesamoid bones of foot.l");
        AddByNames("__SesamoidRight", lowerLimbModels, "Sesamoid bones of foot.r");
        CombineNodes("SesamoidBones", "__SesamoidLeft", "__SesamoidRight");

        // Foot and Lower Limb
        CombineNodes("Foot", "TarsalBones", "MetatarsalBones", "FootPhalanges", "SesamoidBones");
        CombineNodes("LowerLimb", "Femur", "Patella", "Crus", "Foot");

        // ==================== SKULL ====================
        AnatomyModelGroup skullGroup =
            level1ModelMap.GetGroup("Skull");

        List<Transform> skullModels = GetValidModels(skullGroup);

        if (skullModels.Count == 0)
        {
            Debug.LogWarning(
                "[Skeletal L2] Group Skull chưa có model. " +
                "Hãy kiểm tra Level 1 ModelMap."
            );
        }

        // CRANIAL BONES
        AddByNames("FrontalBone", skullModels,
            "Frontal bone");

        AddByNames("ParietalBones", skullModels,
            "Parietal bone.l",
            "Parietal bone.r");

        AddByNames("TemporalBones", skullModels,
            "Temporal bone.l",
            "Temporal bone.r");

        AddByNames("OccipitalBone", skullModels,
            "Occipital bone");

        AddByNames("SphenoidBone", skullModels,
            "Sphenoid bone");

        AddByNames("EthmoidBone", skullModels,
            "Ethmoid bone");

        CombineNodes("CranialBones",
            "FrontalBone",
            "ParietalBones",
            "TemporalBones",
            "OccipitalBone",
            "SphenoidBone",
            "EthmoidBone");

        // FACIAL BONES
        AddByNames("Mandible", skullModels,
            "Mandible");

        AddByNames("Maxillae", skullModels,
            "Maxilla.l",
            "Maxilla.r");

        AddByNames("ZygomaticBones", skullModels,
            "Zygomatic bone.l",
            "Zygomatic bone.r");

        AddByNames("NasalBones", skullModels,
            "Nasal bone.l",
            "Nasal bone.r");

        AddByNames("LacrimalBones", skullModels,
            "Lacrimal bone.l",
            "Lacrimal bone.r");

        AddByNames("PalatineBones", skullModels,
            "Palatine bone.l",
            "Palatine bone.r");

        AddByNames("InferiorNasalConchae", skullModels,
            "Inferior nasal concha bone.l",
            "Inferior nasal concha bone.r");

        AddByNames("Vomer", skullModels,
            "Vomer");

        CombineNodes("FacialBones",
            "Mandible",
            "Maxillae",
            "ZygomaticBones",
            "NasalBones",
            "LacrimalBones",
            "PalatineBones",
            "InferiorNasalConchae",
            "Vomer");

        // AUDITORY OSSICLES
        AddByNames("Malleus", skullModels,
            "Malleus.l",
            "Malleus.r");

        AddByNames("Incus", skullModels,
            "Incus.l",
            "Incus.r");

        AddByNames("Stapes", skullModels,
            "Stapes.l",
            "Stapes.r");

        CombineNodes("AuditoryOssicles",
            "Malleus",
            "Incus",
            "Stapes");

        // TEETH
        AddByNames("UpperIncisors", skullModels,
            "Upper medial incisor.l",
            "Upper medial incisor.r",
            "Upper lateral incisor.l",
            "Upper lateral incisor.r");

        AddByNames("UpperCanines", skullModels,
            "Upper canine.l",
            "Upper canine.r");

        AddByNames("UpperPremolars", skullModels,
            "Upper first premolar.l",
            "Upper first premolar.r",
            "Upper second premolar.l",
            "Upper second premolar.r");

        AddByNames("UpperMolars", skullModels,
            "Upper first molar tooth.l",
            "Upper first molar tooth.r",
            "Upper second molar tooth.l",
            "Upper second molar tooth.r");

        CombineNodes("UpperTeeth",
            "UpperIncisors",
            "UpperCanines",
            "UpperPremolars",
            "UpperMolars");

        AddByNames("LowerIncisors", skullModels,
            "Lower medial incisor.l",
            "Lower medial incisor.r",
            "Lower lateral incisor.l",
            "Lower lateral incisor.r");

        AddByNames("LowerCanines", skullModels,
            "Lower canine.l",
            "Lower canine.r");

        AddByNames("LowerPremolars", skullModels,
            "Lower first premolar.l",
            "Lower first premolar.r",
            "Lower second premolar.l",
            "Lower second premolar.r");

        AddByNames("LowerMolars", skullModels,
            "Lower first molar tooth.l",
            "Lower first molar tooth.r",
            "Lower second molar tooth.l",
            "Lower second molar tooth.r");

        CombineNodes("LowerTeeth",
            "LowerIncisors",
            "LowerCanines",
            "LowerPremolars",
            "LowerMolars");

        CombineNodes("Teeth",
            "UpperTeeth",
            "LowerTeeth");

        // NASAL AND SINUS STRUCTURES
        AddByNames("AnteriorEthmoidCells", skullModels,
            "Anterior cells of ethmoid bone.l",
            "Anterior cells of ethmoid bone.r");

        AddByNames("MiddleEthmoidCells", skullModels,
            "Middle cells of ethmoid bone.l",
            "Middle cells of ethmoid bone.r");

        AddByNames("PosteriorEthmoidCells", skullModels,
            "Posterior cells of ethmoid bone.l",
            "Posterior cells of ethmoid bone.r");

        CombineNodes("EthmoidAirCells",
            "AnteriorEthmoidCells",
            "MiddleEthmoidCells",
            "PosteriorEthmoidCells");

        AddByNames("FrontalSinus", skullModels,
            "Sinus of frontal bone");

        AddByNames("SphenoidSinus", skullModels,
            "Sinus of sphenoid bone");

        CombineNodes("ParanasalSinuses",
            "FrontalSinus",
            "SphenoidSinus");

        AddByNames("NasalSeptum", skullModels,
            "Nasal septal cartilage");

        CombineNodes("NasalAndSinusStructures",
            "EthmoidAirCells",
            "ParanasalSinuses",
            "NasalSeptum");

        // CARTILAGES
        AddByNames("MajorAlarCartilages", skullModels,
            "Major alar cartilage.l",
            "Major alar cartilage.r");

        AddByNames("LateralNasalSeptalProcesses", skullModels,
            "Lateral process of nasal septal cartilage.l",
            "Lateral process of nasal septal cartilage.r");

        CombineNodes("NasalCartilages",
            "MajorAlarCartilages",
            "LateralNasalSeptalProcesses");

        AddByNames("ThyroidCartilage", skullModels,
            "Thyroid cartilage");

        AddByNames("CricoidCartilage", skullModels,
            "Cricoid cartilage");

        AddByNames("ArytenoidCartilages", skullModels,
            "Arytenoid cartilage.l",
            "Arytenoid cartilage.r");

        AddByNames("CorniculateCartilages", skullModels,
            "Corniculate cartilage.l",
            "Corniculate cartilage.r");

        CombineNodes("LaryngealCartilages",
            "ThyroidCartilage",
            "CricoidCartilage",
            "ArytenoidCartilages",
            "CorniculateCartilages");

        CombineNodes("Cartilages",
            "NasalCartilages",
            "LaryngealCartilages");

        // OTHER
        AddByNames("HyoidBone", skullModels,
            "Hyoid bone");

        // =============================================
        // LEVEL 1 SKULL NODE
        // =============================================

        CombineNodes("Skull",
            "CranialBones",
            "FacialBones",
            "AuditoryOssicles",
            "Teeth",
            "NasalAndSinusStructures",
            "Cartilages",
            "HyoidBone");

        // Các node đã khai báo nhưng không tìm thấy model
        // vẫn tồn tại để điều hướng có thể kiểm tra được.
        AddEmptyIfMissing(
            "CranialBones",
            "FacialBones",
            "AuditoryOssicles",
            "Teeth",
            "UpperTeeth",
            "LowerTeeth",
            "UpperIncisors",
            "UpperCanines",
            "UpperPremolars",
            "UpperMolars",
            "LowerIncisors",
            "LowerCanines",
            "LowerPremolars",
            "LowerMolars",
            "NasalAndSinusStructures",
            "EthmoidAirCells",
            "AnteriorEthmoidCells",
            "MiddleEthmoidCells",
            "PosteriorEthmoidCells",
            "ParanasalSinuses",
            "FrontalSinus",
            "SphenoidSinus",
            "NasalSeptum",
            "Cartilages",
            "NasalCartilages",
            "MajorAlarCartilages",
            "LateralNasalSeptalProcesses",
            "LaryngealCartilages",
            "ThyroidCartilage",
            "CricoidCartilage",
            "ArytenoidCartilages",
            "CorniculateCartilages",
            "HyoidBone",
            "FrontalBone",
            "ParietalBones",
            "TemporalBones",
            "OccipitalBone",
            "SphenoidBone",
            "EthmoidBone",
            "Mandible",
            "Maxillae",
            "ZygomaticBones",
            "NasalBones",
            "LacrimalBones",
            "PalatineBones",
            "InferiorNasalConchae",
            "Vomer",
            "Malleus",
            "Incus",
            "Stapes",
            "Skull",
            
            "CervicalVertebrae",
            "UpperCervicalVertebrae",
            "LowerCervicalVertebrae",
            "Atlas",
            "Axis",
            "VertebraC3",
            "VertebraC4",
            "VertebraC5",
            "VertebraC6",
            "VertebraC7",

            "ThoracicVertebrae",
            "UpperThoracicVertebrae",
            "LowerThoracicVertebrae",
            "VertebraT1",
            "VertebraT2",
            "VertebraT3",
            "VertebraT4",
            "VertebraT5",
            "VertebraT6",
            "VertebraT7",
            "VertebraT8",
            "VertebraT9",
            "VertebraT10",
            "VertebraT11",
            "VertebraT12",

            "LumbarVertebrae",
            "UpperLumbarVertebrae",
            "LowerLumbarVertebrae",
            "VertebraL1",
            "VertebraL2",
            "VertebraL3",
            "VertebraL4",
            "VertebraL5",
            "Sacrum",
            "Coccyx",
            "VertebralColumn",
            "Sternum",
            "Manubrium",
            "BodyOfSternum",
            "XiphoidProcess",
            "Ribs",
            "FirstRib", "FirstRibLeft", "FirstRibRight",
            "SecondRib", "SecondRibLeft", "SecondRibRight",
            "ThirdRib", "ThirdRibLeft", "ThirdRibRight",
            "FourthRib", "FourthRibLeft", "FourthRibRight",
            "FifthRib", "FifthRibLeft", "FifthRibRight",
            "SixthRib", "SixthRibLeft", "SixthRibRight",
            "SeventhRib", "SeventhRibLeft", "SeventhRibRight",
            "EighthRib", "EighthRibLeft", "EighthRibRight",
            "NinthRib", "NinthRibLeft", "NinthRibRight",
            "TenthRib", "TenthRibLeft", "TenthRibRight",
            "EleventhRib", "EleventhRibLeft", "EleventhRibRight",
            "TwelfthRib", "TwelfthRibLeft", "TwelfthRibRight",
            "CostalCartilages",
            "FirstCostalCartilage", "FirstCostalCartilageLeft", "FirstCostalCartilageRight",
            "SecondCostalCartilage", "SecondCostalCartilageLeft", "SecondCostalCartilageRight",
            "ThirdCostalCartilage", "ThirdCostalCartilageLeft", "ThirdCostalCartilageRight",
            "FourthCostalCartilage", "FourthCostalCartilageLeft", "FourthCostalCartilageRight",
            "FifthCostalCartilage", "FifthCostalCartilageLeft", "FifthCostalCartilageRight",
            "SixthCostalCartilage", "SixthCostalCartilageLeft", "SixthCostalCartilageRight",
            "SeventhCostalCartilage", "SeventhCostalCartilageLeft", "SeventhCostalCartilageRight",
            "EighthCostalCartilage", "EighthCostalCartilageLeft", "EighthCostalCartilageRight",
            "NinthCostalCartilage", "NinthCostalCartilageLeft", "NinthCostalCartilageRight",
            "TenthCostalCartilage", "TenthCostalCartilageLeft", "TenthCostalCartilageRight",
            "ThoracicCage"
        );

        built = true;

        LogNodeSummary("Skull");
        LogNodeSummary("CranialBones");
        LogNodeSummary("FacialBones");
        LogNodeSummary("Teeth");
        LogNodeSummary("Cartilages");
    }

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

        if (result.Count == 0)
        {
            Debug.LogWarning(
                "[Skeletal L2] Không tìm thấy model cho node: "
                + nodeName
            );
        }
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

    private void AddEmptyIfMissing(params string[] nodeNames)
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
                "[Skeletal L2] "
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
