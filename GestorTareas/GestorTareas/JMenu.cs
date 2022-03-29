using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorTareas {
    public partial class JMenu : Form {

        private bool tareaNueva;
        private bool cambioTxBox;

        public JMenu() {
            InitializeComponent();
            this.reset();
        }
    
        //metodos de tareas

        private void reset() {

            this.bttnGuardar.Enabled = false;
            this.bttnCancelar.Enabled = false;
            this.bttnEliminar.Enabled = false;
            this.listBoxTareas.Enabled = false;
            this.txtTarea.Enabled = false;

            //despues del guardar cambios
            this.bttnAgregar.Enabled = true;

            //Activa la lista solo si hay items en ella
            this.listBoxTareas.Enabled = this.listBoxTareas.Items.Count > 0;

            //quitar la selección de un elemento
            this.listBoxTareas.SelectedIndex = -1;

            //reseteo la bandera de la caja de texto
            this.cambioTxBox = false;
        }
        

        private void guardarCambios() {

            string tarea = txtTarea.Text;

            if (!string.IsNullOrEmpty(tarea)) {

                if (this.tareaNueva == true) {

                    this.listBoxTareas.Items.Add(tarea);

                } else {

                    this.listBoxTareas.Items[this.listBoxTareas.SelectedIndex] = tarea;

                }

                MessageBox.Show("Tarea almacenada exitosamente", "Atención");
                this.txtTarea.Text = "";
                this.reset();

            } else {
                MessageBox.Show("Debe ingresar una tarea antes de Guardar", "Atención");
            }
                
        }

        private void eliminarTarea() {



            if (this.listBoxTareas.SelectedIndex >= 0 && this.listBoxTareas.SelectedIndex < this.listBoxTareas.Items.Count) {

                if (MessageBox.Show("¿Desea eliminar la tarea?", "Mensaje de Error", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {

                    this.listBoxTareas.Items.RemoveAt(this.listBoxTareas.SelectedIndex);
                    this.txtTarea.Text = "";
                    this.reset();

                }
            } else {
                MessageBox.Show("No hay un elemento seleccionado", "Atención");
            }
        }

        private void cancelar() {

            if (this.cambioTxBox) {

                if (MessageBox.Show("¿Desea guardar los cambios antes de salir?", "Atencion", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {

                    this.guardarCambios();

                }

            }

            this.txtTarea.Text = "";
            this.reset();
        }

        private void editarItem() {

            if (this.listBoxTareas.SelectedIndex >= 0 && this.listBoxTareas.SelectedIndex < this.listBoxTareas.Items.Count) {
                string tareaSeleccionada = this.listBoxTareas.Items[listBoxTareas.SelectedIndex].ToString();
                this.txtTarea.Text = tareaSeleccionada;

                this.bttnGuardar.Enabled = true;
                this.bttnEliminar.Enabled = true;
                this.bttnCancelar.Enabled = true;
                this.bttnAgregar.Enabled = false;
                this.txtTarea.Enabled = true;

                this.tareaNueva = false; //el booleano se utiliza para saber si es una tarea nueva o un change
            }
        }

        // Metodos del Frame

        private void bttnAgregar_Click(object sender, EventArgs e) {

            if (this.cambioTxBox) {

                if (MessageBox.Show("¿Guardar Cambios?", "Atencion", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {

                    this.guardarCambios();

                }

            }

            this.txtTarea.Enabled = true;
            this.bttnGuardar.Enabled = true;
            this.bttnEliminar.Enabled = true;
            this.bttnCancelar.Enabled = true;
            this.bttnAgregar.Enabled = false;
            this.tareaNueva = true; //boleano indicando la carga de una nueva tarea
            this.txtTarea.Focus(); //Posiciona el cursor en el jtext

        }

        private void bttnGuardar_Click(object sender, EventArgs e) {
            this.guardarCambios();
        }

        private void bttnEliminar_Click(object sender, EventArgs e) {
            this.eliminarTarea();
        }

        private void bttnCancelar_Click(object sender, EventArgs e) {
            this.cancelar();
        }

        private void listBoxTareas_SelectedIndexChanged(object sender, EventArgs e) {

            this.editarItem();

        }

        private void txtTarea_TextChanged(object sender, EventArgs e) {
            this.cambioTxBox = true;
        }

        private void JMenu_FormClosing(object sender, FormClosingEventArgs e) {

            DialogResult respuesta = MessageBox.Show("¿Guardar Cambios?", "Atencion", MessageBoxButtons.YesNoCancel);

            if (respuesta == DialogResult.Yes) {

                this.guardarCambios();
                e.Cancel = true;
                return;

            }else if (respuesta == DialogResult.Cancel) {
                e.Cancel = true;
            }

        }
    }
}
