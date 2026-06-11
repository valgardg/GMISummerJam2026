using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator _animator;

    InputAction moveAction;
    private Rigidbody2D rb;
    private Vector2 input;

    private float facingDirection = 1f;
    public float FacingDirection => facingDirection;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");

        if (input.x != 0)
        {
            facingDirection = Mathf.Sign(input.x);
            spriteRenderer.flipX = input.x < 0;
        }
        
        if(moveAction.IsPressed())
        {
            _animator.SetBool("isRunning", true);
        }
        if(moveAction.WasReleasedThisFrame())
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            input.x * moveSpeed,
            rb.linearVelocity.y
        );
    }
}