using System;
using System.Collections.Generic;
using System.Text;

namespace NewAirportLibrary
{
    public interface ITicketOffice
    {
        // Купить билет
        void Put(decimal sum);
        // Сдать билет
        decimal Withdraw(decimal sum);
    }
}
