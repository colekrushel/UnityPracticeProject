
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static event UnityAction objectInspect;
    public static event UnityAction objectEnter;
    public static event UnityAction objectLeave;
    public static void OnObjectInteract() => objectInspect?.Invoke();
    public static void OnObjectLeave() => objectLeave?.Invoke();
    public static void OnObjectEnter() => objectEnter?.Invoke();
}
