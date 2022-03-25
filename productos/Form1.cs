using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace productos
{
    public partial class Form1 : Form
    {
        string server = "Data Source = DESKTOP-T0P2KBQ\\SQLEXPRESS; Initial Catalog=super; Integrated Security = True";
        SqlConnection conectar = new SqlConnection();
        Conexion con = new Conexion();
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int stock = Convert.ToInt32(codigo.Text);
            double money = Convert.ToDouble(precio.Text);

            conectar.ConnectionString = server;
            conectar.Open();
            SqlCommand cmd = new SqlCommand("SP_INSERTUPDATEDELETE",conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDPRODUCTO", 1);
            cmd.Parameters.AddWithValue("@NOMBRE", nombre.Text);
            cmd.Parameters.AddWithValue("@CODIGO", stock);
            cmd.Parameters.AddWithValue("@CATEGORIA", categoria.Text);
            cmd.Parameters.AddWithValue("@PRECIO",money);
            cmd.Parameters.AddWithValue("@StatementType", "Insert");

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(nombre.Text + " Agregado correctamente");
            }
            catch (SqlException EX)
            {
                MessageBox.Show(EX.ToString());
                throw;
            }
            con.mostrar("productos", primaryGrid);
            conectar.Close();

        }


        private void eliminar_Click(object sender, EventArgs e)
        {
            conectar.ConnectionString = server;
            conectar.Open();
            SqlCommand cmd = new SqlCommand("SP_INSERTUPDATEDELETE", conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDPRODUCTO", 1);
            cmd.Parameters.AddWithValue("@NOMBRE", nombre.Text);
            cmd.Parameters.AddWithValue("@CODIGO", " ");
            cmd.Parameters.AddWithValue("@CATEGORIA", " ");
            cmd.Parameters.AddWithValue("@PRECIO", " ");
            cmd.Parameters.AddWithValue("@StatementType", "Delete");

            if (MessageBox.Show("El producto " + nombre.Text + " se eliminará", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha eliminado el registro");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            }
            con.mostrar("productos", primaryGrid);
            conectar.Close();
        }

    }
}
