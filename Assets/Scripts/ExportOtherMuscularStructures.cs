using UnityEngine;
using System.IO;
using System.Text;

public class ExportOtherMuscularStructures : MonoBehaviour
{
    [ContextMenu("EXPORT UNGROUPED STRUCTURES")]
    public void Export()
    {
        StringBuilder output = new StringBuilder();

        output.AppendLine("=== UNGROUPED MUSCULAR STRUCTURES ===");
        output.AppendLine("");

        int count = 0;

        foreach (Transform child in transform)
        {
            string name = child.name;

            if (name == "HeadNeck" ||
                name == "Trunk" ||
                name == "LeftUpperLimb" ||
                name == "RightUpperLimb" ||
                name == "LeftLowerLimb" ||
                name == "RightLowerLimb" ||
                name == "OtherStructures")
            {
                continue;
            }

            output.AppendLine(name);
            count++;
        }

        output.AppendLine("");
        output.AppendLine("TOTAL UNGROUPED: " + count);

        string path = Path.Combine(
            System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Desktop
            ),
            "Ungrouped_Muscular_Structures.txt"
        );

        File.WriteAllText(
            path,
            output.ToString(),
            Encoding.UTF8
        );

        Debug.Log(
            "DA XUAT: " + path +
            " | COUNT = " + count
        );
    }
}