using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float passRange = 10f;
    public float shootRange = 15f;
    public float rotationSpeed = 720f; // Graus por segundo
    private Rigidbody rb;
    private Animator animator;
    private Ball ballController;
    [SerializeField] private bool isGoalkeeper, isPlayerControlled;
    [SerializeField] private GameObject ball;

    // Para IA
    public Vector3 aiTargetPosition;
    public float aiMoveThreshold = 0.5f;

    // Limites da quadra
    public Vector3 fieldMin, fieldMax;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        animator = GetComponent<Animator>();
        ballController = ball.GetComponent<Ball>();
    }

    void Update()
    {
        if (isPlayerControlled)
        {
            HandlePlayerInput();
        }
        else
        {
            //HandleAI();
        }
    }

    void HandlePlayerInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        Vector3 velocity = movement * currentSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        if (movement != Vector3.zero)
        {
            //animator.SetBool("isRunning", true);
            RotateTowards(movement);
        }
        else
        {
            //animator.SetBool("isRunning", false);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Chute
            Shoot();
        }

        if (isGoalkeeper)
        {
            //Defend();
        }
    }

    void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Defend()
    {
        if (ball != null)
        {
            aiTargetPosition = ball.transform.position;
            MoveTowards(aiTargetPosition);
        }
    }

    void HandleAI()
    {
        if (isGoalkeeper)
        {
            //Defend();
            //return;
        }

        if (ball != null)
        {
            aiTargetPosition = ball.transform.position;
            MoveTowards(aiTargetPosition);
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;

        // Limitar a posição dentro dos limites do campo
        newPosition.x = Mathf.Clamp(newPosition.x, fieldMin.x, fieldMax.x);
        newPosition.z = Mathf.Clamp(newPosition.z, fieldMin.z, fieldMax.z);

        rb.MovePosition(newPosition);

        if (Vector3.Distance(newPosition, target) < aiMoveThreshold)
        {
            rb.velocity = Vector3.zero;
            //animator.SetBool("isRunning", false);
            return;
        }

        //animator.SetBool("isRunning", true);
        //Vector3 rotateDirection = new Vector3(0f, direction.y, 0f);
        RotateTowards(direction);
    }

    void Shoot()
    {
        // Verifica se a bola está próxima
        if (Vector3.Distance(transform.position, ball.transform.position) <= shootRange)
        {
            // Implementar lógica de chute, por exemplo, aplicar força na bola
            Vector3 direction = (ball.transform.position - transform.position).normalized;
            ballController.Kick(direction * 20f);
            Debug.Log("Chute!");
        }
    }

    private void FixedUpdate()
    {
        // Garantir que o Rigidbody não acumule velocidade indesejada
        if (isPlayerControlled)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }
    }


    // Setters
    public void SetIsPlayerControlled(bool isPlayerControlled){
        this.isPlayerControlled = isPlayerControlled;
    }
}
