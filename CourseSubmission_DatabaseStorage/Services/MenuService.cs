using CourseSubmission_DatabaseStorage.Models.Entities;
using CourseSubmission_DatabaseStorage.Models.Forms;
using Microsoft.IdentityModel.Tokens;

namespace CourseSubmission_DatabaseStorage.Services;

internal class MenuService
{
    private readonly CaseService _caseService = new();
    private readonly ClientService _clientService = new();
    private readonly StatusTypeService _statusService = new();
    public async Task MainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  ♦ ♦ ♦ ♦ ♦ ♦ MAIN MENU CLIENTS ♦ ♦ ♦ ♦ ♦ ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  1. Create a new Case.                ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦ ♦\n");
        Console.WriteLine("  ♦ ♦ ♦ ♦ ♦  MAIN MENU EMPLOYEES  ♦ ♦ ♦ ♦ ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  2. View all Cases in the database.   ♦");
        Console.WriteLine("  ♦                                       ♦");
        Console.WriteLine("  ♦  3. Search for a specific Case with   ♦");
        Console.WriteLine("  ♦    \"CaseId\" and its Comments.         ♦");
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
        var _case = new CaseEntity();

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n---------------- Create New Case ---------------\n");
        Console.Write("\n*Enter your Emailadress: "); Console.ForegroundColor = ConsoleColor.Gray;
        string _email = Console.ReadLine() ?? "";
        var _client = await _clientService.GetAsync(x => x.Email == _email);
        if (_client != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nName: {_client.FirstName} {_client.LastName}");
            Console.WriteLine($"Telephone number: {_client.PhoneNumber}");
            Console.WriteLine($"Adress: {_client.Adress.StreetName}, {_client.Adress.PostalCode} {_client.Adress.City}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n↑ Here is your profile information,\n  do you need to update it (yes/no)? ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string? _answer = Console.ReadLine();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                switch (_answer)
                {
                    case "no":
                        Console.WriteLine("\nOk, then continue with..");
                        Console.Write("\n*Casetitle: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _title = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n*Describe the case: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _description = Console.ReadLine();

                        var _status = await _statusService.GetAsync(x => x.StatusName == "Not Started");

                        _case.Id = new Guid();
                        _case.ClientId = _client.Id;
                        _case.RegistrationDate = DateTime.Now;
                        _case.StatusType = _status;
                        _status.Id = _case.StatusTypeId;

                        if (!string.IsNullOrEmpty(_title))
                        {
                            _case.Title = _title;
                            if (!string.IsNullOrEmpty(_description))
                                _case.Description = _description;
                                var result = await _caseService.SaveAsync(_case);
                                if (result != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"\nThank you {_client.FirstName}, a Case with id: {result?.Id} \nhave been created. We will get back to you as soon as possible.");
                                    Console.WriteLine("\nPress a key to return to main menu..");
                                }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nA case could not be created. Fill in all information needed and try again.");
                            Console.WriteLine("Press a key to return to main menu..");
                        }
                        Console.ReadKey();
                        await MainMenu();
                        break;

                    case "yes":
                        
                        await _clientService.UpdateAsync(_client);

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n---------------- Create New Case ---------------\n");
                            Console.WriteLine("\nYour profile-information is updated ↓");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\nName: {_client.FirstName} {_client.LastName}");
                            Console.WriteLine($"Telephone number: {_client.PhoneNumber}");
                            Console.WriteLine($"Adress: {_client.Adress.StreetName}, {_client.Adress.PostalCode} {_client.Adress.City}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            _answer = "no";
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nI did not understand your answer, try again please (yes/no): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _answer = Console.ReadLine();
                        break;
                }

            }
        }
        else
            Console.WriteLine("\nSorry, I could not find you in our database. Enter a key and I will bring you back \nto type in a correct emailadress that you have registrated here ☻ "); 
            Console.ReadKey();
            await CreateCaseMenu();
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
