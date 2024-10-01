using University.Application.Interfaces.StrategyInterfaces;

namespace University.Infrastructure.Implementation.Repositories.Strategy
{
    public class ErrorMessageStrategy : IErrorMessageStrategy
    {
        public string GetCheckingErrorMessage(string type, string exceptionMessage)
            => $"An error occurred while Checking if {type} is exist : {exceptionMessage}";

        public string GetCreateErrorMessage(string type, string exceptionMessage)
            => $"An error occurred while Creating {type} : {exceptionMessage}";

        public string GetDeleteErrorMessage(string type, string exceptionMessage)
        => $"An error occurred while deleting {type} : {exceptionMessage}";

        public string GetRetrieveErrorMessage(string type, string exceptionMessage)
        => $"An error occurred while retrieving {type}(s) : {exceptionMessage}";

        public string GetUpdateErrorMessage(string type, string exceptionMessage)
        => $"An error occurred while Updating {type} : {exceptionMessage}";
    }
}
