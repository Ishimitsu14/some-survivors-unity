using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public sealed class ActionManager
{
  private static readonly Lazy<ActionManager> _Instance = new(() => new ActionManager());
  private readonly Dictionary<Type, object> _handlers = new();

  private ActionManager()
  {
    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    var handlerTypes = new List<Type>();

    foreach (var asm in assemblies)
    {
      var types = asm.GetTypes();
      handlerTypes.AddRange(from type in types
        where type.IsClass && !type.IsAbstract
        let attr = type.GetCustomAttribute<ActionHandlerAttribute>(false)
        where attr != null
        select type);
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

  public static ActionManager Instance => _Instance.Value;

  public void Execute<TAction>(TAction action) where TAction : struct
  {
    var actionType = typeof(TAction);

    if (!_handlers.TryGetValue(actionType, out var handlerObj))
      throw new Exception($"No handler registered for {actionType.Name}");

    if (handlerObj is not IActionHandler<TAction> typedHandler)
      throw new Exception($"Handler for {actionType.Name} does not implement IActionHandler<TAction>!");
    typedHandler.Execute(action);
  }

  public TResult Execute<TAction, TResult>(TAction action) where TAction : struct
  {
    var actionType = typeof(TAction);

    if (!_handlers.TryGetValue(actionType, out var handlerObj))
      throw new Exception($"No handler registered for {actionType.Name}");

    if (handlerObj is IActionHandler<TAction, TResult> typedHandler) return typedHandler.Execute(action);

    throw new Exception($"Handler for {actionType.Name} does not implement IActionHandler<TAction, TResult>!");
  }
}