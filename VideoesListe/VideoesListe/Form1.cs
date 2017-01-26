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
using System.Xml;
using System.Xml.Linq;

namespace VideoesListe
{
    public partial class Form1 : Form
    {
        List<Video> videos = new List<Video>();
        string geladeneDateiname = "";
        int geladeneAnzahl = 0;

        public Form1()
        {
            InitializeComponent();
            //listViewVideos.Items.AddRange(videos.Select(c => new ListViewItem(c.Name)).ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddVideo();

        }
        public void AddVideo()
        {

            VidListFm fm = new VidListFm();
            DialogResult dia = fm.ShowDialog();

            if (dia == System.Windows.Forms.DialogResult.OK)
            {
                Video v = new Video(fm.TextboxTitle.Text, fm.TextboxZeit.Text, fm.dateTimePicker1.Text, fm.dateTimePicker2.Text);
                videos.Add(v);
                v.isChanged = true;
                this.RefreshList();
            }
        }

        public bool speichernTest()
        {
            if (videos.Count == 0)
            {
                return false;
            }

            if (videos.Count != geladeneAnzahl)
            {
                return true;
            }


            bool oneVideoChanged = false;

            foreach (var video in this.videos)
            {
                if (video.isChanged == true)
                {
                    oneVideoChanged = true;
                    break;

                }
            }

            if (oneVideoChanged == false)
            {
                return false;
            }

            return true;
        }

        public void reset()
        {
            geladeneAnzahl = videos.Count;

            foreach (var video in this.videos)
            {
                video.isChanged = false;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            listViewVideos.View = View.Details;
            listViewVideos.FullRowSelect = true;
            listViewVideos.Columns.Add("Titel", 150);
            listViewVideos.Columns.Add("Zeit", 150);
            listViewVideos.Columns.Add("Erstellung Datum", 150);
            listViewVideos.Columns.Add("Hochladung Datum", 150);

        }

        public void RefeshTitle()
        {
            this.Text = this.geladeneDateiname;
        }

        public void RefreshList()
        {
            listViewVideos.Items.Clear();

            foreach (Video video in this.videos)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = video.Name;
                lvi.SubItems.Add(video.vZeit.ToString());
                lvi.SubItems.Add(video.erdatum.ToShortDateString());
                lvi.SubItems.Add(video.hochdatum.ToShortDateString());
                lvi.Tag = video;
                listViewVideos.Items.Add(lvi);

            }


        }


        private void listViewVideos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Video video = listViewVideos.SelectedItems[0].Tag as Video;
            if (video != null)
            {
                VidListFm fr = new VidListFm();
                fr.TextboxTitle.Text = video.Name;
                fr.TextboxZeit.Text = video.vZeit.ToString();
                fr.dateTimePicker1.Text = video.erdatum.ToShortDateString();
                fr.dateTimePicker2.Text = video.hochdatum.ToShortDateString();
                video.isChanged = true;
                fr.ShowDialog();
                if (fr.DialogResult == DialogResult.OK)
                {
                    video.Name = fr.TextboxTitle.Text;
                    video.vZeit = Convert.ToInt32(fr.TextboxZeit.Text);
                    video.erdatum = Convert.ToDateTime(fr.dateTimePicker1.Text);
                    video.hochdatum = Convert.ToDateTime(fr.dateTimePicker2.Text);
                    this.RefreshList();
                }
            }


        }

