using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public GameObject skellyPrefab;

    readonly private float LIGHT_PHASE_PERIOD = 0.5f;
    readonly private float LIGHT_PHASE_INRCREMENT = 0.001f;
    readonly private float DAY_THRESHOLD = 0.99f;
    readonly private float NIGHT_THRESHOLD = 0.03f;
    readonly private float DAY_LENGTH = 10;
    readonly private float NIGHT_LENGTH = 10;

    private Light2D light2D;
    private bool daytime = true;
    private bool paused = false;

    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
        light2D.intensity = 0.9f;
        InvokeRepeating(nameof(Cycle), 5, LIGHT_PHASE_PERIOD);
    }

    void Update()
    {
        if (light2D.intensity < NIGHT_THRESHOLD && !paused)
        {
            light2D.intensity = NIGHT_THRESHOLD;
            daytime = false;
            paused = true;
            Invoke(nameof(UnPause), NIGHT_LENGTH);
            Instantiate(skellyPrefab, transform.position, Quaternion.identity);
        }
        else if (light2D.intensity > DAY_THRESHOLD && !paused)
        {
            light2D.intensity = DAY_THRESHOLD;
            daytime = true;
            paused = true;
            Invoke(nameof(UnPause), DAY_LENGTH);
        }
        Cycle();
    }

    void Cycle()
    {
        if (paused) return;
        if (daytime)
        {
            light2D.intensity -= LIGHT_PHASE_INRCREMENT;
        }
        else
        {
            light2D.intensity += LIGHT_PHASE_INRCREMENT;
        }
    }

    void UnPause()
    {
        paused = false;
    }
}