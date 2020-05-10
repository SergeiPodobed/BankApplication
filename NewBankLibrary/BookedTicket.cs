using System;
using System.Collections.Generic;
using System.Text;

namespace NewAirportLibrary
{
    class BookedTicket : Ticket
    {
        public BookedTicket(decimal sum, int percentage) : base(sum, percentage)
        {
        }
        protected internal override void Choise()
        {
            base.OnChoise(new TicketOfficeInd($"Забронирован билет! Id билета: {this.Id}", this.Sum));
        }
    }
}
