using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private MoveBehaviour moveBehaviour;
    private Vector3 startPosition;

    public bool PlayerCanMove { get; set; } = false;


    public void SetMoveBehaviour(MoveBehaviour moveBehaviour)
    {
        this.moveBehaviour = moveBehaviour;
        moveSpeed = moveBehaviour.speed;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += () => {
            SetDefaultMove();
            PlayerCanMove = true;
        };

        GameManager.Instance.OnGameEnded += Reset;
    }


    public void SetDefaultMove()
    {
        SetMoveBehaviour(new DefaultMoveBehaviour());
    }

    public void SetModifiedMove(MoveBehaviour moveBehaviour)
    {
        SetMoveBehaviour(moveBehaviour);
    }

    private void Move()
    {
        moveBehaviour?.Move(rb);
    }

    private void Reset()
    {
        PlayerCanMove = false;
        transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        if (PlayerCanMove)
        {
            animator.SetBool("isMoving", true);
            Move();
        } else
        {
            animator.SetBool("isMoving", false);
        }

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
}
