﻿using System;
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
    }
}
