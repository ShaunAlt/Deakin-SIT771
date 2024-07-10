using System;
using SplashKitSDK;

namespace _2_2_TheAccountClass
{
    public class Program
    {
        public static void Main()
        {
            // Account 1
            Account account = new Account("Shaun's Account", 100);
            account.Print();
            account.Deposit(50);
            account.Print();
            account.Withdraw(25);
            account.Print();

            // Account 2
            Account a2 = new Account("New Account", 999);
            a2.Print();
            a2.Deposit(50);
            a2.Print();
            a2.Withdraw(25);
            a2.Print();
        }
    }
}
