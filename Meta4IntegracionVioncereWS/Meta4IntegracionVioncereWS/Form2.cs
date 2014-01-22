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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = M4SESSION.name;
            pictureBox1.Image = M4SESSION.foto;
            label2.Text = M4SESSION.puesto;

            this.Refresh();


            //Ahora llamamos a...
            VincereWSMeta4.VincereWebServiceMeta4 a = new VincereWSMeta4.VincereWebServiceMeta4Client();


            VincereWSMeta4.meta4GetListaNotificacionesRequestBody Body2 = new VincereWSMeta4.meta4GetListaNotificacionesRequestBody();
            Body2.Base64Password = M4SESSION.Password;
            Body2.User = M4SESSION.User;
            Body2.UUID = "001S";
            Body2.Meta4SessionId = M4SESSION.SessionMeta4;
            Body2.Dispositivo = "csharp";

            VincereWSMeta4.meta4GetListaNotificacionesRequest cs2 = new VincereWSMeta4.meta4GetListaNotificacionesRequest(Body2);
            VincereWSMeta4.meta4GetListaNotificacionesResponseBody valores222 = new VincereWSMeta4.meta4GetListaNotificacionesResponseBody();
            VincereWSMeta4.meta4GetListaNotificacionesResponse res2 = new VincereWSMeta4.meta4GetListaNotificacionesResponse(valores222);
            res2 = a.meta4GetListaNotificaciones(cs2);


            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(res2.Body.meta4GetListaNotificacionesReturn);

                        
            //Records
            XmlElement xmlElem = (XmlElement)xDoc.GetElementsByTagName("Records")[0];
            String Records = xmlElem.InnerText;

            int NumReg = Int32.Parse(Records);


            //recorremos cada registro del XML
            for (int i = 0; i < NumReg; i++)
            {
                //Titulo
                String Item = "descripcion_" + i.ToString();
                XmlElement xmlElem1 = (XmlElement)xDoc.GetElementsByTagName(Item)[0];
                String Titulo = xmlElem1.InnerText;

                listBox1.Items.Add(Titulo);



            }



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //messagbeo

        }
    }
}
