
using System.Collections.Generic;
using UnityEngine;

public class SkeletalSystemLevel1Viewer : MonoBehaviour
{
    [Header("Skeletal System")]
    [SerializeField] private Transform skeletalSystem;

    [Header("Camera")]
    [SerializeField] private Camera arCamera;
    [SerializeField] private float distanceFromCamera = 1.0f;
    [SerializeField] private float targetHeight = -0.1f;

    [Header("Scale cấp 3-4")]
    [SerializeField, Min(0.01f)]
    private float level3Level4Scale = 1f;

    private Transform selectedRoot;

    private class OriginalModelState
    {
        public Transform model;
        public Transform originalParent;
        public Vector3 originalLocalPosition;
        public Quaternion originalLocalRotation;
        public Vector3 originalLocalScale;
    }

    private readonly List<OriginalModelState> originalStates =
        new List<OriginalModelState>();

    private readonly Dictionary<Renderer, bool> originalRendererStates =
        new Dictionary<Renderer, bool>();

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
        {
            return;
        }

        GameObject rootObject = new GameObject("SkeletalSelectedRoot");
        selectedRoot = rootObject.transform;
    }

    public void ShowModels(
        List<Transform> models,
        string nodeName,
        bool applyLevel3Level4Scale = false)
    {
        RestorePreviousGroup();

        if (skeletalSystem == null)
        {
            Debug.LogError(
                "[SkeletalSystemViewer] Chưa gán Skeletal System root."
            );
            return;
        }

        if (arCamera == null)
        {
            arCamera = Camera.main;
        }

        if (arCamera == null)
        {
            Debug.LogError(
                "[SkeletalSystemViewer] Không tìm thấy AR Camera."
            );
            return;
        }

        if (models == null || models.Count == 0)
        {
            Debug.LogWarning(
                "[SkeletalSystemViewer] Không có model để hiển thị: "
                + nodeName
            );
            return;
        }

        CreateSelectedRoot();

        // Lưu trạng thái Renderer ban đầu để khôi phục chính xác.
        originalRendererStates.Clear();

        Renderer[] allRenderers =
            skeletalSystem.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in allRenderers)
        {
            if (renderer != null &&
                !originalRendererStates.ContainsKey(renderer))
            {
                originalRendererStates.Add(renderer, renderer.enabled);
                renderer.enabled = false;
            }
        }

        // Đưa các model được chọn vào selectedRoot.
        foreach (Transform model in models)
        {
            if (model == null)
            {
                continue;
            }

            if (!model.IsChildOf(skeletalSystem) &&
                model != skeletalSystem)
            {
                Debug.LogWarning(
                    "[SkeletalSystemViewer] Model không thuộc "
                    + "Skeletal System: " + model.name
                );
                continue;
            }

            OriginalModelState state = new OriginalModelState
            {
                model = model,
                originalParent = model.parent,
                originalLocalPosition = model.localPosition,
                originalLocalRotation = model.localRotation,
                originalLocalScale = model.localScale
            };

            originalStates.Add(state);

            // Giữ nguyên vị trí, góc xoay và kích thước thế giới.
            model.SetParent(selectedRoot, true);

            Renderer[] modelRenderers =
                model.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in modelRenderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = true;
                }
            }
        }

        if (originalStates.Count == 0)
        {
            RestoreRendererStates();
            Debug.LogWarning(
                "[SkeletalSystemViewer] Không có model hợp lệ cho: "
                + nodeName
            );
            return;
        }

        // Chỉ phóng to/thu nhỏ nhóm cấp 3-4.
        if (applyLevel3Level4Scale)
        {
            selectedRoot.localScale = Vector3.one * level3Level4Scale;
        }
        else
        {
            selectedRoot.localScale = Vector3.one;
        }

        // Tính bounds tổng hợp của nhóm model đã chọn.
        Bounds combinedBounds = new Bounds();
        bool hasBounds = false;

        foreach (OriginalModelState state in originalStates)
        {
            if (state.model == null)
            {
                continue;
            }

            Renderer[] renderers =
                state.model.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                if (renderer == null || !renderer.enabled)
                {
                    continue;
                }

                if (!hasBounds)
                {
                    combinedBounds = renderer.bounds;
                    hasBounds = true;
                }
                else
                {
                    combinedBounds.Encapsulate(renderer.bounds);
                }
            }
        }

        if (hasBounds)
        {
            Vector3 targetPoint =
                arCamera.transform.position
                + arCamera.transform.forward * distanceFromCamera
                + arCamera.transform.up * targetHeight;

            // Dịch toàn bộ nhóm để tâm bounds nằm trước camera.
            selectedRoot.position += targetPoint - combinedBounds.center;
        }

        Debug.Log(
            "[SkeletalSystemViewer] Đã hiển thị nhóm "
            + nodeName
            + " với "
            + originalStates.Count
            + " model."
        );
    }

    public void RestorePreviousGroup()
    {
        // Trả model về parent và local transform ban đầu.
        for (int i = originalStates.Count - 1; i >= 0; i--)
        {
            OriginalModelState state = originalStates[i];

            if (state.model == null)
            {
                continue;
            }

            state.model.SetParent(state.originalParent, false);
            state.model.localPosition = state.originalLocalPosition;
            state.model.localRotation = state.originalLocalRotation;
            state.model.localScale = state.originalLocalScale;
        }

        originalStates.Clear();

        RestoreRendererStates();

        if (selectedRoot != null)
        {
            selectedRoot.position = Vector3.zero;
            selectedRoot.rotation = Quaternion.identity;
            selectedRoot.localScale = Vector3.one;
        }
    }

    private void RestoreRendererStates()
    {
        foreach (KeyValuePair<Renderer, bool> pair
                 in originalRendererStates)
        {
            if (pair.Key != null)
            {
                pair.Key.enabled = pair.Value;
            }
        }

        originalRendererStates.Clear();
    }

    public void ShowFullBody()
    {
        RestorePreviousGroup();
    }

    private void OnDisable()
    {
        RestorePreviousGroup();
    }
}
