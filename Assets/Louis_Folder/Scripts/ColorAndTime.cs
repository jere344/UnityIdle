using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAndTime : MonoBehaviour
{
    public SpriteRenderer sr;

    public bool pause;
    public float myTime = 5;
    public Color A;
    public Color B;


    private float t; 

    // Start is called before the first frame update
    void Start()
    {
     //   StartCoroutine(MaCoroutine());
          StartCoroutine(ColorEvolution());
    }
    public IEnumerator MaCoroutine()
    {
        while (true)
        {
            while (pause == true)
            {
                yield return new WaitForEndOfFrame();
            }
            RandomiseColor();
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator ColorEvolution()
    {
        while (t<=1)
        {
            t += 0.001f;
            sr.color = Color.Lerp(A, B, t);
            yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myTime -= Time.deltaTime;
            if(myTime < 0f)
            {
                RandomiseColor();
                myTime = 5;
            }
        }
    }
    public void RandomiseColor()
    {
        var r= Random.Range(0f,1f);
        var g= Random.Range(0f,1f);
        var b = Random.Range(0f, 1f);
        sr.color = new Color (r,g,b);
    }

    private void OnGUI()
    {
        GUILayout.Label(myTime.ToString());
    }
}
