using UnityEngine;
using UnityEngine.UI;

public class MatchController : MonoBehaviour
{
    public int matchTimeInSeconds = 600; // 10 minutos
    private int currentTime;
    public Text timerText;
    public int scoreTeamA = 0;
    public int scoreTeamB = 0;
    public Text scoreText;

    void Start()
    {
        currentTime = matchTimeInSeconds;
        UpdateTimerUI();
        UpdateScoreUI();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Mathf.RoundToInt(Time.deltaTime);
            UpdateTimerUI();
        }
        else
        {
            EndMatch();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = currentTime / 60;
        int seconds = currentTime % 60;
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void AddGoal(string team)
    {
        if (team == "TeamA")
        {
            scoreTeamA++;
        }
        else if (team == "TeamB")
        {
            scoreTeamB++;
        }
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Team A: {scoreTeamA} - Team B: {scoreTeamB}";
    }

    void EndMatch()
    {
        // Implementar lógica de fim de partida, exibir resultado, etc.
        Debug.Log("Fim de Partida!");
    }
}
