
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Mirror;


public class Player : NetworkBehaviour
{

    [SerializeField]
    private float PlayerSpeed;
    [SerializeField]
    private float JumpHeight;
    [SerializeField]
    private float DashMagnitude;
    [SerializeField]
    private Camera PlayerCamera;
    [SerializeField]
    private HealthBar healthBar;
    private Rigidbody RigidBody;
    private int NumberOfCollisions = 0;
    private List<Ball> ClosestBalls = new List<Ball>();
    private List<Ball> balls = new List<Ball>();
    private Vector3 CollisionNormal;
    private Vector3 move;
    private float health = 1f;



    private void Start()
    {
        if (isLocalPlayer)
        {
            PlayerCamera.enabled = true;
            GameObject.FindGameObjectWithTag("ServerCamera").GetComponent<Camera>().enabled = false;
            healthBar.SetActive(false);
        }
    }

    private void Hit()
    {
        health -= 0.1f;
        if (health <= 0)
        {
            NetworkServer.Destroy(this.gameObject);
        }
        healthBar.SetSliderValue(health);
    }
    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        healthBar.SetSliderValue(health);
    }

    public void EnablePlayerCamera(bool enable)
    {
        PlayerCamera.enabled = enable;
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        RigidBody.AddForce(RigidBody.velocity.normalized * DashMagnitude, ForceMode.Impulse);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (NumberOfCollisions > 0)
        {
            CollisionNormal += Vector3.up;
            CollisionNormal.Normalize();
            RigidBody.AddForce(CollisionNormal * JumpHeight, ForceMode.Impulse);
        }
    }



    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 vector2Movement = ctx.ReadValue<Vector2>() * (NumberOfCollisions > 0 ? PlayerSpeed : PlayerSpeed / 2f);
        Vector3 forwardVector = PlayerCamera.transform.forward;
        forwardVector.y = 0;
        forwardVector.Normalize();
        Vector3 rightVector = new Vector3(forwardVector.z, 0, -forwardVector.x);
        //RigidBody.AddForce(forwardVector * vector2Movement.y + rightVector * vector2Movement.x, ForceMode.VelocityChange);
        move = forwardVector * vector2Movement.y + rightVector * vector2Movement.x;
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
        if (balls.Count > 0)
        {
            Ball ball = balls[balls.Count - 1];
            RpcFire(ball);
            balls.RemoveAt(balls.Count - 1);
        }
    }

    [ClientRpc]
    private void RpcFire(Ball ball)
    {
        ball.Throw(PlayerCamera.transform, RigidBody.velocity);
    }

    public void Catch(InputAction.CallbackContext ctx)
    {
        if (ClosestBalls.Count > 0)
        {
            Ball ball = ClosestBalls[ClosestBalls.Count - 1];
            ball.GetComponent<HighLight>().SwapMaterial();
            RpcCatch(ball);
            balls.Add(ball);
            ClosestBalls.RemoveAt(ClosestBalls.Count - 1);
        }
    }

    [ClientRpc]
    private void RpcCatch(Ball ball)
    {
        ball.Catch();
    }

    private void FixedUpdate()
    {
        CollisionNormal = Vector3.zero;
        RigidBody.AddForce(move, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        NumberOfCollisions++;
        CollisionNormal += other.GetContact(0).normal;
        if (other.gameObject.tag == "Ball")
        {
            Hit();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        CollisionNormal += other.GetContact(0).normal;
    }

    private void OnCollisionExit(Collision other)
    {
        NumberOfCollisions--;
    }

    public void AddClosestBall(Ball ball)
    {
        ClosestBalls.Add(ball);
    }

    public void RemoveClosestBall(Ball ball)
    {
        ClosestBalls.Remove(ball);
    }

}
