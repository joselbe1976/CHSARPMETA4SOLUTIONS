using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;

namespace Meta4IntegracionVioncereWS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      //  83.32.91.224
        private void button1_Click(object sender, EventArgs e)
        {


            VincereWSMeta4.VincereWebServiceMeta4 a = new VincereWSMeta4.VincereWebServiceMeta4Client();

            VincereWSMeta4.meta4ControlSessionRequestBody Body = new VincereWSMeta4.meta4ControlSessionRequestBody();
            Body.Base64Password = EncodeTo64(textBox2.Text.ToString());
            Body.User = EncodeTo64(textBox1.Text.ToString()); 
            Body.UUID = "001S";
            Body.Meta4SessionId = "";
            Body.Dispositivo = "CSharp";


            M4SESSION.User = EncodeTo64(textBox1.Text.ToString());
            M4SESSION.Password = EncodeTo64(textBox2.Text.ToString());

            VincereWSMeta4.meta4ControlSessionRequest cs = new VincereWSMeta4.meta4ControlSessionRequest(Body);
            VincereWSMeta4.meta4ControlSessionResponseBody valores = new VincereWSMeta4.meta4ControlSessionResponseBody();
            VincereWSMeta4.meta4ControlSessionResponse res = new VincereWSMeta4.meta4ControlSessionResponse( valores);
            res =  a.meta4ControlSession(cs);

  
    
            String session = res.Body.meta4ControlSessionReturn;
            M4SESSION.SessionMeta4 = session;

            if (session.Equals(""))
            {
                MessageBox.Show("Login meta4", "Credenciales incorrectas");
                return;
            }

            //traemos la ficha del usuario conectado

            VincereWSMeta4.meta4GetInfoUserAppRequestBody Body2 = new VincereWSMeta4.meta4GetInfoUserAppRequestBody();
            Body2.Base64Password = M4SESSION.Password;
            Body2.User = M4SESSION.User;
            Body2.UUID = "001S";
            Body2.Meta4SessionId = session;
            Body2.Dispositivo = "csharp";

            VincereWSMeta4.meta4GetInfoUserAppRequest cs2 = new VincereWSMeta4.meta4GetInfoUserAppRequest(Body2);
            VincereWSMeta4.meta4GetInfoUserAppResponseBody valores222 = new VincereWSMeta4.meta4GetInfoUserAppResponseBody();
            VincereWSMeta4.meta4GetInfoUserAppResponse res2 = new VincereWSMeta4.meta4GetInfoUserAppResponse(valores222);
            res2 = a.meta4GetInfoUserApp(cs2);


            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(res2.Body.meta4GetInfoUserAppReturn);

            
            //nombre
            XmlElement xmlElem = (XmlElement)xDoc.GetElementsByTagName("nombre")[0];
            M4SESSION.name = xmlElem.InnerText;

            //Puesto
            XmlElement xmlElemJob = (XmlElement)xDoc.GetElementsByTagName("puesto")[0];
            M4SESSION.puesto = xmlElemJob.InnerText;

            //foto
            XmlElement xmlElemFoto = (XmlElement)xDoc.GetElementsByTagName("foto")[0];
            string base64ImageString = xmlElemFoto.InnerText;
            Bitmap bmpFromString = base64ImageString.Base64StringToBitmap();
            M4SESSION.foto = bmpFromString;


           // this.Hide();
            this.Close();


          }






        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        static public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        static public string DecodeBase64(string toDecode)
        {


            byte[] Bytescode =  System.Convert.FromBase64String(toDecode);
            return Bytescode.ToString();
           


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }
    }



}
