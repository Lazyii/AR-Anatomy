using System.Collections.Generic;
using UnityEngine;

public class AnatomyModelGroup : MonoBehaviour
{
    [Header("Models linked to this UI branch")]
    [SerializeField]
    private List<Transform> linkedModels =
        new List<Transform>();

    public IReadOnlyList<Transform> LinkedModels
    {
        get { return linkedModels; }
    }

    public void ClearModels()
    {
        linkedModels.Clear();
    }

    public void AddModel(Transform model)
    {
        if (model == null)
            return;

        if (!linkedModels.Contains(model))
        {
            linkedModels.Add(model);
        }
    }

    public bool ContainsModel(Transform model)
    {
        return linkedModels.Contains(model);
    }
}