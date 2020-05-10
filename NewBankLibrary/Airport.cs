using System;
using System.Collections.Generic;
using System.Text;

namespace NewAirportLibrary
{
    // тип счета
    public enum TicketStatus
    {
        Bought,
        Booked
    }
    public class Airport<T> where T : Ticket
    {
        T[] tickets;

        public string Name { get; set; }

        public Airport(string name)
        {
            this.Name = name;
        }
        // метод создания счета
        public void Open(TicketStatus ticketStatus, decimal sum,
            TicketOfficeInfo addTicketHandler, TicketOfficeInfo outTicketHandler,
            TicketOfficeInfo calcHandler, TicketOfficeInfo canselTicketHandler,
            TicketOfficeInfo choiseTicketHandler)
        {
            T newTicket = null;

            switch (ticketStatus)
            {
                case TicketStatus.Bought:
                    newTicket = new BookedTicket(sum, 1) as T;
                    break;
                case TicketStatus.Booked:
                    newTicket = new BoughtTicket(sum, 40) as T;
                    break;
            }

            if (newTicket == null)
                throw new Exception("Ошибка создания счета");
            // добавляем новый счет в массив счетов      
            if (tickets == null)
                tickets = new T[] { newTicket };
            else
            {
                T[] tempTickets = new T[tickets.Length + 1];
                for (int i = 0; i < tickets.Length; i++)
                    tempTickets[i] = tickets[i];
                tempTickets[tempTickets.Length - 1] = newTicket;
                tickets = tempTickets;
            }
            // установка обработчиков событий счета
            newTicket.Add += addTicketHandler;
            newTicket.Out += outTicketHandler;
            newTicket.Cansel += canselTicketHandler;
            newTicket.Choise += choiseTicketHandler;
            newTicket.Calc += calcHandler;
            newTicket.Choise();
        }
        //добавление средств на счет
        public void Put(decimal sum, int id)
        {
            T ticket = FindTicket(id);
            if (ticket == null)
                throw new Exception("Билет не найден");
            ticket.Put(sum);
        }
        // вывод средств
        public void Out(decimal sum, int id)
        {
            T ticket = FindTicket(id);
            if (ticket == null)
                throw new Exception("Билет не найден");
            ticket.Out(sum);
        }
        // закрытие счета
        public void Cansel(int id)
        {
            int index;
            T ticket = FindTicket(id, out index);
            if (ticket == null)
                throw new Exception("билет не найден");

            ticket.Cansel();

            if (tickets.Length <= 1)
                tickets = null;
            else
            {
                // уменьшаем массив счетов, удаляя из него закрытый счет
                T[] tempTickets = new T[tickets.Length - 1];
                for (int i = 0, j = 0; i < tickets.Length; i++)
                {
                    if (i != index)
                        tempTickets[j++] = tickets[i];
                }
                tickets = tempTickets;
            }
        }
        // начисление процентов по счетам
        public void CalculatePercentage()
        {
            if (tickets == null) // если массив не создан, выходим из метода
                return;
            for (int i = 0; i < tickets.Length; i++)
            {
                tickets[i].IncrementDays();
                tickets[i].Calculate();
            }
        }
        // поиск счета по id
        public T FindTicket(int id)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i].Id == id)
                    return tickets[i];
            }
            return null;
        }
        // перегруженная версия поиска счета
        public T FindTicket(int id, out int index)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i].Id == id)
                {
                    index = i;
                    return tickets[i];
                }
            }
            index = -1;
            return null;
        }
    }
}

