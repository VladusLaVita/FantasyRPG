using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Settings")]
    public string sceneName;

    string path = Application.dataPath + "/Scenes";
    private string realSceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            realSceneName = FindScene(sceneName);
            SceneManager.LoadScene(realSceneName);
        }
    }

    string FindScene(string sceneNumber)
    {
        if (Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Debug.Log(file);
                if (file.Contains(sceneNumber))
                {
                    // C://Unity/Assets/Scenes/001 Safetown.unity
                    // 001 Safetown
                    char[] dilimiters = { '/', ';', '.', ':', '\\' };
                    string[] fileName = file.Split(dilimiters);
                    string filename = fileName[fileName.Length - 2];

                    return filename;
                }
            }
        }
        return "Null";
    }
}