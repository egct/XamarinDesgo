using Desgo_Xamarin.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLiteNetExtensions.Extensions;
using Desgo_Xamarin.Models.Db;
using SQLite;

namespace Desgo_Xamarin.Data
{
    public class DataAccess: IDisposable
    {
        private SQLiteConnection connection;
        
        public DataAccess()
        {
            try
            {
                var config = DependencyService.Get<IConfig>();
                connection = new SQLiteConnection(System.IO.Path.Combine(config.DirectoryDB, "DesgoXamarin.db3"), false);
                //deleteTable();
                connection.CreateTable<tipousuario>();
                connection.CreateTable<persona>();
                connection.CreateTable<usuario>();
                connection.CreateTable<formulario>();
                connection.CreateTable<estadoSqlite>();
                connection.CreateTable<Direccion_DDPLote>();
                connection.CreateTable<DDescriptivosPredio_IULote>();
                connection.CreateTable<identificacionubicacionlote>();
                connection.CreateTable<formularioAll>();
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error create sqlite",
                      ">>" + e,
                      "Accept");
            }
        }

        public SQLiteConnection SQLiteConnection()
        {
            return connection;
        }

        public void Insert(persona model)
        {
            try
            {
                connection.Insert(model);
            }
            catch (Exception e)
            {

            }
        }
        public void Insertusuario(usuario model)
        {
            connection.Insert(model);
        }
        public void Insertformulario(formulario model)
        {
            connection.Insert(model);
        }
        public void InsertipoUser(tipousuario model)
        {
            connection.Insert(model);
        }
        public void Insertformularioall(formularioAll model)
        {
            connection.Insert(model);
            /****Identificacion Ubicacion****/
            Insertidentificacionubicacionlote(model.identificacionubicacionlote);
            InsertDDescriptivosPredio_IULote(model.identificacionubicacionlote.dDescriptivosPredio_IULote);
            InsertDireccion_DDPLote(model.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote);

            /********/
        }
        public void Insertidentificacionubicacionlote(identificacionubicacionlote model)
        {
            connection.Insert(model);
        }
        public void InsertDireccion_DDPLote(Direccion_DDPLote model)
        {
            connection.Insert(model);
        }
        public void InsertDDescriptivosPredio_IULote(DDescriptivosPredio_IULote model)
        {
            connection.Insert(model);
        }
        public persona getPersona(int idpersona)
        {
            return connection.Table<persona>().FirstOrDefault(c => c.ID_PERSONA == idpersona);
        }
        public usuario getUsuario(int idpersona)
        {
            return connection.Table<usuario>().FirstOrDefault(c => c.ID_USUARIO == idpersona);
        }
        public formulario getFormulario(int idpersona)
        {
            return connection.Table<formulario>().FirstOrDefault(c => c.ID_FORMULARIO == idpersona);
        }
        public persona getPersona(string cedulapersona)
        {
            return connection.Table<persona>().FirstOrDefault(c => c.CEDULA_PERSONA == cedulapersona);
        }
        public usuario getUsuario(string usuario)
        {
            return connection.Table<usuario>().FirstOrDefault(c => c.USUARIO_USUARIO == usuario);
        }
        public formulario getFormularioCodigo(int codigo)
        {
            return connection.Table<formulario>().FirstOrDefault(c => c.CODIGO_FORMULARIO == codigo);
        }

