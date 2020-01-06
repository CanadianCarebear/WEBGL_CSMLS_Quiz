using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoScript : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        StartCoroutine(LogoFader());
    }

    IEnumerator LogoFader()
    {
        animator.SetTrigger("isExit");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
    

}
