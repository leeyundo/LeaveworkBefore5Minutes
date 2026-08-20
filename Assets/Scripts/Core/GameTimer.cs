using System;
using UnityEngine;

public sealed class GameTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 300f;

    public float CurrentTime { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsFinished { get; private set; }

    public event Action<float> OnTimeChanged;
    public event Action OnTimerFinished;

    private void Awake()
    {
        CurrentTime = Mathf.Max(0f, startTime);
    }

    private void Start() => StartTimer();

    private void Update()
    {
        if (!IsRunning || IsFinished)
            return;

        CurrentTime = Mathf.Max(0f, CurrentTime - Time.deltaTime);
        OnTimeChanged?.Invoke(CurrentTime);

        if (CurrentTime <= 0f)
        {
            IsRunning = false;
            IsFinished = true;
            OnTimerFinished?.Invoke();
        }
    }

    public void StartTimer()
    {
        CurrentTime = Mathf.Max(0f, startTime);
        IsFinished = CurrentTime <= 0f;
        IsRunning = !IsFinished;
        OnTimeChanged?.Invoke(CurrentTime);

        if (IsFinished)
            OnTimerFinished?.Invoke();
    }

    public void PauseTimer()
    {
        if (!IsFinished)
            IsRunning = false;
    }

    public void ResumeTimer()
    {
        if (!IsFinished)
            IsRunning = true;
    }

    public void StopTimer() => IsRunning = false;
}
