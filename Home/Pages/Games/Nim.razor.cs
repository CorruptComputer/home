using Home.Pages.Games.NimModels;

namespace Home.Pages.Games;

public partial class Nim(ILogger<Nim> logger)
{
    protected int NumberOfRows { get; set; } = 4;

    protected ColumnConfiguration ColumnConfiguration { get; set; } = ColumnConfiguration.Triangle;

    protected GameState State { get; set; } = new(logger);

    protected string GetPlayerMoveText()
    {
        if (State.GameBoard.IsGameOver)
        {
            if (State.AgainstAi)
            {
                return State.IsPlayer1Move ? "AI wins!" : "Player wins!";
            }

            return "Player " + (State.IsPlayer1Move ? "2" : "1") + " wins!";
        }

        if (State.AgainstAi)
        {
            return State.IsPlayer1Move ? "Player is moving..." : "AI is moving...";
        }

        return State.IsPlayer1Move ? "Player 1 is moving..." : "Player 2 is moving...";
    }

    protected void TakeCoinsOnClick(ushort row, ushort count)
    {
        if (State.AgainstAi && State.IsPlayer1Move)
        {
            return;
        }

        if (State.GameBoard.CurrentBoard[row] >= count)
        {
            State.GameBoard.CurrentBoard[row] = count;
        }

        if (State.GameBoard.CurrentBoard.All(x => x == 0))
        {
            State.GameBoard.IsGameOver = true;
        }

        if (!State.GameBoard.IsGameOver)
        {
            State.ChangePlayer();
        }
    }
}