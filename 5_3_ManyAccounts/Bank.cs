using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_3_ManyAccounts
{
    public class Bank
    {
        // private variables
        private List<Account> _accounts;

        // public variables
        public List<Account> Accounts { get { return _accounts; } }

        // class constructor
        public Bank() {
            _accounts = new List<Account>();
        }

        // add account to bank
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        // get account from bank
        public Account GetAccount(string name)
        {
            return _accounts.FirstOrDefault(a => a.Name == name);
        }

        // execute transaction - withdraw
        public void ExecuteTransaction(WithdrawTransaction transaction)
        {
            transaction.Execute();
        }

        // execute transaction - deposit
        public void ExecuteTransaction(DepositTransaction transaction)
        {
            transaction.Execute();
        }
        // execute transaction - transfer
        public void ExecuteTransaction(TransferTransaction transaction)
        {
            transaction.Execute();
        }
    }
}