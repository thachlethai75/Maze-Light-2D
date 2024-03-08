
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : FreshieMonoBehaviour
{
    private static CubeController instance;
    public static CubeController Instance => instance;

    public CubeMovement CubeMovement => cubeMovement;

    [SerializeField] private CubeMovement cubeMovement;


    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCubeMovement();
    }

    protected void LoadCubeMovement()
    {
        if (this.cubeMovement != null) return;
        this.cubeMovement = transform.GetComponentInChildren<CubeMovement>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {cubeMovement.name}");
    }

}
