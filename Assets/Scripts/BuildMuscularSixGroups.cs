using UnityEngine;
using System.Collections.Generic;

public class BuildMuscularSixGroups : MonoBehaviour
{
    private Transform headNeck;
    private Transform trunk;
    private Transform leftUpperLimb;
    private Transform rightUpperLimb;
    private Transform leftLowerLimb;
    private Transform rightLowerLimb;

    private readonly HashSet<string> usedObjects =
        new HashSet<string>();

    [ContextMenu("BUILD 6 MUSCULAR GROUPS")]
    public void BuildGroups()
    {
        CreateGroups();

        usedObjects.Clear();

        List<Transform> models =
            new List<Transform>();

        foreach (Transform child in transform)
        {
            if (IsGroup(child))
                continue;

            models.Add(child);
        }

        int count = 0;

        foreach (Transform model in models)
        {
            string group = GetTargetGroup(model.name);

            if (string.IsNullOrEmpty(group))
                continue;

            Transform target = GetGroup(group);

            if (target == null)
                continue;

            model.SetParent(target, true);

            usedObjects.Add(model.name);
            count++;
        }

        Debug.Log(
            "BUILD 6 GROUPS DONE. Added: " +
            count
        );
    }

    private void CreateGroups()
    {
        headNeck = GetOrCreateGroup("HeadNeck");
        trunk = GetOrCreateGroup("Trunk");

        leftUpperLimb =
            GetOrCreateGroup("LeftUpperLimb");

        rightUpperLimb =
            GetOrCreateGroup("RightUpperLimb");

        leftLowerLimb =
            GetOrCreateGroup("LeftLowerLimb");

        rightLowerLimb =
            GetOrCreateGroup("RightLowerLimb");
    }

    private Transform GetOrCreateGroup(string groupName)
    {
        Transform existing =
            transform.Find(groupName);

        if (existing != null)
            return existing;

        GameObject group =
            new GameObject(groupName);

        group.transform.SetParent(
            transform,
            false
        );

        return group.transform;
    }

    private bool IsGroup(Transform t)
    {
        return
            t.name == "HeadNeck" ||
            t.name == "Trunk" ||
            t.name == "LeftUpperLimb" ||
            t.name == "RightUpperLimb" ||
            t.name == "LeftLowerLimb" ||
            t.name == "RightLowerLimb";
    }

    private Transform GetGroup(string group)
    {
        switch (group)
        {
            case "HeadNeck":
                return headNeck;

            case "Trunk":
                return trunk;

            case "LeftUpperLimb":
                return leftUpperLimb;

            case "RightUpperLimb":
                return rightUpperLimb;

            case "LeftLowerLimb":
                return leftLowerLimb;

            case "RightLowerLimb":
                return rightLowerLimb;
        }

        return null;
    }

