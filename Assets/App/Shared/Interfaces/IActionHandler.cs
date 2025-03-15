public interface IActionHandler<TAction, TOut> where TAction : struct
{
  TOut Execute(TAction action);
}

public interface IActionHandler<TAction> where TAction : struct
{
  void Execute(TAction action);
}

