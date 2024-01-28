using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAnimation : MonoBehaviour
{
    public float speed;
    public SpriteRenderer kid;
    public SpriteRenderer dog;
    
    private bool waitForVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
        if (!waitForVisible && !kid.isVisible && !dog.isVisible)
        {
            waitForVisible = true;
            speed *= -1;
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
        }

        if (kid.isVisible && dog.isVisible)
            waitForVisible = false;
    }
}