    private string GetTargetGroup(string name)
    {
        string n = name.ToLower();

        // --------------------------------------------------
        // IGNORE NON-MUSCLE STRUCTURES
        // --------------------------------------------------

        if (ContainsAny(n,
            "fascia",
            "bursa",
            "tendon sheath",
            "tendinous arch",
            "retinaculum",
            "aponeurosis",
            "ligament",
            "intermuscular septum",
            "synovial sheath",
            "tarsus",
            "tendon of"))
        {
            return null;
        }

        // --------------------------------------------------
        // HEAD / FACE
        // --------------------------------------------------

        if (ContainsAny(n,
            "masseter",
            "temporalis",
            "pterygoid",
            "orbicularis oculi",
            "orbicularis oris",
            "zygomaticus",
            "buccinator",
            "risorius",
            "mentalis",
            "procerus",
            "nasalis",
            "platysma",
            "frontalis",
            "epicranius",
            "auricular",
            "masticatory",
            "muscle of tongue",
            "tongue",
            "extra-ocular",
            "superior rectus",
            "inferior rectus",
            "medial rectus",
            "lateral rectus",
            "superior oblique",
            "inferior oblique",
            "levator palpebrae"))
        {
            return "HeadNeck";
        }

        // --------------------------------------------------
        // NECK
        // --------------------------------------------------

        if (ContainsAny(n,
            "sternocleidomastoid",
            "scalenus",
            "longus capitis",
            "longus colli",
            "rectus anterior capitis",
            "rectus lateralis capitis",
            "rectus posterior capitis",
            "splenius",
            "suboccipital",
            "levator scapulae",
            "digastric",
            "stylohyoid",
            "mylohyoid",
            "geniohyoid",
            "sternohyoid",
            "sternothyroid",
            "thyrohyoid",
            "omohyoid",
            "infrahyoid",
            "suprahyoid",
            "pharyngeal",
            "pharynx",
            "laryngeal",
            "larynx",
            "cricothyroid",
            "arytenoid",
            "thyro-arytenoid"))
        {
            return "HeadNeck";
        }

        // --------------------------------------------------
        // UPPER LIMB
        // --------------------------------------------------

        if (ContainsAny(n,
            "deltoid",
            "supraspinatus",
            "infraspinatus",
            "subscapularis",
            "teres major",
            "teres minor",
            "pectoralis major",
            "pectoralis minor",
            "subclavius",
            "coracobrachialis",
            "biceps brachii",
            "brachialis",
            "brachioradialis",
            "triceps brachii",
            "anconeus",
            "pronator teres",
            "pronator quadratus",
            "supinator",
            "flexor carpi",
            "extensor carpi",
            "flexor digitorum",
            "extensor digitorum",
            "flexor pollicis",
            "extensor pollicis",
            "flexor digiti",
            "extensor digiti",
            "abductor pollicis",
            "adductor pollicis",
            "opponens pollicis",
            "opponens digiti",
            "abductor digiti",
            "lumbrical",
            "palmar interossei",
            "dorsal interossei",
            "interossei of hand",
            "muscles of hand",
            "muscles of upper limb"))
        {
            return GetSideGroup(
                n,
                "LeftUpperLimb",
                "RightUpperLimb"
            );
        }

        // --------------------------------------------------
        // LOWER LIMB
        // --------------------------------------------------

        if (ContainsAny(n,
            "gluteus",
            "piriformis",
            "obturator internus",
            "obturator externus",
            "gemellus",
            "quadratus femoris",
            "iliopsoas",
            "iliacus",
            "psoas",
            "pectineus",
            "sartorius",
            "rectus femoris",
            "vastus",
            "adductor",
            "gracilis",
            "semitendinosus",
            "semimembranosus",
            "biceps femoris",
            "tensor fasciae latae",
            "popliteus",
            "gastrocnemius",
            "soleus",
            "plantaris",
            "tibialis anterior",
            "tibialis posterior",
            "fibularis",
            "peroneus",
            "flexor hallucis",
            "flexor digitorum longus",
            "extensor hallucis",
            "extensor digitorum longus",
            "quadratus plantae",
            "abductor hallucis",
            "adductor hallucis",
            "flexor hallucis brevis",
            "flexor digitorum brevis",
            "extensor digitorum brevis",
            "extensor hallucis brevis",
            "dorsal interossei of foot",
            "plantar interossei",
            "muscles of foot",
            "muscles of lower limb"))
        {
            return GetSideGroup(
                n,
                "LeftLowerLimb",
                "RightLowerLimb"
            );
        }

        // --------------------------------------------------
        // TRUNK
        // --------------------------------------------------

        if (ContainsAny(n,
            "trapezius",
            "rhomboid",
            "latissimus dorsi",
            "erector spinae",
            "iliocostalis",
            "longissimus",
            "spinalis",
            "multifidus",
            "rotatores",
            "interspinales",
            "intertransversarii",
            "semispinalis",
            "quadratus lumborum",
            "serratus anterior",
            "serratus posterior",
            "intercostal",
            "transversus thoracis",
            "diaphragm",
            "pectoralis",
            "rectus abdominis",
            "external abdominal oblique",
            "internal abdominal oblique",
            "transversus abdominis",
            "pyramidalis",
            "thoracic",
            "abdominal",
            "pubococcygeus",
            "pubo-analis",
            "levator ani",
            "coccygeus",
            "pelvic diaphragm",
            "perineal"))
        {
            return "Trunk";
        }

        return null;
    }

    private string GetSideGroup(
        string name,
        string leftGroup,
        string rightGroup)
    {
        if (name.EndsWith(".l"))
            return leftGroup;

        if (name.EndsWith(".r"))
            return rightGroup;

        return null;
    }

    private bool ContainsAny(
        string text,
        params string[] words)
    {
        foreach (string word in words)
        {
            if (text.Contains(word))
                return true;
        }

        return false;
    }
}