        public void SaveVideo()
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nurSave(saveFileDialog1.FileName);
            }

            this.RefreshList();
        }

        public void nurSave(string filename)
        {
            try
            {
                XmlTextWriter xr = new XmlTextWriter(filename, Encoding.Unicode);
                xr.WriteStartDocument();
                xr.WriteStartElement("XMLFILE");
                foreach (var video in this.videos)
                {
                    xr.WriteStartElement("Video");
                    xr.WriteElementString("Name", video.Name);
                    xr.WriteElementString("Zeit", video.vZeit.ToString());
                    xr.WriteElementString("erDatum", video.erdatum.ToString());
                    xr.WriteElementString("hochDatum", video.hochdatum.ToString());
                    xr.WriteEndElement();
                    video.isChanged = false;
                }
                xr.WriteEndElement();
                xr.WriteEndDocument();
                xr.Close();

                this.reset();
                this.RefreshList();
            }
            catch (Exception ex) { MessageBox.Show("Keine Datei zu Speischern !", "Fehler Meldung !"); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            nurSave(geladeneDateiname);

        }

        private void ladbtn_Click(object sender, EventArgs e)
        {
            LoadVideo();

        }



        public void Laden(string dateiname)
        {
            try
            {
                geladeneDateiname = dateiname;

                videos.Clear();
                DataSet ds = new DataSet();
                ds.ReadXml(dateiname);

                if (ds.Tables["Video"] != null)
                {
                    foreach (DataRow dr in ds.Tables["Video"].Rows)
                    {
                        string n = dr["Name"].ToString();
                        string z = dr["Zeit"].ToString();
                        string er = dr["erDatum"].ToString();
                        string ho = dr["hochDatum"].ToString();
                        Video fm = new Video(n, z, er, ho);
                        videos.Add(fm);
                        this.RefreshList();
                        this.RefeshTitle();
                    }

                    geladeneAnzahl = videos.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die Datei ist ungültig oder Leer!", "Fehler !");
            }
        }


        public void LoadVideo()
        {
            Video fm;
            VidListFm vp = new VidListFm();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "txt files (*.txt)|*.txt|ALL files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog.RestoreDirectory = true;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (speichernTest() == true)
                {
                    if (File.Exists(openDialog.FileName))
                    {
                        DialogResult r1 = MessageBox.Show("Willst du Speichern ?", "Speichern", MessageBoxButtons.YesNo);
                        if (r1 == System.Windows.Forms.DialogResult.Yes)
                        {
                            nurSave(geladeneDateiname);


                        }
                    }
                    else
                    {
                        DialogResult res = MessageBox.Show("Willst du Speichern ?", "Speichern", MessageBoxButtons.YesNo);


                        if (res == System.Windows.Forms.DialogResult.Yes)
                        {
                            SaveVideo();
                        }
                    }
                }

                Laden(openDialog.FileName);
            }
        }

        public void ladex()
        {
            Video fm;
            VidListFm vp = new VidListFm();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "txt files (*.txt)|*.txt|ALL files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog.RestoreDirectory = true;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {

                if ((speichernTest() == true))
                {
                    nurSave(geladeneDateiname);
                }

                Laden(openDialog.FileName);
            }

        }



        private void button1_Click_2(object sender, EventArgs e)
        {
            deleteVideos();
        }

        public void deleteVideo()
        {
            if (listViewVideos.SelectedItems.Count != 0)
            {

                Video video = listViewVideos.SelectedItems[0].Tag as Video;
                if (video != null)
                {

                    videos.Remove(video);
                    RefreshList();
                    video.isChanged = true;

                }
            }
            else { MessageBox.Show("Keine Datei zu löschen !", "Fehler Meldung"); }
        }

        public void deleteVideos()
        {
            for (int i = 0; i < listViewVideos.Items.Count; i++)
            {

                if (listViewVideos.Items[i].Selected == true)
                {
                    Video video = listViewVideos.Items[i].Tag as Video;
                    videos.Remove(video);
                }
            }
            RefreshList();
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVideo();
        }

        private void öffenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadVideo();

        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveVideo();
        }

        private void speichernToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nurSave(geladeneDateiname);
        }

        private void listViewVideos_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listViewVideos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult de = MessageBox.Show("Willst du den Datei löschen ?", "Löschen !", MessageBoxButtons.YesNo);
                if (de == DialogResult.Yes)
                {
                    deleteVideos();
                }
                e.Handled = true;
            }
            if (e.KeyCode == Keys.A && e.Control)
            {
                listViewVideos.MultiSelect = true;
                if (listViewVideos.SelectedItems.Count != 0)
                {
                    foreach (ListViewItem item in listViewVideos.Items)
                    {
                        item.Selected = true;
                    }
                }
            }
            if (e.KeyCode == Keys.N && e.Control)
            {
                AddVideo();
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                nurSave(geladeneDateiname);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewVideos.Items.Count; i++)
            {

                if (listViewVideos.Items[i].Selected == true)
                {
                    Video video = listViewVideos.Items[i].Tag as Video;
                    videos.Remove(video);
                }
            }
            RefreshList();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Filtering();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtering();

        }

        public void Filtering()
        {
            string value = textBox1.Text.ToLower();

            listViewVideos.Items.Clear();

            foreach (Video video in this.videos)
            {
                if (video.Name.StartsWith(value))
                {
                    ListViewItem lvi = new ListViewItem();
                    covertVideo(video, lvi);
                    listViewVideos.Items.Add(lvi);
                }
            }



            // List<Video> filterVideos = this.videos.Where(a => a.Name.StartsWith(value)).ToList();



            /*
            foreach (Video video in this.videos)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = video.Name;
                lvi.SubItems.Add(video.vZeit.ToString());
                lvi.SubItems.Add(video.erdatum.ToShortDateString());
                lvi.SubItems.Add(video.hochdatum.ToShortDateString());
                lvi.Tag = video;
                listViewVideos.Items.Add(lvi);

                string value = textBox1.Text.ToLower();

                for (int i = listViewVideos.Items.Count - 1; -1 < i; i--)
                {
                                if (listViewVideos.Items[i].Text.ToLower().StartsWith(value) == true)
                    {
                        // listViewVideos.Items[i].Remove();
                        listViewVideos.Items.Add(lvi);
                    }
                }
            }*/
        }

        private static void covertVideo(Video video, ListViewItem lvi)
        {
            lvi.Text = video.Name;
            lvi.SubItems.Add(video.vZeit.ToString());
            lvi.SubItems.Add(video.erdatum.ToShortDateString());
            lvi.SubItems.Add(video.hochdatum.ToShortDateString());
            lvi.Tag = video;
        }

        //public virtual ListViewItem Add(ListViewItem value);
        //public virtual ListViewItem Addit(string name, int z, string t1, string t2);

        public void addlist()
        {
            /*
            string n = "mas";
            int z = 12;
            string t1 = "1.1.2017";
            string t2 = "12.1.2017";*/
            /*
            Video video = new Video();
            ListViewItem lvi = new ListViewItem();

            lvi.Text = n;
            lvi.SubItems.Add(video.Name.ToString());
            lvi.SubItems.Add(t1.ToString());
            lvi.SubItems.Add(t2.ToString());
            lvi.Tag = video;
            listViewVideos.Items.Add(lvi);
            */


        }

    }

}


















