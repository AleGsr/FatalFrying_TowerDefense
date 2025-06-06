using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    [SerializeField] float totalMinutes = 3;
    [SerializeField] Slider levelSlider;
    public float minutes;
    public float seconds;
    public float countTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seconds = 60;
        minutes = 0;
        UpdateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        Timing();
        WiningGame();
    }

    void WiningGame()
    {
        for (int i = 0; minutes <= totalMinutes; i++)
        {
            for (int j = 0; countTime <= 60; j++)
            {
                minutes++;
                countTime = 0;
            }
            UpdateSlider();
        }

        if (minutes == totalMinutes)
        {
            SceneManager.LoadScene("Win");
        }
    }

    void Timing()
    {

        countTime += Time.deltaTime;

    }

    void UpdateSlider()
    {
        levelSlider.value = minutes;
    }

}
