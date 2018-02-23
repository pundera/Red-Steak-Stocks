using OxyPlot;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RedSteakStocks.Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apiWrapper = AlphaVantageApiWrapper;

namespace RedSteakStocks.Plots.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(IEventAggregator eventAggregator)
        {
            Message = "View A from your Prism Module PLOTs..";

        }


        private async Task TestAsync(List<apiWrapper.AlphaVantageApiWrapper.ApiParam> pars)
        {
            if (pars == null)
            {
                throw new ArgumentNullException(nameof(pars));
            }

            var root = await apiWrapper.AlphaVantageApiWrapper.GetTechnical(pars, "A903Z1G7NAB6A551");

            this.Title = pars[1].ParamValue + " " + (pars.Count>3 ? pars[2].ParamValue : " - - - ");
            this.Points = new ObservableCollection<DataPoint>();

            var ix = 0;
            foreach (var t in root.TechnicalsByDate)
            {
                var start = "";

                var ddd = t.Data.Aggregate(start, (a, b) => a + b.TechnicalKey + "->" + b.TechnicalValue);
                Console.WriteLine(t.Date + ":" + ddd);

                foreach (var v in t.Data) if (v.TechnicalKey.Equals("1. open")) Points.Add(new DataPoint(t.Date.Ticks, v.TechnicalValue));
                ix++;

            }

        }

        public string Title { get; private set; }

        private ObservableCollection<DataPoint> points;
        public ObservableCollection<DataPoint> Points { get { return points; } set { SetProperty(ref points, value); } }

    }
}
