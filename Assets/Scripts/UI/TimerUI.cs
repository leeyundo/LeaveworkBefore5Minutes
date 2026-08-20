using TMPro;
using UnityEngine;

public sealed class TimerUI : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private TMP_Text timeText;

    private void OnEnable()
    {
        if (gameTimer != null)
            gameTimer.OnTimeChanged += Refresh;
    }

    private void Start()
    {
        if (gameTimer != null)
            Refresh(gameTimer.CurrentTime);
    }

    private void OnDisable()
    {
        if (gameTimer != null)
            gameTimer.OnTimeChanged -= Refresh;
    }

    public void Refresh(float seconds)
    {
        if (timeText != null)
            timeText.text = FormatTime(seconds);
    }

    public static string FormatTime(float seconds)
    {
        int totalSeconds = Mathf.FloorToInt(Mathf.Max(0f, seconds));
        return $"{totalSeconds / 60:00}:{totalSeconds % 60:00}";
    }
}
