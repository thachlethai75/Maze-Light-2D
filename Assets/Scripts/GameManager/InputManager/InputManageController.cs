using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManageController : FreshieMonoBehaviour
{
    [SerializeField] private SwipeInput swipeInput;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSwipeInput();
    }


    protected void LoadSwipeInput()
    {
        if(swipeInput != null) return;
        this.swipeInput = transform.GetComponentInChildren<SwipeInput>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {swipeInput.name}");
    }



}
