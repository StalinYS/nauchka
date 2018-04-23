using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace nauchka
{
    public partial class Result : Form
    {

        string n, f;
        public Result(string file, string num)
        {
            InitializeComponent();
            n = num;
            f = file;
        }

        DataTable lecturerData;
        private void CreateLecturerData()
        {
            lecturerData = new DataTable();
            lecturerData.Columns.Add("number", typeof(Int32));
            lecturerData.Columns.Add("topic", typeof(String));
            lecturerData.Columns.Add("type", typeof(String));
            lecturerData.Columns.Add("date", typeof(String));
            lecturerData.Columns.Add("time", typeof(String));
            lecturerData.Columns.Add("hoursAmount", typeof(Int32));
            lecturerData.Columns.Add("signature", typeof(String));

        }


        private void Result_Load(object sender, EventArgs e)
        {
            label1.Text = n;
            CreateLecturerData();

            dataGridView1.DataSource = lecturerData;
            dataGridView1.Columns["number"].HeaderText = "№";
            dataGridView1.Columns["number"].Width = 100;
            dataGridView1.Columns["topic"].HeaderText = "Тема";
            dataGridView1.Columns["topic"].Width = 250;
            dataGridView1.Columns["type"].HeaderText = "Тип";
            dataGridView1.Columns["type"].Width = 200;
            dataGridView1.Columns["date"].HeaderText = "Дата";
            dataGridView1.Columns["date"].Width = 150;
            dataGridView1.Columns["time"].HeaderText = "Время";
            dataGridView1.Columns["time"].Width = 150;
            dataGridView1.Columns["hoursAmount"].HeaderText = "Количество часов";
            dataGridView1.Columns["hoursAmount"].Width = 150;
            dataGridView1.Columns["signature"].HeaderText = "Роспись";
            dataGridView1.Columns["signature"].Width = 150;

            string path = "http://timetable.sbmt.by/shedule/lecturer/" + f;

            XDocument doc = XDocument.Load(path);
            int i = 1;
            var elemList =
                from el in doc.Descendants("lesson")
                where ((string)el.Element("group")).IndexOf(n) > -1
                select el;

            foreach (var elem in elemList)
            {

                DataRow tempRow = lecturerData.NewRow();
                tempRow["type"] = elem.Element("type").Value;
                tempRow["date"] = elem.Element("date").Value;
                tempRow["time"] = elem.Element("time").Value;
                tempRow["number"] = i;
                tempRow["hoursAmount"] = 2;
                i++;
                lecturerData.Rows.Add(tempRow);


            }
        }
    }
}

