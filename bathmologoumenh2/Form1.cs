using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bathmologoumenh2
{
    public partial class Form1 : Form
    {
        String connectionString = "Data Source=c:DB1.db;Version=3;";
        SQLiteConnection conn;

        Graphics g;
        Pen p;
        SolidBrush brush;

        int x1, y1;
        int option;
        int option2;
        int x, y;
        int number = 0;
        int number2 = 0;
        int number3 = 0;

        bool freestyle;
        bool clean;

        string shape;

        public Form1()
        {
            InitializeComponent();
        }

        //MouseMove
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString() + ", " + e.Y.ToString() + " px";

            if (option == 1 && freestyle) //Freestyle //Mouse is down and option = 1
            {
                g.DrawLine(p, x1, y1, e.X, e.Y);
                x1 = e.X;
                y1 = e.Y;
            }
            else if (option == 6 && clean) //Erase //Mouse is down and option = 6
            {
                p.Color = Color.White;
                g.DrawLine(p, x1, y1, e.X, e.Y);
                x1 = e.X;
                y1 = e.Y;
            }
        }

        //Form
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            conn = new SQLiteConnection(connectionString);

            g = this.CreateGraphics();
            p = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
        }

        //Premade drawings
        private void premadeDrawingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            timer1.Enabled = false;
        }

        //Drawing style
        private void drawingStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Colors
        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            timer1.Enabled = false;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p.Color = colorDialog1.Color;
                brush.Color = colorDialog1.Color;
            }
        }

        //MouseDown
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;

            if (option == 1) //Freestyle
            {
                freestyle = true; //Mouse is down

                //SQLite
                shape = "Freestyle"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 6) //Erase
            {
                freestyle = false;
                clean = true; //Mouse is down

                //SQLite
                shape = "Eraser"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
        }

        //Freestyle
        private void freestyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = 1;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Rectangle
        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = 2;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Ellipse
        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = 3;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Circle
        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = 4;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //MouseUp
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            freestyle = false; //Mouse is up
            clean = false; //Mouse is up

            if (option == 2) /* Rectangle */
            {
                //to the right and down
                if (x1 < e.X && y1 < e.Y)
                {
                    g.DrawRectangle(p, new Rectangle(new Point(x1, y1), new Size(e.X - x1, e.Y - y1)));
                }
                //to the left and down
                else if (x1 > e.X && y1 < e.Y)
                {
                    g.DrawRectangle(p, new Rectangle(new Point(e.X, e.Y - (e.Y - y1)), new Size(x1 - e.X, e.Y - y1)));
                }
                //to the right and up
                else if (x1 < e.X && y1 > e.Y)
                {
                    g.DrawRectangle(p, new Rectangle(new Point(x1, y1 - (y1 - e.Y)), new Size(e.X - x1, y1 - e.Y)));
                }
                //to the left and up
                else if (x1 > e.X && y1 > e.Y)
                {
                    g.DrawRectangle(p, new Rectangle(new Point(e.X, e.Y), new Size(x1 - e.X, y1 - e.Y)));
                }

                //Adds new coordinates of the mouse
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Rectangle"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 3) /* Ellipse */
            {
                //to the right and down
                if (x1 < e.X && y1 < e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(x1, y1), new Size(e.X - x1, e.Y - y1)));
                }
                //to the left and down
                else if (x1 > e.X && y1 < e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(e.X, e.Y - (e.Y - y1)), new Size(x1 - e.X, e.Y - y1)));
                }
                //to the right and up
                else if (x1 < e.X && y1 > e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(x1, y1 - (y1 - e.Y)), new Size(e.X - x1, y1 - e.Y)));
                }
                //to the left and up
                else if (x1 > e.X && y1 > e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(e.X, e.Y), new Size(x1 - e.X, y1 - e.Y)));
                }

                //Adds new coordinates of the mouse
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Ellipse"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 4) /* Circle */
            {
                //to the right and down
                if (x1 < e.X && y1 < e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(x1, y1), new Size(e.X - x1, e.X - x1)));
                }
                //to the left and down
                else if (x1 > e.X && y1 < e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(e.X, e.Y - (e.Y - y1)), new Size(x1 - e.X, x1 - e.X)));
                }
                //to the right and up
                else if (x1 < e.X && y1 > e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(x1, y1 - (y1 - e.Y)), new Size(e.X - x1, e.X - x1)));
                }
                //to the left and up
                else if (x1 > e.X && y1 > e.Y)
                {
                    g.DrawEllipse(p, new Rectangle(new Point(e.X, e.Y), new Size(x1 - e.X, x1 - e.X)));
                }

                //Adds new coordinates of the mouse
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Circle"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 5) /* Lines */
            {
                g.DrawLine(p, x1, y1, e.X, e.Y);
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Line"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 7) /* Filled Rectangle */
            {
                //to the right and down
                if (x1 < e.X && y1 < e.Y)
                {
                    g.FillRectangle(brush, new Rectangle(new Point(x1, y1), new Size(e.X - x1, e.Y - y1)));
                }
                //to the left and down
                else if (x1 > e.X && y1 < e.Y)
                {
                    g.FillRectangle(brush, new Rectangle(new Point(e.X, e.Y - (e.Y - y1)), new Size(x1 - e.X, e.Y - y1)));
                }
                //to the right and up
                else if (x1 < e.X && y1 > e.Y)
                {
                    g.FillRectangle(brush, new Rectangle(new Point(x1, y1 - (y1 - e.Y)), new Size(e.X - x1, y1 - e.Y)));
                }
                //to the left and up
                else if (x1 > e.X && y1 > e.Y)
                {
                    g.FillRectangle(brush, new Rectangle(new Point(e.X, e.Y), new Size(x1 - e.X, y1 - e.Y)));
                }

                //Adds new coordinates of the mouse
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Filled Rectangle"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 8) /* Filled Ellipse */
            {
                //to the right and down
                if (x1 < e.X && y1 < e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(x1, y1), new Size(e.X - x1, e.Y - y1)));
                }
                //to the left and down
                else if (x1 > e.X && y1 < e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(e.X, e.Y - (e.Y - y1)), new Size(x1 - e.X, e.Y - y1)));
                }
                //to the right and up
                else if (x1 < e.X && y1 > e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(x1, y1 - (y1 - e.Y)), new Size(e.X - x1, y1 - e.Y)));
                }
                //to the left and up
                else if (x1 > e.X && y1 > e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(e.X, e.Y), new Size(x1 - e.X, y1 - e.Y)));
                }

                //Adds new coordinates of the mouse
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Filled Ellipse"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
            else if (option == 9) /* Filled Circle */
            {
                //to the right and down
                if (x1 < e.X && y1 < e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(x1, y1), new Size(e.X - x1, e.X - x1)));
                }
                //to the left and down
                else if (x1 > e.X && y1 < e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(e.X, e.Y - (e.Y - y1)), new Size(x1 - e.X, x1 - e.X)));
                }
                //to the right and up
                else if (x1 < e.X && y1 > e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(x1, y1 - (y1 - e.Y)), new Size(e.X - x1, e.X - x1)));
                }
                //to the left and up
                else if (x1 > e.X && y1 > e.Y)
                {
                    g.FillEllipse(brush, new Rectangle(new Point(e.X, e.Y), new Size(x1 - e.X, x1 - e.X)));
                }

                //Adds new coordinates of the mouse
                x1 = e.X;
                y1 = e.Y;

                //SQLite
                shape = "Filled Circle"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
        }

        //Erase
        private void eraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Eraser
        private void eraserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = 6;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Erase All
        private void eraseAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            groupBox1.Visible = false;
            var answer = MessageBox.Show("Are you sure you want to erase all?", "Erase All", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                g.Clear(Color.White);

                //SQLite
                shape = "Erase All"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
                exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (option2 == 1) /* House */ //Creates lines on timer's tick
            {
                switch (number)
                {
                    case 0:
                        g.DrawLine(p, 350, 450, 600, 450);
                        number = 1;
                        break;
                    case 1:
                        g.DrawLine(p, 350, 450, 350, 230);
                        number = 2;
                        break;
                    case 2:
                        g.DrawLine(p, 600, 450, 600, 230);
                        number = 3;
                        break;
                    case 3:
                        g.DrawLine(p, 350, 230, 600, 230);
                        number = 4;
                        break;
                    case 4:
                        number = 5;
                        g.DrawLine(p, 350, 230, 475, 70);
                        break;
                    case 5:
                        number = 6;
                        g.DrawLine(p, 600, 230, 475, 70);
                        break;
                    case 6:
                        number = 7;
                        g.DrawLine(p, 380, 250, 440, 250);
                        break;
                    case 7:
                        number = 8;
                        g.DrawLine(p, 510, 250, 570, 250);
                        break;
                    case 8:
                        number = 9;
                        g.DrawLine(p, 380, 250, 380, 310);
                        break;
                    case 9:
                        number = 10;
                        g.DrawLine(p, 510, 250, 510, 310);
                        break;
                    case 10:
                        number = 11;
                        g.DrawLine(p, 380, 310, 440, 310);
                        break;
                    case 11:
                        number = 12;
                        g.DrawLine(p, 510, 310, 570, 310);
                        break;
                    case 12:
                        number = 13;
                        g.DrawLine(p, 440, 250, 440, 310);
                        break;
                    case 13:
                        number = 14;
                        g.DrawLine(p, 570, 310, 570, 250);
                        break;
                    case 14:
                        number = 15;
                        g.DrawLine(p, 465, 450, 465, 400);
                        break;
                    case 15:
                        number = 16;
                        g.DrawLine(p, 485, 450, 485, 400);
                        break;
                    case 16:
                        number = 17;
                        g.DrawLine(p, 465, 400, 485, 400);
                        break;
                    case 17:
                        number = 18;
                        g.DrawLine(p, 520, 127, 520, 90);
                        break;
                    case 18:
                        number = 19;
                        g.DrawLine(p, 520, 90, 540, 90);
                        break;
                    case 19:
                        number = 20;
                        g.DrawLine(p, 540, 90, 540, 152);
                        break;
                    case 20:
                        number = 21;
                        g.DrawLine(p, 540, 250, 540, 310);
                        break;
                    case 21:
                        number = 22;
                        g.DrawLine(p, 510, 280, 570, 280);
                        break;
                    case 22:
                        number = 23;
                        g.DrawLine(p, 410, 250, 410, 310);
                        break;
                    case 23:
                        number = 0; //Setting number back to 0 so the House can be redraw without closing the program
                        g.DrawLine(p, 380, 280, 440, 280);
                        timer1.Enabled = false;
                        break;
                }
            }
            else if (option2 == 2) /* Sun */ //Creates lines on timer's tick
            {
                switch (number2)
                {
                    case 0:
                        number2 = 1;
                        g.DrawEllipse(p, new Rectangle(new Point(850, 60), new Size(80, 80)));
                        break;
                    case 1:
                        number2 = 2;
                        g.DrawLine(p, 890, 45, 890, 25);
                        break;
                    case 2:
                        number2 = 3;
                        g.DrawLine(p, 890, 155, 890, 175);
                        break;
                    case 3:
                        number2 = 4;
                        g.DrawLine(p, 945, 100, 965, 100);
                        break;
                    case 4:
                        number2 = 5;
                        g.DrawLine(p, 835, 100, 815, 100);
                        break;
                    case 5:
                        number2 = 6;
                        g.DrawLine(p, 935, 60, 950, 40);
                        break;
                    case 6:
                        number2 = 7;
                        g.DrawLine(p, 850, 140, 830, 160);
                        break;
                    case 7:
                        number2 = 8;
                        g.DrawLine(p, 858, 60, 840, 42);
                        break;
                    case 8:
                        number2 = 0; //Setting number2 back to 0 so the Sun can be redraw without closing the program
                        g.DrawLine(p, 924, 140, 944, 159);
                        timer1.Enabled = false;
                        break;
                }
            }
            else if (option2 == 3) /* Ground */ //Creates lines on timer's tick
            {
                x += 300; // x+=300 so it looks like the program is drawing at the moment`
                y = 450;
                g.DrawLine(p, 0, 450, x, y);

                if (x >= 1000) // x>=1000 because the Form's y=998
                {
                    timer1.Enabled = false;
                    x = 0; //Setting x back to 0 so the Ground can be redraw without closing the program
                }
            }
            else /* Trees */ //Creates lines on timer's tick
            {
                switch (number3)
                {
                    case 0:
                        number3 = 1;
                        g.DrawLine(p, 160, 450, 160, 250);
                        break;
                    case 1:
                        number3 = 2;
                        g.DrawLine(p, 160, 450, 170, 450);
                        break;
                    case 2:
                        number3 = 3;
                        g.DrawLine(p, 170, 450, 170, 250);
                        break;
                    case 3:
                        number3 = 4;
                        g.DrawLine(p, 160, 250, 170, 250);
                        break;
                    case 4:
                        number3 = 5;
                        g.DrawEllipse(p, new Rectangle(new Point(117, 150), new Size(100, 100)));
                        break;
                    case 5:
                        number3 = 6;
                        g.DrawLine(p, 780, 450, 790, 450);
                        break;
                    case 6:
                        number3 = 7;
                        g.DrawLine(p, 780, 450, 780, 320);
                        break;
                    case 7:
                        number3 = 8;
                        g.DrawLine(p, 790, 450, 790, 320);
                        break;
                    case 8:
                        number3 = 9;
                        g.DrawLine(p, 780, 320, 790, 320);
                        break;
                    case 9:
                        g.DrawEllipse(p, new Rectangle(new Point(755, 230), new Size(60, 90)));
                        number3 = 0; //Setting number3 back to 0 so the Trees can be redraw without closing the program
                        timer1.Enabled = false;
                        break;
                }
            }
        }

        //Brush Size
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            p.Width = (int)numericUpDown1.Value;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        //Sun
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            option2 = 2;
            timer1.Enabled = true;
            groupBox1.Visible = false;

            //SQLite
            shape = "Premade Drawing"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
            exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
        }

        //House
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            option2 = 1;
            timer1.Enabled = true;
            groupBox1.Visible = false;

            //SQLite
            shape = "Premade Drawing"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
            exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
        }

        //Ground
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            option2 = 3;
            timer1.Enabled = true;
            groupBox1.Visible = false;

            //SQLite
            shape = "Premade Drawing"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
            exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
        }

        //Trees
        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            option2 = 4;
            timer1.Enabled = true;
            groupBox1.Visible = false;

            //SQLite
            shape = "Premade Drawing"; //Setting drawing type to a variable (shape) so we can then sent it to exodia function
            exodia(shape); //Calling exodia function which is responsible for saving data in the SQLite database
        }

        //Filled Rectangle
        private void rectangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            option = 7;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Filled Ellipse
        private void ellipseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            option = 8;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Filled Circle
        private void circleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            option = 9;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //Lines
        private void linesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = 5;
            groupBox1.Visible = false;
            timer1.Enabled = false;
        }

        //exodia function, responsible for SQLite 
        public void exodia(string shape)
        {
            string timestamp = DateTime.Now.ToString();

            conn.Open();
            string insertQuery = $"Insert into User(Shape, Timestamp) values('{shape}','{timestamp}')";
            SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn);
            int count = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
