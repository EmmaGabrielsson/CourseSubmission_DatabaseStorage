namespace CourseSubmission_DatabaseStorage.Services;

internal class MenuService
{
    //private readonly CaseService _caseService = new CaseService();
    public async Task MainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦ MAIN MENU ♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦");
        Console.WriteLine("\n1. Create a new complaint report/case.\n");
        Console.WriteLine("2. View all cases in the database.\n");
        Console.WriteLine("3. Search for a specific report/case with \n   \"CaseId\" and also all its comments.\n");
        Console.WriteLine("4. Update the status of a case.\n");
        Console.WriteLine("5. Create a comment on a case.\n");
        Console.WriteLine("6. Exit the program.");
        Console.WriteLine("\n♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("\n☻ Enter a menu option (1-6): ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                //await CreateCaseMenu();
                break;

            case "2":
                //await ShowAllCasesMenu();
                break;

            case "3":
                //await SearchSpecificMenu();
                break;

            case "4":
                //await UpdateStatusMenu();
                break;

            case "5":
                //await CreateCaseCommentMenu();
                break;

            case "6":
                Environment.Exit(1);
                break;

            default:
                ErrorMenu();
                break;
        }

        Console.ReadKey();
    }

    public static void ErrorMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nOps, you entered an invalid menu option. \nPress enter and try again..");
    }
}
