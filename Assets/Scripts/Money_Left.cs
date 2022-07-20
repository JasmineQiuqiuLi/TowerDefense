using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Left : MonoBehaviour
{
    public static Money_Left instance;
    public Text money_Left;
    Animator animator;
    //public AnimationClip textTransform;
    

    private void Awake()
    {
        instance = this;
        animator = GameObject.Find("Money_Left").GetComponent<Animator>();

    }
    public void UpdateTextUI()
    {
        money_Left.text = "Money:\n" +"$"+ TurretSpawner.instance.Money.ToString();
    }

    public void PlayTextEffect()
    {
        animator.SetTrigger("Scale");
    }
}
