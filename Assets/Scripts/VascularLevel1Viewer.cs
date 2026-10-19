using System;
using System.Collections.Generic;
using UnityEngine;

public class VascularLevel1Viewer : MonoBehaviour
{
    [Header("Vascular Root")]
    [SerializeField] private Transform vascularSystem;

    [Header("AR Camera")]
    [SerializeField] private Camera arCamera;
    [SerializeField] private float distanceFromCamera = 1.0f;
    [SerializeField] private float targetHeight = -0.1f;
    [SerializeField] private float displayScale = 2.0f;

    [Header("Common Scale For Level 4-5 Nodes")]
    [SerializeField, Min(0.01f)]
    private float HeadneckScale = 2.0f;

private static readonly HashSet<string> NodesToScale =
    new HashSet<string>(System.StringComparer.OrdinalIgnoreCase)
{
    "AnteriorCommunicatingArtery",
    "PosteriorCommunicating_Left",
    "PosteriorCommunicating_Right",
    "AnteriorSpinal_Left",
    "AnteriorSpinal_Right",
    "CommonCarotid_Left",
    "CommonCarotid_Right",
    "InternalJugular_Left",
    "InternalJugular_Right",
    "SuperiorOphthalmic_Left",
    "SuperiorOphthalmic_Right",
    "InferiorOphthalmic_Left",
    "InferiorOphthalmic_Right",
    "InferiorPetrosal_Left",
    "InferiorPetrosal_Right",
    "SuperiorSagittalSinus",
    "InferiorSagittalSinus",
    "OccipitalSinus",
    "SigmoidSinus_Left",
    "SigmoidSinus_Right",
    "StraightSinus"
};

    private Transform selectedRoot;
    private readonly List<Transform> movedModels = new List<Transform>();

    private readonly Dictionary<Transform, Transform> originalParents =
        new Dictionary<Transform, Transform>();

    private readonly Dictionary<Transform, Vector3> originalPositions =
        new Dictionary<Transform, Vector3>();

    private readonly Dictionary<Transform, Quaternion> originalRotations =
        new Dictionary<Transform, Quaternion>();

    private readonly Dictionary<Transform, Vector3> originalScales =
        new Dictionary<Transform, Vector3>();

    private void Awake()
    {
        if (arCamera == null)
            arCamera = Camera.main;

        CreateSelectedRoot();
    }

    private void CreateSelectedRoot()
    {
        if (selectedRoot != null)
            return;

        GameObject root = new GameObject("VascularSelectedRoot");
        selectedRoot = root.transform;
        selectedRoot.SetParent(transform, false);
        selectedRoot.localPosition = Vector3.zero;
        selectedRoot.localRotation = Quaternion.identity;
        selectedRoot.localScale = Vector3.one;
    }

    public void ShowModels(List<Transform> models, string nodeName)
    {
        RestorePreviousGroup();

        if (models == null || models.Count == 0)
        {
            Debug.LogWarning(
                "[Vascular Viewer] Không có model để hiển thị: " + nodeName);
            return;
        }

        if (vascularSystem == null)
        {
            Debug.LogError("[Vascular Viewer] Chưa gán VascularSystem.");
            return;
        }

        if (arCamera == null)
            arCamera = Camera.main;

        CreateSelectedRoot();

        foreach (Transform model in vascularSystem)
        {
            if (model == null)
                continue;

            foreach (Renderer renderer in
                     model.GetComponentsInChildren<Renderer>(true))
            {
                renderer.enabled = false;
            }
        }

        bool applyCommonScale = ShouldApplyCommonScale(nodeName);

        foreach (Transform model in models)
        {
            if (model == null || movedModels.Contains(model))
                continue;

            originalParents[model] = model.parent;
            originalPositions[model] = model.position;
            originalRotations[model] = model.rotation;
            originalScales[model] = model.localScale;

            model.SetParent(selectedRoot, true);
            movedModels.Add(model);

            if (applyCommonScale)
                model.localScale = originalScales[model] * HeadneckScale;

            foreach (Renderer renderer in
                     model.GetComponentsInChildren<Renderer>(true))
            {
                renderer.enabled = true;
            }
        }

        if (movedModels.Count == 0)
            return;

        FrameSelectedModels();

        Debug.Log(
            "[Vascular Viewer] Hiển thị " + nodeName
            + " | Models: " + movedModels.Count
            + " | Common scale: " + applyCommonScale);
    }

    
private bool ShouldApplyCommonScale(string nodeName)
{
    return !string.IsNullOrWhiteSpace(nodeName)
        && NodesToScale.Contains(nodeName.Trim());
}


    private void FrameSelectedModels()
    {
        if (movedModels.Count == 0)
            return;

        if (arCamera == null)
            arCamera = Camera.main;

        if (arCamera == null)
        {
            Debug.LogError("[VascularViewer] Không tìm thấy AR Camera.");
            return;
        }

        Bounds bounds = new Bounds();
        bool hasBounds = false;

        foreach (Transform model in movedModels)
        {
            if (model == null)
                continue;

            foreach (Renderer renderer in
                     model.GetComponentsInChildren<Renderer>(true))
            {
                if (!renderer.enabled)
                    continue;

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
        {
            Debug.LogWarning("[VascularViewer] Model không có Renderer.");
            return;
        }

        Vector3 targetCenter =
            arCamera.ViewportToWorldPoint(
                new Vector3(0.5f, 0.5f, distanceFromCamera));

        targetCenter.y += targetHeight;

        selectedRoot.position += targetCenter - bounds.center;
    }

    public void RestorePreviousGroup()
    {
        foreach (Transform model in movedModels)
        {
            if (model == null)
                continue;

            if (originalParents.TryGetValue(model, out Transform parent))
                model.SetParent(parent, true);

            if (originalPositions.TryGetValue(model, out Vector3 position))
                model.position = position;

            if (originalRotations.TryGetValue(model, out Quaternion rotation))
                model.rotation = rotation;

            if (originalScales.TryGetValue(model, out Vector3 scale))
                model.localScale = scale;
        }

        movedModels.Clear();
        originalParents.Clear();
        originalPositions.Clear();
        originalRotations.Clear();
        originalScales.Clear();

        if (selectedRoot != null)
        {
            selectedRoot.localPosition = Vector3.zero;
            selectedRoot.localRotation = Quaternion.identity;
            selectedRoot.localScale = Vector3.one;
        }

        RestoreAllRenderers();
    }

    private void RestoreAllRenderers()
    {
        if (vascularSystem == null)
            return;

        foreach (Renderer renderer in
                 vascularSystem.GetComponentsInChildren<Renderer>(true))
        {
            renderer.enabled = true;
        }
    }

    public void ShowFullBody()
    {
        RestorePreviousGroup();
        RestoreAllRenderers();
    }
}
