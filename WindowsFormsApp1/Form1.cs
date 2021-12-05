using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Aragog - the social media crawler";
            this.Icon = Properties.Resources.AcromantulaWU;
        }

        static string s = "/user/locuri-de-munca/";
        static List<Job> list = new List<Job>();

        private class Job
        {
            public string name;
            public string link;
            public int score;
            public string company;

            public Job(string name, string link, int score, string company)
            {
                this.name = name;
                this.link = link;
                this.score = score;
                this.company = company;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string city = textBox1.Text;
            string domenii = textBox2.Text;
            string[] cuvCheie = textBox3.Text.Split(' ');

            domenii = domenii.Replace(" ", "-");

            WebClient web1 = new WebClient();

            string url = "https://www.ejobs.ro/locuri-de-munca/" + city + "/" + domenii;


            string data = web1.DownloadString(url);
            //Console.WriteLine(data);
            int i = data.IndexOf(s);
            int contor = 0;
            int maxscor = 0;

            while (i > 0)
            {
                int k = data.IndexOf("/company/", i);
                string company = data.Substring(k + 9, 100);
                int end = company.IndexOf("/");
                company = company.Substring(0, end);
                company = company.Replace("-", " ");
                company = company.ToUpper();


                string joblink = data.Substring(i, 100);
                end = joblink.IndexOf("\"");
                joblink = joblink.Substring(0, end);
                joblink = "https://www.ejobs.ro" + joblink;

                int score = 0;
                contor++;
                if (contor < 100)
                    score = readJobLink(web1, joblink, cuvCheie);
                if (score > maxscor)
                    maxscor = score;
                label4.Text = "job searches: " + contor*17;
                string subs = data.Substring(i + s.Length, 100);
                end = subs.IndexOf("/");
                subs = subs.Substring(0, end);
                subs = subs.Replace("-", " ");
                //subs = char.ToUpper(subs[0]) + subs.Substring(1);
                subs = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(subs.ToLower());
                if (score > 0)
                    list.Add(new Job(subs,joblink,score, company));

                //listView1.Items.Add(subs + " " + score);
                i = data.IndexOf(s, i + 60);


            }
            //Console.ReadLine();

            list.Sort((x, y) => y.score.CompareTo(x.score));
            foreach(Job job in list)
            {
                if (cuvCheie.Length == 0)
                    listView1.Items.Add(job.name);
                else
                    listView1.Items.Add(job.score * 100 / (maxscor + 1) + "% ->  " + job.name);
            }

        }

        private int readJobLink(WebClient web1 ,string url, string[] cuvcheie)
        {
            try
            {
                if (cuvcheie.Length == 0)
                {
                    return 1;
                }
                int s = 0;
                web1 = new WebClient();
                string data = web1.DownloadString(url);
                foreach (string cuv in cuvcheie)
                {
                    s += Regex.Matches(data, cuv).Count;
                }
                return s;
            }
            catch
            {
                return 0;
            }
        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Job selectedJob = list.ElementAt(listView1.SelectedIndices[0]);
                textBox5.Text = "";
                textBox5.AppendText(selectedJob.link);
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText(selectedJob.company);
                textBox5.AppendText(Environment.NewLine);
                textBox5.AppendText(Environment.NewLine);
                WebClient web1 = new WebClient();

                string companyname = selectedJob.company.Replace("SRL", "").Replace("S R L", "");
                companyname = companyname.Replace(" ", "+");
                string html = web1.DownloadString("http://www.google.com/search?q=" + companyname + "&btnI");

                int k = html.IndexOf("href=\"");
                string site = html.Substring(k + 6, 100);
                int end = site.IndexOf("\"");
                site = site.Substring(0, end);

                if (site.Contains("https"))
                    textBox5.AppendText(site);
                else
                    textBox5.AppendText("The company doesn't have a site!");


            }
            catch
            {
                return;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            list.Clear();
            listView1.Items.Clear();
            label4.Text = "job searches: 0";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "detalii selectie";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
