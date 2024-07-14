using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_3_ManyAccounts
{
    public class DepositTransaction
    {
        // private variables
        private Account _account; // account being deposited into
        private decimal _amount; // amount to deposit
        private bool _executed = false; // if deposit has been executed
        private bool _success = false; // if deposit was successful
        private bool _reversed = false; // if deposit was reversed

        // public variables
        public bool Executed { get { return _executed; } }
        public bool Success { get { return _success; } }
        public bool Reversed { get { return _reversed; } }

        // class constructor
        public DepositTransaction(Account account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        // complete deposit and remember success
        public void Execute()
        {
            if (_executed) // cannot execute multiple times
            {
                throw new Exception(
                    "Cannot execute transaction multiple times"
                );
            }

            // execute transaction
            _executed = true;
            _success = _account.Deposit(_amount);
        }

        // output transaction details
        public void Print()
        {
            Console.Write(
                "Deposit Transaction Details:\n"
                // + $"\t- Account: {_account.Name} (${_account.amount:N2})\n"
                + $"\t- Account: {_account.Name}\n"
                + $"\t- Amount Deposited: ${_amount:N2}\n"
                + "\t- Execution Status: "
                + (Executed ? "Executed" : "Awaiting Execution") + "\n"
                + "\t- Transaction Result: "
                + (Success ? "Success" : "Failed") + "\n"
                + "\t- Reversed: "
                + (Reversed ? "Reversed" : "Not Reversed") + "\n"
            );
        }

        // reverses transaction if it was already successful
        public void Rollback()
        {
            if (!_executed) // cannot reverse non-executed transaction
            {
                throw new Exception("Transaction has not been executed");
            }

            if (_reversed) // cannot reverse multiple times
            {
                throw new Exception(
                    "Cannot reverse transaction multiple times"
                );
            }

            // reverse transaction
            _reversed = _account.Withdraw(_amount);

            // validate that transaction was reversed
            if (!_reversed)
            {
                throw new Exception("Failed to reverse deposit");
            }
        }
    }
}