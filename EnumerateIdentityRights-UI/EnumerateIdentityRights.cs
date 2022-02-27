using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerateIdentityRights
{
    internal class EnumerateIdentityRights_UI
    {
        static void Main(string[] args)
        {
            int exit_code = 0;
            string identity = Environment.UserName;

            if (args.Length == 1)
            {
                identity = args[0];
            }
            else
            {
                Console.WriteLine("Argument position 0 not specified; defaulting to current user '" + identity + "'");
                Console.WriteLine();
            }

            try
            {
                using (EnumerateIdentityRights.LsaWrapper lsa = new EnumerateIdentityRights.LsaWrapper())
                {
                    Console.WriteLine("Identity '" + identity + "' contains the following rights (if any)");
                    Console.WriteLine("*\t" + string.Join("\n*\t", lsa.GetPrivileges(identity)));
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Re-run this application using an account which has administrative privileges");
                exit_code = 5;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception was thrown: " + ex.Message);
                exit_code = 1;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Environment.Exit(exit_code);
        }
    }
}