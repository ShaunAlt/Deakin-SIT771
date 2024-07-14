using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4_1_Transactions
{
    public class TransferTransaction
    {
        // private variables
        private Account _fromAccount; // account to transfer from
        private Account _toAccount; // account to transfer to
        private decimal _amount; // amount to transfer
        private DepositTransaction _theDeposit; // deposit transaction
        private WithdrawTransaction _theWithdraw; // withdraw transaction
        private bool _executed = false; // if transfer has been executed
        // private bool _success = false; // if transfer was successful
        private bool _reversed = false; // if transfer was reversed

        // public variables
        public bool Executed { get { return _executed; } }
        public bool Success { get {
            return (_theDeposit.Success && _theWithdraw.Success);
        } }
        public bool Reversed { get { return _reversed; } }

        // class constructor
        public TransferTransaction(
                Account fromAccount,
                Account toAccount,
                decimal amount
        )
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;
            _theDeposit = new DepositTransaction(_toAccount, _amount);
            _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
        }

        // complete transfer and remember success
        public void Execute()
        {
            if (_executed) // cannot execute multiple times
            {
                throw new Exception(
                    "Cannot execute transaction multiple times"
                );
            }

            // execute transactions
            _executed = true;
            _theWithdraw.Execute();
            if (_theWithdraw.Success) {
                _theDeposit.Execute();
                if (!_theDeposit.Success) { _theWithdraw.Rollback(); }
            }
        }

        // output transaction details
        public void Print()
        {
            Console.Write(
                "Transfer Transaction Details:\n"
                + $"\t- From Account: {_fromAccount.Name}\n"
                + $"\t- To Account: {_toAccount.Name}\n"
                + $"\t- Amount Transferred: ${_amount:N2}\n"
                + "\t- Execution Status: "
                + (Executed ? "Executed" : "Awaiting Execution") + "\n"
                + "\t- Transfer Result: "
                + (Success ? "Success" : "Failed") + "\n"
                + "\t- Reversed: "
                + (Reversed? "Reversed" : "Not Reversed") + "\n"
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

            // reverse transactions
            _reversed = true;
            if (_theWithdraw.Success) { _theWithdraw.Rollback(); }
            if (_theDeposit.Success) { _theDeposit.Rollback(); }
        }
    }
}