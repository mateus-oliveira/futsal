using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerControlled;
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float passRange = 10f;
    public float shootRange = 15f;
    public NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Ball ball;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (isPlayerControlled)
        {
            HandlePlayerInput();
        }
        else
        {
            HandleAI();
        }
    }

    void HandlePlayerInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            agent.Move(movement * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Implementar comandos de passe, chute, defesa aqui
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Passe
            Pass();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Chute
            Shoot();
        }
    }

    void HandleAI()
    {
        // L�gica de IA para movimenta��o, passe, chute, defesa
        // Exemplo b�sico: seguir a bola
        if (ball != null)
        {
            agent.SetDestination(ball.transform.position);
        }
    }

    void Pass()
    {
        // Implementar l�gica de passe
        Debug.Log("Passe");
    }

    void Shoot()
    {
        // Implementar l�gica de chute
        Debug.Log("Chute");
    }
}
