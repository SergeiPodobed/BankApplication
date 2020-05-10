using System;
using System.Collections.Generic;
using System.Text;

namespace NewAirportLibrary
{
    public abstract class Ticket : ITicketOffice
    {
        //Событие, возникающее при выводе денег //сдаче билета
        protected internal event TicketOfficeInfo Out;
        // Событие возникающее при добавление на счет //покупке билета
        protected internal event TicketOfficeInfo Add;
        // Событие возникающее при открытии счета //выборе рейса - массив дат и рейсов
        protected internal event TicketOfficeInfo Choise;
        // Событие возникающее при закрытии счета // выборе рейса - определение конкретного рейса
        protected internal event TicketOfficeInfo Cansel;
        // Событие возникающее при начислении процентов // штраф за возрат билета
        protected internal event TicketOfficeInfo Calc;

        static int counter = 0;
        protected int _days = 0; // время с момента открытия счета // до даты вылета

        public Ticket (decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        // Текущая сумма на счету   - стоимость купленного билета
        public decimal Sum { get; set; }
        // Процент начислений          -  процент штрафа
        public int Percentage { get; set; }
        // Уникальный идентификатор счета      - номер билета
        public int Id { get; set; }
        // вызов событий
        private void CallEvent(TicketOfficeInd k, TicketOfficeInfo processor)
        {
            if (k != null)
                processor?.Invoke(this, k);
        }
        // вызов отдельных событий. Для каждого события определяется свой витуальный метод
        protected virtual void OnChoise(TicketOfficeInd k)
        {
            CallEvent(k, Choise);
        }
        protected virtual void OnOut(TicketOfficeInd k)
        {
            CallEvent(k, Out);
        }
        protected virtual void OnAdd(TicketOfficeInd k)
        {
            CallEvent(k, Add);
        }
        protected virtual void OnCansel(TicketOfficeInd k)
        {
            CallEvent(k, Cansel);
        }
        protected virtual void OnCalc(TicketOfficeInd k)
        {
            CallEvent(k, Calc);
        }

        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdd(new TicketOfficeInd ("Куплено билетов " + sum, sum)); // куплено билетов на сумму 
        }
        // метод снятия со счета, возвращает сколько снято со счета  - возвращает сумму за вычетом штрафа
        public virtual decimal Out(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnOut(new TicketOfficeInd($"Сумма {sum} возвращено билетов на сумму {Id}", sum));
            }
            else
            {
                OnOut(new TicketOfficeInd($"Указано неверное число билетов {Id}", 0)); // указано неверное число билетов
            }
            return result;
        }
        // открытие счета
        protected internal virtual void Choise()  //покупка билета
        {
            OnChoise(new TicketOfficeInd($"Куплен билет! Id билета: {Id}", Sum)); // куплен билет №
        }
        // закрытие счета
        protected internal virtual void Cansel() //возврат билета
        {
            OnCansel(new TicketOfficeInd($"Билет {Id} Аннулирован.  Сумма к возврату: {Sum}", Sum)); // билет закрыт, сумма к возврату --
        }

        protected internal void IncrementDays() // начисление штрафа
        {
            _days++;
        }
        // начисление процентов
        protected internal virtual void Calc()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalc(new TicketOfficeInd($"Начислен штраф в размере: {increment}", increment));
        }
    }
}
