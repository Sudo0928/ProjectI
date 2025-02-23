using System;

public abstract class Event
{
    public DateTime Timestamp { get; }
    public bool IsCancelled { get; private set; }

    public Event()
    {
        Timestamp = DateTime.Now;
        IsCancelled = false;
    }

    // 이벤트 전파를 중단하기 위한 메서드 (마인크래프트의 Cancellable 인터페이스와 유사)
    public void Cancel()
    {
        IsCancelled = true;
    }
}