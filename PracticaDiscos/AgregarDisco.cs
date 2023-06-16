using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace PracticaDiscos
{
    public partial class AgregarDisco : Form
    {
        public AgregarDisco()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NegocioDisco negocio = new NegocioDisco();
            Disco nuevoDisco = new Disco();
            try
            {
                nuevoDisco.Titulo = txtTitulo.Text;
                nuevoDisco.FechaLanzamiento = dateTimePicker.Value;
                nuevoDisco.CantCanciones = int.Parse(txtCanciones.Text);
                nuevoDisco.UrlImagen = txtUrlImg.Text;
                nuevoDisco.Estilo = (Estilo)cmbEstilo.SelectedItem;
                nuevoDisco.TipoEdicion = (TipoEdicion)cmbEdicion.SelectedItem;

                negocio.agregar(nuevoDisco);
                MessageBox.Show("Agregado con exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error en la carga: "+ ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgregarDisco_Load(object sender, EventArgs e)
        {
            NegocioEstilo negocioEstilo = new NegocioEstilo();
            NegocioTipoEdicion negEdicion = new NegocioTipoEdicion();

            try
            {
                cmbEstilo.DataSource = negocioEstilo.listar();
                cmbEdicion.DataSource = negEdicion.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo cargar: "+ ex.ToString());
            }
        }
    }
}
