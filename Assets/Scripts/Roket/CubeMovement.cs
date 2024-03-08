
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CubeMovement : FreshieMonoBehaviour
{
    [SerializeField] private CubeController cubeController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float swipeThreshold = 0.5f;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 initialTouchPos;
    [SerializeField] private Vector2 currentTouchPos;
    [SerializeField] public MovingDir movingDir = MovingDir.None;


    #region LoadComponent
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCubeController();
        this.LoadRigidbody();
    }

    protected void LoadCubeController()
    {
        if (this.cubeController != null) return;
        this.cubeController = transform.parent.GetComponent<CubeController>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {cubeController.name}");
    }

    protected void LoadRigidbody()
    {
        if (this._rb != null) return;
        this._rb = transform.parent.GetComponent<Rigidbody2D>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {_rb.name}");
    }
    #endregion
    protected override void Update()
    {
        base.Update();
        this.CheckCubeIsMoving();
        if (!this.isMoving)
        {
            this.GetInputDirFormPlayer();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.MoveByDirection();
    }

    protected void CheckCubeIsMoving()
    {
        if (_rb.velocity == Vector2.zero)
        {
             this.isMoving = false;
        }
        else
        {
             this.isMoving = true;
        }
    }

    protected void GetInputDirFormPlayer()
    {
        // Store initial touch position when a touch begins
        if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            if (currentTouch.phase == TouchPhase.Began)
            {
                initialTouchPos = currentTouch.position;
            }

            // Calculate horizontal movement based on touch position
            if (currentTouch.phase != TouchPhase.Ended && currentTouch.phase != TouchPhase.Canceled)
            {
                 currentTouchPos = currentTouch.position;
                Vector2 swipDelta = currentTouchPos - initialTouchPos;
                // Check for valid swipe (above threshold)
                if (Mathf.Abs(swipDelta.x) > swipeThreshold || Mathf.Abs(swipDelta.y) > swipeThreshold)
                {
                    if (Mathf.Abs(swipDelta.x) > Mathf.Abs(swipDelta.y))
                    {
                        movingDir = swipDelta.x > 0 ? MovingDir.Right : MovingDir.Left;
                        isMoving = true;
                    }
                    else
                    {
                        movingDir = swipDelta.y > 0 ? MovingDir.Up : MovingDir.Down;
                        isMoving=true;
                    }
                }
            }
            // Reset movement on touch end
            if (currentTouch.phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled)
            {
                this.initialTouchPos = Vector2.zero;
            }
        }
    }

    protected void MoveByDirection()
    {
        switch (movingDir)
        {
            case MovingDir.Up:
                _rb.velocity = new Vector2(0, moveSpeed * Time.fixedDeltaTime);
                break;
            case MovingDir.Down:
                _rb.velocity = new Vector2(0, -moveSpeed * Time.fixedDeltaTime);
                break;
            case MovingDir.Left:
                _rb.velocity = new Vector2(-moveSpeed * Time.fixedDeltaTime, 0);
                break;
            case MovingDir.Right:
                _rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, 0);
                break;

        }
    }
}





