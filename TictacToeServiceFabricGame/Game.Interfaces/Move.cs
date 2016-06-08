namespace Game.Interfaces
{
    public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool State { get; set; }
        public int MoveOrder { get; set; }

        public int PlayerId { get; set; }
    }
}