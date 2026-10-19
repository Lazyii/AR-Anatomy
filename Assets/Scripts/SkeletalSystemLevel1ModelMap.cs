
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkeletalSystemLevel1ModelMap : MonoBehaviour
{
    [Header("Root: ARAnatomyRoot/SkeletalSystem")]
    [SerializeField] private Transform skeletalSystem;

    [Header("Existing Level 1 groups - do not rename")]
    [SerializeField] private AnatomyModelGroup skullGroup;
    [SerializeField] private AnatomyModelGroup vertebralColumnGroup;
    [SerializeField] private AnatomyModelGroup thoracicCageGroup;
    [SerializeField] private AnatomyModelGroup pectoralGirdleGroup;
    [SerializeField] private AnatomyModelGroup upperLimbGroup;
    [SerializeField] private AnatomyModelGroup pelvicGirdleGroup;
    [SerializeField] private AnatomyModelGroup lowerLimbGroup;

    private readonly Dictionary<string, AnatomyModelGroup> groupByTag =
        new Dictionary<string, AnatomyModelGroup>();

    // Skull: xương đầu, răng, xương con tai, sụn và xoang vùng đầu.
    private static readonly string[] SkullModels =
    {
        "Anterior cells of ethmoid bone.l",
        "Anterior cells of ethmoid bone.r",
        "Arytenoid cartilage.l",
        "Arytenoid cartilage.r",
        "Corniculate cartilage.l",
        "Corniculate cartilage.r",
        "Cricoid cartilage",
        "Ethmoid bone",
        "Frontal bone",
        "Hyoid bone",
        "Incus.l",
        "Incus.r",
        "Inferior nasal concha bone.l",
        "Inferior nasal concha bone.r",
        "Lacrimal bone.l",
        "Lacrimal bone.r",
        "Lateral process of nasal septal cartilage.l",
        "Lateral process of nasal septal cartilage.r",
        "Lower canine.l",
        "Lower canine.r",
        "Lower first molar tooth.l",
        "Lower first molar tooth.r",
        "Lower first premolar.l",
        "Lower first premolar.r",
        "Lower lateral incisor.l",
        "Lower lateral incisor.r",
        "Lower medial incisor.l",
        "Lower medial incisor.r",
        "Lower second molar tooth.l",
        "Lower second molar tooth.r",
        "Lower second premolar.l",
        "Lower second premolar.r",
        "Major alar cartilage.l",
        "Major alar cartilage.r",
        "Malleus.l",
        "Malleus.r",
        "Mandible",
        "Maxilla.l",
        "Maxilla.r",
        "Middle cells of ethmoid bone.l",
        "Middle cells of ethmoid bone.r",
        "Nasal bone.l",
        "Nasal bone.r",
        "Nasal septal cartilage",
        "Occipital bone",
        "Palatine bone.l",
        "Palatine bone.r",
        "Parietal bone.l",
        "Parietal bone.r",
        "Posterior cells of ethmoid bone.l",
        "Posterior cells of ethmoid bone.r",
        "Sinus of frontal bone",
        "Sinus of sphenoid bone",
        "Sphenoid bone",
        "Stapes.l",
        "Stapes.r",
        "Temporal bone.l",
        "Temporal bone.r",
        "Thyroid cartilage",
        "Upper canine.l",
        "Upper canine.r",
        "Upper first molar tooth.l",
        "Upper first molar tooth.r",
        "Upper first premolar.l",
        "Upper first premolar.r",
        "Upper lateral incisor.l",
        "Upper lateral incisor.r",
        "Upper medial incisor.l",
        "Upper medial incisor.r",
        "Upper second molar tooth.l",
        "Upper second molar tooth.r",
        "Upper second premolar.l",
        "Upper second premolar.r",
        "Vomer",
        "Zygomatic bone.l",
        "Zygomatic bone.r"
    };

    // Vertebral column: các đốt sống, xương cùng và xương cụt.
    private static readonly string[] VertebralColumnModels =
    {
        "Atlas (C1)",
        "Axis (C2)",
        "Coccyx",
        "Sacrum",
        "Vertebra C3",
        "Vertebra C4",
        "Vertebra C5",
        "Vertebra C6",
        "Vertebra C7",
        "Vertebra L1",
        "Vertebra L2",
        "Vertebra L3",
        "Vertebra L4",
        "Vertebra L5",
        "Vertebra T1",
        "Vertebra T10",
        "Vertebra T11",
        "Vertebra T12",
        "Vertebra T2",
        "Vertebra T3",
        "Vertebra T4",
        "Vertebra T5",
        "Vertebra T6",
        "Vertebra T7",
        "Vertebra T8",
        "Vertebra T9"
    };

    // Thoracic cage: xương ức, xương sườn và sụn sườn.
    private static readonly string[] ThoracicCageModels =
    {
        "Body of sternum",
        "Costal cartilage of eighth rib.l",
        "Costal cartilage of eighth rib.r",
        "Costal cartilage of fifth rib.l",
        "Costal cartilage of fifth rib.r",
        "Costal cartilage of first rib.l",
        "Costal cartilage of first rib.r",
        "Costal cartilage of fourth rib.l",
        "Costal cartilage of fourth rib.r",
        "Costal cartilage of ninth rib.l",
        "Costal cartilage of ninth rib.r",
        "Costal cartilage of second rib.l",
        "Costal cartilage of second rib.r",
        "Costal cartilage of seventh rib.l",
        "Costal cartilage of seventh rib.r",
        "Costal cartilage of sixth rib.l",
        "Costal cartilage of sixth rib.r",
        "Costal cartilage of tenth rib.l",
        "Costal cartilage of tenth rib.r",
        "Costal cartilage of third rib.l",
        "Costal cartilage of third rib.r",
        "Eighth rib.l",
        "Eighth rib.r",
        "Eleventh rib.l",
        "Eleventh rib.r",
        "Fifth rib.l",
        "Fifth rib.r",
        "First rib.l",
        "First rib.r",
        "Fourth rib.l",
        "Fourth rib.r",
        "Manubrium of sternum",
        "Ninth rib.l",
        "Ninth rib.r",
        "Second rib.l",
        "Second rib.r",
        "Seventh rib.l",
        "Seventh rib.r",
        "Sixth rib.l",
        "Sixth rib.r",
        "Tenth rib.l",
        "Tenth rib.r",
        "Twelfth rib.l",
        "Twelfth rib.r",
        "Third rib.l",
        "Third rib.r",
        "Xiphoid process"
    };

    // Pectoral girdle: xương đòn và xương vai.
    private static readonly string[] PectoralGirdleModels =
    {
        "Clavicle.l",
        "Clavicle.r",
        "Scapula.l",
        "Scapula.r"
    };

    // Upper limb: cánh tay, cẳng tay, cổ tay, bàn tay và ngón tay.
    private static readonly string[] UpperLimbModels =
    {
        "Capitate bone.l",
        "Capitate bone.r",
        "Distal phalanx of fifth finger of hand.l",
        "Distal phalanx of fifth finger of hand.r",
        "Distal phalanx of first finger of hand.l",
        "Distal phalanx of first finger of hand.r",
        "Distal phalanx of fourth finger of hand.l",
        "Distal phalanx of fourth finger of hand.r",
        "Distal phalanx of second finger of hand.l",
        "Distal phalanx of second finger of hand.r",
        "Distal phalanx of third finger of hand.l",
        "Distal phalanx of third finger of hand.r",
        "Fifth metacarpal bone.l",
        "Fifth metacarpal bone.r",
        "First metacarpal bone.l",
        "First metacarpal bone.r",
        "Fourth metacarpal bone.l",
        "Fourth metacarpal bone.r",
        "Hamate bone.l",
        "Hamate bone.r",
        "Humerus.l",
        "Humerus.r",
        "Lunate bone.l",
        "Lunate bone.r",
        "Middle phalanx of fifth finger of hand.l",
        "Middle phalanx of fifth finger of hand.r",
        "Middle phalanx of fourth finger of hand.l",
        "Middle phalanx of fourth finger of hand.r",
        "Middle phalanx of second finger of hand.l",
        "Middle phalanx of second finger of hand.r",
        "Middle phalanx of third finger of hand.l",
        "Middle phalanx of third finger of hand.r",
        "Pisiform bone.l",
        "Pisiform bone.r",
        "Proximal phalanx of fifth finger of hand.l",
        "Proximal phalanx of fifth finger of hand.r",
        "Proximal phalanx of first finger of hand.l",
        "Proximal phalanx of first finger of hand.r",
        "Proximal phalanx of fourth finger of hand.l",
        "Proximal phalanx of fourth finger of hand.r",
        "Proximal phalanx of second finger of hand.l",
        "Proximal phalanx of second finger of hand.r",
        "Proximal phalanx of third finger of hand.l",
        "Proximal phalanx of third finger of hand.r",
        "Radius.l",
        "Radius.r",
        "Scaphoid bone.l",
        "Scaphoid bone.r",
        "Second metacarpal bone.l",
        "Second metacarpal bone.r",
        "Third metacarpal bone.l",
        "Third metacarpal bone.r",
        "Trapezium bone.l",
        "Trapezium bone.r",
        "Trapezoid bone.l",
        "Trapezoid bone.r",
        "Triquetrum bone.l",
        "Triquetrum bone.r",
        "Ulna.l",
        "Ulna.r"
    };

    // Pelvic girdle: xương chậu hai bên.
    private static readonly string[] PelvicGirdleModels =
    {
        "Hip bone.l",
        "Hip bone.r"
    };

    // Lower limb: đùi, cẳng chân, cổ chân, bàn chân và ngón chân.
    private static readonly string[] LowerLimbModels =
    {
        "Calcaneus.l",
        "Calcaneus.r",
        "Cuboid bone.l",
        "Cuboid bone.r",
        "Distal phalanx of fifth finger of foot.l",
        "Distal phalanx of fifth finger of foot.r",
        "Distal phalanx of first finger of foot.l",
        "Distal phalanx of first finger of foot.r",
        "Distal phalanx of fourth finger of foot.l",
        "Distal phalanx of fourth finger of foot.r",
        "Distal phalanx of second finger of foot.l",
        "Distal phalanx of second finger of foot.r",
        "Distal phalanx of third finger of foot.l",
        "Distal phalanx of third finger of foot.r",
        "Femur.l",
        "Femur.r",
        "Fibula.l",
        "Fibula.r",
        "Fifth metatarsal bone.l",
        "Fifth metatarsal bone.r",
        "First metatarsal bone.l",
        "First metatarsal bone.r",
        "Fourth metatarsal bone.l",
        "Fourth metatarsal bone.r",
        "Intermediate cuneiform bone.l",
        "Intermediate cuneiform bone.r",
        "Lateral cuneiform bone.l",
        "Lateral cuneiform bone.r",
        "Medial cuneiform bone.l",
        "Medial cuneiform bone.r",
        "Middle phalanx of fifth finger of foot.l",
        "Middle phalanx of fifth finger of foot.r",
        "Middle phalanx of fourth finger of foot.l",
        "Middle phalanx of fourth finger of foot.r",
        "Middle phalanx of second finger of foot.l",
        "Middle phalanx of second finger of foot.r",
        "Middle phalanx of third finger of foot.l",
        "Middle phalanx of third finger of foot.r",
        "Navicular bone.l",
        "Navicular bone.r",
        "Patella.l",
        "Patella.r",
        "Proximal phalanx of fifth finger of foot.l",
        "Proximal phalanx of fifth finger of foot.r",
        "Proximal phalanx of first finger of foot.l",
        "Proximal phalanx of first finger of foot.r",
        "Proximal phalanx of fourth finger of foot.l",
        "Proximal phalanx of fourth finger of foot.r",
        "Proximal phalanx of second finger of foot.l",
        "Proximal phalanx of second finger of foot.r",
        "Proximal phalanx of third finger of foot.l",
        "Proximal phalanx of third finger of foot.r",
        "Second metatarsal bone.l",
        "Second metatarsal bone.r",
        "Sesamoid bones of foot.l",
        "Sesamoid bones of foot.r",
        "Talus.l",
        "Talus.r",
        "Third metatarsal bone.l",
        "Third metatarsal bone.r",
        "Tibia.l",
        "Tibia.r"
    };

    [ContextMenu("Build SkeletalSystem Level 1 Map")]
    public void Build()
    {
        if (skeletalSystem == null)
        {
            Debug.LogError(
                "[SkeletalSystem] Chưa gán ARAnatomyRoot/SkeletalSystem."
            );
            return;
        }

        groupByTag.Clear();

        RegisterGroup("Skull", skullGroup);
        RegisterGroup("VertebralColumn", vertebralColumnGroup);
        RegisterGroup("ThoracicCage", thoracicCageGroup);
        RegisterGroup("PectoralGirdle", pectoralGirdleGroup);
        RegisterGroup("UpperLimb", upperLimbGroup);
        RegisterGroup("PelvicGirdle", pelvicGirdleGroup);
        RegisterGroup("LowerLimb", lowerLimbGroup);

        foreach (AnatomyModelGroup group in groupByTag.Values)
        {
            if (group != null)
                group.ClearModels();
        }

        Dictionary<string, Transform> modelsByName =
            new Dictionary<string, Transform>(StringComparer.Ordinal);

        // Chỉ lấy các object cấp 1 trực tiếp dưới SkeletalSystem.
        foreach (Transform model in skeletalSystem)
        {
            if (modelsByName.ContainsKey(model.name))
            {
                Debug.LogError(
                    "[SkeletalSystem] Trùng tên object cấp 1: " + model.name
                );
                continue;
            }

            modelsByName.Add(model.name, model);
        }

        HashSet<string> assignedNames =
            new HashSet<string>(StringComparer.Ordinal);

        AssignNames("Skull", SkullModels, modelsByName, assignedNames);
        AssignNames("VertebralColumn", VertebralColumnModels, modelsByName, assignedNames);
        AssignNames("ThoracicCage", ThoracicCageModels, modelsByName, assignedNames);
        AssignNames("PectoralGirdle", PectoralGirdleModels, modelsByName, assignedNames);
        AssignNames("UpperLimb", UpperLimbModels, modelsByName, assignedNames);
        AssignNames("PelvicGirdle", PelvicGirdleModels, modelsByName, assignedNames);
        AssignNames("LowerLimb", LowerLimbModels, modelsByName, assignedNames);

        foreach (KeyValuePair<string, Transform> entry in modelsByName)
        {
            if (!assignedNames.Contains(entry.Key))
            {
                Debug.LogError(
                    "[SkeletalSystem] CHƯA MAPPING: " + entry.Key
                );
            }
        }

        Debug.Log(
            $"[SkeletalSystem] Tổng model cấp 1: {modelsByName.Count}; " +
            $"đã mapping: {assignedNames.Count}; " +
            $"chưa mapping: {modelsByName.Count - assignedNames.Count}."
        );
    }

    private void RegisterGroup(string tag, AnatomyModelGroup group)
    {
        if (group == null)
        {
            Debug.LogError(
                "[SkeletalSystem] Chưa gán AnatomyModelGroup cho tag: " + tag
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
                "[SkeletalSystem] Không có group được gán cho tag: " + tag
            );
            return;
        }

        foreach (string name in names)
        {
            if (!modelsByName.TryGetValue(name, out Transform model))
            {
                Debug.LogError(
                    "[SkeletalSystem] Không tìm thấy model: " + name +
                    " (tag: " + tag + ")"
                );
                continue;
            }

            if (!assignedNames.Add(name))
            {
                Debug.LogError(
                    "[SkeletalSystem] Model bị khai báo mapping nhiều lần: " +
                    name
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
            case "Skull":
                return skullGroup;

            case "VertebralColumn":
                return vertebralColumnGroup;

            case "ThoracicCage":
                return thoracicCageGroup;

            case "PectoralGirdle":
                return pectoralGirdleGroup;

            case "UpperLimb":
                return upperLimbGroup;

            case "PelvicGirdle":
                return pelvicGirdleGroup;

            case "LowerLimb":
                return lowerLimbGroup;

            default:
                return null;
        }
    }
}
