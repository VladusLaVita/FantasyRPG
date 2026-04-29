using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class ManiMenu : MonoBehaviour
{
    public Canvas pause;
    public Canvas settings;
    public string SceneName;
    string path = Application.dataPath + "/Scenes";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause.enabled = true;
        settings.enabled = false;
    }
    // Update is called once per frame

    public void Play()
    {
        SceneManager.LoadScene(FindScene(SceneName));
    }
    public void Settings()
    {
        pause.enabled = false;
        settings.enabled = true;
    }
    public void Back()
    {
        pause.enabled = true;
        settings.enabled = false;
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
