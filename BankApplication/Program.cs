using System;
using NewAirportLibrary;

namespace AirportApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Airport<Ticket> airport = new Airport<Ticket>("Belavia");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen; // выводим список команд зеленым цветом
                Console.WriteLine("1. Купить билет \t 2. Сдать билет  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            Choise(airport);
                            break;
                        case 2:
                            Out(airport);
                            break;
                        case 3:
                            Put(airport);
                            break;
                        case 4:
                            CanselTicket(airport);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    airport.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    // выводим сообщение об ошибке красным цветом
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
        private static void Choise(Airport<Ticket> airport)
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета: 1. До востребования 2. Депозит");
            TicketStatus ticketStatus;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                ticketStatus = TicketStatus.Bought;
            else
                ticketStatus = TicketStatus.Booked;

            airport.Choise(ticketStatus,
                sum,
                AddSumHandler,  // обработчик добавления средств на счет
                OutSumHandler, // обработчик вывода средств
                (o, k) => Console.WriteLine(k.Message), // обработчик начислений процентов в виде лямбда-выражения
                CanselTicketHandler, // обработчик закрытия счета
                ChoiseTicketHandler); // обработчик открытия счета
        }

        private static void Out(Airport<Ticket> airport)
        {
            Console.WriteLine("Укажите сумму для вывода со счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            airport.Out(sum, id);
        }

        private static void Put(Airport<Ticket> airport)
        {
            Console.WriteLine("Укажите сумму, чтобы положить на счет:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());
            airport.Put(sum, id);
        }

        private static void CanselTicket(Airport<Ticket> airport)
        {
            Console.WriteLine("Введите id счета, который надо закрыть:");
            int id = Convert.ToInt32(Console.ReadLine());

            airport.Cansel(id);
        }
        // обработчики событий класса Account
        // обработчик открытия счета
        private static void ChoiseTicketHandler(object sender, TicketOfficeInd k)
        {
            Console.WriteLine(k.Message);
        }
        // обработчик добавления денег на счет
        private static void AddSumHandler(object sender, TicketOfficeInd k)
        {
            Console.WriteLine(k.Message);
        }
        // обработчик вывода средств
        private static void OutSumHandler(object sender, TicketOfficeInd k)
        {
            Console.WriteLine(k.Message);
            if (k.Sum > 0)
                Console.WriteLine("Идем тратить деньги");
        }
        // обработчик закрытия счета
        private static void CanselAccountHandler(object sender, TicketOfficeInd k)
        {
            Console.WriteLine(k.Message);
        }
    }
}

