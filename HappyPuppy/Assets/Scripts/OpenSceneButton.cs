using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneButton : MonoBehaviour
{
    public string scene;
    
    public void OpenScene()
    {
        SceneManager.LoadScene(scene);
    }
}
