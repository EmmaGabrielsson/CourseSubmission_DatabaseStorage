using CourseSubmission_DatabaseStorage.Services;

internal class Program
{
    private static async Task Main()
    {
        var _menu = new MenuService();
        while (true)
        {
            await _menu.MainMenu();
        }
    }
}