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
        public int idlote;
        public int idddplote;
        public string claveCatastralAntiguo;
        public string claveCatastralNuevo;
        public string numeroPredio;

        public int idddploteDd;
        public int iddlote;
        public string nombreSector;
        public string nombreEdificio;
        public string usoPredio;

        public int iddloteDir;
        public string callePrincipal;
        public string no;
        public string interseccion;
        
        /*****/
        public List<TipoPredio> TipoPredioList { get; set; }
        public List<RegimenTenencia> RegimenTenenciaList { get; set; }

        public formularioAll formularioall { get; set; }
        /********/
        
        public int ID_IULOTE
        {
            get
            {
                return idlote;
            }   // get method
            set { idlote = value; }  // set method
        }
        public int ID_DDPLOTE
        {
            get
            {
                return idddplote;
            }   // get method
            set { idddplote = value; }  // set method
        }
        public string CLAVECATASTRALANTIGUO_IULOTE
        {
            get {
                return claveCatastralAntiguo;
            }   // get method
            set { claveCatastralAntiguo = value; }  // set method
        }
        public string CLAVECATASTRALNUEVO_IULOTE
        {
            get
            {
                return claveCatastralNuevo;
            }   // get method
            set { claveCatastralNuevo = value; }  // set method
        }
        public string NUMEROPREDIO_IULOTE
        {
            get
            {
                return numeroPredio;
            }   // get method
            set { numeroPredio = value; }  // set method
        }

        public int ID_DDPLOTEDP
        {
            get
            {
                return idddploteDd;
            }   // get method
            set { idddploteDd = value; }  // set method
        }
        public int ID_DLOTE
        {
            get
            {
                return iddlote;
            }   // get method
            set { iddlote = value; }  // set method
        }
        public string NOMBRESECTOR_DDPLOTE
        {
            get
            {
                return nombreSector;
            }   // get method
            set { nombreSector = value; }  // set method
        }
        public string NOMBREEDIFICIO_DDPLOTE
        {
            get
            {
                return nombreEdificio;
            }   // get method
            set { nombreEdificio = value; }  // set method
        }
        public string USOPREDIO_DDPLOTE
        {
            get
            {
                return usoPredio;
            }   // get method
            set { usoPredio = value; }  // set method
        }

        public int ID_DLOTEDIR
        {
            get
            {
                return iddloteDir;
            }   // get method
            set { iddloteDir = value; }  // set method
        }
        public string CALLEP_DLOTE
        {
            get
            {
                return callePrincipal;
            }   // get method
            set { callePrincipal = value; }  // set method
        }
        public string NO_DLOTE
        {
            get
            {
                return no;
            }   // get method
            set { no = value; }  // set method
        }
        public string INTERSECCION_DLOTE
        {
            get
            {
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
                    MyTipoPredio = _selectedTipoPredio.Value;
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
                    MyRegimenTenencia = _selectedRegimenTenencia.Value;
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
