using UnityEngine;

public class AutoGroupMuscularModel : MonoBehaviour
{
    private Transform headNeck;
    private Transform trunk;
    private Transform leftUpperLimb;
    private Transform rightUpperLimb;
    private Transform leftLowerLimb;
    private Transform rightLowerLimb;
    private Transform otherStructures;

    [ContextMenu("BUILD MUSCULAR GROUPS")]
    public void BuildMuscularGroups()
    {
        CreateGroups();

        Transform[] children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        int classified = 0;
        int other = 0;

        foreach (Transform child in children)
        {
            if (IsGroup(child))
                continue;

            string groupName = GetTargetGroup(child.name);

            Transform target = GetGroupTransform(groupName);

            if (target == null)
                target = otherStructures;

            child.SetParent(target, true);

            if (target == otherStructures)
                other++;
            else
                classified++;
        }

        Debug.Log(
            "=== MUSCULAR GROUPING COMPLETE ===\n" +
            "Classified: " + classified + "\n" +
            "Other structures: " + other
        );
    }

    private void CreateGroups()
    {
        headNeck = GetOrCreateGroup("HeadNeck");
        trunk = GetOrCreateGroup("Trunk");

        leftUpperLimb = GetOrCreateGroup("LeftUpperLimb");
        rightUpperLimb = GetOrCreateGroup("RightUpperLimb");

        leftLowerLimb = GetOrCreateGroup("LeftLowerLimb");
        rightLowerLimb = GetOrCreateGroup("RightLowerLimb");

        otherStructures = GetOrCreateGroup("OtherStructures");
    }

    private Transform GetOrCreateGroup(string groupName)
    {
        Transform existing = transform.Find(groupName);

        if (existing != null)
            return existing;

        GameObject group = new GameObject(groupName);

        group.transform.SetParent(
            transform,
            false
        );

        return group.transform;
    }

    private bool IsGroup(Transform child)
    {
        return child == headNeck
            || child == trunk
            || child == leftUpperLimb
            || child == rightUpperLimb
            || child == leftLowerLimb
            || child == rightLowerLimb
            || child == otherStructures;
    }

    private Transform GetGroupTransform(string groupName)
    {
        switch (groupName)
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

            case "OtherStructures":
                return otherStructures;
        }

