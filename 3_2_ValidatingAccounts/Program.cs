using System;
using SplashKitSDK;

namespace _3_2_ValidatingAccounts
{
    public class Program
    {
        // Menu Options Enumerable
        public enum MenuOption {
            Withdraw,
            Deposit,
            Print,
            Quit,
        }

        // Deposit into Account
        public static void DoDeposit(Account account) {
            // initialize variables
            decimal amount = 0;
            bool confirmed;

            // loop until valid amount is set
            while (amount <= 0) {
                Console.Write("Enter an amount to deposit: $");
                decimal.TryParse(Console.ReadLine(), out amount);
            }

            // round to 2 decimal places (dollars + cents)
            amount = Math.Round(amount, 2);
            Console.WriteLine($"Depositing ${amount} to Account");

            // complete deposit
            confirmed = account.Deposit(amount);
            if (confirmed) { Console.WriteLine("Action Successful"); }
            else { Console.WriteLine("Action Failed"); }
        }

        // Print Account Data
        public static void DoPrint(Account account) {
            account.Print();
        }

        // Withdraw from Account
        public static void DoWithdraw(Account account) {
            // initialize variables
            decimal amount = 0;
            bool confirmed;

            // loop until valid amount is set
            while (amount <= 0) {
                Console.Write("Enter an amount to withdraw: $");
                decimal.TryParse(Console.ReadLine(), out amount);
            }

            // round to 2 decimal places (dollars + cents)
            amount = Math.Round(amount, 2);
            Console.WriteLine($"Withdrawing ${amount} from Account");

            // complete withdraw
            confirmed = account.Withdraw(amount);
            if (confirmed) { Console.WriteLine("Action Successful"); }
            else { Console.WriteLine("Action Failed"); }
        }

        // Main Function
        public static void Main()
        {
            // initialize variables
            Account account = new Account("Shaun's Account", 1000);
            MenuOption userSelection;

            // main loop
            do {
                // get selected option
                userSelection = ReadUserOption();

                // run requested function
                switch (userSelection) {
                    case MenuOption.Withdraw:
                        DoWithdraw(account);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(account);
                        break;
                    case MenuOption.Print:
                        DoPrint(account);
                        break;
                }
            } while (userSelection != MenuOption.Quit); // stop on quit
        }

        // Read User Option
        private static MenuOption ReadUserOption() {
            // initialize variables
            int option = 0;
            int[] validOptions = new int[] { 1, 2, 3, 4, };

            // loop until valid option selected
            do {
                Console.Write(
                    ".~~~~~~~~~~~~~~~~~~~~~~~~.\n"
                    + "| 4.1 - Account Withdraw |\n"
                    + "|------------------------|\n"
                    + "| 1. Withdraw            |\n"
                    + "| 2. Deposit             |\n"
                    + "| 3. Print               |\n"
                    + "| 4. Quit                |\n"
                    + "|________________________|\n"
                );
                int.TryParse(Console.ReadLine(), out option);
            } while (!validOptions.Contains(option));
            
            // return selected option
            return (MenuOption) (option - 1);
        }
    }
}
