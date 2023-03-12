using CourseSubmission_DatabaseStorage.Models.Forms;

namespace CourseSubmission_DatabaseStorage.Services;

internal class MenuService
{
    //private readonly CaseService _caseService = new CaseService();
    public async Task MainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ MAIN MENU ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  1. Create a new Case.                ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  2. View all Cases in the database.   ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  3. Search for a specific Case with   ♦");
        Console.WriteLine("  ♦    \"CaseNumber\" and its Comments.     ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  4. Update the Status of a Case.      ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  5. Create a Comment on a Case.       ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  6. Exit the program.                 ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("\n  ☻ Enter a menu option (1-6): ");
        var option = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Yellow;

        switch (option)
        {
            case "1":
                await CreateCaseMenu();
                break;

            case "2":
                //await ShowAllCasesMenu();
                break;

            case "3":
                //await SearchSpecificCaseMenu();
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

    public async Task CreateCaseMenu()
    {
        var form = new CaseRegistrationForm();

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------------ Create New Case -----------\n");
        Console.Write("Client Firstname: "); Console.ForegroundColor = ConsoleColor.Gray;
        form.FirstName = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Client Lastname: "); Console.ForegroundColor = ConsoleColor.Gray;
        form.LastName = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Client Email: "); Console.ForegroundColor = ConsoleColor.Gray;
        form.ClientEmail = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Client PhoneNumber: "); Console.ForegroundColor = ConsoleColor.Gray;
        form.ClientPhoneNumber = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Describe the case: "); Console.ForegroundColor = ConsoleColor.Gray;
        form.CaseDescription = Console.ReadLine() ?? "";
        form.RegistrationDate = DateTime.Now;
        form.CaseStatus = "Not Started";
        /*
        var result = await _clientService.CreateAsync(form);
        if (result == null)
            Console.WriteLine("\nA case with the same casenumber already exists.");
        else
            Console.WriteLine($"\nA Case with id {result.CaseNumber} have been created.");
        */
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
