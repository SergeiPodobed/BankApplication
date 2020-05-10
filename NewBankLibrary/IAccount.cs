using System;
using System.Collections.Generic;
using System.Text;

namespace NewBankLibrary
{
    public interface IAccount
    {
        // Купить билет
        void Put(decimal sum);
        // Сдать билет
        decimal Withdraw(decimal sum);
    }
}
