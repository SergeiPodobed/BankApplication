using System;
using System.Collections.Generic;
using System.Text;

namespace NewAirportLibrary
{
    public delegate void TicketOfficeInfo(object sender, TicketOfficeInd k);
    public class TicketOfficeInd
    {
        // Сообщение
        public string Message { get; set; }
        // Сумма, на которую изменился счет // число купленных-сданных билетов
        public decimal Sum { get; set; }

        public TicketOfficeInd(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
