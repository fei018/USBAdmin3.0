using System;
using System.IO;
using System.Linq;

namespace SetupClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                _SetupHelp = new SetupHelp();

                _SetupHelp.TryUninstall_Old_HHITtools();

                if (args.Count() > 0)
                {
                    if (args[0].ToLower() == "-uninstall")
                    {
                        UnSetup();
                    }
                }
                else
                {
                    Setup();
                }
            }
            catch (Exception ex)
            {
                LogHelp.Log(ex.Message);
            }
        }

        private static SetupHelp _SetupHelp;

        static void Setup()
        {
            try
            {
                Console.WriteLine("Setup Start ...");
                Console.WriteLine();

                _SetupHelp.Setup();

                Console.WriteLine("Setup Done !!!");
                LogHelp.Log("Setup done.");
            }
            catch (Exception ex)
            {
                LogHelp.Log(ex.Message);
            }
        }

        static void UnSetup()
        {
            try
            {
                Console.WriteLine("UnSetup Start ...");

                _SetupHelp.UnSetup();

                Console.WriteLine("UnSetup Done !!!");
                LogHelp.Log("UnSetup done.");
            }
            catch (Exception ex)
            {
                LogHelp.Log(ex.Message);
            }
        }
    }
}