        public List<persona> GetListPersona()
        {  
            return connection.Table<persona>().OrderBy(c => c.PNOMBRE_PERSONA).ToList();
        }
        public List<usuario> GetListUsuario()
        {
            return connection.Table<usuario>().OrderBy(c => c.USUARIO_USUARIO).ToList();
        }
        public List<formulario> GetListFormulario()
        {
            return connection.Table<formulario>().OrderBy(c => c.ID_FORMULARIO).ToList();
        }
        /***********/
        public estadoSqlite getEstadoSqlite()
        {
            try
            {
                return connection.Table<estadoSqlite>().FirstOrDefault(c => c.ID_ESTADOSQLITE == 1);
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public void setEstadoSqlite(estadoSqlite model)
        {
            connection.Insert(model);
        }
        public void updateCambiosEstadoSqlite(bool cambio)
        {
            connection.Query<estadoSqlite>("UPDATE estadoSqlite SET  CAMBIOS_ESTADOSQLITE=? WHERE ID_ESTADOSQLITE=?", cambio,1);
        }

        public void updateEstadoFormulario(bool cambio,int codigo,int id)
        {
            connection.Query<formulario>("UPDATE formulario SET  ESTADO_FORMULARIO=? WHERE CODIGO_FORMULARIO=? AND ID_FORMULARIO=? ", cambio, codigo,id);
            connection.Query<formularioAll>("UPDATE formularioAll SET  ESTADO_FORMULARIO=? WHERE CODIGO_FORMULARIO=? AND ID_FORMULARIO=? ", cambio, codigo, id);
        }
        /***********/
        public formularioAll getFormularioAllCodigo(int codigo)
        {
            formularioAll fall = new formularioAll();
            formulario f = new formulario();
            /*Identifucacion Ubicacion*/
            Direccion_DDPLote direccion_DDPLote = new Direccion_DDPLote();
            DDescriptivosPredio_IULote dDescriptivosPredio_IULote = new DDescriptivosPredio_IULote();
            identificacionubicacionlote identificacionubicacionlote = new identificacionubicacionlote();
            /***/
            
            f = getFormularioCodigo(codigo);
            //fall = connection.Table<formularioAll>().FirstOrDefault(c => c.CODIGO_FORMULARIO == codigo);
            fall = connection.Query<formularioAll>("SELECT * FROM formularioAll WHERE  CODIGO_FORMULARIO=?", codigo).FirstOrDefault();
            fall.identificacionubicacionlote = getidentificacionubicacionlote(f.ID_IULOTE);
            fall.identificacionubicacionlote.dDescriptivosPredio_IULote = getDDescriptivosPredio_IULote(fall.identificacionubicacionlote.ID_DDPLOTE);
            fall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote = getDireccion_DDPLote(fall.identificacionubicacionlote.dDescriptivosPredio_IULote.ID_DLOTE);

            return fall;
        }
        /**identificacion ubicacion get*/
        public identificacionubicacionlote getidentificacionubicacionlote(int identificacionU_F)
        {
            return connection.Table<identificacionubicacionlote>().FirstOrDefault(c => c.ID_IULOTE == identificacionU_F);
        }
        public DDescriptivosPredio_IULote getDDescriptivosPredio_IULote(int ID_DDPLOTE)
        {
            return connection.Table<DDescriptivosPredio_IULote>().FirstOrDefault(c => c.ID_DDPLOTE == ID_DDPLOTE);
        }
        public Direccion_DDPLote getDireccion_DDPLote(int ID_DLOTE)
        {
            return connection.Table<Direccion_DDPLote>().FirstOrDefault(c => c.ID_DLOTE == ID_DLOTE);
        }
        /***********/
        //public void Insert<T>(T model)
        //{
        //    connection.Insert(model);
        //}

        //public void Update<T>(T model)
        //{
        //    connection.Update(model);
        //}

        //public void Delete<T>(T model)
        //{
        //    connection.Delete(model);
        //}

        //public  T First<T>(bool WithChildren) where T : class
        //{
        //    //if (WithChildren)
        //    //{
        //    //    return connection.GetAllWithChildren<T>(,,,).FirstOrDefault();
        //    //}
        //    //else
        //    //{
        //        return connection.Table<T>().FirstOrDefault();
        //    //}
        //}

        //public List<T> GetList<T>(bool WithChildren) where T : class
        //{
        //    //if (WithChildren)
        //    //{
        //    //    return connection.GetAllWithChildren<T>(,,,).ToList();
        //    //}
        //    //else
        //    //{
        //        return connection.Table<T>().ToList();
        //    //}
        //}

        //public T Find<T>(string us, string pw, bool WithChildren) where T : class
        //{
        //    //if (WithChildren)
        //    //{
        //    //    return connection.GetAllWithChildren<T>(,,,).FirstOrDefault(m => m.GetHashCode() == pk);
        //    //}
        //    //else
        //    //{
        //    //return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == us);
        //    //}
        //    return connection.Table<T>().FirstOrDefault();
        //}

        public void Dispose()
        {
            connection.Dispose();
        }

        /******login******/
        public usuario getUsuario(string pass,string us)
        {
            String[] values = { pass, us };
            return connection.Query<usuario>("SELECT * FROM usuario WHERE  CONTRASENIA_USUARIO=? AND USUARIO_USUARIO=?",values).FirstOrDefault();
        }
        public int getUsuarioSalt(string us)
        {
            String[] values = { us };
            return connection.Query<int>("SELECT SALT_USUARIO FROM usuario WHERE  USUARIO_USUARIO=?", values).FirstOrDefault();
        }
        public int getEstadoSQLITESalt(string us)
        {
            String[] values = { us };
            return connection.Query<int>("SELECT SALT_USUARIO FROM estadoSqlite WHERE  USUARIO_USUARIO=?", values).FirstOrDefault();
        }
        /******Eliminar Tablas*******/
        public void deleteTable()
        {
            connection.DropTable<estadoSqlite>();
            connection.DropTable<tipousuario>();
            connection.DropTable<persona>();
            connection.DropTable<usuario>();
            connection.DropTable<formulario>();
            connection.DropTable<Direccion_DDPLote>();
            connection.DropTable<DDescriptivosPredio_IULote>();
            connection.DropTable<identificacionubicacionlote>();
            connection.DropTable<formularioAll>();

        }
        /******Actualizar Identificacion Ubicacion********/
        public void updateIdentificacionUbicacion(identificacionubicacionlote model)
        {
            connection.Update(model);
        }
        public void updateDDescriptivosPredio_IULote(DDescriptivosPredio_IULote model)
        {
            connection.Update(model);
        }
        public void updateDireccion_DDPLote(Direccion_DDPLote model)
        {
            connection.Update(model);
        }
    }

}
