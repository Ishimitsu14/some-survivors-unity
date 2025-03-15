using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ActionHandlerAttribute : Attribute
{
  public Type ActionType { get; }

  public ActionHandlerAttribute(Type actionType)
  {
    ActionType = actionType;
  }
}
