using KantorAPI.Pobieranie.Interfaces;
using KantorAPI.Pobieranie.Model;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KantorAPI.Pobieranie.Services
{
    public class DownloadService : IDownload
    {
        private readonly string _url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        public async Task<bool> DownloadData()
        {
            List<DaneWalutoweModel> listCurrency = [];
            DateTime date = DateTime.MinValue;

            //asynchroniczne pobieranie XML
            var settings = new XmlReaderSettings
            {
                Async = true
            };

            using (var httpClient = new HttpClient())
            using (var stream = await httpClient.GetStreamAsync(this._url))
            using (var reader = XmlReader.Create(stream, settings))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "Cube" && reader.HasAttributes)
                                {
                                    if (reader.Name == "Cube" && reader.GetAttribute("time") != null)
                                    {
                                        date = Convert.ToDateTime(reader.GetAttribute("time"));
                                        //bool chech = CheckDataInDatabase(date);

                                        //if (chech)
                                        //{
                                        //    break;
                                        //}
                                    }
                                    if (reader.GetAttribute("rate") != null && reader.GetAttribute("currency") != null)
                                    {
                                        DaneWalutoweModel currency = new();

                                        currency.Currency = reader.GetAttribute("currency");
                                        currency.Rate = Convert.ToDouble(reader.GetAttribute("rate").Replace(".", ","));
                                        currency.Time = date;

                                        listCurrency.Add(currency);
                                    }
                                }
                            }
                        }
                    }
                }            

                if (listCurrency.Any())
                {
                    reader.Close();
                    return true;
                }

                return false;
            }
        }
    }
}
