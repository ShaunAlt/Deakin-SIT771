using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_1_AbstractTransactions
{
    public class WithdrawTransaction : Transaction
    {
        // private variables
        private Account _account; // account being withdrawn from
        private bool _success = false; // if withdraw was successful

        // public variables
        public override bool Success { get { return _success; } }

        // class constructor
        public WithdrawTransaction(
                Account account,
                decimal amount
        ) : base(amount)
        {
            _account = account;
        }

        // execute transaction
        public override void Execute()
        {
            base.Execute();
            _success = _account.Withdraw(_amount);
        }

        // print transaction details
        public override void Print(int indent = 0)
        {
            string s = "";
            for (int i = 0; i < indent; i++) { s += "|\t"; }
            Console.Write(
                $"{s}Withdraw Transaction Details:\n"
                + $"{s}\t- Account: {_account.Name} (${_account.Balance:N2})\n"
                + $"{s}\t- Amount Withdrawn: ${_amount:N2}\n"
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
            // _reversed = _account.Deposit(_amount);
            _account.Deposit(_amount);

            // validate that transaction was reversed
            if (!Reversed)
            {
                throw new Exception("Failed to reverse deposit");
            }
        }
    }
}