namespace TicTacToe.Common
{
    public interface IMove
    {
        int MoveOrder { get; }
        IPlayer PlayerId { get; }
        bool State { get; set; }
        int X { get; }
        int Y { get; }
    }
}