using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Messages
{
    class ImportFileMessage
    {
        public ImportFileMessage(Object file)
        {
            File = file;
        }
        public Object File { get; set; }
    }
}
