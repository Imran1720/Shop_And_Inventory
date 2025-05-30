using System;

public class EventController
{
    public event Action baseEvent;
    public void InvokeEvent() => baseEvent?.Invoke();
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;
}
public class EventController<T>
{
    public event Action<T> baseEvent;

    public void InvokeEvent(T value) => baseEvent?.Invoke(value);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}

public class EventController<T1, T2>
{
    public event Action<T1, T2> baseEvent;

    public void InvokeEvent(T1 value1, T2 value2) => baseEvent?.Invoke(value1, value2);
    public void AddListener(Action<T1, T2> listener) => baseEvent += listener;
    public void RemoveListener(Action<T1, T2> listener) => baseEvent -= listener;
}
