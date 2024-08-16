using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Charting;
using Telerik.Web.UI;
using System.Drawing;

namespace paginaOficial.viewsAdmin
{
    public partial class estadisticasMensuales : System.Web.UI.Page
    {
        Controlers.BaseDatos odbusuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }

            odbusuario = new Controlers.BaseDatos();

            int cantidadMensual = (Convert.ToInt32(odbusuario.totalVisitasMes()));
            


            if (cantidadMensual == 0)
            {
                Panel2.Visible = false;

                Panel1.Visible = true;
            }

            else
            {
                Panel2.Visible = true;
                labelVisitas.Text = Convert.ToString(cantidadMensual);

                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.EstadisticaAreaMensual();

                pieChartArea.Width = Unit.Pixel(500);
                pieChartArea.Height = Unit.Pixel(350);
                pieChartArea.PlotArea.XAxis.TitleAppearance.Text = "area";
                pieChartArea.PlotArea.YAxis.TitleAppearance.Text = "Visitas";
                pieChartArea.ChartTitle.Text = "Porcentaje por area";
                pieChartArea.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Custom;

                //contenedor de datos tipo pie
                PieSeries datosPie = new PieSeries();
                datosPie.Name = "Datos porcentuales";
                datosPie.LabelsAppearance.Visible = true;
                datosPie.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieAndDonutLabelsPosition.OutsideEnd;
                datosPie.LabelsAppearance.DataFormatString = "{0} %";
                datosPie.TooltipsAppearance.Color = System.Drawing.Color.Black;

                //conf de bar chart
                BarChart1.Width = Unit.Pixel(500);
                BarChart1.Height = Unit.Pixel(350);
                BarChart1.PlotArea.XAxis.TitleAppearance.Text = "area";
                BarChart1.PlotArea.YAxis.TitleAppearance.Text = "Visitas";
                BarChart1.ChartTitle.Text = "Cantidades por area";
                BarChart1.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Right;

                DataTable dt = ds.Tables[1];
                string nombre = "";
                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString() != nombre & !string.IsNullOrEmpty(row[0].ToString()))
                    {
                        BarSeries datosBar = new BarSeries();
                        datosBar.LabelsAppearance.Visible = true;
                        datosBar.Name = row[0].ToString();
                        datosBar.LabelsAppearance.DataFormatString = "{0}";
                        datosBar.TooltipsAppearance.Color = System.Drawing.Color.Black;
                        CategorySeriesItem datoBar = new CategorySeriesItem(Convert.ToDecimal(row[1].ToString()));
                        datosBar.SeriesItems.Add(datoBar);
                        PieSeriesItem datos = new PieSeriesItem(Convert.ToDecimal(row[2].ToString()));
                        datos.Name = row[0].ToString();
                        datosPie.SeriesItems.Add(datos);
                        BarChart1.PlotArea.Series.Add(datosBar);
                    }
                    nombre = row[0].ToString();
                }
                pieChartArea.PlotArea.Series.Add(datosPie);
            }

        }
    }
}