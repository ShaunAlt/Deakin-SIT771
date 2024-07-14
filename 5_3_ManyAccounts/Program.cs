using System;
using SplashKitSDK;

namespace _5_3_ManyAccounts
{
    public class Program
    {
        // Menu Options Enumerable
        public enum MenuOption
        {
            Withdraw,
            Deposit,
            Transfer,
            Print,
            NewAccount,
            Quit,
        }

        // Deposit into Account
        public static void DoDeposit(Bank b)
        {
            // get the account to deposit to
            Account account = FindAccount(b);
            if (account == null) { return; }

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
            b.ExecuteTransaction(t);
            t.Print();
        }

        // Print Account Data
        public static void DoPrint(Bank b)
        {
            Console.WriteLine("Bank Data:");
            for (int i = 0; i < b.Accounts.Count; i++)
            {
                Console.Write(
                    $"| - Account {i+1}:\n"
                    + $"|\t| - Name: {b.Accounts[i].Name}\n"
                    + $"|\t| - Balance: ${b.Accounts[i].Balance:N2}\n"
                );
            }
        }

        // Transfer Between Accounts
        public static void DoTransfer(Bank b)
        {
            // get the accounts to transfer from and to
            Console.Write("(Withdraw) ");
            Account accountFrom = FindAccount(b);
            if (accountFrom == null) { return; }
            Console.Write("(Deposit) ");
            Account accountTo = FindAccount(b);
            if (accountTo == null) { return; }

            // prevent transfer if accounts are the same
            if (accountFrom.Name == accountTo.Name)
            {
                Console.WriteLine("Cannot transfer to the same account.");
                return;
            }

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
            b.ExecuteTransaction(t);
            t.Print();
        }

        // Withdraw from Account
        public static void DoWithdraw(Bank b)
        {
            // get account to withdraw from
            Account account = FindAccount(b);
            if (account == null) { return; }

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
            b.ExecuteTransaction(t);
            t.Print();
        }

        // Find Account
        public static Account FindAccount(Bank b)
        {
            // initialize variables
            string name = "";

            // loop until valid name and initial balance are set
            while (string.IsNullOrWhiteSpace(name)) {
                Console.Write("Enter the name for the account: ");
                name = Console.ReadLine();
            }

            // get account from bank
            Account a = b.GetAccount(name);
            if (a == null) {
                Console.WriteLine($"No Account with name {name}");
            }
            return a;
        }

        // Main Function
        public static void Main()
        {
            // initialize variables
            Bank b = new Bank();
            MenuOption userSelection;

            // main loop
            do {
                // get selected option
                userSelection = ReadUserOption();

                // run requested function
                switch (userSelection) {
                    case MenuOption.Withdraw:
                        DoWithdraw(b);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(b);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(b);
                        break;
                    case MenuOption.Print:
                        DoPrint(b);
                        break;
                    case MenuOption.NewAccount:
                        NewAccount(b);
                        break;
                }
            } while (userSelection != MenuOption.Quit); // stop on quit
        }

        // Create New Account
        private static void NewAccount(Bank b)
        {
            // initialize variables
            string name = "";
            decimal initialBalance = 0;

            // loop until valid name and initial balance are set
            while (string.IsNullOrWhiteSpace(name)) {
                Console.Write("Enter a name for the new account: ");
                name = Console.ReadLine();
            }

            while (initialBalance <= 0) {
                Console.Write(
                    "Enter an initial balance for the new account: $"
                );
                decimal.TryParse(Console.ReadLine(), out initialBalance);
            }

            // round to 2 decimal places (dollars + cents)
            initialBalance = Math.Round(initialBalance, 2);

            // create new account
            Account newAccount = new Account(name, initialBalance);
            b.AddAccount(newAccount);
            Console.WriteLine(
                $"Created new account: {newAccount.Name} with initial balance "
                + $"{newAccount.Balance}"
            );
        }

        // Read User Option
        private static MenuOption ReadUserOption()
        {
            // initialize variables
            int option = 0;
            int[] validOptions = new int[] { 1, 2, 3, 4, 5, 6, };

            // loop until valid option selected
            do {
                Console.Write(
                    ".~~~~~~~~~~~~~~~~~~~~~~~~.\n"
                    + "| 4.1 - Account Withdraw |\n"
                    + "|------------------------|\n"
                    + "| 1. Withdraw            |\n"
                    + "| 2. Deposit             |\n"
                    + "| 3. Transfer Between    |\n"
                    + "|     Accounts           |\n"
                    + "| 4. Print               |\n"
                    + "| 5. Create New Account  |\n"
                    + "| 6. Quit                |\n"
                    + "|________________________|\n"
                );
                int.TryParse(Console.ReadLine(), out option);
            } while (!validOptions.Contains(option));
            
            // return selected option
            return (MenuOption) (option - 1);
        }
    }
}
