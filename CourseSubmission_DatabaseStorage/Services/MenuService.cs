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
        Console.WriteLine("    1. Create a new User/Client.                   ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    2. Create a new Case.                          ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    3. Delete User/Client.                         ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("  ♦♦♦♦♦♦♦♦♦♦♦♦♦ MAIN MENU - EMPLOYEES ♦♦♦♦♦♦♦♦♦♦♦♦♦");
        Console.WriteLine("                ---------------------              ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    4. View all Cases in the database.             ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    5. View all Active Cases in descending order.  ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    6. Search for a specific Case with \"CaseId\"  ");
        Console.WriteLine("       to view more info & comments.               ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    7. Create a Comment on a Case & Update its     ");
        Console.WriteLine("       Status.                                     ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("    8. Exit the program.                           ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("  ♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦\n");
        Console.Write("    ☻ Enter a menu option ↑ (1-8): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await CreateClientMenu();
                break;

            case "2":
                await CreateCaseMenu();
                break;

            case "3":
                await DeleteClientMenu();
                break;

            case "4":
                await ViewAllCasesMenu();
                break;

            case "5":
                await ViewAllActiveCases();
                break;

            case "6":
                await SearchSpecificCaseMenu();
                break;

            case "7":
                await CreateCommentAndUpdateStatusMenu();
                break;

            case "8":
                Environment.Exit(1);
                break;

            default:
                ErrorMenu();
                break;
        }

        Console.ReadKey();
    }

    private async Task CreateClientMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n---------------- Create New User/Client ---------------\n");

        ClientEntity _client = new();
        AdressEntity _adress = new();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n*Firstname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _client.FirstName = Console.ReadLine()!.Trim();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("*Lastname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _client.LastName = Console.ReadLine()!.Trim();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("*Email: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _client.Email = Console.ReadLine()!.Trim();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Phonenumber (ex. +4670-1234567): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _client.PhoneNumber = Console.ReadLine()!.Trim();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("*Streetname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.StreetName = Console.ReadLine()!.Trim();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("*Postalcode (ex. 12345): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.PostalCode = Console.ReadLine()!.Trim();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("*City: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.City = Console.ReadLine()!.Trim();

        var _checkIfEmailExist = await _clientService.GetAsync(x => x.Email == _client.Email);

        if (!string.IsNullOrEmpty(_adress.StreetName) && !string.IsNullOrEmpty(_adress.PostalCode) && !string.IsNullOrEmpty(_adress.City)
            && !string.IsNullOrEmpty(_client.FirstName) && !string.IsNullOrEmpty(_client.LastName) && !string.IsNullOrEmpty(_client.Email) && _checkIfEmailExist == null)
        {
            var _setAdress = await _adressService.GetOrCreateAsync(_adress, x => x.StreetName == _adress.StreetName && x.PostalCode == _adress.PostalCode && x.City == _adress.City);
            _client.AdressId = _setAdress.Id;
            var _createdClient = await _clientService.SaveAsync(_client);
            if (_createdClient != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n\nWelcome {_createdClient.FirstName} {_createdClient.LastName}, a new user have been created!");
                Console.WriteLine("\nPress a key to return to main menu..");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nA new client could not be created. \nFill in all information needed* and try again.");
            Console.WriteLine("\nMake sure that you´re not already registrated here \nbecause you can´t create more accounts using the same emailadress.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n-------------------------------------------------------\n");
            Console.WriteLine("Press a key to return to main menu..");
        }
    }
    private async Task CreateCaseMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n-------------------- Create New Case -------------------\n");
        Console.Write("\n*Enter your Emailadress (ex. emma@example.com): "); Console.ForegroundColor = ConsoleColor.Gray;
        string _email = Console.ReadLine() ?? "";

        var _client = await _clientService.GetAsync(x => x.Email == _email);

        if (_client != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nName: {_client.FirstName} {_client.LastName}");
            Console.WriteLine($"Telephone: {_client.PhoneNumber}");
            Console.WriteLine($"Adress: {_client.Adress.StreetName}, {_client.Adress.PostalCode} {_client.Adress.City}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n↑ Here is your profile information,\n  do you need to update it (yes/no)? ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string? _answer = Console.ReadLine()?.ToLower().Trim();

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
                        string _title = Console.ReadLine()!.Trim();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n*Describe the case: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string _description = Console.ReadLine()!;

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
                            Console.WriteLine("\nA case could not be created. Fill in all information needed* and try again.");
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
                        string _firstName = Console.ReadLine()!.Trim();
                        if (!string.IsNullOrEmpty(_firstName))
                            _client.FirstName = _firstName;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Lastname: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string _lastName = Console.ReadLine()!.Trim();
                        if (!string.IsNullOrEmpty(_lastName))
                            _client.LastName = _lastName;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Phonenumber (ex. +4670-1234567): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string? _phoneNumber = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(_phoneNumber))
                            _client.PhoneNumber = _phoneNumber;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Streetname: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string _streetName = Console.ReadLine()!.Trim();
                        if (!string.IsNullOrEmpty(_streetName))
                            _adress.StreetName = _streetName;
                        else 
                            _adress.StreetName = _client.Adress.StreetName;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Postalcode (ex. 12345): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var _postalCode = Console.ReadLine()!.Trim();
                        if (!string.IsNullOrEmpty(_postalCode))
                            _adress.PostalCode = _postalCode;
                        else 
                            _adress.PostalCode = _client.Adress.PostalCode;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("City: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string _city = Console.ReadLine()!.Trim();
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
                            Console.WriteLine("\n-------------------- Create New Case -------------------\n");
                            Console.WriteLine("Your profile-information is updated ↓");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\nName: {_updatedClient.FirstName} {_updatedClient.LastName}");
                            Console.WriteLine($"Telephone: {_updatedClient.PhoneNumber}");
                            Console.WriteLine($"Adress: {_updatedClient.Adress.StreetName}, {_updatedClient.Adress.PostalCode} {_updatedClient.Adress.City}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            _answer = "no";
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nI did not understand your answer, try again please (yes/no): ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _answer = Console.ReadLine()?.ToLower().Trim();
                        break;
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry, I could not find you in our database.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.WriteLine("Press (c) to try a correct emailadress that you have registrated here ☻.");
            Console.WriteLine("Press (m) to return to main menu.");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            string _key = Console.ReadLine()!.ToLower().Trim();

            bool _run = true;
            while (_run)
            {
                switch (_key)
                {
                    case "c":
                        _run = false;
                        await CreateCaseMenu();
                        break;

                    case "m":
                        _run = false;
                        await MainMenu();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("I did not understand your option, try again.. ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _key = Console.ReadLine()!.ToLower().Trim();
                        break;
                }
            }

        }
    }
    private async Task DeleteClientMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n---------------- Delete Useraccount/Client ---------------\n");
        Console.Write("\n*Enter your Emailadress (ex. emma@example.com): "); 
        Console.ForegroundColor = ConsoleColor.Gray;
        string _email = Console.ReadLine() ?? "";

        var _client = await _clientService.GetAsync(x => x.Email == _email);

        if (_client != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"\nHi {_client.FirstName}! Sorry to note that you don't want to be registered \nhere anymore, are you sure you want to remove your account (yes/no)? ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string _answer = Console.ReadLine()!.ToLower();
            bool _run = true;

            while (_run)
            {
                switch (_answer)
                {
                    case "yes":
                        _run = false;
                        if(await _clientService.DeleteAsync(_client) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nYou have now been removed from our database, \nlooking forward to hear from you again!");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n----------------------------------------------------------\n");
                            Console.WriteLine("Press a key to return to main menu..");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nSomething went wrong, try again.");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n----------------------------------------------------------\n");
                            Console.WriteLine("Press a key to return to main menu..");
                        }
                        break;

                    case "no":
                        _run = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\nHappy to hear that you staying registrated!");
                        Console.WriteLine("\n----------------------------------------------------------\n");
                        Console.WriteLine("Press a key to return to main menu..");
                        Console.ReadKey();
                        await MainMenu();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nI did not understand your option, try again.. ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _answer = Console.ReadLine()!.ToLower();
                        break;
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry, I could not find you in our database.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----------------------------------------------------------\n");
            Console.WriteLine("Press (r) to try a correct emailadress that you have registrated here ☻.");
            Console.WriteLine("Press (m) to return to main menu.");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            string _key = Console.ReadLine()!.ToLower();
            bool _run = true;

            while (_run)
            {
                switch (_key)
                {
                    case "r":
                        _run = false;
                        await DeleteClientMenu();
                        break;

                    case "m":
                        _run = false;
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
            Console.WriteLine("\n---------------------------------------------");
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
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("\nPress a key to return to main menu..");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Could not find any cases because there is no \nactive cases in the database.");
            Console.WriteLine("\n------------------------------------------------------------------------");
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
        string _searchId = Console.ReadLine()!.Trim();
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
            string _key = Console.ReadLine()!.ToLower().Trim();

            bool _run = true;
            while (_run)
            {
                switch (_key)
                {
                    case "s":
                        _run = false;
                        await SearchSpecificCaseMenu();
                        break;

                    case "m":
                        _run = false;
                        await MainMenu();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("I did not understand your option, try again.. ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        _key = Console.ReadLine()!.ToLower().Trim();
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
        string _searchId = Console.ReadLine()!.Trim();

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
            string _employeeLastName = Console.ReadLine()!.ToLower().Trim();

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
                    string _textComment = Console.ReadLine()!.Trim();

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
                        string _statusOption = Console.ReadLine()!.ToLower().Trim();


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
                                _statusOption = Console.ReadLine()!.ToLower().Trim();
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
                    _employeeLastName = Console.ReadLine()!.ToLower().Trim();
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
