using System;
using System.Collections.Generic;
using System.Reflection;

public sealed class ActionManager
{
  private readonly Dictionary<Type, object> _handlers = new();

  private static readonly Lazy<ActionManager> _instance = new(() => new ActionManager());
  public static ActionManager Instance => _instance.Value;

  private ActionManager()
  {
    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    var handlerTypes = new List<Type>();

    foreach (var asm in assemblies)
    {
      var types = asm.GetTypes();
      foreach (var type in types)
      {
        if (type.IsClass && !type.IsAbstract)
        {
          var attr = type.GetCustomAttribute<ActionHandlerAttribute>(false);
          if (attr != null)
          {
            handlerTypes.Add(type);
          }
        }
      }
    }

    foreach (var hType in handlerTypes)
    {
      var attr = hType.GetCustomAttribute<ActionHandlerAttribute>(false);
      if (attr == null) continue;

      var instance = Activator.CreateInstance(hType);
      if (instance == null) continue;

      var actionType = attr.ActionType;
      _handlers[actionType] = instance;
    }
  }

  public void Execute<TAction>(TAction action) where TAction : struct
  {
    var actionType = typeof(TAction);

    if (!_handlers.TryGetValue(actionType, out var handlerObj))
    {
      throw new Exception($"No handler registered for {actionType.Name}");
    }

    if (handlerObj is IActionHandler<TAction> typedHandler)
    {
      typedHandler.Execute(action);
      return;
    }

    throw new Exception($"Handler for {actionType.Name} does not implement IActionHandler<TAction>!");
  }

  public TResult Execute<TAction, TResult>(TAction action) where TAction : struct
  {
    var actionType = typeof(TAction);

    if (!_handlers.TryGetValue(actionType, out var handlerObj))
    {
      throw new Exception($"No handler registered for {actionType.Name}");
    }

    if (handlerObj is IActionHandler<TAction, TResult> typedHandler)
    {
      return typedHandler.Execute(action);
    }

    throw new Exception($"Handler for {actionType.Name} does not implement IActionHandler<TAction, TResult>!");
  }
}
