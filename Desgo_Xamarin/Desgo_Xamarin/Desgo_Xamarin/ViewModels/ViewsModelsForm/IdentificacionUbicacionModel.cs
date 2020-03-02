using Desgo_Xamarin.Data;
using Desgo_Xamarin.Models.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desgo_Xamarin.ViewModels.ViewsModelsForm
{
    class IdentificacionUbicacionlModel: INotifyPropertyChanged
    {

        public List<TipoPredio> TipoPredioList { get; set; }
        public List<RegimenTenencia> RegimenTenenciaList { get; set; }

        public string txtPNOMBRE_PERSONA { get; set; }
        public string PNOMBRE_PERSONA { get; set; }
        public string StatusMessage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _myTipoPredio;
        private string _myRegimenTenencia;

        public string MyTipoPredio
        {
            get { return _myTipoPredio; }
            set
            {
                if (_myTipoPredio != value)
                {
                    _myTipoPredio = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MyRegimenTenencia
        {
            get { return _myRegimenTenencia; }
            set
            {
                if (_myRegimenTenencia != value)
                {
                    _myRegimenTenencia = value;
                    OnPropertyChanged();
                }
            }
        }


        private TipoPredio _selectedTipoPredio { get; set; }
        private RegimenTenencia _selectedRegimenTenencia { get; set; }

        public TipoPredio SelectedTipoPredio
        {
            get { return _selectedTipoPredio; }
            set
            {
                if (_selectedTipoPredio != value)
                {
                    _selectedTipoPredio = value;
                    // Do whatever functionality you want...When a selectedItem is changed..
                    // write code here..
                    MyTipoPredio = "Selected : " + _selectedTipoPredio.Value;
                }
            }
        }

        public RegimenTenencia SelectedRegimenTenencia
        {
            get { return _selectedRegimenTenencia; }
            set
            {
                if (_selectedRegimenTenencia != value)
                {
                    _selectedRegimenTenencia = value;
                    // Do whatever functionality you want...When a selectedItem is changed..
                    // write code here..
                    MyRegimenTenencia = "Selected : " + _selectedRegimenTenencia.Value;
                }
            }
        }


        public IdentificacionUbicacionlModel()
        {
            TipoPredioList = GetTipoPredio().OrderBy(t => t.Value).ToList();
            MyTipoPredio = "Selected Tipo predio: ";
            RegimenTenenciaList = GetRegimenTenencia().OrderBy(t => t.Value).ToList();
            MyRegimenTenencia = "Selected Regimen tenencia: ";
        }
        public List<TipoPredio> GetTipoPredio()
        {
            var tipoPredioaux = new List<TipoPredio>()
            {
                new TipoPredio(){Key =  1, Value= "Urbano"},
                new TipoPredio(){Key =  2, Value= "Rural"}
            };

            return tipoPredioaux;
        }

        public List<RegimenTenencia> GetRegimenTenencia()
        {
            var regimenTenenciaaux = new List<RegimenTenencia>()
            {
                new RegimenTenencia(){Key =  1, Value= "Unipropiedad"},
                new RegimenTenencia(){Key =  2, Value= "Popiedad Horizontal"},
                new RegimenTenencia(){Key =  3, Value= "OP-Copropiedad (DER-ACC)"}
            };

            return regimenTenenciaaux;
        }

    }

    public class TipoPredio
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class RegimenTenencia
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
