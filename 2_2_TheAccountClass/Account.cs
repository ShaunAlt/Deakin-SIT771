using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2_2_TheAccountClass
{
    public class Account
    {
        // private variables
        private decimal _balance;
        private string _name;

        // class constructor
        public Account(string name, decimal startingBalance) {
            _balance = startingBalance;
            _name = name;
        }

        // deposit function
        public void Deposit(decimal amountToAdd) {
            _balance += amountToAdd;
        }

        // name read-only
        public string Name {
            get { return _name; }
        }

        // print name + balance
        public void Print() {
            Console.WriteLine($"Account: {_name}, Balance: {_balance:N2}");
        }

        // withdraw function
        public void Withdraw(decimal amount) {
            _balance -= amount;
        }
    }
}