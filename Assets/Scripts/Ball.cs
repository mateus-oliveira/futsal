using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float stopDistance = 0.05f; // Distância mínima antes de parar o movimento

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Método para receber um passe, movendo a bola na direção da posição alvo
    public void ReceivePass(Vector3 targetPosition)
    {
        StartCoroutine(MoveBallTowards(targetPosition));
    }

    // Método para chutar a bola aplicando uma força
    public void Kick(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    // Coroutine para mover a bola em direção à posição alvo sem chegar exatamente nela
    private IEnumerator MoveBallTowards(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > stopDistance)
        {
            // Calcular a direção do movimento
            Vector3 direction = (target - transform.position).normalized;
            rb.velocity = direction * 10f; // Ajuste a velocidade conforme necessário

            yield return null;
        }

        // Parar a bola quando estiver perto o suficiente
        rb.velocity = Vector3.zero;
    }

    // Opcional: Resetar a bola para o centro após um gol
    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = Vector3.zero; // Ajuste conforme a posição central do campo
    }

    void OnCollisionEnter(Collision collision)
    {
        // Lógica adicional ao colidir com outros objetos, se necessário
        // Por exemplo, detectar gol ou interagir com jogadores
    }
}
