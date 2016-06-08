namespace TicTacToe.Common
{
    public interface IMove
    {
        int MoveOrder { get; set; }
        int PlayerId { get; set; }
        bool State { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}