public interface IActionHandler<in TAction, out TOut> where TAction : struct
{
  TOut Execute(TAction action);
}

public interface IActionHandler<in TAction> where TAction : struct
{
  void Execute(TAction action);
}