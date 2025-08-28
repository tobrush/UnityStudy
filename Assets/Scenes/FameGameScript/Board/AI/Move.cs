public abstract class Move
{

}

public class MoveTicTac : Move
{
    public int x, y;
    public int player;

    public MoveTicTac(int x, int y, int player)
    {
        this.x = x;
        this.y = y;
        this.player = player;
    }
}