using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    public GameObject[] arrows;
    private bool destroyedArrow1;
    private bool destroyedArrow2;

    private void Update()
    {
        if (destroyedArrow1 && destroyedArrow2)
        {
            Destroy(arrows[2]);
            Destroy(gameObject);
        }
    }
    public void DestroyArrow1()
    {
        destroyedArrow1 = true;
        Destroy(arrows[0]);
    }

    public void DestroyArrow2()
    {
        destroyedArrow2 = true;
        Destroy(arrows[1]);

    }
}
