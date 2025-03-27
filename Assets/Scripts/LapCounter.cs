using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    public int totalLaps = 3;
    private int currentLap = 0;
    private bool firstCross = false;
    private bool raceStarted = false;
    private bool raceEnded = false;

    public Text lapText;
    public Text timerText;
    public Text finishMessageText;

    private float elapsedTime = 0f;

    private void Start()
    {
        UpdateLapText();
        if (finishMessageText != null)
            finishMessageText.gameObject.SetActive(false); // Oculta el mensaje al inicio
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

            if (currentLap < totalLaps)
            {
                UpdateLapText(); // Solo actualiza si no es la última
            }
            else
            {
                raceEnded = true;
                UpdateLapText(); // Muestra Vuelta: 3/3
                ShowFinalTime();
            }
        }
    }

    void UpdateLapText()
    {
        lapText.text = "Vuelta: " + Mathf.Min(currentLap + 1, totalLaps) + "/" + totalLaps;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;
        timerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    void ShowFinalTime()
    {
        if (finishMessageText != null)
        {
            finishMessageText.text = "¡Carrera terminada!";
            finishMessageText.gameObject.SetActive(true);
        }
        // Detener el carro
        CarController car = FindObjectOfType<CarController>();
        if (car != null)
        {
            car.isRaceFinished = true;
        }
    }
}
