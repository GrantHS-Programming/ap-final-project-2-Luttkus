using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator anim;
    public void FadeToLevel()
    {
        anim.SetTrigger("Fade Out");
    }
}
