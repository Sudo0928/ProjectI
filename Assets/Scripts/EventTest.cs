using UnityEngine;

public class EventTest : MonoBehaviour
{
    // Unity에서 버튼 클릭이나 특정 트리거에 의해 호출
    private void Start()
    {
        TestEvents();
    }

    public void TestEvents()
    {
        // 플레이어 입장 이벤트 발행
        PlayerJoinEvent playerJoinEvent = new PlayerJoinEvent("강현종");
        EventManager.DispatchEvent(playerJoinEvent);

        PlayerDamagedEvent damagedEvent = new PlayerDamagedEvent("Steve" , 10);
        EventManager.DispatchEvent(damagedEvent);

        // 비동기 발행 예제
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