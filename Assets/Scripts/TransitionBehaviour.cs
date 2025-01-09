using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator _transitionAnim;

    private void Start()
    {
        StartCoroutine(Transition());
    }
    private IEnumerator Transition()
    {
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
