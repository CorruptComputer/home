namespace Home.Pages.Games.NimModels;

public class GameState(ILogger logger)
{
    public GameBoard GameBoard { get; set; } = new(logger);

    public bool IsPlayer1Move { get; set; } = true;
    public bool AgainstAi { get; set; } = true;
    public (ushort row, ushort col)? AiMove { get; set; } = null;

    public void ChangePlayer()
    {
        IsPlayer1Move = !IsPlayer1Move;

        if (AgainstAi && !IsPlayer1Move)
        {
            for (ushort row = 0; row < GameBoard.CurrentBoard.Count; row++)
            {
                List<ushort> board = [.. GameBoard.CurrentBoard];
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
                        AiMove = (row, board[row]);
                        GameBoard.CurrentBoard[row] = board[row];
                        ChangePlayer();
                        return;
                    }
                }
            }
        }
    }
}
