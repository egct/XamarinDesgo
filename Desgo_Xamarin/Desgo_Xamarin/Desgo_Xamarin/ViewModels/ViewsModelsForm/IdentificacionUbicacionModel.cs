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
        /*******/
        public string claveCatastralAntiguo;
        public string claveCatastralNuevo;
        public string numeroPredio;

        public string nombreSector;
        public string nombreEdificio;
        public string usoPredio;
        
        public string callePrincipal;
        public string no;
        public string interseccion;
        
        /*****/
        public List<TipoPredio> TipoPredioList { get; set; }
        public List<RegimenTenencia> RegimenTenenciaList { get; set; }

        public formularioAll formularioall { get; set; }
        /********/
        public string CLAVECATASTRALANTIGUO_IULOTE
        {
            get {
                claveCatastralAntiguo = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.CLAVECATASTRALANTIGUO_IULOTE;
                return claveCatastralAntiguo;
            }   // get method
            set { claveCatastralAntiguo = value; }  // set method
        }
        public string CLAVECATASTRALNUEVO_IULOTE
        {
            get
            {
                claveCatastralNuevo = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.CLAVECATASTRALNUEVO_IULOTE;
                return claveCatastralNuevo;
            }   // get method
            set { claveCatastralNuevo = value; }  // set method
        }
        public string NUMEROPREDIO_IULOTE
        {
            get
            {
                numeroPredio = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.NUMEROPREDIO_IULOTE;
                return numeroPredio;
            }   // get method
            set { numeroPredio = value; }  // set method
        }
        public string NOMBRESECTOR_DDPLOTE
        {
            get
            {
                nombreSector = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.NOMBRESECTOR_DDPLOTE;
                return nombreSector;
            }   // get method
            set { nombreSector = value; }  // set method
        }
        public string NOMBREEDIFICIO_DDPLOTE
        {
            get
            {
                nombreEdificio = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.NOMBREEDIFICIO_DDPLOTE;
                return nombreEdificio;
            }   // get method
            set { nombreEdificio = value; }  // set method
        }
        public string USOPREDIO_DDPLOTE
        {
            get
            {
                usoPredio = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.USOPREDIO_DDPLOTE;
                return usoPredio;
            }   // get method
            set { usoPredio = value; }  // set method
        }
        public string CALLEP_DLOTE
        {
            get
            {
                callePrincipal = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.CALLEP_DLOTE;
                return callePrincipal;
            }   // get method
            set { callePrincipal = value; }  // set method
        }
        public string NO_DLOTE
        {
            get
            {
                no = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.NO_DLOTE;
                return no;
            }   // get method
            set { no = value; }  // set method
        }
        public string INTERSECCION_DLOTE
        {
            get
            {
                interseccion = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.INTERSECCION_DLOTE;
                return interseccion;
            }   // get method
            set { interseccion = value; }  // set method
        }
        /********/

        public string txtPNOMBRE_PERSONA { get; set; }
        public string PNOMBRE_PERSONA { get; set; }
        public string StatusMessage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /*Antiguo*/
        private string _myTipoPredioForm;
        private string _myRegimenTenenciaForm;

        public string MyTipoPredioForm
        {
            get
            {
                _myTipoPredioForm = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.TIPOPREDIO_DDPLOTE;
                return _myTipoPredioForm;
            }
            set
            {
                if (_myTipoPredioForm != value)
                {
                    _myTipoPredioForm = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MyRegimenTenenciaForm
        {
            get
            {
                _myRegimenTenenciaForm = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.REGIMENTENECIA_DDPLOTE;
                return _myRegimenTenenciaForm;
            }
            set
            {
                if (_myRegimenTenenciaForm != value)
                {
                    _myRegimenTenenciaForm = value;
                    OnPropertyChanged();
                }
            }
        }

        /**/
        private string _myTipoPredio;
        private string _myRegimenTenencia;

        public string MyTipoPredio
        {
            get {return _myTipoPredio; }
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
            get {return _myRegimenTenencia; }
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
            get {
             
                return _selectedTipoPredio; }
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
            get {
                return _selectedRegimenTenencia; }
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
