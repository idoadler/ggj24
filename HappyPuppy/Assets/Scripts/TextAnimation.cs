using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextAnimation : MonoBehaviour
{
    public int FPS = 5;
    [TextArea]
    public string[] texts;

    private TMP_Text _label;
    
    // Start is called before the first frame update
    void Start()
    {
        _label = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int ms = (int)(Time.time * 3600);
        int interval = 3600 / FPS;
        _label.text = texts[(ms / interval) % texts.Length];
    }
}
