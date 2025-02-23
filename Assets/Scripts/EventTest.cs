using UnityEngine;

public class EventTest : MonoBehaviour
{
    // Unity���� ��ư Ŭ���̳� Ư�� Ʈ���ſ� ���� ȣ��
    private void Start()
    {
        TestEvents();
    }

    public void TestEvents()
    {
        // �÷��̾� ���� �̺�Ʈ ����
        PlayerDamagedEvent joinEvent = new PlayerDamagedEvent("Steve" , 10);
        EventManager.DispatchEvent(joinEvent);

        // �񵿱� ���� ����
        StartCoroutine(DispatchAsyncCoroutine());
    }

    private System.Collections.IEnumerator DispatchAsyncCoroutine()
    {
        PlayerDamagedEvent asyncBreakEvent = new PlayerDamagedEvent("Herobrine", 20);
        var task = EventManager.DispatchEventAsync(asyncBreakEvent);
        while (!task.IsCompleted)
        {
            yield return null;
        }
        Debug.Log("Asynchronous event dispatch complete.");
    }
}