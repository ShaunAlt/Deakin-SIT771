using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_1_AbstractTransactions
{
    public class Account
    {
        // private variables
        private decimal _balance;
        private string _name;

        // public variables
        public decimal Balance { get { return _balance; } }
        public string Name { get { return _name; } }

        // class constructor
        public Account(string name, decimal startingBalance) {
            _balance = startingBalance;
            _name = name;
        }

        // deposit function
        public bool Deposit(decimal amount) {
            if (amount > 0) {
                _balance += amount;
                return true;
            }
            return false;
        }

        // print name + balance
        public void Print() {
            Console.WriteLine($"Account: {_name}, Balance: {_balance:N2}");
        }

        // withdraw function
        public bool Withdraw(decimal amount) {
            if (amount > 0 && amount <= _balance) {
                _balance -= amount;
                return true;
            }
            return false;
        }
    }
}