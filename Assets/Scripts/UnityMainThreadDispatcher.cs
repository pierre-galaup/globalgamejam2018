using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <inheritdoc />
/// <summary>
/// A thread-safe class which holds a queue with actions to execute on the next Update() method. It can be used to make calls to the main thread for
/// things such as UI Manipulation in Unity. It was developed for use in combination with the Firebase Unity plugin, which uses separate threads for event handling
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<IEnumerator> ExecutionQueue = new Queue<IEnumerator>();
    private static UnityMainThreadDispatcher _instance;

    /// <summary>
    /// Locks the queue and adds the IEnumerator to the queue
    /// </summary>
    /// <param name="action">IEnumerator function that will be executed from the main thread.</param>
    public static void Enqueue(IEnumerator action)
    {
        lock (ExecutionQueue)
        {
            ExecutionQueue.Enqueue(action);
        }
    }

    /// <summary>
    /// Locks the queue and adds the Action to the queue
    /// </summary>
    /// <param name="action">function that will be executed from the main thread.</param>
    /// <param name="data">parameter that will be used by the function action</param>
    public static void Enqueue(Action<object> action, object data)
    {
        Enqueue(_instance.ActionWrapper(action, data));
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        lock (ExecutionQueue)
        {
            while (ExecutionQueue.Count > 0)
            {
                StartCoroutine(ExecutionQueue.Dequeue());
            }
        }
    }

    private IEnumerator ActionWrapper(Action<object> a, object data)
    {
        a(data);
        yield return null;
    }
}