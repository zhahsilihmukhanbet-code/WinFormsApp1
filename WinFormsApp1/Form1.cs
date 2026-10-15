using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7 
{
    public partial class Form1 : Form
    {
        bool drawing = false;          // Хулганы товч дарагдсан эсэх
        Point last;                    // Харандаагаар зурахад: өмнөх цэг
        Point start;                   // Тэгш өнцөгт зурахад: эхлэх цэг

        Color penColor = Color.Black;  // Өнгө
        int penWidth = 1;              // Өргөн
        string mode = "pen";           // "pen" = хулганаар зурах, "rect" = тэгш өнцөгт

        // Цагаан дэвсгэртэй зургийг хадгалах Bitmap ба Graphics
        Bitmap canvasBitmap;
        Graphics canvasGraphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Формын фон өнгийг ЦАГААН болгох
            this.BackColor = Color.White;

            // Дэлгэцийн хэмжээтэй адил цагаан дэвсгэр үүсгэх
            canvasBitmap = new Bitmap(this.Width, this.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasGraphics.Clear(Color.White); // Дэвсгэрийг цагаанаар будна
        }

        // ===== Зургийг дэлгэцэнд дахин зурах үйл явдал =====
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Санах ойд зурсан зургийг дэлгэц дээр дүрсэлнэ
            if (canvasBitmap != null)
            {
                e.Graphics.DrawImage(canvasBitmap, Point.Empty);
            }
        }

        // ===== Хулганы үйл явдлууд =====

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
            last = e.Location;
            start = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing && mode == "pen")
            {
                using (Pen pen = new Pen(penColor, penWidth))
                {
                    // Шугамыг Bitmap дээр зурна
                    canvasGraphics.DrawLine(pen, last, e.Location);
                }

                last = e.Location;
                this.Invalidate(); // Дэлгэцийг сэргээж шинэчилнэ
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing && mode == "rect")
            {
                int x = Math.Min(start.X, e.X);
                int y = Math.Min(start.Y, e.Y);
                int w = Math.Abs(e.X - start.X);
                int h = Math.Abs(e.Y - start.Y);

                using (Pen pen = new Pen(penColor, penWidth))
                {
                    // Тэгш өнцөгтийг Bitmap дээр зурна
                    canvasGraphics.DrawRectangle(pen, x, y, w, h);
                }

                this.Invalidate(); // Дэлгэцийг сэргээж шинэчилнэ
            }
            drawing = false;
        }

        // ===== Tools цэс =====

        // Tools → Хулганаар зурах
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "pen";
        }

        // Tools → Тэгш өнцөгт
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "rect";
        }

        // Tools → Өнгө
        private void өнгөToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = penColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                penColor = colorDialog1.Color;
            }
        }

        // Өргөн сонгох ComboBox
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                penWidth = int.Parse(toolStripComboBox1.SelectedItem.ToString());
            }
        }
    }
}