using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Application.Interfaces.StrategyInterfaces
{
    public interface IMessageStrategy
    {
        string GetCreateSuccessMessage(string type);
        string GetUpdateSuccessMessage(string type);
        string GetDeleteSuccessMessage(string type);
    }
}
