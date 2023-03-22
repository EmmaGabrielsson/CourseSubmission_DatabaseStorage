using CourseSubmission_DatabaseStorage.Services;

internal class Program
{
    private static async Task Main()
    {
        StatusTypeService _statusTypeService = new ();
        AdressService _adressService = new ();
        RoleService _roleService = new ();
        EmployeeService _employeeService = new ();
        ClientService _clientService = new ();
        CaseService _caseService = new ();
        CommentService _commentService = new ();

        MenuService _menuService = new();

        //Initialize Data in Database-tables, with code-first approach, when starting Program
        await _statusTypeService.CreateInitializedStatusAsync();
        await _adressService.CreateInitializedAdressAsync();
        await _roleService.CreateInitializedRoleAsync();
        await _employeeService.CreateInitializedEmployeeAsync();
        await _clientService.CreateInitializedClientAsync();
        await _caseService.CreateInitializedCaseAsync();
        await _commentService.CreateInitializedCommentAsync();

        //Run Program
        while (true)
        {
            await _menuService.MainMenu();
        }
    }
}