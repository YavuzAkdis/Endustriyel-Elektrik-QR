using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace EndustriyelElektrikQRKod
{
    public partial class Form1 : Form
    {
        Bitmap qrCodeImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textToEncode = inputTextBox.Text.Trim(); // QR kodu oluşturulacak metni alın
            if (!string.IsNullOrEmpty(textToEncode))
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator(); // QR kodu oluşturucu nesnesi oluşturun
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q); // QR kodu verilerini oluşturun

                QRCode qrCode = new QRCode(qrCodeData); // QR kodu oluşturun
                qrCodeImage = qrCode.GetGraphic(10); // QR kodunun görüntüsünü alın

                qrCodePictureBox.Image = qrCodeImage; // QR kodunu PictureBox kontrolüne atayın
            }
            else
            {
                MessageBox.Show("Lütfen QR kodu oluşturulacak metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (qrCodeImage != null)
            {
                // Kullanıcıya dosya kaydetme iletişim kutusunu göster
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PNG dosyaları (*.png)|*.png|Tüm dosyalar (*.*)|*.*";
                    saveDialog.FilterIndex = 1;
                    saveDialog.FileName = "QRCode.png";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Kullanıcının seçtiği yolu kullanarak dosyayı PNG olarak kaydet
                        qrCodeImage.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);

                        // Kaydedilen dosyanın yolu hakkında kullanıcıyı bilgilendirin
                        MessageBox.Show($"QR kodu başarıyla kaydedildi: {saveDialog.FileName}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Öncelikle bir QR kodu oluşturun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
