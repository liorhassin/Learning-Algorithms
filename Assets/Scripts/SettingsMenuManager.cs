using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Animator animator;
    
    public void OpenSettingsMenu()
    {
        animator.SetBool("Open", true);
    }
    
    public void CloseSettingsMenu()
    {
        animator.SetBool("Open", false);
    }
}
