using UnityEngine;

public class RandomLightFlicker : MonoBehaviour
{
    public Light lightSource; // Reference to the Light component
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 1.5f; // Maximum light intensity
    public float minFlickerDelay = 0.1f; // Minimum delay between flickers in seconds
    public float maxFlickerDelay = 1.0f; // Maximum delay between flickers in seconds
    public float flickerDuration = 0.05f; // Duration of each flicker in seconds

    private float originalIntensity; // The original intensity of the light
    private float flickerTimer = 0f; // Timer to manage delay between flickers
    private float flickerEndTime = 0f; // Time when the flicker effect should end
    private float nextFlickerTime = 0f; // Time when the next flicker should occur

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>(); // Automatically get the Light component if not set
        }

        // Store the original intensity of the light
        originalIntensity = lightSource.intensity;

        // Set the initial flicker delay
        SetNextFlickerTime();
    }

    void Update()
    {
        // Increment the flicker timer
        flickerTimer += Time.deltaTime;

        // Check if it's time to flicker
        if (flickerTimer >= nextFlickerTime)
        {
            // Randomly set a new light intensity within the specified range
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);

            // Set the end time for the flicker effect
            flickerEndTime = Time.time + flickerDuration;

            // Set the time for the next flicker
            SetNextFlickerTime();

            // Reset the flicker timer
            flickerTimer = 0f;
        }

        // Restore the original intensity quickly after the flicker duration
        if (Time.time >= flickerEndTime)
        {
            lightSource.intensity = originalIntensity;
        }
    }

    void SetNextFlickerTime()
    {
        // Randomize the delay for the next flicker within the specified range
        nextFlickerTime = Random.Range(minFlickerDelay, maxFlickerDelay);
    }
}