        return otherStructures;
    }

    private string GetTargetGroup(string objectName)
    {
        string n = objectName.ToLowerInvariant();

        bool isLeft = IsLeft(n);
        bool isRight = IsRight(n);

        // =====================================================
        // 1. CÁC CẤU TRÚC KHÔNG PHẢI CƠ
        // =====================================================

        if (ContainsAny(n,
            "fascia",
            "bursa",
            "tendon sheath",
            "tendon sheaths",
            "retinaculum",
            "retinacula",
            "aponeurosis",
            "ligament",
            "intermuscular septum",
            "synovial sheath",
            "tendinous arch",
            "plantar aponeurosis",
            "tarsus"))
        {
            return "OtherStructures";
        }

        // =====================================================
        // 2. HEAD / FACE / EYE / EAR
        // =====================================================

        if (ContainsAny(n,
            "orbicularis oculi",
            "orbicularis oris",
            "palpebral part",
            "superior rectus",
            "inferior rectus",
            "medial rectus",
            "lateral rectus",
            "superior oblique",
            "inferior oblique",
            "trochlea of superior oblique",
            "levator palpebrae",
            "temporalis",
            "temporoparietalis",
            "masseter",
            "buccinator",
            "zygomaticus",
            "risorius",
            "mentalis",
            "procerus",
            "platysma",
            "nasalis",
            "depressor",
            "levator labii",
            "levator anguli",
            "frontalis",
            "occipitalis",
            "epicranius",
            "auricular",
            "masticatory",
            "tongue",
            "genioglossus",
            "hyoglossus",
            "styloglossus",
            "palatoglossus"))
        {
            return "HeadNeck";
        }

        // =====================================================
        // 3. NECK / PHARYNX / LARYNX
        // =====================================================

        if (ContainsAny(n,
            "sternocleidomastoid",
            "scalenus",
            "longus capitis",
            "longus colli",
            "rectus anterior capitis",
            "rectus lateralis capitis",
            "rectus posterior major capitis",
            "rectus posterior minor capitis",
            "splenius capitis",
            "splenius colli",
            "semispinalis capitis",
            "suboccipital",
            "digastric",
            "mylohyoid",
            "geniohyoid",
            "stylohyoid",
            "sternohyoid",
            "sternothyroid",
            "thyrohyoid",
            "stylopharyngeus",
            "pharyngeal constrictor",
            "crico-arytenoid",
            "cricothyroid",
            "arytenoid",
            "thyro-arytenoid",
            "thyro-epiglottic",
            "suprahyoid",
            "infrahyoid"))
        {
            return "HeadNeck";
        }

        // =====================================================
        // 4. UPPER LIMB
        // =====================================================

        if (ContainsAny(n,
            "deltoid",
            "supraspinatus",
            "infraspinatus",
            "subscapularis",
            "teres major",
            "teres minor",
            "coracobrachialis",
            "biceps brachii",
            "triceps brachii",
            "brachialis",
            "brachioradialis",
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
            "abductor pollicis",
            "adductor pollicis",
            "opponens pollicis",
            "flexor pollicis brevis",
            "abductor digiti minimi of hand",
            "opponens digiti minimi of hand",
            "lumbrical",
            "interossei",
            "palmaris longus",
            "palmaris brevis"))
        {
            if (isLeft)
                return "LeftUpperLimb";

            if (isRight)
                return "RightUpperLimb";

            return "OtherStructures";
        }

        // =====================================================
        // 5. LOWER LIMB
        // =====================================================

        if (ContainsAny(n,
            "gluteus",
            "gluteal",
            "piriformis",
            "obturator internus",
            "obturator externus",
            "gemellus",
            "quadratus femoris",
            "iliopsoas",
            "iliacus",
            "psoas",
            "sartorius",
            "rectus femoris",
            "vastus",
            "adductor longus",
            "adductor brevis",
            "adductor magnus",
            "adductor hallucis",
            "gracilis",
            "pectineus",
            "tensor fasciae latae",
            "biceps femoris",
            "semitendinosus",
            "semimembranosus",
            "popliteus",
            "gastrocnemius",
            "soleus",
            "plantaris",
            "tibialis anterior",
            "tibialis posterior",
            "fibularis",
            "peroneus",
            "extensor hallucis",
            "extensor digitorum longus",
            "flexor hallucis",
            "flexor digitorum longus",
            "flexor digitorum brevis",
            "abductor hallucis",
            "quadratus plantae",
            "plantar interossei",
            "dorsal interossei",
            "opponens digiti minimi of foot",
            "abductor digiti minimi of foot"))
        {
            if (isLeft)
                return "LeftLowerLimb";

            if (isRight)
                return "RightLowerLimb";

            return "OtherStructures";
        }

        // =====================================================
        // 6. TRUNK / BACK / THORAX / ABDOMEN
        // =====================================================

        if (ContainsAny(n,
            "trapezius",
            "latissimus dorsi",
            "rhomboid",
            "erector spinae",
            "iliocostalis",
            "longissimus",
            "spinalis",
            "multifidus",
            "semispinalis",
            "rotatores",
            "transversospinal",
            "interspinales",
            "intertransversarii",
            "serratus anterior",
            "serratus posterior",
            "pectoralis major",
            "pectoralis minor",
            "subclavius",
            "intercostal",
            "diaphragm",
            "rectus abdominis",
            "external abdominal oblique",
            "internal abdominal oblique",
            "transversus abdominis",
            "quadratus lumborum",
            "pyramidalis",
            "transversus thoracis"))
        {
            return "Trunk";
        }

        // =====================================================
        // 7. MIDLINE / UNPAIRED MUSCLES
        // =====================================================

        if (ContainsAny(n,
            "transverse arytenoid",
            "diaphragm"))
        {
            return "Trunk";
        }

        // =====================================================
        // 8. CHƯA XÁC ĐỊNH
        // =====================================================

        return "OtherStructures";
    }

    private bool IsLeft(string name)
    {
        return name.EndsWith(".l")
            || name.Contains(".l")
            || name.Contains(" left");
    }

    private bool IsRight(string name)
    {
        return name.EndsWith(".r")
            || name.Contains(".r")
            || name.Contains(" right");
    }

    private bool ContainsAny(
        string value,
        params string[] keywords)
    {
        foreach (string keyword in keywords)
        {
            if (value.Contains(keyword))
                return true;
        }

        return false;
    }
}