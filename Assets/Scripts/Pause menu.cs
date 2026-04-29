using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class Pausemenu : MonoBehaviour
{
    public bool Enabled;
    public Canvas pause;
    public Canvas settings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enabled = false;
        pause.enabled = Enabled;
        settings.enabled = Enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Enabled = !Enabled;
            pause.enabled = Enabled;
        }
    }

    public void Resume()
    {
        pause.enabled = false;
        Enabled = false;
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
    string path = Application.dataPath + "/Scenes";
    public string sceneName;

    public void Quit()
    {
        SceneManager.LoadScene(FindScene(sceneName));
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
