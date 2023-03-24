using CourseSubmission_DatabaseStorage.Models.Entities;

namespace CourseSubmission_DatabaseStorage.Services;

internal class MenuService
{
    private readonly CaseService _caseService = new();
    private readonly ClientService _clientService = new();
    private readonly StatusTypeService _statusService = new();
    private readonly EmployeeService _employeeService = new();
    private readonly CommentService _commentService = new();
    private readonly AdressService _adressService = new();
    public async Task MainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  ♦♦♦♦♦♦♦♦♦♦♦♦♦♦ MAIN MENU - CLIENTS ♦♦♦♦♦♦♦♦♦♦♦♦♦♦");
        Console.WriteLine("                 -------------------               ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    1. Create a new Case.                          ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("  ♦♦♦♦♦♦♦♦♦♦♦♦♦ MAIN MENU - EMPLOYEES ♦♦♦♦♦♦♦♦♦♦♦♦♦");
        Console.WriteLine("                ---------------------              ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    2. View all Cases in the database.             ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    3. View all Active Cases in descending order.  ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    4. Search for a specific Case with \"CaseId\"  ");
        Console.WriteLine("       to view more info & comments.               ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    5. Create a Comment on a Case & Update its     ");
        Console.WriteLine("       Status.                                     ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    6. Exit the program.                           ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("  ♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦\n");
        Console.Write("\n    ☻ Enter a menu option ↑ (1-6): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await CreateCaseMenu();
                break;

            case "2":
                await ViewAllCasesMenu();
                break;

            case "3":
                await ViewAllActiveCases();
                break;

            case "4":
                await SearchSpecificCaseMenu();
                break;

            case "5":
                await CreateCommentAndUpdateStatusMenu();
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

    //update adress doesnt work properly, it updates in db but doesent show update correct
    //in console until it restarts, is value still set to old one and not cleared?
    private async Task CreateCaseMenu()
    {
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
            string? _answer = Console.ReadLine()?.ToLower();

            bool _run = true;
            while (_run)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                switch (_answer)
                {
                    case "no":
                        var _case = new CaseEntity();

                        Console.WriteLine("\nOk, then continue with..");
                        Console.Write("\n*Casetitle: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _title = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n*Describe the case: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _description = Console.ReadLine();

                        var _status = await _statusService.GetAsync(x => x.StatusName.ToLower() == "not started");

                        _case.Id = new Guid();
                        _case.ClientId = _client.Id;
                        _case.RegistrationDate = DateTime.Now;
                        _case.StatusTypeId = _status.Id;

                        if (!string.IsNullOrEmpty(_title))
                        {
                            _case.Title = _title;
                            if (!string.IsNullOrEmpty(_description))
                                _case.Description = _description;
                                var _result = await _caseService.SaveAsync(_case);
                                if (_result != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"\n\nThank you {_client.FirstName}, a Case with id: {_result.Id} \nhave been created. We will get back to you as soon as possible.");
                                    Console.WriteLine("\nPress a key to return to main menu..");
                                }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nA case could not be created. Fill in all information needed and try again.");
                            Console.WriteLine("Press a key to return to main menu..");
                        }
                        _run = false;
                        break;

                    case "yes":
                        AdressEntity _adress = new();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nFill in the following..");
                        Console.Write("\nFirstname: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _firstName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(_firstName))
                            _client.FirstName = _firstName;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Lastname: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _lastName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(_lastName))
                            _client.LastName = _lastName;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Phone number (ex. +4670-1234567): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _phoneNumber = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(_phoneNumber))
                            _client.PhoneNumber = _phoneNumber;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Streetname: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _streetName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(_streetName))
                            _adress.StreetName = _streetName;
                        else 
                            _adress.StreetName = _client.Adress.StreetName;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Postalcode (ex. 12345): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var _postalCode = Console.ReadLine();
                        if (!string.IsNullOrEmpty(_postalCode))
                            _adress.PostalCode = _postalCode;
                        else 
                            _adress.PostalCode = _client.Adress.PostalCode;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("City: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _city = Console.ReadLine();
                        if (!string.IsNullOrEmpty(_city))
                            _adress.City = _city;
                        else
                            _adress.City = _client.Adress.City;

                        var _setAdress = await _adressService.GetOrCreateAsync(_adress, x => x.StreetName == _adress.StreetName && x.PostalCode == _adress.PostalCode && x.City == _adress.City);

                        if (_setAdress != null)
                        {
                            _client.AdressId = _setAdress.Id;
                            _client.Adress = _setAdress;
                        }

                        var _updatedClient =  await _clientService.UpdateAsync(_client);

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n---------------- Create New Case ---------------\n");
                            Console.WriteLine("Your profile-information is updated ↓");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\nName: {_updatedClient.FirstName} {_updatedClient.LastName}");
                            Console.WriteLine($"Telephone number: {_updatedClient.PhoneNumber}");
                            Console.WriteLine($"Adress: {_updatedClient.Adress.StreetName}, {_updatedClient.Adress.PostalCode} {_updatedClient.Adress.City}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            _answer = "no";
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nI did not understand your answer, try again please (yes/no): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _answer = Console.ReadLine()?.ToLower();
                        break;
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry, I could not find you in our database. Enter a key and I will bring you back \nto type in a correct emailadress that you have registrated here ☻ "); 
            Console.ReadKey();
            await CreateCaseMenu();
        }
    }
    private async Task ViewAllCasesMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n--------------- View All Cases --------------\n");

        var _resultList = await _caseService.GetAllAsync();

        if (_resultList.Any())
        {
            foreach ( var _result in _resultList ) 
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Case Id: {_result.Id}");
                Console.WriteLine($"Created: {_result.RegistrationDate}");
                Console.WriteLine($"Status: {_result.StatusType.StatusName}");
                Console.WriteLine($"Title: {_result.Title}");
                Console.WriteLine($"Clients email: {_result.Client.Email}\n");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("\nPress a key to return to main menu..");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Could not find any cases because there is no \nregistrated cases in the database.");
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine("\nPress a key to return to main menu..");
        }
    }
    private async Task ViewAllActiveCases()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n--------------- View All Active Cases in descending order --------------\n");

        var _resultList = await _caseService.GetAllActiveAsync();

        if (_resultList.Any())
        {
            foreach (var _result in _resultList)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Case Id: {_result.Id}");
                Console.WriteLine($"Created: {_result.RegistrationDate}");
                Console.WriteLine($"Status: {_result.StatusType.StatusName}");
                Console.WriteLine($"Title: {_result.Title}");
                Console.WriteLine($"Clients email: {_result.Client.Email}\n");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------------------------------\n");
            Console.WriteLine("\nPress a key to return to main menu..");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Could not find any cases because there is no \nactive cases in the database.");
            Console.WriteLine("\n------------------------------------------------------------------------\n");
            Console.WriteLine("\nPress a key to return to main menu..");
        }

    }
    private async Task SearchSpecificCaseMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n---------------- Search Specific Case ---------------\n");
        Console.Write("Enter Case Id: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        string? _searchId = Console.ReadLine();
        CaseEntity _foundCase = new();
        _foundCase = await _caseService.GetAsync(x => x.Id.ToString() == _searchId);

        if (_foundCase != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nCase Id: {_foundCase.Id}");
            Console.WriteLine($"Case Created: {_foundCase.RegistrationDate}");
            if(!string.IsNullOrEmpty(_foundCase.UpdatedDate.ToString()))
                Console.WriteLine($"Updated: {_foundCase.UpdatedDate}");

            Console.WriteLine($"Status: {_foundCase.StatusType.StatusName}");
            Console.WriteLine($"Title: {_foundCase.Title}");
            Console.WriteLine($"Description: {_foundCase.Description}\n");
            Console.WriteLine($"Client: {_foundCase.Client.FirstName} {_foundCase.Client.LastName}");
            Console.WriteLine($"Clients adress: {_foundCase.Client.Adress.StreetName}, {_foundCase.Client.Adress.PostalCode} {_foundCase.Client.Adress.City}");
            Console.WriteLine($"Clients email: {_foundCase.Client.Email}\n");

            if (_foundCase.Comments.Any())
            {
                Console.WriteLine($"Employee Comments");
                Console.WriteLine("------------------");
                foreach (var comment in _foundCase.Comments)
                {
                    Console.WriteLine($"Comment Id: {comment.Id}");
                    Console.WriteLine($"Comment Created: {comment.Created}");
                    Console.WriteLine($"Comment: {comment.TextComment}");
                    Console.WriteLine($"Made by: {comment.Employee.Role.RoleName}, {comment.Employee.FirstName} {comment.Employee.LastName}\n");
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------------\n");
            Console.WriteLine("Press a key to return to main menu..");
            Console.ReadKey();
            await MainMenu();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry, I could not find that Case. \nYou entered an invalid Id.\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------------\n");
            Console.WriteLine("\nPress (s) to try another search.");
            Console.WriteLine("Press (m) to return to main menu.");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            string _key = Console.ReadLine()!.ToLower();

            while (true)
            {
                switch (_key)
                {
                    case "s":
                        await SearchSpecificCaseMenu();
                        break;

                    case "m":
                        await MainMenu();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("I did not understand your option, try again.. ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _key = Console.ReadLine()!.ToLower();
                        break;
                }
            }
        }
    }
    private async Task CreateCommentAndUpdateStatusMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------------------ Update Status & Create Comment on a Case -----------------\n");
        Console.Write("Enter Case Id: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        string? _searchId = Console.ReadLine();

        var _foundCase = await _caseService.GetAsync(x => (x.Id).ToString() == _searchId);

        if (_foundCase != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nHere are some brief info about the case you want to update ↓");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nCurrent Status: {_foundCase.StatusType.StatusName}\n");
            if (!string.IsNullOrEmpty(_foundCase.UpdatedDate.ToString()))
                Console.WriteLine($"Updated: {_foundCase.UpdatedDate}");
            Console.WriteLine($"Created: {_foundCase.RegistrationDate}");
            Console.WriteLine($"\nTitle: {_foundCase.Title}");
            Console.WriteLine($"Description: {_foundCase.Description}");
            Console.WriteLine($"Client: {_foundCase.Client.FirstName} {_foundCase.Client.LastName}");

            if (_foundCase.Comments.Any())
            {
                Console.WriteLine($"\nComments");
                Console.WriteLine("--------");
                foreach (var comment in _foundCase.Comments)
                {
                    Console.WriteLine($"Comment Created: {comment.Created}");
                    Console.WriteLine($"Comment: {comment.TextComment}");
                    Console.WriteLine($"Made by: {comment.Employee.Role.RoleName}, {comment.Employee.FirstName} {comment.Employee.LastName}\n");
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("Current working employees/agents:");
            var _currentEmployees = await _employeeService.GetAllAsync();
            foreach (var employee in _currentEmployees)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"♦{employee.FirstName} {employee.LastName}, ");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n");
            Console.Write("Hi employee, can you enter your lastname (select from above): ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string _employeeLastName = Console.ReadLine()!.ToLower();

            bool _runIf = true;
            while (_runIf == true)
            {
            var _employee = await _employeeService.GetAsync(x => x.LastName.ToLower() == _employeeLastName);

                if (_employee != null)
                {
                    var _createdComment = new CommentEntity();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Enter your comment: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    string? _textComment = Console.ReadLine()!;

                    if (!string.IsNullOrEmpty(_textComment))
                    {
                        _createdComment.CaseId = _foundCase.Id;
                        _createdComment.Created = DateTime.Now;
                        _createdComment.EmployeeId = _employee.Id;
                        _createdComment.Id = new Guid();
                        _createdComment.TextComment = _textComment;
                        await _commentService.SaveAsync(_createdComment);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nThank you, the comment is now created.");
                        Console.Write("What status do you want to update the case to (ongoing/completed): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string _statusOption = Console.ReadLine()!.ToLower();


                        bool _runIfStatus = true;
                        while (_runIfStatus == true)
                        {
                            var _status = await _statusService.GetAsync(x => x.StatusName.ToLower() == _statusOption);
                            if (_status != null)
                            {
                                var _case = await _caseService.GetAsync(x => (x.Id).ToString() == _searchId);
                                var _updatedCase = await _caseService.UpdateCaseStatusAsync(_case.Id, _status.Id);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"\nThank you, the status of the case is now updated to {_status.StatusName}.");
                                Console.WriteLine("\nPress a key to return to main menu..");
                                _runIfStatus = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nInvalid input, try again.. ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                _statusOption = Console.ReadLine()!.ToLower();
                            }
                        }
                    }
                    _runIf = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\nI could not find you, did you spell your lastname right? \nEnter your lastname again: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    _employeeLastName = Console.ReadLine()!.ToLower();
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry, I could not find that Case. \nYou entered an invalid Id.\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------------------------------------------------------\n");
            Console.WriteLine("Press a key to return to main menu..");
        }
    }
    private static void ErrorMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nOps, you entered an invalid menu option. \nPress a key and try again..");
    }
}
