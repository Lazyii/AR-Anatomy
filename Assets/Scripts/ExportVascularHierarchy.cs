using System.IO;
using System.Text;
using UnityEngine;

public class ExportVascularHierarchy : MonoBehaviour
{
[Header("Kéo object VascularSystem vào đây")]
[SerializeField] private Transform vascularSystem;

[ContextMenu("Export Vascular Level 1")]
public void ExportVascularLevel1()
{
    if (vascularSystem == null)
    {
        Debug.LogError(
            "Chưa gán VascularSystem vào Inspector!"
        );
        return;
    }

    string desktopPath = System.Environment.GetFolderPath(
        System.Environment.SpecialFolder.Desktop
    );

    if (string.IsNullOrEmpty(desktopPath) ||
        !Directory.Exists(desktopPath))
    {
        desktopPath = Application.persistentDataPath;
    }

    string filePath = Path.Combine(
        desktopPath,
        "Vascular_Level1_Models.txt"
    );

    StringBuilder sb = new StringBuilder();

    sb.AppendLine("VASCULAR LEVEL 1 MODELS");
    sb.AppendLine("==============================");
    sb.AppendLine("Root: " + vascularSystem.name);
    sb.AppendLine();

    int count = vascularSystem.childCount;

    for (int i = 0; i < count; i++)
    {
        Transform child = vascularSystem.GetChild(i);

        sb.AppendLine($"{i + 1}. {child.name}");
        sb.AppendLine("   Type: " +
            child.gameObject.GetType().Name);
        sb.AppendLine("   Children: " + child.childCount);
        sb.AppendLine("   Path: " +
            GetHierarchyPath(child, vascularSystem));
        sb.AppendLine();
    }

    File.WriteAllText(
        filePath,
        sb.ToString(),
        Encoding.UTF8
    );

    Debug.Log(
        $"Đã xuất {count} nhánh cấp 1 của {vascularSystem.name}."
    );

    Debug.Log("File được lưu tại: " + filePath);
}

private string GetHierarchyPath(
    Transform obj,
    Transform root
)
{
    string path = obj.name;
    Transform current = obj.parent;

    while (current != null && current != root)
    {
        path = current.name + "/" + path;
        current = current.parent;
    }

    return root.name + "/" + path;
}

}
