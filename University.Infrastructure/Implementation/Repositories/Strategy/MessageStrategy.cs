using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Interfaces.StrategyInterfaces;

namespace University.Infrastructure.Implementation.Repositories.Strategy
{
    public class MessageStrategy : IMessageStrategy
    {
        public string GetCreateSuccessMessage(string type)
            => $"{type} Created successfully";

        public string GetDeleteSuccessMessage(string type)
            => $"{type} Deleted successfully";

        public string GetUpdateSuccessMessage(string type)
            => $"{type} Updated successfully";
    }
}
