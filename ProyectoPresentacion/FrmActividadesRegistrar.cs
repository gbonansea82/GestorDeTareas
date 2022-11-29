using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Negocio;

namespace ProyectoPresentacion
{
    public partial class FrmActividadesRegistrar : Form
    {
        public FrmActividadesRegistrar()
        {
            InitializeComponent();
        }

        private void FrmActividadesRegistrar_Load(object sender, EventArgs e)
        {
            this.cmbProyectos.SelectedIndexChanged -= cmbProyectos_SelectedIndexChanged;
            LlenarComboProyectos();

            this.cmbProyectos.SelectedIndexChanged += cmbProyectos_SelectedIndexChanged;

            cmbProyectos.SelectedIndex = 1;
            LlenarComboUsuarios();
        }

        void LlenarComboProyectos()
        {
            clsProyecto cls = new clsProyecto();
            List<Proyectos> lista = cls.LeerProyectos();
            cmbProyectos.DataSource = lista;
            cmbProyectos.ValueMember = "Id";
            cmbProyectos.DisplayMember = "Nombre";
        }

        void LlenarComboActividades()
        {
            clsActividades cls = new clsActividades();
            int idProyectoSeleccionado = Convert.ToInt32(cmbProyectos.SelectedValue);
            List<Actividades> lista = cls.LeerActividades(idProyectoSeleccionado);
            cmbActividades.DataSource = lista;
            cmbActividades.ValueMember = "Id";
            cmbActividades.DisplayMember = "Nombre";
        }

        void LlenarComboUsuarios()
        {
            clsUsuario cls = new clsUsuario();
            List<Usuarios> lista = cls.LeerUsuarios();
            cmbUsuarios.DataSource = lista;
            cmbUsuarios.ValueMember = "Id";
            cmbUsuarios.DisplayMember = "Nombre";
        }

        private void cmbProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboActividades();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            TareasRealizadas entidad = new TareasRealizadas();
            entidad.Id = 0;
            entidad.Fecha = this.dtpFecha.Value;
            entidad.HoraInicio = dtpHoraInicio.Value;
            entidad.HoraFin = dtpHoraFin.Value;
            entidad.IdActividad = Convert.ToInt32(cmbActividades.SelectedValue);
            entidad.IdUsuario = Convert.ToInt32(cmbUsuarios.SelectedValue);
            entidad.Comentario = txtObservacion.Text;

            clsTareas cls = new clsTareas();
            long idCreado = cls.Crear(entidad);
            if (idCreado > 0)
                MessageBox.Show("Tarea creada con éxito");
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
