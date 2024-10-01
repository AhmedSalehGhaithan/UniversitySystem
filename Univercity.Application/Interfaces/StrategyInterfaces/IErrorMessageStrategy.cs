using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Application.Interfaces.StrategyInterfaces
{
    public interface IErrorMessageStrategy
    {
        string GetRetrieveErrorMessage(string type,string exceptionMessage);
        string GetCreateErrorMessage  (string type, string exceptionMessage);
        string GetUpdateErrorMessage  (string type, string exceptionMessage);
        string GetDeleteErrorMessage  (string type, string exceptionMessage);
        string GetCheckingErrorMessage(string type, string exceptionMessage);
    }
}
