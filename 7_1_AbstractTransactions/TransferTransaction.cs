using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_1_AbstractTransactions
{
    public class TransferTransaction : Transaction
    {
        // private variables
        private Account _fromAccount; // account to transfer from
        private Account _toAccount; // account to transfer to
        private DepositTransaction _theDeposit; // deposit transaction
        private WithdrawTransaction _theWithdraw; // withdraw transaction
        
        // public variables
        public override bool Success {
            get {
                return (_theDeposit.Success && _theWithdraw.Success);
            }
        }
        
        // class constructor
        public TransferTransaction(
                Account fromAccount,
                Account toAccount,
                decimal amount
        ) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _theDeposit = new DepositTransaction(_toAccount, _amount);
            _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
        }

        // execute transaction
        public override void Execute()
        {
            base.Execute();
            // _success = _account.Deposit(_amount);
            _theWithdraw.Execute();
            if (_theWithdraw.Success)
            {
                _theDeposit.Execute();
                if (!_theDeposit.Success) { _theWithdraw.Rollback(); }
            }
        }

        // print transaction details
        public override void Print(int indent = 0)
        {
            string s = "";
            for (int i = 0; i < indent; i++) { s += "|\t"; }
            Console.Write(
                $"{s}Transfer Transaction Details:\n"
                + $"{s}\t- From Account: {_fromAccount.Name} (${_fromAccount.Balance:N2})\n"
                + $"{s}\t- To Account: {_toAccount.Name} (${_toAccount.Balance:N2})\n"
                + $"{s}\t- Amount Transferred: ${_amount:N2}\n"
                + $"{s}\t- Execution Status: "
                + (Executed ? "Executed" : "Awaiting Execution") + "\n"
                + $"{s}\t- Transaction Result: "
                + (Success ? "Success" : "Failed") + "\n"
                + $"{s}\t- Reversed: "
                + (Reversed ? "Reversed" : "Not Reversed") + "\n"
            );
        }

        // rollback transaction
        public override void Rollback()
        {
            base.Rollback();
            if (_theWithdraw.Success) { _theWithdraw.Rollback(); }
            if (_theDeposit.Success) { _theDeposit.Rollback(); }
        }
    }
}