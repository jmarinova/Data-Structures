using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.LinkedQueue
{
    public class Account
    {
        private decimal balance;

        public decimal Balance
        {
            get { return this.balance; }
        }

        public void Deposit(decimal money)
        {
            if(money < 0)
            {
                throw new ArgumentOutOfRangeException("eiiii, ne luji");
            }
            this.balance += money;
        }
    }
}
