using System;
using System.Collections.Generic;

public static class EventManager<TEvent> where TEvent : struct
{
    private static event Action<TEvent> GlobalEvent;

    private static readonly Dictionary<int, Action<TEvent>> FilteredEvents = new();

    public static event Action<TEvent> On
    {
        add => GlobalEvent += value;
        remove => GlobalEvent -= value;
    }

    public static void Emit(TEvent eventData, int? id = null)
    {
        GlobalEvent?.Invoke(eventData);

        if (id.HasValue && FilteredEvents.TryGetValue(id.Value, out var specificEvent))
        {
            specificEvent?.Invoke(eventData);
        }
    }

    public static EventManagerWithId WithId(int id)
    {
        return new EventManagerWithId(id);
    }

    public class EventManagerWithId
    {
        private readonly int _id;

        public EventManagerWithId(int id)
        {
            _id = id;
        }

        public event Action<TEvent> On
        {
            add
            {
                if (!FilteredEvents.ContainsKey(_id))
                {
                    FilteredEvents[_id] = null;
                }

                FilteredEvents[_id] += value;
            }
            remove
            {
                if (FilteredEvents.ContainsKey(_id))
                {
                    FilteredEvents[_id] -= value;

                    if (FilteredEvents[_id] == null)
                    {
                        FilteredEvents.Remove(_id);
                    }
                }
                ;
            }
        }
    }

}