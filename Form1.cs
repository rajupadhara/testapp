using ClosedXML.Excel;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Web
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable dtOneWkHigh;
        DataTable dtTwoWkHigh;        
        DataTable dtOneMonthHigh;
        DataTable dtTwoMonthHigh;
        DataTable dtThreeMonthHigh;
        DataTable dtSixMonthHigh;
        DataTable dtOneYearHigh;

        DataTable dtOneWkLow;
        DataTable dtTwoWkLow;
        DataTable dtOneMonthLow;
        DataTable dtTwoMonthLow;
        DataTable dtThreeMonthLow;
        DataTable dtSixMonthLow;
        DataTable dtOneYearLow;

        DataTable dtHighVolume;

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void WriteFileFromDataTable(DataTable tbl, string fileName)
        {
            DataTable dtResult = dtHighVolume.Clone();
            foreach (DataRow dRow in dtHighVolume.Rows)
            {
                var oRows = tbl.Select("Name = '" + dRow["Name"].ToString() + "'");
                foreach (var iRow in oRows)
                {
                    dtResult.ImportRow(iRow);
                }
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dtResult, fileName);
            wb.SaveAs(fileName + ".xlsx");
        }

        private DataTable  FillData(string webUrl)
        {
            var web = new HtmlWeb();
            var doc = web.Load(webUrl);
            var table = doc.DocumentNode.SelectNodes("//table").Last();
            var thead = table.SelectNodes("thead/tr/th");
            var dt = new DataTable();
            foreach (var th in thead)
            {
                dt.Columns.Add(th.InnerText.Trim());
            }

            var trCollections = table.SelectNodes("//tbody/tr");

            foreach (var tr in trCollections)
            {
                int i = 0;
                var tdCollections = tr.SelectNodes("td");
                var dRow = dt.NewRow();
                foreach (var td in tdCollections)
                {
                    dRow[i] = common.RemoveSpecialCharacters(td.InnerText.Trim());
                    i++;
                }
                dt.Rows.Add(dRow);
            }
            return dt;
        }

        private void btnOneWkHigh_Click(object sender, EventArgs e)
        {
            dtOneWkHigh = FillData(common.OneWkHigh);
        }

        private void btnTwoWkHigh_Click(object sender, EventArgs e)
        {
            dtTwoWkHigh =FillData(common.TwoWkHigh);
        }     

        private void btnOneMonthHigh_Click(object sender, EventArgs e)
        {
            dtOneMonthHigh  = FillData( common.OneMonthHigh);
        }            

        private void btnThreeMonthHigh_Click(object sender, EventArgs e)
        {
            dtThreeMonthHigh = FillData(common.ThreeMonthHigh);
        }

        private void btnSixMonthHigh_Click(object sender, EventArgs e)
        {
            dtSixMonthHigh = FillData( common.SixMonthHigh);
        }

        private void btnOneYearHigh_Click(object sender, EventArgs e)
        {
            dtOneYearHigh = FillData( common.OneYearHigh);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteFileFromDataTable(dtOneWkHigh, "OneWeek");
            WriteFileFromDataTable(dtTwoWkHigh, "TwoWeek");
            WriteFileFromDataTable(dtOneMonthHigh, "OneMonth");
            WriteFileFromDataTable(dtThreeMonthHigh, "ThreeMonth");
            WriteFileFromDataTable(dtSixMonthHigh, "SixMonth");
            WriteFileFromDataTable(dtOneYearHigh, "YearHigh");


            //DataTable dtResult = dtHighVolume.Clone();
            //foreach(DataRow dRow in dtHighVolume.Rows)
            //{
            //    var oRows = dtOneWkHigh.Select("Name = '" + dRow["Name"].ToString() + "'");
            //    foreach(var iRow in oRows)
            //    {
            //        dtResult.ImportRow(iRow);
            //    }                
            //}
            //var wb = new XLWorkbook();
            //wb.Worksheets.Add(dtResult, "WorksheetName");
            //wb.SaveAs("OneWeek.xlsx");


        }

        private void btnOneWkLow_Click(object sender, EventArgs e)
        {
            dtOneWkLow = FillData(common.OneWkLow);
        }

        private void btnTwoWkLow_Click(object sender, EventArgs e)
        {
            dtTwoWkLow = FillData(common.TwoWkLow);
        }

        private void btnOneMonthLow_Click(object sender, EventArgs e)
        {
            dtOneMonthLow = FillData(common.OneMonthLow);
        }

        private void btnThreeMonthLow_Click(object sender, EventArgs e)
        {
            dtThreeMonthLow = FillData(common.ThreeMonthLow);
        }

        private void btnSixMonthLow_Click(object sender, EventArgs e)
        {
            dtSixMonthLow  = FillData(common.SixMonthLow);
        }

        private void btnOneYearLow_Click(object sender, EventArgs e)
        {
            dtOneYearLow = FillData(common.OneYearLow);
        }

        private void btnHighVolume_Click(object sender, EventArgs e)
        {
            dtHighVolume = FillData(common.HighVolume);
        }
    }

    
}
