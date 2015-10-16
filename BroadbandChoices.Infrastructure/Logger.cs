using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Infrastructure
{
    public class Logger : ILogger
    {
        public void Handle(Exception ex)
        {
            var errorMessageBuilder = new StringBuilder("Error: ");
            errorMessageBuilder.Append("Source: " + ex.Source);
            errorMessageBuilder.Append("Message: " + ex.Message);
            errorMessageBuilder.Append("StackTrace: " + ex.StackTrace);
            errorMessageBuilder.Append("Date: " + DateTime.Now);
            File.WriteAllText(@"c:\Error.txt", ex.Message);
        }
    }
}
