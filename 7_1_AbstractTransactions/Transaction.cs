using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_1_AbstractTransactions
{
    public abstract class Transaction
    {
        // private variables
        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        // protected variables
        protected decimal _amount;

        // public variables
        public bool Executed { get { return _executed; } }
        public bool Reversed { get { return _reversed; } }
        public abstract bool Success { get; }

        // class constructor
        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        // execute transaction
        public virtual void Execute()
        {
            if (_executed) // cannot execute multiple times
            {
                throw new Exception(
                    "Cannot execute transaction multiple times"
                );
            }

            // execute transaction
            _executed = true;

            // save date/time of transaction
            _dateStamp = DateTime.Now;
        }

        // print transaction details
        public abstract void Print(int indent = 0) ;

        // rollback transaction
        public virtual void Rollback()
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

            // rollback transaction
            _reversed = true;

            // save date/time of transaction
            _dateStamp = DateTime.Now;
        }
    }
}