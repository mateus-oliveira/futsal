using UnityEngine;
using System.Collections.Generic;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private bool withTheBall;
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    private GameObject selectedPlayer;
    private PlayerController selectedPlayerController;
    private int currentPlayerIndex = 0;
    private Ball ballController;
    [SerializeField] private GameObject ball;

    void Start() {
        selectedPlayer = players[0];
        selectedPlayerController = selectedPlayer.GetComponent<PlayerController>();
        ballController = ball.GetComponent<Ball>();
    }

    void Update()
    {
        if (withTheBall){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pass();
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchControl();
            }
        }
    }


    void Pass()
    {
        // Encontrar um companheiro de equipe próximo
        GameObject teammate = this.FindClosestTeammate();
        if (teammate != null && Vector3.Distance(
            selectedPlayer.transform.position,
            teammate.transform.position) <= 10f)
            // TODO - teammate.transform.position) <= passRange)
        {
            // Implementar lógica de passe, por exemplo, movendo a bola para o companheiro
            selectedPlayerController.SetIsPlayerControlled(false);
            PlayerController teammateController = teammate.GetComponent<PlayerController>();
            teammateController.SetIsPlayerControlled(true);
            selectedPlayer = teammate;
            selectedPlayerController = teammateController;
            ballController.ReceivePass(teammate.transform.position);
            Debug.Log("Passe para " + teammate.name);
        }
    }

    private GameObject FindClosestTeammate()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject teammate in players)
        {
            if (teammate != selectedPlayer)
            {
                float distance = Vector3.Distance(
                    selectedPlayer.transform.position,
                    teammate.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = teammate;
                }
            }
        }
        return closest;
    }

    void SwitchControl()
    {
        // Desativa controle do jogador atual
        GameObject player = players[currentPlayerIndex];
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.SetIsPlayerControlled(false);

        // Incrementa o índice e garante que está dentro do limite
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;

        // Ativa controle do novo jogador
        controller.SetIsPlayerControlled(true);
    }
}
