using System;
using System.Collections.Generic;
using System.Text;

namespace NewAirportLibrary
{
    class BoughtTicket : Ticket
    {
        public BoughtTicket(decimal sum, int percentage) : base(sum, percentage)
        {
        }
        protected internal override void Choise()
        {
            base.OnChoise(new TicketOfficeInd($"Оплачен билет! Id билета: {this.Id}", this.Sum));
        }

        public override void Put(decimal sum)
        {
            if (_days % 30 == 0)
                base.Put(sum);
            else
                base.OnAdd(new TicketOfficeInd("На счет можно положить только после 30-ти дневного периода", 0));
        }
        public override decimal Out(decimal sum)
        {
            if (_days % 30 == 0)
                return base.Out(sum);
            else
                base.OnOut(new TicketOfficeInd("Вывести средства можно только после 30-ти дневного периода", 0));
            return 0;
        }
        protected internal override void Calc()
        {
            if (_days % 30 == 0)
                base.Calc();
        }
    }
}
