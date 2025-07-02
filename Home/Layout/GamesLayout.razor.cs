using Home.Components;

namespace Home.Layout;

public partial class GamesLayout
{
    private GamesNavbar.GameButton? _gameButton { get; set; }

    private string GetTitle()
    {
        if (_gameButton == null || _gameButton == GamesNavbar.GameButton.None)
        {
            return "Games";
        }

        string gameTitle = _gameButton switch
        {
            GamesNavbar.GameButton.Nim => "Nim",
            GamesNavbar.GameButton.Voronoi => "Voronoi",
            _ => "Unknown"
        };

        return $"Games - {gameTitle}";
    }
}
