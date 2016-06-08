namespace TicTacToe.Common
{
    public interface IMove
    {
        int MoveOrder { get; }
        IPlayer PlayerId { get; }
        int X { get; }
        int Y { get; }
    }
}