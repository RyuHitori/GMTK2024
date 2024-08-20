using TMPro;
using UnityEngine;

public class SimpleClock : MonoBehaviour
{
    // Public variables to set the start time
    public int hour = 0;
    public int minute = 0;
    public int second = 0;

    private float timer = 0f, blinkTimer = 0f;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI secondText;

    bool showColon;

    void Start()
    {
        // Set initial time based on public variables
        timer = hour * 3600 + minute * 60 + second + Time.realtimeSinceStartup;
    }

    void Update()
    {
        // Increase the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Update hour, minute, and second based on the timer
        hour = (int)(timer / 3600) % 24;
        minute = (int)(timer / 60) % 60;
        second = (int)(timer % 60);

        // Update the blink timer
        blinkTimer += Time.deltaTime;

        // Toggle colon visibility every half second
        showColon = second % 2 == 0;

        // Update the time text with or without the colon
        string colon = showColon ? ":" : " ";
        timeText.text = hour.ToString("D2") + colon + minute.ToString("D2");
        secondText.text = second.ToString("D2");
    }
}