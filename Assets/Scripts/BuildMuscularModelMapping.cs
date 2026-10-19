using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildMuscularModelMapping : MonoBehaviour
{
    [Header("PROJECT / UI GROUPS")]
    [SerializeField] private AnatomyModelGroup headNeckGroup;
    [SerializeField] private AnatomyModelGroup trunkGroup;
    [SerializeField] private AnatomyModelGroup leftUpperLimbGroup;
    [SerializeField] private AnatomyModelGroup rightUpperLimbGroup;
    [SerializeField] private AnatomyModelGroup leftLowerLimbGroup;
    [SerializeField] private AnatomyModelGroup rightLowerLimbGroup;

    [Header("Z-ANATOMY MODEL ROOT")]
    [SerializeField] private Transform modelRoot;

    private Dictionary<string, AnatomyModelGroup> mapping;

    private void BuildMappingTable()
    {
        mapping = new Dictionary<string, AnatomyModelGroup>(
            StringComparer.OrdinalIgnoreCase
        );

        AddHeadNeck();
        AddTrunk();
        AddLeftUpperLimb();
        AddRightUpperLimb();
        AddLeftLowerLimb();
        AddRightLowerLimb();
    }

    private void Add(string groupName, AnatomyModelGroup group)
    {
        if (group == null)
        {
            Debug.LogError(
                "GROUP NULL: " + groupName
            );

            return;
        }
    }

    private void AddNames(
        AnatomyModelGroup group,
        params string[] names)
    {
        if (group == null)
            return;

        foreach (string name in names)
        {
            if (string.IsNullOrWhiteSpace(name))
                continue;

            if (mapping.ContainsKey(name))
            {
                Debug.LogWarning(
                    "DUPLICATE MAPPING: " + name
                );

                continue;
            }

            mapping.Add(name, group);
        }
    }

        private void AddHeadNeck()
    {
        AddNames(
            headNeckGroup,

            "Anterior belly of digastric muscle.l",
            "Anterior belly of digastric muscle.r",
            "Ary-epiglottic part of oblique arytenoid muscle.l",
            "Ary-epiglottic part of oblique arytenoid muscle.r",
            "Bucinator.l",
            "Bucinator.r",
            "Corrugator supercilii.l",
            "Corrugator supercilii.r",
            "Deep part of masseter.l",
            "Deep part of masseter.r",
            "Depressor anguli oris.l",
            "Depressor anguli oris.r",
            "Depressor labii inferioris.l",
            "Depressor labii inferioris.r",
            "Depressor septi nasi.l",
            "Depressor septi nasi.r",
            "External part of thyro-arytenoid muscle.l",
            "External part of thyro-arytenoid muscle.r",
            "Frontalis muscle.l",
            "Frontalis muscle.r",
            "Genioglossus muscle.l",
            "Genioglossus muscle.r",
            "Geniohyoid muscle.l",
            "Geniohyoid muscle.r",
            "Hyoglossus muscle.l",
            "Hyoglossus muscle.r",
            "Inferior head of lateral pterygoid muscle.l",
            "Inferior head of lateral pterygoid muscle.r",
            "Inferior oblique muscle.l",
            "Inferior oblique muscle.r",
            "Inferior pharyngeal constrictor.l",
            "Inferior pharyngeal constrictor.r",
            "Inferior rectus muscle.l",
            "Inferior rectus muscle.r",
            "Lateral crico-arytenoid muscle.l",
            "Lateral crico-arytenoid muscle.r",
            "Lateral rectus muscle.l",
            "Lateral rectus muscle.r",
            "Levator anguli oris.l",
            "Levator anguli oris.r",
            "Levator labii superioris.l",
            "Levator labii superioris.r",
            "Levator nasolabialis.l",
            "Levator nasolabialis.r",
            "Levator palpebrae superioris.l",
            "Levator palpebrae superioris.r",
            "Longus capitis muscle.l",
            "Longus capitis muscle.r",
            "Longus colli muscle.l",
            "Longus colli muscle.r",
            "Medial pterygoid muscle.l",
            "Medial pterygoid muscle.r",
            "Medial rectus muscle.l",
            "Medial rectus muscle.r",
            "Mentalis muscle.l",
            "Mentalis muscle.r",
            "Middle pharyngeal constrictor.l",
            "Middle pharyngeal constrictor.r",
            "Mylohyoid muscle.l",
            "Mylohyoid muscle.r",
            "Nasalis muscle.l",
            "Nasalis muscle.r",
            "Oblique part of cricothyroid muscle.l",
            "Oblique part of cricothyroid muscle.r",
            "Obliquus inferior capitis muscle.l",
            "Obliquus inferior capitis muscle.r",
            "Obliquus superior capitis muscle.l",
            "Obliquus superior capitis muscle.r",
            "Occipitalis muscle.l",
            "Occipitalis muscle.r",
            "Omohyoid muscle.l",
            "Omohyoid muscle.r",
            "Orbicularis oris muscle.l",
            "Orbicularis oris muscle.r",
            "Orbital part of orbicularis oculi.l",
            "Orbital part of orbicularis oculi.r",
            "Palatopharyngeus muscle.l",
            "Palatopharyngeus muscle.r",
            "Palpebral part of orbicularis oculi.l",
            "Palpebral part of orbicularis oculi.r",
            "Platysma.l",
            "Platysma.r",
            "Posterior belly of digastric muscle.l",
            "Posterior belly of digastric muscle.r",
            "Posterior crico-arytenoid muscle.l",
            "Posterior crico-arytenoid muscle.r",
            "Procerus muscle.l",
            "Procerus muscle.r",
            "Rectus anterior capitis muscle.l",
            "Rectus anterior capitis muscle.r",
            "Rectus lateralis capitis muscle.l",
            "Rectus lateralis capitis muscle.r",
            "Rectus posterior major capitis muscle.l",
            "Rectus posterior major capitis muscle.r",
            "Rectus posterior minor capitis muscle.l",
            "Rectus posterior minor capitis muscle.r",
            "Risorius muscle.l",
            "Risorius muscle.r",
            "Scalenus anterior muscle.l",
            "Scalenus anterior muscle.r",
            "Scalenus medius muscle.l",
            "Scalenus medius muscle.r",
            "Scalenus posterior muscle.l",
            "Scalenus posterior muscle.r",
            "Splenius capitis muscle.l",
            "Splenius capitis muscle.r",
            "Splenius colli muscle.l",
            "Splenius colli muscle.r",
            "Sternocleidomastoid muscle.l",
            "Sternocleidomastoid muscle.r",
            "Sternohyoid muscle.l",
            "Sternohyoid muscle.r",
            "Sternothyroid muscle.l",
            "Sternothyroid muscle.r",
            "Straight part of cricothyroid muscle.l",
            "Straight part of cricothyroid muscle.r",
            "Stylohyoid muscle.l",
            "Stylohyoid muscle.r",
            "Stylopharyngeus muscle.l",
            "Stylopharyngeus muscle.r",
            "Superficial part of masseter.l",
            "Superficial part of masseter.r",
            "Superior head of lateral pterygoid muscle.l",
            "Superior head of lateral pterygoid muscle.r",
            "Superior oblique muscle.l",
            "Superior oblique muscle.r",
            "Superior pharyngeal constrictor.l",
            "Superior pharyngeal constrictor.r",
            "Superior rectus muscle.l",
            "Superior rectus muscle.r",
            "Temporalis muscle.l",
            "Temporalis muscle.r",
            "Temporoparietalis muscle.l",
            "Temporoparietalis muscle.r",
            "Thyro-epiglottic part of thyro-arytenoid muscle.l",
            "Thyro-epiglottic part of thyro-arytenoid muscle.r",
            "Thyrohyoid muscle.l",
            "Thyrohyoid muscle.r",
            "Transverse arytenoid muscle",
            "Zygomaticus major muscle.l",
            "Zygomaticus major muscle.r",
            "Zygomaticus minor muscle.l",
            "Zygomaticus minor muscle.r"
        );
    }

        private void AddTrunk()
    {
        AddNames(
            trunkGroup,

            "(Abdominal part of pectoralis major muscle).l",
            "(Abdominal part of pectoralis major muscle).r",
            "Ascending part of trapezius muscle.l",
            "Ascending part of trapezius muscle.r",
            "Clavicular head of pectoralis major muscle.l",
            "Clavicular head of pectoralis major muscle.r",
            "Coccygeus muscle.l",
            "Coccygeus muscle.r",
            "Descending part of trapezius muscle.l",
            "Descending part of trapezius muscle.r",
            "Diaphragm",
            "Dorsal parts of lateral intertransversarii lumborum muscles.l",
            "Dorsal parts of lateral intertransversarii lumborum muscles.r",
            "External abdominal oblique muscle.l",
            "External abdominal oblique muscle.r",
            "External anal sphincter.l",
            "External anal sphincter.r",
            "External intercostal muscles.l",
            "External intercostal muscles.r",
            "Iliococcygeus muscle.l",
            "Iliococcygeus muscle.r",
            "Iliocostalis colli muscle",
            "Iliocostalis colli muscle.r",
            "Iliocostalis lumborum muscle.l",
            "Iliocostalis lumborum muscle.r",
            "Iliocostalis thoracis muscle.l",
            "Iliocostalis thoracis muscle.r",
            "Innermost intercostal muscles.l",
            "Innermost intercostal muscles.r",
            "Internal abdominal oblique muscle.l",
            "Internal abdominal oblique muscle.r",
            "Internal intercostal muscles.l",
            "Internal intercostal muscles.r",
            "Interspinales colli muscles.l",
            "Interspinales colli muscles.r",
            "Interspinales lumborum muscles.l",
            "Interspinales lumborum muscles.r",
            "Interspinales thoracis muscles.l",
            "Interspinales thoracis muscles.r",
            "Latissimus dorsi muscle.l",
            "Latissimus dorsi muscle.r",
            "Levator scapulae.l",
            "Levator scapulae.r",
            "Longissimus capitis muscle.l",
            "Longissimus capitis muscle.r",
            "Longissimus colli muscle.l",
            "Longissimus colli muscle.r",
            "Longissimus thoracis muscle.l",
            "Longissimus thoracis muscle.r",
            "Multifidus colli muscle.l",
            "Multifidus colli muscle.r",
            "Multifidus lumborum muscle.l",
            "Multifidus lumborum muscle.r",
            "Multifidus thoracis muscle.l",
            "Multifidus thoracis muscle.r",
            "Pectoralis minor muscle.l",
            "Pectoralis minor muscle.r",
            "Pubo-analis muscle.l",
            "Pubo-analis muscle.r",
            "Pubococcygeus muscle.l",
            "Pubococcygeus muscle.r",
            "Pyramidalis muscle.l",
            "Pyramidalis muscle.r",
            "Quadratus lumborum muscle.l",
            "Quadratus lumborum muscle.r",
            "Rectus abdominis muscle.l",
            "Rectus abdominis muscle.r",
            "Rhomboid major muscle.l",
            "Rhomboid major muscle.r",
            "Rhomboid minor muscle.l",
            "Rhomboid minor muscle.r",
            "Rotatores.l",
            "Rotatores.r",
            "Semispinalis colli muscle.l",
            "Semispinalis colli muscle.r",
            "Semispinalis thoracis muscle.l",
            "Semispinalis thoracis muscle.r",
            "Serratus anterior muscle.l",
            "Serratus anterior muscle.r",
            "Serratus posterior inferior muscle.l",
            "Serratus posterior inferior muscle.r",
            "Serratus posterior superior muscle.l",
            "Serratus posterior superior muscle.r",
            "Spinalis capitis muscle.l",
            "Spinalis capitis muscle.r",
            "Spinalis colli muscle.l",
            "Spinalis colli muscle.r",
            "Spinalis thoracis muscle.l",
            "Spinalis thoracis muscle.r",
            "Sternocostal head of pectoralis major muscle.l",
            "Sternocostal head of pectoralis major muscle.r",
            "Subclavius muscle.l",
            "Subclavius muscle.r",
            "Transverse part of trapezius muscle.l",
            "Transverse part of trapezius muscle.r",
            "Transversus abdominis muscle.l",
            "Transversus abdominis muscle.r",
            "Transversus thoracis muscle.l",
            "Transversus thoracis muscle.r",
            "Ventral parts of lateral intertransversarii lumborum muscles.l",
            "Ventral parts of lateral intertransversarii lumborum muscles.r",

            "Levatores breves costarum.l",
            "Levatores breves costarum.r",
            "Levatores longi costarum.l",
            "Levatores longi costarum.r"
        );
    }

        private void AddLeftUpperLimb()
    {
        AddNames(
            leftUpperLimbGroup,

            "Abductor digiti minimi of hand.l",
            "Abductor pollicis brevis.l",
            "Abductor pollicis longus.l",
            "Acromial part of deltoid muscle.l",
            "Anconeus muscle.l",
            "Brachialis muscle.l",
            "Brachioradialis muscle.l",
            "Clavicular part of deltoid muscle.l",
            "Coracobrachialis muscle.l",
            "Deep head of flexor pollicis brevis.l",
            "Deep head of pronator teres.l",
            "Dorsal interossei muscles of hand.l",
            "Extensor carpi radialis brevis.l",
            "Extensor carpi radialis longus.l",
            "Extensor digiti minimi.l",
            "Extensor digitorum.l",
            "Extensor indicis.l",
            "Extensor pollicis brevis.l",
            "Extensor pollicis longus.l",
            "Flexor carpi radialis.l",
            "Flexor digiti minimi of hand.l",
            "Flexor digitorum profundus.l",
            "Flexor pollicis longus.l",
            "Humeral head of extensor carpi ulnaris.l",
            "Humeral head of flexor carpi ulnaris.l",
            "Humero-ulnar head of flexor digitorum superficialis.l",
            "Infraspinatus muscle.l",
            "Lateral head of triceps brachii.l",
            "Long head of biceps brachii.l",
            "Long head of triceps brachii.l",
            "Lumbrical muscles of hand.l",
            "Medial head of triceps brachii.l",
            "Oblique head of adductor pollicis.l",
            "Opponens digiti minimi muscle of hand.l",
            "Opponens pollicis muscle.l",
            "Palmar interossei muscles.l",
            "Palmaris longus muscle.l",
            "Pronator quadratus.l",
            "Radial head of flexor digitorum superficialis.l",
            "Scapular spinal part of deltoid muscle.l",
            "Short head of biceps brachii.l",
            "Subscapularis muscle.l",
            "Superficial head of flexor pollicis brevis.l",
            "Superficial head of pronator teres.l",
            "Supinator.l",
            "Supraspinatus muscle.l",
            "Teres major muscle.l",
            "Teres minor muscle.l",
            "Transverse head of adductor pollicis.l",
            "Ulnar head of extensor carpi ulnaris.l",
            "Ulnar head of flexor carpi ulnaris.l"
        );
    }

        private void AddRightUpperLimb()
    {
        AddNames(
            rightUpperLimbGroup,

            "Abductor digiti minimi of hand.r",
            "Abductor pollicis brevis.r",
            "Abductor pollicis longus.r",
            "Acromial part of deltoid muscle.r",
            "Anconeus muscle.r",
            "Brachialis muscle.r",
            "Brachioradialis muscle.r",
            "Clavicular part of deltoid muscle.r",
            "Coracobrachialis muscle.r",
            "Deep head of flexor pollicis brevis.r",
            "Deep head of pronator teres.r",
            "Dorsal interossei muscles of hand.r",
            "Extensor carpi radialis brevis.r",
            "Extensor carpi radialis longus.r",
            "Extensor digiti minimi.r",
            "Extensor digitorum.r",
            "Extensor indicis.r",
            "Extensor pollicis brevis.r",
            "Extensor pollicis longus.r",
            "Flexor carpi radialis.r",
            "Flexor digiti minimi of hand.r",
            "Flexor digitorum profundus.r",
            "Flexor pollicis longus.r",
            "Humeral head of extensor carpi ulnaris.r",
            "Humeral head of flexor carpi ulnaris.r",
            "Humero-ulnar head of flexor digitorum superficialis.r",
            "Infraspinatus muscle.r",
            "Lateral head of triceps brachii.r",
            "Long head of biceps brachii.r",
            "Long head of triceps brachii.r",
            "Lumbrical muscles of hand.r",
            "Medial head of triceps brachii.r",
            "Oblique head of adductor pollicis.r",
            "Opponens digiti minimi muscle of hand.r",
            "Opponens pollicis muscle.r",
            "Palmar interossei muscles.r",
            "Palmaris longus muscle.r",
            "Pronator quadratus.r",
            "Radial head of flexor digitorum superficialis.r",
            "Scapular spinal part of deltoid muscle.r",
            "Short head of biceps brachii.r",
            "Subscapularis muscle.r",
            "Superficial head of flexor pollicis brevis.r",
            "Superficial head of pronator teres.r",
            "Supinator.r",
            "Supraspinatus muscle.r",
            "Teres major muscle.r",
            "Teres minor muscle.r",
            "Transverse head of adductor pollicis.r",
            "Ulnar head of extensor carpi ulnaris.r",
            "Ulnar head of flexor carpi ulnaris.r"
        );
    }

        private void AddLeftLowerLimb()
    {
        AddNames(
            leftLowerLimbGroup,

            "(Adductor minimus).l",
            "(Opponens digiti minimi muscle of foot).l",
            "Abductor digiti minimi of foot.l",
            "Abductor hallucis.l",
            "Adductor brevis.l",
            "Adductor longus.l",
            "Adductor magnus.l",
            "Dorsal interossei muscles of foot.l",
            "Extensor digitorum brevis.l",
            "Extensor digitorum longus.l",
            "Extensor hallucis brevis.l",
            "Extensor hallucis longus.l",
            "Fibularis brevis muscle.l",
            "Fibularis longus muscle.l",
            "Fibularis tertius muscle.l",
            "Flexor digiti minimi of foot.l",
            "Flexor digitorum brevis.l",
            "Flexor digitorum longus.l",
            "Flexor hallucis longus.l",
            "Gluteus maximus muscle.l",
            "Gluteus medius muscle.l",
            "Gluteus minimus muscle.l",
            "Gracilis muscle.l",
            "Iliacus muscle.l",
            "Inferior gemellus muscle.l",
            "Lateral head of flexor hallucis brevis.l",
            "Lateral head of gastrocnemius.l",
            "Long head of biceps femoris.l",
            "Lumbrical muscles of foot.l",
            "Medial head of flexor hallucis brevis.l",
            "Medial head of gastrocnemius.l",
            "Oblique head of adductor hallucis.l",
            "Obturator externus.l",
            "Obturator internus.l",
            "Pectineus muscle.l",
            "Piriformis muscle.l",
            "Plantaris muscle.l",
            "Popliteus muscle.l",
            "Psoas major.l",
            "Quadratus femoris muscle.l",
            "Quadratus plantae muscle.l",
            "Rectus femoris muscle.l",
            "Sartorius muscle.l",
            "Semimembranosus muscle.l",
            "Semitendinosus muscle.l",
            "Short head of biceps femoris.l",
            "Soleus muscle.l",
            "Superior gemellus muscle.l",
            "Tibialis anterior muscle.l",
            "Tibialis posterior muscle.l",
            "Transverse head of adductor hallucis.l",
            "Vastus intermedius muscle.l",
            "Vastus lateralis muscle.l",
            "Vastus medialis muscle.l",

            "Plantar interossei muscles.l"
        );
    }

        private void AddRightLowerLimb()
    {
        AddNames(
            rightLowerLimbGroup,

            "(Adductor minimus).r",
            "(Opponens digiti minimi muscle of foot).r",
            "Abductor digiti minimi of foot.r",
            "Abductor hallucis.r",
            "Adductor brevis.r",
            "Adductor longus.r",
            "Adductor magnus.r",
            "Dorsal interossei muscles of foot.r",
            "Extensor digitorum brevis.r",
            "Extensor digitorum longus.r",
            "Extensor hallucis brevis.r",
            "Extensor hallucis longus.r",
            "Fibularis brevis muscle.r",
            "Fibularis longus muscle.r",
            "Fibularis tertius muscle.r",
            "Flexor digiti minimi of foot.r",
            "Flexor digitorum brevis.r",
            "Flexor digitorum longus.r",
            "Flexor hallucis longus.r",
            "Gluteus maximus muscle.r",
            "Gluteus medius muscle.r",
            "Gluteus minimus muscle.r",
            "Gracilis muscle.r",
            "Iliacus muscle.r",
            "Inferior gemellus muscle.r",
            "Lateral head of flexor hallucis brevis.r",
            "Lateral head of gastrocnemius.r",
            "Long head of biceps femoris.r",
            "Lumbrical muscles of foot.r",
            "Medial head of flexor hallucis brevis.r",
            "Medial head of gastrocnemius.r",
            "Oblique head of adductor hallucis.r",
            "Obturator externus.r",
            "Obturator internus.r",
            "Pectineus muscle.r",
            "Piriformis muscle.r",
            "Plantaris muscle.r",
            "Popliteus muscle.r",
            "Psoas major.r",
            "Quadratus femoris muscle.r",
            "Quadratus plantae muscle.r",
            "Rectus femoris muscle.r",
            "Sartorius muscle.r",
            "Semimembranosus muscle.r",
            "Semitendinosus muscle.r",
            "Short head of biceps femoris.r",
            "Soleus muscle.r",
            "Superior gemellus muscle.r",
            "Tibialis anterior muscle.r",
            "Tibialis posterior muscle.r",
            "Transverse head of adductor hallucis.r",
            "Vastus intermedius muscle.r",
            "Vastus lateralis muscle.r",
            "Vastus medialis muscle.r",

            "Plantar interossei muscles.r"
        );
    }

        [ContextMenu("BUILD MUSCULAR MODEL MAPPING")]
    public void BuildMuscularModelMappingNow()
    {
        if (modelRoot == null)
        {
            Debug.LogError(
                "MODEL ROOT CHUA DUOC GAN."
            );

            return;
        }

        if (headNeckGroup == null ||
            trunkGroup == null ||
            leftUpperLimbGroup == null ||
            rightUpperLimbGroup == null ||
            leftLowerLimbGroup == null ||
            rightLowerLimbGroup == null)
        {
            Debug.LogError(
                "MOT HOAC NHIEU GROUP CHUA DUOC GAN."
            );

            return;
        }

        BuildMappingTable();

        ClearAllGroups();

        List<Transform> allModels =
            new List<Transform>();

        CollectTransforms(
            modelRoot,
            allModels
        );

        int linked = 0;
        int missing = 0;
        int ignored = 0;

        HashSet<Transform> linkedSet =
            new HashSet<Transform>();

        foreach (Transform model in allModels)
        {
            if (model == null)
                continue;

            string modelName =
                model.name;

            if (modelName == "Muscular system.g")
            {
                ignored++;
                continue;
            }

            AnatomyModelGroup targetGroup;

            if (!mapping.TryGetValue(
                modelName,
                out targetGroup))
            {
                missing++;

                Debug.LogWarning(
                    "MUSCLE NOT MAPPED: "
                    + modelName
                );

                continue;
            }

            if (!linkedSet.Contains(model))
            {
                targetGroup.AddModel(model);
                linkedSet.Add(model);
                linked++;
            }
        }

        Debug.Log(
            "===================================="
        );

        Debug.Log(
            "Z-ANATOMY EXACT MUSCLE MAPPING DONE"
        );

        Debug.Log(
            "FOUND MODEL OBJECTS = "
            + allModels.Count
        );

        Debug.Log(
            "LINKED = "
            + linked
        );

        Debug.Log(
            "MISSING = "
            + missing
        );

        Debug.Log(
            "IGNORED = "
            + ignored
        );

        Debug.Log(
            "HEAD_NECK = "
            + headNeckGroup.LinkedModels.Count
        );

        Debug.Log(
            "TRUNK = "
            + trunkGroup.LinkedModels.Count
        );

        Debug.Log(
            "LEFT_UPPER_LIMB = "
            + leftUpperLimbGroup.LinkedModels.Count
        );

        Debug.Log(
            "RIGHT_UPPER_LIMB = "
            + rightUpperLimbGroup.LinkedModels.Count
        );

        Debug.Log(
            "LEFT_LOWER_LIMB = "
            + leftLowerLimbGroup.LinkedModels.Count
        );

        Debug.Log(
            "RIGHT_LOWER_LIMB = "
            + rightLowerLimbGroup.LinkedModels.Count
        );

        Debug.Log(
            "===================================="
        );
    }

    private void ClearAllGroups()
    {
        headNeckGroup.ClearModels();
        trunkGroup.ClearModels();
        leftUpperLimbGroup.ClearModels();
        rightUpperLimbGroup.ClearModels();
        leftLowerLimbGroup.ClearModels();
        rightLowerLimbGroup.ClearModels();
    }

    private void CollectTransforms(
        Transform root,
        List<Transform> result)
    {
        if (root == null)
            return;

        foreach (Transform child in root)
        {
            if (child == null)
                continue;

            result.Add(child);

            CollectTransforms(
                child,
                result
            );
        }
    }
}