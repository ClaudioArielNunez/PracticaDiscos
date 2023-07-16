using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Configuration;

namespace PracticaDiscos
{
    public partial class AgregarDisco : Form
    {
        private Disco nuevoDisco = null;
        public AgregarDisco()
        {
            InitializeComponent();
        }
        public AgregarDisco(Disco disco) //metodo para boton modificar
        {
            InitializeComponent();
            this.nuevoDisco = disco;
        }

        //Disco nuevoDisco = new Disco();
        private void btnOk_Click(object sender, EventArgs e)
        {
            NegocioDisco negocio = new NegocioDisco();
            try
            {
                if (nuevoDisco == null)
                {
                    nuevoDisco = new Disco();
                }             
                
                nuevoDisco.Titulo = txtTitulo.Text;
                nuevoDisco.FechaLanzamiento = dateTimePicker.Value;
                nuevoDisco.CantCanciones = int.Parse(txtCanciones.Text);
                nuevoDisco.UrlImagen = txtUrlImg.Text;
                
                
                nuevoDisco.Estilo = (Estilo)cmbEstilo.SelectedItem;
                nuevoDisco.TipoEdicion = (TipoEdicion)cmbEdicion.SelectedItem;

                if (nuevoDisco.Id != 0)
                {
                    negocio.modificar(nuevoDisco);
                    MessageBox.Show("Modificado con exito");
                }
                else
                {
                    negocio.agregar(nuevoDisco);
                    MessageBox.Show("Agregado con exito");
                }                

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
                cmbEstilo.ValueMember = "Id";
                cmbEstilo.DisplayMember = "Descripcion";
                
                cmbEdicion.DataSource = negEdicion.listar();
                cmbEdicion.DisplayMember = "Descripcion";
                cmbEdicion.ValueMember = "Id";



                //Agrego validacion para articulo null
                if (nuevoDisco != null)
                {
                    txtTitulo.Text = nuevoDisco.Titulo;
                    txtCanciones.Text = nuevoDisco.CantCanciones.ToString();
                    txtUrlImg.Text = nuevoDisco.UrlImagen;
                    dateTimePicker.Value = nuevoDisco.FechaLanzamiento;
                    cmbEstilo.SelectedValue = nuevoDisco.Estilo.Id;
                    cmbEdicion.SelectedValue = nuevoDisco.TipoEdicion.Id;
                    cargarImagen(nuevoDisco.UrlImagen);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Source);
                MessageBox.Show("No se pudo cargar: "+ ex.ToString());
            }
        }

        private void txtUrlImg_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImg.Text);
        }
        
        private void cargarImagen(string img)
        {
           try
           {
                pbImgDisco.Load(img);
           }
           catch (Exception ex)
           {
                pbImgDisco.Load("https://cdn-icons-png.flaticon.com/512/1178/1178479.png");
               //MessageBox.Show("No se pudo cargar la imagen\nEl error se debe a: "+ex.ToString());
           }
        }

        private void btnAgregarImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "*.jpg;|*.jpg;|*.png;|*.png";
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImg.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                //Guardamos imagen en carpeta en C:
                
                File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images_folder"] + archivo.SafeFileName);
            }
        }
        
    }
}
