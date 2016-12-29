﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mantenimiento_Carl_Ros
{
    public partial class FrmMantenimientoComputoInsertar : Form
    {
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        OleDbCommandBuilder constructor;
        OleDbCommand comando;
        DataSet datos;

        static internal FrmMantenimientoComputoInsertar mci; 

        public FrmMantenimientoComputoInsertar()
        {
            FrmMantenimientoComputoInsertar.mci = this; 
            InitializeComponent();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (cboMantenimiento.Text == "" || cboProveedor.Text == "" || txtObservaciones.Text == "" || cboNota.Text == "")
            {
                MessageBox.Show("Faltan datos por llenar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                conexion = new OleDbConnection(ConexionBase.conectar());
                comando = new OleDbCommand();
                comando.Connection = conexion;

                comando.CommandText = "INSERT INTO MANTENIMIENTO_COMPUTO(cons, fecha, mantenimiento, proveedor, descripcion_actividades_realizadas, fecha_proximo_mto, observaciones, codigo_equipo, nota) VALUES(@cons, @fecha, @mantenimiento, @proveedor, @descripcion_actividades_realizadas, @fecha_proximo_mto, @observaciones, @codigo_equipo, @nota)";

                comando.Parameters.AddWithValue("@cons", txtCons.Text);
                comando.Parameters.AddWithValue("@fecha", dtpFecha.Text.ToString());
                comando.Parameters.AddWithValue("@mantenimiento", cboMantenimiento.Text);
                comando.Parameters.AddWithValue("@proveedor", cboProveedor.Text);
                comando.Parameters.AddWithValue("@descripcion_actividades_realizadas", txtDescripcionActividadesRealizadas.Text);
                comando.Parameters.AddWithValue("@fecha_proximo_mto", dtpfechaProximoMantenimiento.Text.ToString());
                comando.Parameters.AddWithValue("@observaciones", txtObservaciones.Text);
                comando.Parameters.AddWithValue("@codigo_equipo", txtCodigoEquipo.Text);
                comando.Parameters.AddWithValue("@nota", cboNota.Text);

                adaptador = new OleDbDataAdapter(comando);
                constructor = new OleDbCommandBuilder(adaptador);
                datos = new DataSet();

                conexion.Open();
                int i = comando.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show(i + " Datos insertados");
            }
        }

        private void cboMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMantenimiento.Text == "PREVENTIVO")
            {
                txtObservaciones.Text = "SOLICITO MANTENIMIENTO PREVENTIVO PARA ESTE EQUIPO";
                FrmCheckListMantenimientoComputo frmCheckListMantenimientoComputo = new FrmCheckListMantenimientoComputo();
                frmCheckListMantenimientoComputo.Show();
            }
            else if (cboMantenimiento.Text == "CORRECTIVO")
            {
                txtObservaciones.Clear();
                txtDescripcionActividadesRealizadas.Clear();
            }
        }

        private void FrmMantenimientoComputoInsertar_Load(object sender, EventArgs e)
        {
            for (double i = 0.0; i < 5; i += 0.1)
            {
                cboNota.Items.Add(Math.Round(i, 3));
            }
        }
    }
}
