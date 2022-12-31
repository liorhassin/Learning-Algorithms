using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //animator property
    public Animator animator;
    // Start is called before the first frame update

    public void CloseMainMenu()
    {
        animator.SetBool("Open", false);
    }

    public void OpenMainMenu()
    {
        animator.SetBool("Open", true);
    }
}
