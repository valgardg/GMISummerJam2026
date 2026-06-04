using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class Door : MonoBehaviour
{

    public SceneAsset targetScene;

    public void OpenDoor()
    {
        if (targetScene != null)
        {
            string scenePath = AssetDatabase.GetAssetPath(targetScene);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Target scene is not assigned.");
        }
    }
}
