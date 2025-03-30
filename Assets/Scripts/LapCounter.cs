using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

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
    //Lista de mejores tiempos locales y la cantidad máxima a almacenar.
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

    //Mientras la carrera esté activa, va sumando tiempo y actualizando el cronómetro en pantalla.
    private void Update()
    {
        if (raceStarted && !raceEnded)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }
    //Se ejecuta cada vez que el auto pasa por la meta
    public void CrossedFinishLine()
    {
        //Si es la primera vez, inicia la carrera.
        if (!firstCross)
        {
            firstCross = true;
            raceStarted = true;
        }
        else
        {   //Si ya está en carrera, suma una vuelta.
            currentLap++;

            if (currentLap < totalLaps)
            {
                UpdateLapText();
            }
            else
            {
                //Si completa las vueltas, termina la carrera y lanza el panel final.
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

    //Muestra los elementos de fin de carrera y llama a funciones para guardar y mostrar los mejores tiempos.
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
        Debug.Log("Panel activado");
        Debug.Log("LapManager activo: " + gameObject.activeSelf);

    }

    // Carga anteriores tiempos de PlayerPrefs
    // Añade el nuevo, ordena, y guarda los 10 mejores
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

    // Muestra los mejores tiempos ordenados en un Text de la UI
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
        //Reinicia la carrera cargando la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        //Reegresa al Menu Principal
        SceneManager.LoadScene("MainMenu");
    }

    public void DebugClickTest()
    {
        Debug.Log("¡Se hizo clic en el botón!");
    }

}
