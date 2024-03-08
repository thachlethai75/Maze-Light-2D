using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : FreshieMonoBehaviour
{
    [SerializeField] private InputManageController inputController;
    [SerializeField] private CubeController cubeController;
    public CubeController CubeController => cubeController;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadInputController();
        this.LoadCubeController();
    }


    protected void LoadInputController()
    {
        if (this.inputController != null) return;
        this.inputController = transform.GetComponentInChildren<InputManageController>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {inputController.name}");
    }

    protected void LoadCubeController()
    {
        if (this.cubeController != null) return;
        this.cubeController = GameObject.FindObjectOfType<CubeController>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {cubeController.name}");
    }

}
