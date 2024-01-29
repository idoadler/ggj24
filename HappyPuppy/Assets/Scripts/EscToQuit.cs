using UnityEngine;

public class EscToQuit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Debug.Log("Can't quit on web");
#else
            Application.Quit();
#endif
        }
    }
}
