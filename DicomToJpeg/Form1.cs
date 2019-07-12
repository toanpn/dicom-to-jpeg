using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dicom;
using Dicom.Imaging;

namespace DicomToJpeg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"E:\Revest\DICOM\file sample\680000-20190711T104957Z-001\680000\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "dcm",
                Filter = "dicom files (*.DCM)|*.DCM",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                filePath = openFileDialog1.FileName;
                textBox1.Text = filePath;
            }

            //BinaryReader file;
            //file = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read));
            DicomDecoder dd = new DicomDecoder();
            dd.DicomFileName = filePath;

            //List<ushort> pixels = new List<ushort>();
            //dd.GetPixels16(ref pixels);
            //Byte[] bytes = new byte[pixels.Count];
            //for(int u = 0; u< pixels.Count; u++)
            //{
            //    bytes[u] = (byte)pixels[u];
            //}

            var image = new DicomImage(filePath);

            var file = DicomFile.Open(filePath);

            //var patientid = file.Dataset.Get<string>(DicomTag.PatientID);
            //var StudyDesciption = file.Dataset.Get<string>(DicomTag.StudyDescription);


            var StudyDesciption = file.Dataset.GetValue<string>(DicomTag.StudyDescription, 0);



            Bitmap btm = image.RenderImage().AsClonedBitmap();
            btm.Save("Output Image\\" + StudyDesciption + ".jpeg");
            this.pictureBox1.Image = btm;
            //byte[] jpegBytes = ConvertBytestoJpegBytes(bytes, dd.width, dd.height);
            //File.WriteAllBytes(@"D:\TestForest.jpeg", jpegBytes);

        }
        //private byte[] ConvertBytestoJpegBytes(byte[] pixels24bpp, int W, int H)
        //{
        //    GCHandle gch = GCHandle.Alloc(pixels24bpp, GCHandleType.Pinned);
        //    int stride = 4 * (((24 * W +31) / 32));
        //    Bitmap bmp = new Bitmap(W, H, stride, PixelFormat.Format24bppRgb, gch.AddrOfPinnedObject());
        //    MemoryStream ms = new MemoryStream();
        //    bmp.Save(ms, ImageFormat.Jpeg);
        //    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
        //    Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ms.ToArray());
        //    pictureBox1.Image = bitmap1;
        //    gch.Free();
        //    return ms.ToArray();
        //}
    }
}
