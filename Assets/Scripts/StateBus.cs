using System;
using System.Collections.Generic;
using UnityEngine;


public static class StateBus
{
    #region Custom states and events
    //Input
    public static StateQueue<bool> Input_Enable;
    public static StateQueue<bool> Input_Disable;
    public static StateQueue<int> Input_Horizontal;
    public static StateQueue<int> Input_Vertical;

    //Boosts
    public static StateQueue<bool> Boost_Magnet;

    //Camera


    //Global States
    public static StateQueue<bool> GlobalState_Menu;
    public static StateQueue<bool> GlobalState_Game;
    public static StateQueue<bool> GlobalState_GameOver;

    //Player
    public static Transform Player_Transform;
    public static PlayerData Player_Data;
    public static bool Player_IsGrounded;
    public static StateQueue<bool> Player_ChangedFlagIsGrounded;
    public static int Player_CurrentLine;
    public static LayerMask Player_WhatIsObstacle;
    public static StateQueue<bool> Player_SideImpact;
    public static StateQueue<bool> Player_DisableHighCollider;
    public static StateQueue<bool> Player_EnableHighCollider;

    //World
    public static float World_DifficultyCoefficient;
    public static StateQueue<bool> World_DifficultyChanged;
    public static bool World_IsGameActive;
     
    //Treadmill
    public static float Treadmill_LineWidht;
    public static float Treadmill_LeftLineCoordinate;
    public static float Treadmill_MiddleLineCoordinate;
    public static float Treadmill_RightLineCoordinate;

    #endregion

    #region Standard events

    public static event Action OnAwake = delegate { };
    public static event Action OnStart = delegate { };
    public static event Action OnUpdate = delegate { };
    public static event Action OnLateUpdate = delegate { };

    #endregion

    #region Utils

    private static readonly List<IStateQueue> stateQueues = new List<IStateQueue>();

    class Updater : MonoBehaviour
    {
        private void Awake() { StateBus.Awake(); }
        private void Start() { StateBus.Start(); }
        private void Update() { StateBus.Update(); }
        private void LateUpdate() { StateBus.LateUpdate(); }
    }

    [RuntimeInitializeOnLoadMethod]
#pragma warning disable IDE0051 // Удалите неиспользуемые закрытые члены
    static void Init()
#pragma warning restore IDE0051 // Удалите неиспользуемые закрытые члены
    {
        var updater = new GameObject() { name = "StateBusUpdater" };
        updater.AddComponent<Updater>();
        GameObject.DontDestroyOnLoad(updater);
    }

    static void Awake()
    {
        stateQueues.Clear();

        foreach (var fi in typeof(StateBus).GetFields())
            if (typeof(IStateQueue).IsAssignableFrom(fi.FieldType))
            {
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
                IStateQueue stateQueue = fi.GetValue(null) as IStateQueue;
#pragma warning restore IDE0019 // Используйте сопоставление шаблонов
                //create StateQueue object, if not created
                if (stateQueue == null)
                {
                    stateQueue = Activator.CreateInstance(fi.FieldType) as IStateQueue;
                    fi.SetValue(null, stateQueue);
                }
                //save to queues list
                stateQueues.Add(stateQueue);
            }
        OnAwake();
    }

    static void Start()
    {
        OnStart();
    }

    static void Update()
    {
        OnUpdate();
    }

    static void LateUpdate()
    {
        OnLateUpdate();

        foreach (var queue in stateQueues)
            queue.Dequeue();
    }

    struct QueueItem<T>
    {
        public T Value;
        public float TimeToFire;
    }

    interface IStateQueue
    {
        void Dequeue();
    }

    /// <summary>
    /// Queue of states
    /// </summary>
    public class StateQueue<T> : IStateQueue
    {
        // Queue of events
#pragma warning disable IDE0044 // Добавить модификатор только для чтения
        private Queue<QueueItem<T>> queue = new Queue<QueueItem<T>>();
#pragma warning restore IDE0044 // Добавить модификатор только для чтения

        /// <summary>
        /// Current value of state (in current frame)
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Put event to queue
        /// </summary>
        public void Enqueue(T value, float deltaTime = 0)
        {
            queue.Enqueue(new QueueItem<T> { Value = value, TimeToFire = Time.time + deltaTime });
        }

        /// <summary>
        /// Implicit conversion to T
        /// </summary>
        public static implicit operator T(StateQueue<T> val)
        {
            return val.Value;
        }

        /// <summary>
        /// Conversion to true/false
        /// </summary>
        public static bool operator true(StateQueue<T> val)
        {
            return !val.Value.Equals(default(T));
        }

        /// <summary>
        /// Conversion to true/false
        /// </summary>
        public static bool operator false(StateQueue<T> val)
        {
            return val.Value.Equals(default(T));
        }

        /// <summary>
        /// Put event to queue via operator +
        /// </summary>
        public static StateQueue<T> operator +(StateQueue<T> vq, T val)
        {
            vq.Enqueue(val);
            return vq;
        }

        /// <summary>
        /// Clear queue, set default value
        /// </summary>
        public void Reset()
        {
            queue.Clear();
#pragma warning disable IDE0034 // Упростить выражение default
            Value = default(T);
#pragma warning restore IDE0034 // Упростить выражение default
        }

        void IStateQueue.Dequeue()
        {
            var count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                //get next event
                var item = queue.Dequeue();

                //time elapsed?
                if (item.TimeToFire <= Time.time)
                {
                    //set event to current value
                    Value = item.Value;
                    return;
                }

                //time is not elapsed => enqueue again
                queue.Enqueue(item);
            }

            //set default value
#pragma warning disable IDE0034 // Упростить выражение default
            Value = default(T);
#pragma warning restore IDE0034 // Упростить выражение default
        }
    }

    #endregion
}