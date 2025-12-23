using Home.Pages.Games.Nim.Models;

namespace Home.Pages.Games.Nim;

public partial class Index(ILogger<Index> logger)
{
    private GameState State { get; set; } = new(logger);

    private NewGameOptions Options { get; set; } = new();

    private class NewGameOptions
    {
        public int NumberOfRows { get; set; } = 3;
        public ColumnConfiguration ColumnConfiguration { get; set; } = ColumnConfiguration.Triangle;
        public bool AgainstComputer { get; set; } = true;
    }

    private void StartNewGame()
    {
        logger.LogInformation("Starting new game with {NumberOfRows} rows, {ColumnConfiguration} configuration, against Computer: {AgainstComputer}", Options.NumberOfRows, Options.ColumnConfiguration, Options.AgainstComputer);

        State.AgainstComputer = Options.AgainstComputer;
        State.IsPlayer1Move = true;
        State.IsGameOver = false;
        State.ComputerMove = null;

        State.CreateGameBoard(Options.NumberOfRows, Options.ColumnConfiguration);
    }

    private string GetPlayerMoveText()
    {
        if (State.IsGameOver)
        {
            if (State.AgainstComputer)
            {
                return State.IsPlayer1Move ?  "Player wins!" : "Computer wins!";
            }

            return "Player " + (State.IsPlayer1Move ? "1" : "2") + " wins!";
        }

        if (State.AgainstComputer)
        {
            return State.IsPlayer1Move ? "Player is moving..." : "Computer is moving...";
        }

        return State.IsPlayer1Move ? "Player 1 is moving..." : "Player 2 is moving...";
    }

    private void TakeCoinsOnClick(int row, int col)
    {
        logger.LogInformation("Player clicked to take {Count} coins from row {Row}", col, row);

        if (State.AgainstComputer && !State.IsPlayer1Move)
        {
            return;
        }

        State.TakeCoins(row, col);
        StateHasChanged();

        if (!State.IsGameOver)
        {
            State.ChangePlayer(StateHasChanged);
        }
    }
}