using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    public GameObject endGamePanel; // <- Panel con botones y mensaje final

    public Text topTimesText;
    private List<float> bestTimes = new List<float>();
    private const int maxRecords = 10;

    private float elapsedTime = 0f;

    private void Start()
    {
        UpdateLapText();

        if (finishMessageText != null)
            finishMessageText.gameObject.SetActive(false);

        if (endGamePanel != null)
            endGamePanel.SetActive(false); // Ocultar al inicio
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
                UpdateLapText();
            }
            else
            {
                raceEnded = true;
                UpdateLapText();
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

        if (endGamePanel != null)
            endGamePanel.SetActive(true); // Mostrar panel de final

        CarController car = FindObjectOfType<CarController>();
        if (car != null)
        {
            car.isRaceFinished = true;
        }
        SaveBestTime(elapsedTime);
        DisplayTopTimes();
    }

    void SaveBestTime(float newTime)
    {
        // Cargar anteriores
        bestTimes.Clear();
        for (int i = 0; i < maxRecords; i++)
        {
            float saved = PlayerPrefs.GetFloat("BestTime" + i, -1);
            if (saved >= 0)
                bestTimes.Add(saved);
        }

        // Agregar y ordenar
        bestTimes.Add(newTime);
        bestTimes.Sort(); // menor tiempo = mejor
        if (bestTimes.Count > maxRecords)
            bestTimes.RemoveAt(bestTimes.Count - 1);

        // Guardar actualizados
        for (int i = 0; i < bestTimes.Count; i++)
        {
            PlayerPrefs.SetFloat("BestTime" + i, bestTimes[i]);
        }
        PlayerPrefs.Save();
    }

    void DisplayTopTimes()
    {
        if (topTimesText == null) return;

        string content = "Mejores tiempos:\n";
        for (int i = 0; i < bestTimes.Count; i++)
        {
            int minutes = Mathf.FloorToInt(bestTimes[i] / 60f);
            float seconds = bestTimes[i] % 60f;
            content += $"{i + 1}. {minutes:00}:{seconds:00.00}\n";
        }
        topTimesText.text = content;
    }


    // Botones de UI:
    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
