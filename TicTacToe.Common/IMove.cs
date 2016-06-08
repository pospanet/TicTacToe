namespace TicTacToe.Common
{
    public interface IMove
    {
        int MoveOrder { get; }
        int PlayerId { get; }
        bool State { get; }
        int X { get; }
        int Y { get; }
    }
}