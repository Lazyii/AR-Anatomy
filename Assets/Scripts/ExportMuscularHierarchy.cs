using UnityEngine;
using System.IO;
using System.Text;

public class ExportMuscularHierarchy : MonoBehaviour
{
    private StringBuilder output = new StringBuilder();

    void Start()
    {
        output.AppendLine("=== MUSCULAR SYSTEM HIERARCHY ===");
        output.AppendLine("");

        ExportChildren(transform, 0);

        string path = Path.Combine(
            System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Desktop
            ),
            "Muscular_Unity_Hierarchy.txt"
        );

        File.WriteAllText(
            path,
            output.ToString(),
            Encoding.UTF8
        );

        Debug.Log("DA XUAT: " + path);
    }

    void ExportChildren(Transform parent, int level)
    {
        foreach (Transform child in parent)
        {
            output.AppendLine(
                new string(' ', level * 4)
                + child.name
            );

            ExportChildren(child, level + 1);
        }
    }
}