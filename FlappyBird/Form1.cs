using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {

        int yercekimi = 5;//kuşu zıplatma için değer
        int hiz = 10;//boruların hareketi için hız değeri
        Random rnd = new Random();//boruları random şekilde çağırmak için
        int score;//skor puanı
        bool pbPipe1Control, pbPipe3Control;//skorun artışını kontrol için


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Space tuşu ile kuşu zıplatma
            if (e.KeyCode==Keys.Space&&tmrGame.Enabled)
            {
                if (pbBird.Top>0)
                {
                    pbBird.Top -= yercekimi * 10;
                }
            }
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            //Kuşun düşmesi
            pbBird.Top += yercekimi;

            //Pipeların sola doğru kayması
            pbPipe1.Left -= hiz;
            pbPipe2.Left -= hiz;
            pbPipe3.Left -= hiz;
            pbPipe4.Left -= hiz;


            //Pipe ları tekrardan ekranın sapından getirme
            if (pbPipe1.Right<0)
            {
                pbPipe1.Left = ClientSize.Width + rnd.Next(200);
                //Pipe her kaybolduğunda kuş geçmiş demektir bu sebeple tekrar geçtiğinde score alabilmesi için false yapıyoruz
                pbPipe1Control = false;
            }
            if (pbPipe2.Right < 0)
            {
                pbPipe2.Left = ClientSize.Width + rnd.Next(200);
            }
            if (pbPipe3.Right < 0)
            {
                pbPipe3.Left = ClientSize.Width + rnd.Next(200);
                pbPipe3Control = false;
            }
            if (pbPipe4.Right < 0)
            {
                pbPipe4.Left = ClientSize.Width + rnd.Next(200);
            }

            //ölme durumu
            if (pbPipe1.Bounds.IntersectsWith(pbBird.Bounds)|| pbPipe2.Bounds.IntersectsWith(pbBird.Bounds) || pbPipe3.Bounds.IntersectsWith(pbBird.Bounds) || pbPipe4.Bounds.IntersectsWith(pbBird.Bounds) || pbGround.Bounds.IntersectsWith(pbBird.Bounds))//4 tane pipe veya ground çevresi ile kuşun kesişimi
            {
                tmrGame.Stop();
                //yeniden oynama durumu
                DialogResult dr=MessageBox.Show("Tekrar oynamak ister misiniz","Flappy Bird",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dr==DialogResult.Yes)
                {
                    pbBird.Left = 0;
                    pbBird.Top = 170;
                    pbPipe1.Left += ClientSize.Width;
                    pbPipe2.Left += ClientSize.Width;
                    pbPipe3.Left += ClientSize.Width;
                    pbPipe4.Left += ClientSize.Width;

                    pbPipe1Control = false;
                    pbPipe3Control = false;
                    score = 0;
                    tmrGame.Start();
                }
                else
                {
                    Close();
                }
            }


            //score artırma
            if (pbBird.Right>pbPipe1.Left&&!pbPipe1Control)
            {
                score++;
                pbPipe1Control = true;
            }
            if (pbBird.Right > pbPipe3.Left && !pbPipe3Control)
            {
                score++;
                pbPipe3Control = true;
            }
            lblScore.Text = "Score: " + score;






        }
    }
}
