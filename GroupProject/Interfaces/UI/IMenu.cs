
namespace ProjectManagementApp
{
    public interface IMenu
    {
        void DisplayMenu();

        /*added int choice / IServicesUI servicesUI to interface for HandleMenuChoice
         * excluded UserRole role as roles divided into seperate Menu* classes under UI
         * Tim - 17/12/2024 - 21:00
         */
        void HandleMenuChoice(int choice,IServicesUI servicesUI);

        
    }
}