using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace подготовка
{
    internal class DataClass
    {
        static double percent;

        static ПодготовкаEntities db = ПодготовкаEntities.GetContext();

        public static double GetWorkingPercent()
        {
            TaskForGetWorkingPercent().GetAwaiter().GetResult();
            return percent; 
        }

        private async static Task TaskForGetWorkingPercent()
        {

            
                int working = 0;
                int total = db.vending_machines.Count();                         // все автоматы
                if (total == 0)
                    percent = 0;
                for (int i = 0; i < total; i++)
                {
                    working = db.vending_machines
                                    .Count(vm => vm.status.ToLower() == "работает");
                }// только «Работает»

                percent = (double)working / total * 100.0;
            
        }

        static double work = 0;
            static double broken = 0;
            static double service = 0;

        static public (double work, double broken, double service) GetStatusPercents()
        {
            TaskForGetStatusPercents().GetAwaiter().GetResult();
            return (work,
                broken,
                service);
        }

        public async static Task TaskForGetStatusPercents()
        {

            
                int total = db.vending_machines.Count();
                if (total == 0)
                {
                    work = 0;
                    broken = 0;
                    service = 0;
                }

                int work1 = db.vending_machines.Count(v => v.status == "Работает");
                int broken1 = db.vending_machines.Count(v => v.status == "Сломан");
                int service1 = db.vending_machines.Count(v => v.status == "Обслуживается");

                work = work1 * 100.0 / total;
                broken = broken1 * 100.0 / total;
                service = service1 * 100.0 / total;
            
        }

        static public decimal revenueToday { get; set; }
         static public decimal  revenueYesterday { get; set; }
        static public int servicedToday { get; set; }
        static public int servicedYesterday { get; set; }




        public async static Task TaskForSummary()
        {
            // today и yesterday — DateTime.Date
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            string td = today.ToString("yyyy' - 'MM' - 'dd");
            string ys = yesterday.ToString("yyyy' - 'MM' - 'dd");

            

            // выручка сегодня
            /*
            revenueToday = db.sales
                .Where(s => s.timestamp == td)
                .Sum(s => (decimal?) Convert.ToDecimal(s.total_price)) ?? 0;
            */
            

            
            // выручка вчера
            /* revenueYesterday = db.sales
                .Where(s => s.timestamp == ys)
                .Sum(s => (decimal?)Convert.ToDouble(s.total_price)) ?? 0;
            */

            // обслужено ТА сегодня
            servicedToday = db.dbomaintenances
                .Where(m => m.date == today)
                .Select(m => m.vending_machine_id)
                .Distinct()
                .Count();

            // обслужено ТА вчера
            servicedYesterday = db.dbomaintenances
                .Where(m => m.date == yesterday)
                .Select(m => m.vending_machine_id)
                .Distinct()
                .Count();

        }

    }
}
