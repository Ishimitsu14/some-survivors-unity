using System;
using System.Collections.Generic;

public static class EventManager<TEvent> where TEvent : struct
{
  private static readonly Dictionary<int, Action<TEvent>> _FilteredEvents = new();
  private static event Action<TEvent> GlobalEvent;

  public static event Action<TEvent> On
  {
    add => GlobalEvent += value;
    remove => GlobalEvent -= value;
  }

  public static void Emit(TEvent eventData, int? id = null)
  {
    GlobalEvent?.Invoke(eventData);

    if (id.HasValue && _FilteredEvents.TryGetValue(id.Value, out var specificEvent)) specificEvent?.Invoke(eventData);
  }

  public static EventManagerWithScope WithScope(int id)
  {
    return new EventManagerWithScope(id);
  }

  public class EventManagerWithScope
  {
    private readonly int _id;

    public EventManagerWithScope(int id)
    {
      _id = id;
    }

    public event Action<TEvent> On
    {
      add
      {
        _FilteredEvents.TryAdd(_id, null);

        _FilteredEvents[_id] += value;
      }
      remove
      {
        if (_FilteredEvents.ContainsKey(_id))
        {
          _FilteredEvents[_id] -= value;

          if (_FilteredEvents[_id] == null) _FilteredEvents.Remove(_id);
        }

        ;
      }
    }
  }
}