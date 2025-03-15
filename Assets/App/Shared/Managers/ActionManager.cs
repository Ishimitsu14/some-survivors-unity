using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ActionManager
{
  private readonly Dictionary<Type, object> _handlers = new();

  public ActionManager()
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
    }
    else
    {
      throw new Exception($"Handler for {actionType.Name} is not of type IActionHandler<{actionType.Name}>");
    }
  }

  
  public TOut Execute<TAction, TOut>(TAction action) where TAction : struct
  {
    var actionType = typeof(TAction);

    if (!_handlers.TryGetValue(actionType, out var handlerObj))
    {
      throw new Exception($"No handler registered for {actionType.Name}");
    }

    if (handlerObj is IActionHandler<TAction, TOut> typedHandler)
    {
      return typedHandler.Execute(action);
    }
    else
    {
      throw new Exception($"Handler for {actionType.Name} is not of type IActionHandler<{actionType.Name}, {typeof(TOut).Name}>");
    }
  }
}
