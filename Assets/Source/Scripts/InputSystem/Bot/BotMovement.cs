using Extensions;

namespace InputSystem.Bot
{
    public class BotMovement
    {
        public int Direction { get; private set; }

        public void MoveLeft()
        {
            Direction = Directions.Left;
        }

        public void MoveRight()
        {
            Direction = Directions.Right;
        }

        public void Stop()
        {
            Direction = Directions.InPlace;
        }
    }
}