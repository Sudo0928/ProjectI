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

    // �̺�Ʈ ���ĸ� �ߴ��ϱ� ���� �޼��� (����ũ����Ʈ�� Cancellable �������̽��� ����)
    public void Cancel()
    {
        IsCancelled = true;
    }
}