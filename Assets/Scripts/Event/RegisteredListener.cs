using System;

// ��ϵ� ������ ������ ��� Ŭ����
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