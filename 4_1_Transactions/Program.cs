using System;
using SplashKitSDK;

namespace _4_1_Transactions
{
    public class Program
    {
        // Menu Options Enumerable
        public enum MenuOption {
            Withdraw,
            Deposit,
            Transfer,
            Print,
            Quit,
        }

        // Deposit into Account
        public static void DoDeposit(Account account) {
            // initialize variables
            decimal amount = 0;

            // loop until valid amount is set
            while (amount <= 0) {
                Console.Write("Enter an amount to deposit: $");
                decimal.TryParse(Console.ReadLine(), out amount);
            }

            // round to 2 decimal places (dollars + cents)
            amount = Math.Round(amount, 2);
            Console.WriteLine($"Depositing ${amount} to Account");

            // // complete deposit
            DepositTransaction t = new DepositTransaction(account, amount);
            t.Execute();
            t.Print();
        }

        // Print Account Data
        public static void DoPrint(Account account) {
            account.Print();
        }

        // Transfer Between Accounts
        public static void DoTransfer(Account accountFrom, Account accountTo) {
            // initialize variables
            decimal amount = 0;

            // loop until valid amount is set
            while (amount <= 0) {
                Console.Write("Enter an amount to transfer: $");
                decimal.TryParse(Console.ReadLine(), out amount);
            }

            // round to 2 decimal places (dollars + cents)
            amount = Math.Round(amount, 2);
            Console.WriteLine($"Transfering ${amount} from Account to Account");

            // complete transfer
            TransferTransaction t = new TransferTransaction(
                accountFrom,
                accountTo,
                amount
            );
            t.Execute();
            t.Print();
        }

        // Withdraw from Account
        public static void DoWithdraw(Account account) {
            // initialize variables
            decimal amount = 0;

            // loop until valid amount is set
            while (amount <= 0) {
                Console.Write("Enter an amount to withdraw: $");
                decimal.TryParse(Console.ReadLine(), out amount);
            }

            // round to 2 decimal places (dollars + cents)
            amount = Math.Round(amount, 2);
            Console.WriteLine($"Withdrawing ${amount} from Account");

            // complete withdraw
            WithdrawTransaction t = new WithdrawTransaction(account, amount);
            t.Execute();
            t.Print();
        }

        // Main Function
        public static void Main()
        {
            // initialize variables
            Account accountShaun = new Account("Shaun's Account", 1000);
            Account accountJake = new Account("Jake Acc", 10000000);
            MenuOption userSelection;

            // main loop
            do {
                // get selected option
                userSelection = ReadUserOption();

                // run requested function
                switch (userSelection) {
                    case MenuOption.Withdraw:
                        DoWithdraw(accountShaun);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(accountShaun);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(accountJake, accountShaun);
                        break;
                    case MenuOption.Print:
                        DoPrint(accountShaun);
                        break;
                }
            } while (userSelection != MenuOption.Quit); // stop on quit
        }

        // Read User Option
        private static MenuOption ReadUserOption() {
            // initialize variables
            int option = 0;
            int[] validOptions = new int[] { 1, 2, 3, 4, 5, };

            // loop until valid option selected
            do {
                Console.Write(
                    ".~~~~~~~~~~~~~~~~~~~~~~~~.\n"
                    + "| 4.1 - Account Withdraw |\n"
                    + "|------------------------|\n"
                    + "| 1. Withdraw            |\n"
                    + "| 2. Deposit             |\n"
                    + "| 3. Transfer from Jake  |\n"
                    + "| 4. Print               |\n"
                    + "| 5. Quit                |\n"
                    + "|________________________|\n"
                );
                int.TryParse(Console.ReadLine(), out option);
            } while (!validOptions.Contains(option));
            
            // return selected option
            return (MenuOption) (option - 1);
        }
    }
}
