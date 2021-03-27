using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConsAppCurrency
{
    class Program
    {
        static CurrencyRepository rep = new CurrencyRepository();
        static void Main(string[] args)
        {
            ShowAll();
            bool start = true;
            while (start)
            {
                Console.Write("Code: ");
                string code = Console.ReadLine();
                Console.Write("Date (format - 30.12.2021): ");
                var dateData = Console.ReadLine().Split('.');
                DateTime date = new DateTime(Convert.ToInt32(dateData[2]),
                                             Convert.ToInt32(dateData[1]),
                                             Convert.ToInt32(dateData[0]));
                string res = rep.GetCurByCode(code, date);
                if (!string.IsNullOrEmpty(res))
                {
                    Console.WriteLine(res);
                    Console.WriteLine();

                    using (AppContext context = new AppContext())
                    {
                        if (!context.Currency.Any(x => x.Code == code && x.Date.Date == date.Date))
                        {
                            Currency cur = new Currency();
                            cur.Name = rep.GetCurName(code);
                            cur.Code = code;
                            cur.Date = date;
                            cur.Nominal = Convert.ToInt32(res.Split(" = ")[0]);
                            cur.Value = res.Split(" = ")[1];
                            context.Currency.Add(cur);
                            context.SaveChanges();
                            GetAll(context);
                        }
                        else Console.WriteLine("Запись уже добавлена.");
                    }
                }
                else Console.WriteLine("Данные не найдены.");
                start = string.IsNullOrEmpty(Console.ReadLine());
            }
        }

        static void ShowAll()
        {
            foreach (var item in rep.GetCurCodeAll())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        static void GetAll(AppContext context)
        {
            foreach (var item in context.Currency)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
