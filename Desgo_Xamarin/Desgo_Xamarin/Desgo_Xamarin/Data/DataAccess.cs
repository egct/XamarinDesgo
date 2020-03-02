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
                var config = DependencyService.Get<IConfig>();
                connection = new SQLiteConnection(System.IO.Path.Combine(config.DirectoryDB, "DesgoXamarin.db3"),false);
                connection.CreateTable<tipousuario>();
                connection.CreateTable<persona>();
                connection.CreateTable<usuario>();
//                connection.CreateTable<tipousuario>();
                connection.CreateTable<formulario>();

            //connection.Query<persona>("");

        }

        public SQLiteConnection SQLiteConnection()
        {
            return connection;
        }

        public void Insert(persona model)
        {
            connection.Insert(model);
        }
        public void Insertusuario(usuario model)
        {
            connection.Insert(model);
        }
        public void Insertformulario(formulario model)
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
    }

}
