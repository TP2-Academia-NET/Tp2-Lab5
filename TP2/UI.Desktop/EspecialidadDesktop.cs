using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class EspecialidadDesktop : ApplicationForm
    {
        public EspecialidadDesktop()
        {
            InitializeComponent();
        }

        public Especialidad EspecialidadActual { get; set; }

        public EspecialidadDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public EspecialidadDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            EspecialidadActual = new EspecialidadLogic().GetOne(ID);
            MapearDeDatos();
        }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.EspecialidadActual.ID.ToString();
            this.txtDescripcion.Text = this.EspecialidadActual.Descripcion;
        }

        public virtual void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                EspecialidadActual = new Especialidad();
                EspecialidadActual.State = Especialidad.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.EspecialidadActual.Descripcion = this.txtDescripcion.Text;
            }
            if (Modo == ModoForm.Modificacion)
            {
                EspecialidadActual.State = Usuario.States.Modified;
            }
            if (Modo == ModoForm.Consulta)
            {
                EspecialidadActual.State = Usuario.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                EspecialidadActual.State = Usuario.States.Deleted;
            }
        }

        public virtual void GuardarCambios()
        {
            MapearADatos();
            new EspecialidadLogic().Save(EspecialidadActual);
        }

        public virtual bool Validar()
        {
            bool valida = false;
            string mensaje = "";

            if (txtDescripcion.Text.Trim() == "")
                mensaje += "El nombre no puede estar en blanco" + "\n";          

            if (!String.IsNullOrEmpty(mensaje))
            {
                this.Notificar(mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                valida = true;
            }
            return valida;
        } 

        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                this.GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
