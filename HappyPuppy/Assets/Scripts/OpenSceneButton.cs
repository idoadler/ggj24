using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneButton : MonoBehaviour
{
    public string scene;

    private void Start()
    {
        MusicManager.Instance.MenuSettings();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(scene);
    }
}
