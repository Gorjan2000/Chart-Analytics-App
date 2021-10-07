using IT_Proekt.Models;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Proekt.Controllers
{
    public class HomeController : Controller
    {
        private proektEntities _db = new proektEntities();
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        public ActionResult Index()
        {
            List<DataPoint> list = new List<DataPoint>();
            var pod = _db.podatoci2.ToList();
            foreach (var item in pod)
            {

                //ОВДЕ ПРОБАВ ДА ГО РАЗДЕЛИМ ДАТУМОТ НА ДЕН, МЕСЕЦ, ГОДИНА 
                string[] datum = item.datum.Split('-');
                int den = Convert.ToInt32(datum[0]);
                int mesec = Convert.ToInt32(datum[1]);
                int godina = Convert.ToInt32(datum[2]);

                //ОВДЕ ВРЕМЕТО МИ Е РАЗДЕЛЕНО НА СААТ, МИНУТА, СЕКУНДА
                string[] vreme = item.vreme.Split(':');
                int hour = Convert.ToInt32(vreme[0]);
                int min = Convert.ToInt32(vreme[1]);
                int sec = Convert.ToInt32(vreme[2]);

                //myDate е објект креиран од датумот (преземен од база) - како string и дополнително сетиран посакуван формат
                //DateTime myDate = DateTime.ParseExact(item.datum, "dd-MM-yy", System.Globalization.CultureInfo.InvariantCulture);
                //double myDate2 = myDate.ToOADate();
                //double myDate3 = Convert.ToDouble(myDate);


                string d = item.datum + " " + item.vreme; //конкатанација на датумот и времето и креирање на Double Објект
                DateTime ddd = Convert.ToDateTime(d);
                double dd = Convert.ToDouble(Convert.ToString(ddd.Day) + Convert.ToString(ddd.Month) + Convert.ToString(ddd.Year)
                    + Convert.ToString(ddd.Hour)+ Convert.ToString(ddd.Minute)+ Convert.ToString(ddd.Second));

                list.Add(new DataPoint(dd, Convert.ToInt32(item.temperatura)));
                //креирање на точка од графиконот со координати x-датум и време , y-температура
                //пополнетата листа се серијализира во JSON и се испраќа до View-to Index
                
            }

            /*
            list.Add(new DataPoint(new DateTime(2021, 09, 10), 9));
            list.Add(new DataPoint(new DateTime(2021, 09, 11), 10));            
            list.Add(new DataPoint(new DateTime(2021, 09, 12), 11));            
            list.Add(new DataPoint(new DateTime(2021, 09, 13), 12));            
            list.Add(new DataPoint(new DateTime(2021, 09, 14), 13));            
            list.Add(new DataPoint(new DateTime(2021, 09, 15), 14));            
            list.Add(new DataPoint(new DateTime(2021, 09, 16), 15));            
            list.Add(new DataPoint(new DateTime(2021, 09, 17), 16));            
            */

            
            ViewBag.DataPoints = JsonConvert.SerializeObject(list, _jsonSetting);
            return View();

        }
        

        



        // GET: Home
        /*
        public ActionResult Index()
        {
            double count = 365, y = 100;
            double x = 1546281000000; // 1st Jan 2019 in timestamp (seconds);

            Random random = new Random(DateTime.Now.Millisecond);

            List<DataPoint> dataPoints = new List<DataPoint>();

            for (int i = 0; i < count; i++)
            {
                y = Math.Abs(y + (random.Next(0, 20) - 10));
                x += 24 * 60 * 60 * 1000;
                dataPoints.Add(new DataPoint(x, y));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
        */
    }
}