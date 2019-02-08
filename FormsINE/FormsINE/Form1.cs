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

namespace FormsINE
{
    public partial class Form1 : Form
    {
        int transactions = 0;
        string mensaje = "";
        Timer time = new Timer();
        Persona per;

        public Form1()
        {
            InitializeComponent();
        }

        private void LeerCodigo(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Return && e.KeyCode != Keys.OemOpenBrackets)
                    mensaje += e.KeyCode.ToString()[e.KeyCode.ToString().Length - 1];


                if (e.KeyCode == Keys.Return)
                {
                    lblError.Text = "";
                    Conexion con = new Conexion();
                    per = new Persona();
                    per = con.GetAll(mensaje);

                    lblEdad.Text = CalcularEdad(per.FechaNacimiento).ToString();
                    lblId.Text = per.ine_id;
                    lblNombre.Text = per.Nombre;
                    imgFoto.Image = per.Imagen;
                    per.ine_id = mensaje;

                    mensaje = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "NO SE ENCONTRO ESA PERSONA.\nINTENTE DE NUEVO";
                lblNombre.Text = "";
                lblEdad.Text = "";
                lblId.Text = "";
                imgFoto.Image = null;
                mensaje = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var ahh = ImageToString(@"C:\Users\SILVA\Downloads\alvaro-min-min_opt.jpg");
            lblTrans.Text = "Transactions: 0";
            time.Tick += new EventHandler(fechaTiempoReal);
            time.Interval = 1000;
            time.Start();

            Conexion conexion = new Conexion();
            lblTrans.Text = "Transactions: " + (transactions = conexion.GetTransactions(DateTime.Now.ToString("yyyy-MM-dd")));
        }

        public void fechaTiempoReal(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString();
            var a = DateTime.Now.ToString("hh:mm:ss tt");
            if (a == "12:00:00 a. m.")
                lblTrans.Text = "Transactions: " + (transactions = 0);
        }

        public int CalcularEdad(DateTime FechaNacimiento)
        {
            int edad = 0;
            int anoA = int.Parse(DateTime.Now.ToString("yyyy"));
            int mesA = int.Parse(DateTime.Now.ToString("MM"));
            int diaA = int.Parse(DateTime.Now.ToString("dd"));
            int anoN = int.Parse(FechaNacimiento.ToString("yyyy"));
            int mesN = int.Parse(FechaNacimiento.ToString("MM"));
            int diaN = int.Parse(FechaNacimiento.ToString("dd"));
            if (mesA <= mesN)
            {
                if(diaA < diaN)
                {
                    edad = anoA - int.Parse(FechaNacimiento.ToString("yyyy"));
                }
                else
                {
                    edad = anoA - int.Parse(FechaNacimiento.ToString("yyyy")) - 1;
                }
            }
            else
            {
                edad = anoA - int.Parse(FechaNacimiento.ToString("yyyy"));
            }
            return edad;
        }

        public string ImageToString(string path)
        {

            if (path == null)

                throw new ArgumentNullException("path");

            Image im = Image.FromFile(path);

            MemoryStream ms = new MemoryStream();

            im.Save(ms, im.RawFormat);

            byte[] array = ms.ToArray();

            return Convert.ToBase64String(array);

        }

        public Image StringToImage(string imageString)
        {
            if (imageString == null)

                throw new ArgumentNullException("imageString");

            byte[] array = Convert.FromBase64String(imageString);

            Image image = Image.FromStream(new MemoryStream(array));

            return image;
        }

        public string IdConverter(int transactions)
        {
            string ceros;
            if (transactions <= 9)
            {
                ceros = "000";
            }
            else if(transactions >9 && transactions <=99)
            {
                ceros = "00";
            }
            else if(transactions > 99 && transactions <= 999)
            {
                ceros = "0";
            }
            else
            {
                ceros = "";
            }
            return ceros;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction();
            Conexion con = new Conexion();

            try
            {
                transaction.id = long.Parse((string)DateTime.Now.ToString("yyyyMMdd") + IdConverter(transactions) + transactions);
                transaction.persona = per;
                transaction.date_transaction = DateTime.Now.ToString("yyyy-MM-dd");
                transaction.hour_transaction = DateTime.Now.ToString("hh:mm:ss tt");
                transaction.status = true;
                con.InsertTransaction(transaction);

                lblTrans.Text = "Transactions: " + ++transactions;
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);
            }
        }

        private void btnFailure_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction();
            Conexion con = new Conexion();

            try
            {
                transaction.id = long.Parse((string)DateTime.Now.ToString("yyyyMMdd") + IdConverter(transactions) + transactions);
                transaction.persona = per;
                transaction.date_transaction = DateTime.Now.ToString("yyyy-MM-dd");
                transaction.hour_transaction = DateTime.Now.ToString("hh:mm:ss tt");
                transaction.status = false;
                con.InsertTransaction(transaction);

                //lblTrans.Text = "Transactions: " + ++transactions;
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);
            }
        }
    }
}
