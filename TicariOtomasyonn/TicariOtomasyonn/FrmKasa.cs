using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Charts;

namespace TicariOtomasyonn
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;

            musterihareket();
            firmahareket();

            //Toplam tutarı hesaplama
            SqlCommand komut1 = new SqlCommand("Select Sum(TUTAR) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString() + "TL";
            }
            bgl.baglanti().Close();

            //Son ayın faturalarını hesaplama
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) From TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + "TL";
            }
            bgl.baglanti().Close();

            //Son ayın personel maaşlarını hesaplama
            SqlCommand komut3 = new SqlCommand("Select MAASLAR From TBL_GIDERLER", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString() + "TL";
            }
            bgl.baglanti().Close();

            //Toplam müşteri sayısı hesaplama
            SqlCommand komut4 = new SqlCommand("Select Count(*) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam firma sayısı hesaplama
            SqlCommand komut5 = new SqlCommand("Select Count(*) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma şehir sayısı hesaplama
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Müşteri şehir sayısı hesaplama
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblSehirSayisi2.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam personel sayısı hesaplama
            SqlCommand komut8 = new SqlCommand("Select Count(*) From TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Stok sayısı hesaplama
            SqlCommand komut9 = new SqlCommand("Select Sum(ADET) From TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblPersonelSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            
            
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //Chartcontrol'de son dört ayın elektrik faturasını görüntüleme
            if (sayac > 0 && sayac <= 5)
            {
                groupControl10.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select Top 4 Ay,ELEKTRIK From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın su faturasını görüntüleme
            if (sayac>5 && sayac <= 10)
            {
                groupControl10.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,SU From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın doğalgaz faturasını görüntüleme
            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut12 = new SqlCommand("Select Top 4 Ay,DOGALGAZ From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın internet faturasını görüntüleme
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "Internet";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut13 = new SqlCommand("Select Top 4 Ay,DOGALGAZ From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın ekstra faturasını görüntüleme
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut14 = new SqlCommand("Select Top 4 Ay,DOGALGAZ From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }
        }
        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //Chartcontrol'de son dört ayın elektrik faturasını görüntüleme
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl10.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut15 = new SqlCommand("Select Top 4 Ay,ELEKTRIK From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr15 = komut15.ExecuteReader();
                while (dr15.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr15[0], dr15[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın su faturasını görüntüleme
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl10.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut16 = new SqlCommand("Select Top 4 Ay,SU From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr16 = komut16.ExecuteReader();
                while (dr16.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr16[0], dr16[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın doğalgaz faturasını görüntüleme
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut17 = new SqlCommand("Select Top 4 Ay,DOGALGAZ From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr17 = komut17.ExecuteReader();
                while (dr17.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr17[0], dr17[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın internet faturasını görüntüleme
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl10.Text = "Internet";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut18 = new SqlCommand("Select Top 4 Ay,DOGALGAZ From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr18 = komut18.ExecuteReader();
                while (dr18.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr18[0], dr18[1]));
                }
                bgl.baglanti().Close();
            }

            //Chartcontrol'de son dört ayın ekstra faturasını görüntüleme
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut19 = new SqlCommand("Select Top 4 Ay,DOGALGAZ From TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr19 = komut19.ExecuteReader();
                while (dr19.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr19[0], dr19[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
