using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConsAppCurrency
{
    class CurrencyRepository
    {
        public string GetCurName(string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string url = "http://www.cbr.ru/scripts/XML_valFull.asp";
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Item"];
            foreach (DataRow row in currency.Rows)
            {
                if (row["ISO_Char_Code"].ToString() == code)//Ищу нужный код валюты
                {
                    return row["Name"].ToString(); //Возвращаю значение курсы валюты
                }
            }
            return string.Empty;
        }

        public List<string> GetCurCodeAll()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string url = "http://www.cbr.ru/scripts/XML_valFull.asp";
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Item"];
            List<string> list = new List<string>();
            foreach (DataRow row in currency.Rows)
            {
                list.Add(row["ISO_Char_Code"].ToString() + " - " + row["Name"].ToString());
            }
            return list;
        }

        public string GetCurByCode(string code, DateTime date)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string url = "http://www.cbr.ru/scripts/XML_daily.asp" + $"?date_req={date.ToShortDateString()}";
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Valute"];
            if (currency?.Rows?.Count > 0)
            foreach (DataRow row in currency.Rows)
            {
                if (row["CharCode"].ToString() == code)//Ищу нужный код валюты
                {
                    return row["Nominal"].ToString() + " = " + row["Value"].ToString(); //Возвращаю значение курсы валюты
                }
            }
            return string.Empty;
        }
    }
}
