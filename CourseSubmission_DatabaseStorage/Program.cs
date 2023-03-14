using CourseSubmission_DatabaseStorage.Services;

var _menu = new MenuService();
while (true)
    await _menu.MainMenu();
