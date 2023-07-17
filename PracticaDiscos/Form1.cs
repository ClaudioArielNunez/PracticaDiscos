using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Entidades;

namespace PracticaDiscos
{
    public partial class Form1 : Form
    {
        List<Disco> Discos = new List<Disco>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Titulo");
            cboCampo.Items.Add("Estilo");
            cboCampo.Items.Add("Tipo Edición");
            cboCampo.Items.Add("Cantidad Canciones");

        }
        private void cargar()
        {
            NegocioDisco negocio = new NegocioDisco();
            Discos = negocio.Listar();
            dgvDiscos.DataSource = Discos;
            dgvDiscos.Columns["UrlImagen"].Visible = false;
            dgvDiscos.Columns["Id"].Visible = false;            
            pbImgDisco.Load(Discos[0].UrlImagen);            

        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDiscos.CurrentRow != null)
            {
                Disco filaSeleccionada = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                //pbImgDisco.Load(filaSeleccionada.UrlImagen);
                cargarImagenxDefecto(filaSeleccionada.UrlImagen);
            }
            
        }

        public void cargarImagenxDefecto(string img)
        {
            try
            {
                pbImgDisco.Load(img);
            }
            catch (Exception ex)
            {
                pbImgDisco.Load("https://images.vexels.com/media/users/3/149953/isolated/preview/52364c140c4876f5d7296471f14959f6-silueta-de-cantante-de-banda-de-musica.png");
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarDisco discoNuevo = new AgregarDisco();
            discoNuevo.ShowDialog();
            cargar(); 
        }        
        

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco discoEditar;
            discoEditar = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            AgregarDisco frmModificar = new AgregarDisco(discoEditar);
            frmModificar.ShowDialog();
            Text = "Modificar Disco";
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioDisco negocio = new NegocioDisco();
                Disco discoSeleccionado; //No se puede instanciar de nuevo, ya existe

                DialogResult respuesta = MessageBox.Show("Seguro de eliminarlo?","Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {                    
                    discoSeleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                    negocio.eliminar(discoSeleccionado.Id);
                    cargar();
                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("El disco no se pudo eliminar: "+ ex.ToString());
            }
        }

        private void btnPapelera_Click(object sender, EventArgs e)
        {
            NegocioDisco negocio = new NegocioDisco();
            DialogResult resultado = MessageBox.Show("Envias a la papelera este disco?", "Papelera", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            try
            {
                if(resultado == DialogResult.Yes)
                {
                    Disco discoSeleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                    negocio.enviarPapelera(discoSeleccionado.Id);
                    MessageBox.Show("Disco enviado a papelera");
                    cargar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo enviar a la papelera este disco");
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Disco> listaFiltrada;
            listaFiltrada = Discos.FindAll(x => x.Titulo.ToUpper().Contains(textFiltro.Text.ToUpper()));
                        
            dgvDiscos.DataSource = null;
            dgvDiscos.DataSource = listaFiltrada;
            ocultarColumnas();
        }
        
        private void textFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Disco> lista;
            string titulo = textFiltro.Text;
            if (titulo.Length >= 3)
            {
                
                lista = Discos.FindAll(x => x.Titulo.ToUpper().Contains(titulo.ToUpper()));
            }
            else
            {
                lista = Discos;
            }
            dgvDiscos.DataSource = null;
            dgvDiscos.DataSource = lista;
            ocultarColumnas();            
        }
        
        private void ocultarColumnas()
        {
            dgvDiscos.Columns["Id"].Visible = false;
            dgvDiscos.Columns["UrlImagen"].Visible = false;
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargar();
            string opcion = cboCampo.SelectedItem.ToString();
            if(opcion == "Tipo Edición" ||  opcion =="Estilo")
            {
                //txtFiltroCombo.ReadOnly = true;
                txtFiltroCombo.Visible = false;
                lblFiltro.Visible = false;
                txtFiltroCombo.Clear();
            }
            else
            {
                //txtFiltroCombo.ReadOnly = false;
                txtFiltroCombo.Visible = true;
                lblFiltro.Visible = true;
                txtFiltroCombo.Clear();
            }

            switch (opcion)
            {
                case "Cantidad Canciones":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Mayor a");
                    cboCriterio.Items.Add("Menor a");
                    cboCriterio.Items.Add("Igual a");
                    break;
                case "Titulo":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con");
                    cboCriterio.Items.Add("Termina con");
                    cboCriterio.Items.Add("Contiene");
                    break;
                case "Tipo Edición":
                    
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Vinilo");                    
                    cboCriterio.Items.Add("CD");
                    cboCriterio.Items.Add("Tape");
                    cboCriterio.Items.Add("OnDemand");
                    break;
                default:
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Pop");
                    cboCriterio.Items.Add("Pop Punk");
                    cboCriterio.Items.Add("Rock");
                    cboCriterio.Items.Add("Reggae");
                    cboCriterio.Items.Add("Country");
                    cboCriterio.Items.Add("Electrónica");
                    cboCriterio.Items.Add("Heavy Metal");
                    break;
                    
            }
            /*
            if(opcion == "Cantidad Canciones")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a ");
            }
            else if(opcion == "Titulo")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
            else if(opcion == "Tipo Edición")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Vinilo");
                cboCriterio.Items.Add("CD");
                cboCriterio.Items.Add("Tape");
                cboCriterio.Items.Add("OnDemand");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Pop Punk");
                cboCriterio.Items.Add("Pop");
                cboCriterio.Items.Add("Rock");
                cboCriterio.Items.Add("Reggae");
                cboCriterio.Items.Add("Country");
                cboCriterio.Items.Add("Electrónica");
                cboCriterio.Items.Add("Heavy Metal");
            }
            */

        }

        private void btnFiltrarDos_Click(object sender, EventArgs e)
        {
            
            NegocioDisco negocio = new NegocioDisco();
            try
            {
                if (validarFiltro())
                {
                    return;
                }
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroCombo.Text;
                if(campo == "Tipo Edición")
                {
                    filtro = "";                    
                }
                else if (campo == "Estilo")
                {
                    filtro = "";                    
                }

                dgvDiscos.DataSource = negocio.filtrar(campo,criterio,filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool validarFiltro()
        {
            if(cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo a filtrar");
                return true;
            }
            
            if(cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio a filtrar");
                return true;
            }

            if(cboCampo.SelectedItem.ToString() == "Cantidad Canciones")
            {
                if (string.IsNullOrEmpty(txtFiltroCombo.Text))
                {
                    MessageBox.Show("Cargar cantidad de canciones");
                    return true;
                }
                if (!(soloNumeros(txtFiltroCombo.Text)))
                {
                    MessageBox.Show("Ingrese solo números");
                    return true;
                }
            }

            return false;
        }
        private bool soloNumeros(string cadena)
        {
            foreach(char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnVerPapelera_Click(object sender, EventArgs e)
        {
            NegocioDisco negocio = new NegocioDisco(); ;
            try
            {
                List<Disco> listaEnPapelera = new List<Disco>();
                listaEnPapelera = negocio.mostrarPapelera();
                dgvDiscos.DataSource = listaEnPapelera;
                dgvDiscos.Columns["UrlImagen"].Visible = false;
                dgvDiscos.Columns["Id"].Visible = false;
                if(listaEnPapelera.Count > 0)
                {
                    pbImgDisco.Load(listaEnPapelera[0].UrlImagen);
                }
                else
                {
                    pbImgDisco.Load("https://www.pngall.com/wp-content/uploads/3/Band-Silhouette-Transparent.png");
                }
                

            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error: "+ ex.ToString());
            }
        }
    }
}
