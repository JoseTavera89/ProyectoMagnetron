using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Infrastructure.Extensions
{
    public static class Log
    {

        private static readonly string _path = "log2.txt";


        public static void Save(string message)
        {
            File.AppendAllText(_path, message + Environment.NewLine);

        }
    }
}
