using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_1_AbstractTransactions
{
    public class Bank
    {
        // private variables
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        // public variables
        public List<Account> Accounts { get { return _accounts; } }
        public List<Transaction> Transactions { get { return _transactions; } }

        // class constructor
        public Bank() {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        // add account to bank
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        // execute transaction
        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        // get account from bank
        public Account GetAccount(string name)
        {
            return _accounts.FirstOrDefault(a => a.Name == name);
        }

        // print transaction history
        public void PrintTransactionHistory()
        {
            Console.WriteLine("Transaction History:");
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine($"|\tTransaction {i+1}:");
                // _transactions[i].Print(indent = 2);
                _transactions[i].Print(2);
            }
        }
    }
}