using CourseSubmission_DatabaseStorage.Models.Entities;
using CourseSubmission_DatabaseStorage.Models.Forms;

namespace CourseSubmission_DatabaseStorage.Services;

internal class MenuService
{
    private readonly CaseService _caseService = new CaseService();

    //private readonly CaseService _caseService = new CaseService();
    public async Task MainMenu()
    {
        Console.Clear();
        var _datetime = DateTime.Now;
        Console.WriteLine(_datetime);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  ♦ ♦ ♦ ♦ ♦ ♦ MAIN MENU CLIENTS ♦ ♦ ♦ ♦ ♦ ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  1. Create a new Case.                ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  2. View all your Cases.              ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦\n");
        Console.WriteLine("  ♦ ♦ ♦ ♦ ♦ ♦ MAIN MENU WORKERS ♦ ♦ ♦ ♦ ♦ ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  3. View all Cases in the database.   ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  4. Search for a specific Case with   ♦");
        Console.WriteLine("  ♦    \"CaseId\" and its Comments.         ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  5. Update the Status of a Case.      ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  6. Create a Comment on a Case.       ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  7. Exit the program.                 ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("\n  ☻ Enter a menu option (1-7): ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await CreateCaseMenu();
                break;

            case "2":
                break;

            case "3":
                //await ShowAllCasesMenu();
                break;

            case "4":
                //await SearchSpecificCaseMenu();
                break;

            case "5":
                //await UpdateStatusMenu();
                break;

            case "6":
                //await CreateCaseCommentMenu();
                break;

            case "7":
                Environment.Exit(1);
                break;

            default:
                ErrorMenu();
                break;
        }

        Console.ReadKey();
    }

    public async Task CreateCaseMenu()
    {
        var _form = new CaseRegistrationForm();
        var _case = new CaseEntity();

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------------ Create New Case -----------\n");
        Console.Write("Client Firstname: "); Console.ForegroundColor = ConsoleColor.Gray;
        _form.FirstName = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Client Lastname: "); Console.ForegroundColor = ConsoleColor.Gray;
        _form.LastName = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Client Email: "); Console.ForegroundColor = ConsoleColor.Gray;
        _form.ClientEmail = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Client PhoneNumber: "); Console.ForegroundColor = ConsoleColor.Gray;
        _form.ClientPhoneNumber = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Describe the case: "); Console.ForegroundColor = ConsoleColor.Gray;
        _form.CaseDescription = Console.ReadLine() ?? "";
        _form.RegistrationDate = DateTime.Now;
        _form.CaseStatus = "Not Started";
        
        var result = await _caseService.SaveAsync(_case);
        if (result == null)
            Console.WriteLine("\nA case with the same casenumber already exists.");
        else
            Console.WriteLine($"\nThank you, a Case with id: {result.Id} have been created.\nWe will get back to you as soon as possible.");
    }
    /*
    public async Task ShowAllCasesMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------------- View All Cases ------------\n");
        foreach (var case in await _caseService.GetAllAsync())
        { Console.WriteLine($"{case.CaseNumber}, {Case.CaseTitle}, ({case.CaseStatus})\n{Case.CaseDescription}\nRegistrationDate:{case.RegistrationDate}"); 
        
        }
        Console.WriteLine("");
    }
    public async Task SearchSpecificCaseMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------------- View a specific Case ------------\n");
        await ShowAllCasesMenu();

        Console.Write("\nEnter Case Number: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        var caseNumber = Console.ReadLine();

        if (!string.IsNullOrEmpty(caseNumber))
        {
            var case = await _caseService.GetAsync(caseNumber);
            if (case != null)
            {
                Console.Clear();
                Console.WriteLine("\n------- CASEINFORMATION (WITH COMMENTS) -------\n");
                Console.Write("Case number: "); case.CaseNumber = Console.ReadLine() ?? "";
                Console.Write("Title: "); case.CaseTitle = Console.ReadLine() ?? "";
                Console.Write("Status: "); case.CaseStatus = Console.ReadLine() ?? "";
                Console.Write("Registration date: "); case.RegistrationDate = Console.ReadLine() ?? "";
                Console.Write("Completed date: "); case.CompletedDate = Console.ReadLine() ?? "";
                Console.Write("Description: "); case.CaseDescription = Console.ReadLine() ?? "";
                Console.Write("Client: ");
            }
            else
            {
                Console.WriteLine($"\nNo case number with {caseNumber} was found.");
            }
        }
        else
        {
            Console.WriteLine("\nNo case number was specified.");
        }

    }
    */

    public static void ErrorMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nOps, you entered an invalid menu option. \nPress a key and try again..");
    }
}
