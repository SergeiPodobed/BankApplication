using System;
using System.Collections.Generic;
using System.Text;

namespace NewBankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        // Сообщение
        public string Message { get; set; }
        // Сумма, на которую изменился счет // число купленных-сданных билетов
        public decimal Sum { get; set; }

        public AccountEventArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
