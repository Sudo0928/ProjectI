using System;

// 등록된 리스너 정보를 담는 클래스
public class RegisteredListener
{
    public Delegate Listener { get; }
    public int Priority { get; }

    public RegisteredListener(Delegate Listener, int priority)
    {
        this.Listener = Listener;
        Priority = priority;
    }
}