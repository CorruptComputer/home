namespace Home.Pages.Games.Nim.Models;

public class GameState(ILogger logger)
{
    public bool IsGameOver { get; set; } = false;
    public List<int> StartingBoard { get; set; } = [1, 2, 3];
    public List<int> CurrentBoard { get; set; } = [1, 2, 3];

    public bool IsPlayer1Move { get; set; } = true;
    public bool AgainstComputer { get; set; } = true;
    public (int row, int col)? ComputerMove { get; set; } = null;

    public void CreateGameBoard(int numberOfRows, ColumnConfiguration columnConfiguration)
    {
        logger.LogInformation("Creating game board with {NumberOfRows} rows and {ColumnConfiguration} configuration", numberOfRows, columnConfiguration);

        StartingBoard = new List<int>(numberOfRows);
        for (int i = 0; i < numberOfRows; i++)
        {
            StartingBoard.Add(0);
        }

        switch (columnConfiguration)
        {
            case ColumnConfiguration.Triangle:
                for (int i = 0; i < numberOfRows; i++)
                {
                    StartingBoard[i] = (ushort)(i + 1);
                }
                break;
            case ColumnConfiguration.Square:
                for (int i = 0; i < numberOfRows; i++)
                {
                    StartingBoard[i] = (ushort)numberOfRows;
                }
                break;
            case ColumnConfiguration.Random:
                for (int i = 0; i < numberOfRows; i++)
                {
                    StartingBoard[i] = (ushort)Random.Shared.Next(1, numberOfRows + 1);
                }
                break;
        }

        logger.LogInformation("Starting board: {StartingBoard}", StartingBoard);

        CurrentBoard = [.. StartingBoard];
    }

    public bool TakeCoins(int row, int col)
    {
        if (!IsMoveValid(row, col))
        {
            logger.LogWarning("Invalid move attempted: row {Row}, col {Col}", row, col);
            return false;
        }

        CurrentBoard[row] = col;

        if (CurrentBoard.All(x => x == 0))
        {
            IsGameOver = true;
        }

        logger.LogInformation("Set {Row} to {Col} coins.", row, col);
        logger.LogInformation("Current board: {CurrentBoard}", CurrentBoard.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
        return true;
    }

    private bool IsMoveValid(int row, int col)
    {
        if (row < 0 || row >= CurrentBoard.Count)
        {
            return false;
        }

        if (col < 0 || col >= CurrentBoard[row])
        {
            return false;
        }

        return true;
    }

    public void ChangePlayer(Action stateHasChanged)
    {
        IsPlayer1Move = !IsPlayer1Move;
        stateHasChanged();

        if (AgainstComputer && !IsPlayer1Move)
        {
            ComputerMove = FindComputerMove();
            stateHasChanged();

            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1)); // Pause for dramatic effect
                bool successfulMove = TakeCoins(ComputerMove.Value.row, ComputerMove.Value.col);

                if (!successfulMove)
                {
                    logger.LogError("Computer attempted an invalid move: row {Row}, col {Col}\nWith board state: {CurrentBoard}", ComputerMove.Value.row, ComputerMove.Value.col, CurrentBoard.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                }

                if (!IsGameOver)
                {
                    IsPlayer1Move = !IsPlayer1Move;
                }

                stateHasChanged();
            });
        }
    }

    private (int row, int count) FindComputerMove()
    {
        int row;
        for (row = 0; row < CurrentBoard.Count; row++)
        {
            List<int> board = [.. CurrentBoard];
            board[row] -= 1;

            while (board[row] >= 0)
            {
                int accum = 0;

                for (int i = 0; i < board.Count; i++)
                {
                    accum ^= board[i];
                }

                if (accum == 0)
                {
                    logger.LogInformation("AI move: {Row} {Count}", row, board[row]);
                    return (row, board[row]);
                }

                board[row] -= 1;
            }
        }

        // If no perfect move can be found, take a random valid move
        do
        {
            row = Random.Shared.Next(CurrentBoard.Count);
        } while (CurrentBoard[row] == 0);

        int col = Random.Shared.Next(CurrentBoard[row]);

        logger.LogInformation("Computer random move: {Row} {Count}", row, col);
        return (row, col);
    }
}
