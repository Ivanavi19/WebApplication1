using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Invocamos el servicio WSNav
                WSNav.JobQueue_Service ServicioJobQueue = new WSNav.JobQueue_Service();

                ServicioJobQueue.Url = "http://veronin.arbentia.com:7051/NAVExt/WS/CRONUS%20International%20Ltd./Page/JobQueue";
                ServicioJobQueue.Credentials = new NetworkCredential("WSAdmin", "Alcachofa2014");

                //Creamos un array que almacenará los distintos servicios
                WSNav.JobQueue[] ListaTareas;
                WSNav.JobQueue_Filter[] ListaTareasFiltro = new WSNav.JobQueue_Filter[0];

                ListaTareas = ServicioJobQueue.ReadMultiple(ListaTareasFiltro, null, 0);

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Inicio de la tabla.
                html.Append("<table border = '1'>");

                //Creamos las cabeceras de la tabla.
                html.Append("<tr>");
                int n = 0;
                foreach (WSNav.JobQueue tarea in ListaTareas)
                {
                    html.Append("<th>");
                    html.Append("Servicio " + (n + 1));
                    html.Append("</th>");
                    n++;
                }

                html.Append("</tr>");

                //Creamos las filas de la tabla con los datos.
                html.Append("<tr>");

                foreach (WSNav.JobQueue tarea in ListaTareas)
                {
                    html.Append("<td>");
                    html.Append(tarea.Object_Caption_to_Run);
                    html.Append("</td>");
                }

                html.Append("</tr>");

                //Fin de la tabla.
                html.Append("</table>");

                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }
    }
}
