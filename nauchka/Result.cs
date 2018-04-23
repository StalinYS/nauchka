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

namespace nauchka
{
    public partial class Result : Form
    {

        string n, f;
        public Result(string file,string num)
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
        private void LoadData()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("http://timetable.sbmt.by/shedule/lecturer/" + f);


            XmlNodeList elemList = doc.GetElementsByTagName("lesson");
            for (int i = 0; i < elemList.Count; i++)
            {
                DataRow tempRow = lecturerData.NewRow();

                tempRow["type"] = elemList[i]["type"].InnerText;
                tempRow["date"] = elemList[i]["date"].InnerText;
                tempRow["time"] = elemList[i]["time"].InnerText;
                lecturerData.Rows.Add(tempRow);
            }

        }


        private void Result_Load(object sender, EventArgs e)
        {
            label1.Text = n;
            CreateLecturerData();
            try
            {
                LoadData();
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить данные преподавателей");
            }
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



            //dataSet1.ReadXml("http://timetable.sbmt.by/shedule/lecturer/" + f);

            //dataGridView1.DataSource = dataSet1;


            //DataTable lecturerTable = dataSet1.Tables["lesson"];

            //foreach (DataRow workRow in lecturerData.Rows)
            //{

            //    DataRow tempRow = lecturerTable.NewRow();
            //    tempRow["type"] = workRow["type"];
            //    tempRow["date"] = workRow["date"];
            //    tempRow["time"] = workRow["time"];

            //    lecturerTable.Rows.Add(tempRow);
            //}

        }
        }
    }

