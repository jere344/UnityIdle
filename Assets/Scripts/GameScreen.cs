using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    private Animator animator;
    public void Start()
    {
        animator.SetTrigger("Transition");
    }
}
