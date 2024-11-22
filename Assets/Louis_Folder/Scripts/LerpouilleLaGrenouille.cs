using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpouilleLaGrenouille : MonoBehaviour
{
    public Vector3 positionA;
    public Vector3 positionB;

    public float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(positionA, positionB, t);
    }
}
