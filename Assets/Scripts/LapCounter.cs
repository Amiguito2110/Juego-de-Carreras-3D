using UnityEngine;
using UnityEngine.UI; 

public class LapCounter : MonoBehaviour
{
    public int totalLaps = 3;
    private int currentLap = 1;
    private bool firstCross = false;
    private bool raceStarted = false;
    private bool raceEnded = false;

    public Text lapText;
    public Text timerText;

    private float elapsedTime = 0f;

    private void Start()
    {
        UpdateLapText();
    }

    private void Update()
    {
        if (raceStarted && !raceEnded)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void CrossedFinishLine()
    {
        if (!firstCross)
        {
            firstCross = true;
            raceStarted = true;
        }
        else
        {
            currentLap++;
            UpdateLapText();

            if (currentLap >= totalLaps)
            {
                raceEnded = true;
                ShowFinalTime();
            }
        }
    }

    void UpdateLapText()
    {
        lapText.text = "Vuelta: " + currentLap + "/" + totalLaps;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;
        timerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    void ShowFinalTime()
    {
        timerText.text += " - ¡Carrera terminada!";
    }
}
