using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using System.Threading.Tasks;

// 이벤트 매니저 – 리스너 등록, 해제, 이벤트 발행(동기/비동기) 및 로깅 기능 포함
public static class EventManager
{
    // 키: 이벤트 타입, 값: 해당 이벤트를 처리할 리스너 리스트
    private static readonly Dictionary<Type, List<RegisteredListener>> registers = new Dictionary<Type, List<RegisteredListener>>();
    private static readonly object lockObj = new object();

    public delegate void EventListener<in T>(T e) where T : Event;

    // 리스너 객체 등록 (마인크래프트 플러그인처럼 한 객체에 여러 이벤트 핸들러 메서드가 있을 수 있음)
    public static void RegisterListener<T>(EventListener<T> listener, int priority = 0) where T : Event
    {
        Type eventType = typeof(T);

        lock (lockObj)
        {
            if (!registers.ContainsKey(eventType))
                registers[eventType] = new List<RegisteredListener>();

            registers[eventType].Add(new RegisteredListener(listener, priority));
            // 우선순위 내림차순 정렬 (높은 우선순위부터 호출)
            registers[eventType].Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }

        Debug.Log($"Registered {listener.Method.Name} for event {eventType.Name} with priority {priority}");
    }

    // 리스너 객체 해제
    public static void UnregisterListener<T>(EventListener<T> listener) where T : Event
    {
        Type eventType = typeof(T);

        lock (lockObj)
        {
            if(registers.ContainsKey(eventType))
            {
                registers[eventType].RemoveAll(s => s.Listener.Equals(listener));
            }
        }
        Debug.Log($"Unregistered listener: {listener.GetType().Name}");
    }

    // 동기식 이벤트 발행
    public static void DispatchEvent(Event gameEvent)
    {
        var eventType = gameEvent.GetType();
        Debug.Log($"Dispatching event: {eventType.Name} at {gameEvent.Timestamp}");

        List<RegisteredListener> targets = null;
        lock (lockObj)
        {
            if (registers.ContainsKey(eventType))
            {
                targets = new List<RegisteredListener>(registers[eventType]);
            }
        }

        if (targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                // 이벤트가 취소되었으면 더 이상 리스너 호출 중단 (마인크래프트에서는 일부 리스너(예: MONITOR)는 호출되지만 여기서는 단순화)
                if (gameEvent.IsCancelled)
                {
                    Debug.Log($"Event {eventType.Name} cancelled; stopping propagation.");
                    break;
                }

                try
                {
                    targets[i].Listener.DynamicInvoke(gameEvent);
                    Debug.Log($"Invoked {targets[i].Listener.Method.Name} on {targets[i].Listener.GetType().Name}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error invoking {targets[i].Listener.Method.Name}: {ex.Message}");
                }
            }
        }
    }

    // 비동기 이벤트 발행 (Task 기반)
    public static async Task DispatchEventAsync(Event gameEvent)
    {
        await Task.Run(() => DispatchEvent(gameEvent));
    }
}