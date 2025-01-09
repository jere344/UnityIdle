using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    public List <GameObject> Arrows;
    private bool destroyedArrow1;
    private bool destroyedArrow2;

    private void Update()
    {
        if (GameManager.Instance.GestionShop.Workers[1].activeSelf)
        {
            destroyedArrow1 = true;
            Destroy(Arrows[0]);
        }

        if (GameManager.Instance.GestionShop.Workers[2].activeSelf)
        {
            destroyedArrow2 = true;
            Destroy(Arrows[1]);
        }

        if (destroyedArrow1 && destroyedArrow2)
        {
            Destroy(Arrows[2]);
            Destroy(gameObject);
        }
    }
    public void DestroyArrow1()
    {
        destroyedArrow1 = true;
        Destroy(Arrows[0]);
    }

    public void DestroyArrow2()
    {
        destroyedArrow2 = true;
        Destroy(Arrows[1]);

    }
}
