using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum MovingDir
{
    Up,
    Down,
    Left,
    Right,
    None,
}

public class SwipeInput : FreshieMonoBehaviour
{
    private static SwipeInput instance;
    public static SwipeInput Instance => instance;
    [SerializeField] private CubeMovement cubeMovement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private bool isHorizontal = false, canCheck = true;
    [SerializeField] public MovingDir movingDir = MovingDir.None;
    public Transform player;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    #region LoadComponent
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadGameManager();
        this.LoadCubeMovement();
    }

    protected void LoadGameManager()
    {
        if (this.gameManager != null) return;
        this.gameManager = transform.parent.parent.GetComponent<GameManager>();
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {gameManager.name}");
    }

    protected void LoadCubeMovement()
    {
        if (this.cubeMovement != null) return;
        this.cubeMovement = this.gameManager.CubeController.CubeMovement;
        Debug.LogWarning($"Game Object: {transform.name} is LoadComponent {cubeMovement.name}");
    }
    #endregion 
    protected override void Update()
    {
        base.Update();
        #region Touch Input
        //Get startPos right first time touch in screen
        /*if (isTouching == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            isTouching = true;
            Debug.Log($"You are touch at {startPos.x},{startPos.y}");
        }
        if(isTouching)
        {
            if (Input.touches[0].position.y >= startPos.y + dictToDetectSwipe)
            {
                isTouching = false;
                Debug.Log("Up");
            }
        }
        //Player leave there finger out screen 
        if(isTouching && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            isTouching=false;
        }*/
        #endregion
        if(this.isHorizontal)
        {
            if(Physics2D.Raycast(this.player.position, transform.TransformDirection(Vector2.left), 1, this.obstacleMask) || Physics2D.Raycast(this.player.position, transform.TransformDirection(Vector2.right), 1, this.obstacleMask))
            {
                this.canCheck = true;
            }
            else
            {
                this.canCheck = false;
            }
            Debug.DrawLine(this.player.position, transform.TransformDirection(Vector2.left * 1f));
            Debug.DrawLine(this.player.position, transform.TransformDirection(Vector2.right * 1f));

        }
        else
        {
            if (Physics2D.Raycast(this.player.position, transform.TransformDirection(Vector2.up), 1, this.obstacleMask) || Physics2D.Raycast(this.player.position, transform.TransformDirection(Vector2.down), 1, this.obstacleMask))
            {
                this.canCheck = true;
            }
            else
            {
                this.canCheck = false;
            }
            Debug.DrawLine(this.player.position, transform.TransformDirection(Vector2.up * 1f));
            Debug.DrawLine(this.player.position, transform.TransformDirection(Vector2.down * 1f));
        }

        #region PC Mouse Input
        if (this.canCheck) {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                this.isHorizontal = true;
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    movingDir = MovingDir.Right;
                } else
                {
                    movingDir = MovingDir.Left;
                }
            }
            else if (Input.GetAxisRaw("Vertical") != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                this.isHorizontal = false;
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    movingDir = MovingDir.Up;
                }
                else
                {
                    movingDir = MovingDir.Down;
                }
            }
        }

        #endregion
    }

}
