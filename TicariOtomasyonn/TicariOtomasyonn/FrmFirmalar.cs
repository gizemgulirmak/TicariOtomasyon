﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TicariOtomasyonn
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtYetkiliGorev.Text = "";
            TxtYetkili.Text = "";
            MskYetkiliTC.Text = "";
            TxtSektor.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            TxtMail.Text = "";
            MskFax.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            TxtVergi.Text = "";
            RchAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 From TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            temizle();
            sehirlistesi();
            carikodaciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskYetkiliTC.Text = dr["YETKILITC"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", TxtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
            komut.Parameters.AddWithValue("@P11", Cmbil.Text);
            komut.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P14", RchAdres.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER where SEHIR=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FIRMALAR where ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,FAX=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 where ID=@P18", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", TxtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
            komut.Parameters.AddWithValue("@P11", Cmbil.Text);
            komut.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P14", RchAdres.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komut.Parameters.AddWithValue("@P18", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma bilgileri güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
