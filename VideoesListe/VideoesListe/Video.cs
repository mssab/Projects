using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoesListe
{
    public class Video
    {
        public Video(string Name, int vZeit, DateTime erdatum, DateTime hochdatum)
        {
            this.Name = Name;
            this.vZeit = vZeit;
            this.erdatum = erdatum;
            this.hochdatum = hochdatum;
            this.isChanged = false;

        }

        public string Name { set; get; }
        public int vZeit { set; get; }
        public DateTime erdatum { set; get; }
        public DateTime hochdatum { set; get; }
        public bool isChanged { get; set; }

        public Video(string Name, string vZeit, string erdatum, string hochdatum)
        {
            this.Name = Convert.ToString(Name.Trim());

            int dummy = 0;
            if (int.TryParse(vZeit, out dummy) == true)
            {
                this.vZeit = dummy;
            }
            else
            {
                this.vZeit = 0;
            }

            this.erdatum = Convert.ToDateTime(erdatum);
            this.hochdatum = Convert.ToDateTime(hochdatum);
            this.isChanged = false;

        }
        public bool speichernTestbt()
        {

            if (isChanged == true)
            {
                VideoesListe.Form1 vl = new VideoesListe.Form1();
                vl.spichbtn.PerformClick();
            }
            return false;
        }
        /*
        public void addVideos()
        {
            System.Windows.Forms.ListView ls = new ListView();
            List<Video> videos = new List<Video>();

                ListViewItem lvi = new ListViewItem();
                Video video = new Video();
                lvi.Text = video.Name;
                lvi.SubItems.Add(video.vZeit.ToString());
                lvi.SubItems.Add(video.erdatum.ToShortDateString());
                lvi.SubItems.Add(video.hochdatum.ToShortDateString());
                lvi.Tag = video;
                ls.Items.Add(lvi);

            }*/
    }


}
