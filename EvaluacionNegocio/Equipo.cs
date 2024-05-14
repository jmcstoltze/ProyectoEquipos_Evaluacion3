using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionNegocio
{
    // Se define la clase Equipo que hereda de ObservableObject, IPersistente Y IDataInfo
    public class Equipo : ObservableObject, IPersistente, IDataErrorInfo
    {

        // Diccionario que almacenará los mensajes de error
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public int EquipoId { get; set; }       

        // Variables privadas
        private string _NombreEquipo;
        private int _CantidadJugadores;
        private string _NombreDT;
        private string _TipoEquipo;
        private string _CapitanEquipo;
        private bool _TieneSub21;

        // Manejo de las propiedades observables
        public string NombreEquipo {

            get { return _NombreEquipo; }
            set
            {
                OnPropertyChanged(ref _NombreEquipo, value);
            }
        }
        public int CantidadJugadores {
            
            get { return _CantidadJugadores; }
            set
            {
                OnPropertyChanged(ref _CantidadJugadores, value);
            }
        }
        public string NombreDT {

            get { return _NombreDT; }
            set
            {
                OnPropertyChanged(ref _NombreDT, value);
            }
        }
        public string TipoEquipo {

            get { return _TipoEquipo; }
            set
            {
                OnPropertyChanged(ref _TipoEquipo, value);
            }
        }
        public string CapitanEquipo {

            get { return _CapitanEquipo; }
            set
            {
                OnPropertyChanged(ref _CapitanEquipo, value);
            }
        }
        public bool TieneSub21 {

            get { return _TieneSub21; }
            set
            {
                OnPropertyChanged(ref _TieneSub21, value);
            }
        }

        public string Error { get { return null; } }
        public string this[string name] 
        {
            get
            {
                string res = null; // String del error que cambiará según el campo

                switch (name)
                {
                    case "NombreEquipo":
                        // int lenEquipo = NombreEquipo.ToString().Length;
                        // if (string.IsNullOrEmpty(NombreEquipo.ToString()))

                        int lenEquipo = NombreEquipo?.Length ?? 0;

                        if (string.IsNullOrEmpty(NombreEquipo))
                            res = "El nombre del equipo es obligatorio";

                        if (lenEquipo < 8)
                            res = "Mínimo 8 caracteres";

                        if (lenEquipo > 25)
                            res = "Máximo 25 caracteres";
                        break;

                    case "CantidadJugadores":
                        int cant = Convert.ToInt32(CantidadJugadores);

                        if (cant < 16)
                            res = "La cantidad mínima es de 16 jugadores";
                        else if (cant > 25)
                            res = "La cantidad máxima es de 25 jugadores";
                        break;

                    case "NombreDT":
                        int lenDT = NombreDT?.Length ?? 0;
                        if (string.IsNullOrEmpty(NombreDT.ToString()))
                            res = "El nombre del entrenador es obligatorio";

                        if (lenDT < 5)
                            res = "Mínimo 5 caracteres";

                        if (lenDT > 30)
                            res = "Máximo 30 caracteres";
                        break;

                    // case "TipoEquipo":
                       // if (TipoEquipo.ToString() != "Masculino" && TipoEquipo.ToString() != "Femenino")
                        // {
                           // res = "Equipos solamente pueden ser masculinos o femeninos";
                        // }
                        // break;

                    case "CapitanEquipo":
                        int lenCapitan = CapitanEquipo?.Length ?? 0;
                        if (string.IsNullOrEmpty(CapitanEquipo.ToString()))
                            res = "El nombre del capitán de equipo es obligatorio";

                        if (lenCapitan < 5)
                            res = "Mínimo 5 caracteres";

                        if (lenCapitan > 30)
                            res = "Máximo 30 caracteres";
                        break;
                }

                // Si existe un error en el campo va cambiando la respuesta de acuerdo con el caso
                //if (ErrorCollection.ContainsKey(name))
                //  ErrorCollection[res] = res;
                //else if (res != null)
                //  ErrorCollection.Add(name, res);

                //OnPropertyChanged("ErrorCollection");

                if (res != null)
                    ErrorCollection[name] = res; // Utilizar el nombre de la propiedad como clave en lugar del valor del error

                OnPropertyChanged("ErrorCollection"); // Asegurarse de que se notifiquen los cambios en la colección de errores

                return res;

                
            }
        }

        // Método para crear registro de un equipo
        public bool Create()
        {
            try
            {
                CommonBC.EquipoModelo.spEquipoSave(                    
                    
                    AES_Helper.EncryptString(this.NombreEquipo),
                    this.CantidadJugadores,                    
                    AES_Helper.EncryptString(this.NombreDT),                                        
                    AES_Helper.EncryptString(this.TipoEquipo),                    
                    AES_Helper.EncryptString(this.CapitanEquipo),
                    this.TieneSub21
                    );

                CommonBC.EquipoModelo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Método para leer registro de un equipo
        public bool Read(int EquipoId)
        {
            try
            {
                EvaluacionDALC.VwGetEquipos equipo = CommonBC.EquipoModelo.VwGetEquipos.First(eq => eq.EquipoId == EquipoId);
                
                this.EquipoId = equipo.EquipoId;
                this.NombreEquipo = AES_Helper.DecryptString(equipo.NombreEquipo);
                this.CantidadJugadores = equipo.CantidadJugadores;
                this.NombreDT = AES_Helper.DecryptString(equipo.NombreDT);
                this.TipoEquipo = AES_Helper.DecryptString(equipo.TipoEquipo);
                this.CapitanEquipo = AES_Helper.DecryptString(equipo.CapitanEquipo);
                this.TieneSub21 = equipo.TieneSub21;                

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Método para actualizar registro de un equipo
        public bool Update(int EquipoId)
        {
            try
            {
                this.NombreEquipo = AES_Helper.EncryptString(this.NombreEquipo);
                this.NombreDT = AES_Helper.EncryptString(this.NombreDT);
                this.TipoEquipo = AES_Helper.EncryptString(this.TipoEquipo);
                this.CapitanEquipo = AES_Helper.EncryptString(this.CapitanEquipo);

                CommonBC.EquipoModelo.spEquipoUpdateById(
                    this.EquipoId,
                    this.NombreEquipo,
                    this.CantidadJugadores,
                    this.NombreDT,
                    this.TipoEquipo,
                    this.CapitanEquipo,
                    this.TieneSub21
                );

                CommonBC.EquipoModelo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Método para eliminar registro de equipo
        public bool Delete(int EquipoId)
        {
            try
            {
                CommonBC.EquipoModelo.spEquipoDeleteById(EquipoId);
                CommonBC.EquipoModelo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
