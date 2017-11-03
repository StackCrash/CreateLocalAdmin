using System;
using System.DirectoryServices;

namespace CreateLocalAdmin
{
    class CreateLocalAdmin
    {
        public static DirectoryEntry CreateLocalAccount (string AccountName, string Password)
        {
            try
            {
                DirectoryEntry DirEntry = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntries DirEntries = DirEntry.Children;
                DirectoryEntry NewUser = DirEntries.Add(AccountName, "user");
                NewUser.Properties["FullName"].Add(AccountName);
                NewUser.Invoke("SetPassword", Password);
                NewUser.CommitChanges();

                return NewUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void AddLocalAdmin (DirectoryEntry User)
        {
            try
            {
                DirectoryEntry DirEntry = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry Group = DirEntry.Children.Find("Administrators", "group");

                if (Group != null)
                {
                    Group.Invoke("Add", new object[] { User.Path.ToString()});
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void WriteHelper ()
        {
            Console.WriteLine("Please enter a username and password.");
            Console.WriteLine("Usage: CreateLocalAdmin.exe <username> <password>");
        }

        static int Main(string[] args)
        {
            /* Uncomment to pass use commandline arguments
            if (args.Length == 0)
            {
                WriteHelper();
                return 1;
            }
            else if (args.Length >= 3)
            {
                WriteHelper();
                return 1;
            }
            */
            try
            {
                /* Uncomment to pass use commandline arguments
                string UserName = args[0];
                string Password = args[1];
                */


                DirectoryEntry NewUser = CreateLocalAccount(UserName, Password);
                AddLocalAdmin(NewUser);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return 0;
        }
    }
}