namespace Home.Pages.Games.NimModels;

public class GameBoard(ILogger logger)
{
    public bool IsGameOver { get; set; } = false;
    public List<ushort> StartingBoard { get; set; } = [1, 2, 3];
    public List<ushort> CurrentBoard { get; set; } = [1, 2, 3];

    public void CreateGameBoard(int numberOfRows, ColumnConfiguration columnConfiguration)
    {
        logger.LogInformation("Creating game board with {NumberOfRows} rows and {ColumnConfiguration} configuration", numberOfRows, columnConfiguration);

        StartingBoard = new List<ushort>(numberOfRows);
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
                Random random = new();
                for (int i = 0; i < numberOfRows; i++)
                {
                    StartingBoard[i] = (ushort)random.Next(1, numberOfRows + 1);
                }
                break;
        }

        logger.LogInformation("Starting board: {StartingBoard}", StartingBoard);

        CurrentBoard = [.. StartingBoard];
    }

    public bool IsValidMove(ushort row, ushort count)
    {
        bool isValid = row < CurrentBoard.Count
            && count > 0
            && count <= CurrentBoard[row];

        logger.LogInformation("Taking {Count} coins from {Row} with is {IsValid}", count, row, isValid ? "valid" : "invalid");

        return isValid;
    }
}
