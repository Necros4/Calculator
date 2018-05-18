using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursOVA
{
    public partial class Form1 : Form
    {   // Змінна яка містить номер вибраної функції
        int chk = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {// Змінюємо тексти Label при виборі цієї кнопки і вимикаємо інші
            if (radioButton2.Checked == true)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                label1.Text = "Еліпс";
                label8.Text = "х^2 / a^2 + у^2 / b^2 = 1";
                label5.Text = "Без обмежень";
                label6.Text = "Крім нуля";
                label7.Text = "Крім нуля";
                chk = 1;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {// Змінюємо тексти Label при виборі цієї кнопки і вимикаємо інші
            if (radioButton3.Checked == true)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                radioButton2.Checked = false;
                radioButton1.Checked = false;
                label1.Text = "Гіпербола";
                label8.Text = "х^2 / a^2 - у^2 / b^2 = 1";
                label5.Text = "Без обмежень";
                label6.Text = "Крім нуля";
                label7.Text = "Крім нуля";
                chk = 2;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {// Змінюємо тексти Label при виборі цієї кнопки і вимикаємо інші
            if (radioButton1.Checked == true)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                label1.Text = "Пряма";
                label8.Text="y = ax + b";
                label5.Text = "Без обмежень";
                label6.Text = "Без обмежень";
                label7.Text = "Без обмежень";
                chk = 0;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {// Встановлюємо обмеження на константи
            if (radioButton2.Checked == true || radioButton3.Checked == true)
            {
                if (textBox2.Text != "") {
                    if (Double.Parse(textBox2.Text) == 0)
                    {
                        textBox2.Text = "";
                        MessageBox.Show("Ніяких нулів у дільнику!");
                    }
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {// Встановлюємо обмеження на константи
            if (radioButton2.Checked == true || radioButton3.Checked == true)
            {
                if (textBox1.Text != "") {
                    if (Double.Parse(textBox1.Text) == 0)
                    {
                        textBox1.Text = "";
                        MessageBox.Show("Ніяких нулів у дільнику!");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "") //Перевіряємо наявність усіх даних
            {
                double x = Double.Parse(textBox3.Text);
                double xmin = x - 3;
                double xmax = x + 3;
                int count = 6;
                // Змінна для перевірки кореня
                bool ok = true;
                // Оголошуємо масиви значень для графіку
                double[] xz = new double[count];
                double[] yz = new double[count];
                double a1 = Double.Parse(textBox2.Text), b1 = Double.Parse(textBox1.Text);
                // В залежності від функції оголошуємо об'єкти різних класів
                switch (chk)
                {
                    case 0:
                        {
                            Line a = new Line(a1,b1); //Оголошуємо лінійну функцію
                            textBox4.Text = (a.Value(x)).ToString(); //Значення функції
                            
                            for (int i = 0; i < count; i++)
                            {
                                // Вычисляем значение X
                                xz[i] = xmin + i;
                                // Вычисляем значение функций в точке X
                                yz[i] = a1 * xz[i] + b1;
                            } 
                            break;
                        }
                    case 1:
                        {
                            Ellipse b = new Ellipse(a1, b1); //Оголошуємо еліпс
                            double m=b.Value(x);
                            if (m != -989898)
                            {
                                textBox4.Text = (b.Value(x)).ToString();//Значення функції
                                for (int i = 0; i < count; i++)
                                {
                                    // Обчислюемо значення X
                                    xz[i] = xmin + i;
                                    // Обчислюемо значення функції в точці X
                                    yz[i] = Math.Pow(-((((xz[i] * xz[i]) / (a1 * a1)) - 1) * (b1 * b1)), 0.5);
                                }
                            }
                            else
                            {//Якщо корінь менше 0
                                ok = false;
                                MessageBox.Show("Функція в цій точці не існує");
                            }
                            break;
                        }
                    case 2:
                        {
                            Hyperbole c = new Hyperbole(a1, b1); //Оголошуємо гіперболу
                            double m = c.Value(x);
                            if (m != -989898)
                            {
                                textBox4.Text = (c.Value(x)).ToString();//Значення функції
                                for (int i = 0; i < count; i++)
                                {
                                    // Обчислюемо значення X
                                    xz[i] = xmin + i;
                                    // Обчислюемо значення функції в точці X
                                    yz[i] = Math.Pow(((((xz[i] * xz[i]) / (a1 * a1)) - 1) * (b1 * b1)), 0.5);
                                }
                            }
                            else
                            {//Якщо корінь менше 0
                                ok =false;
                                MessageBox.Show("Функція в цій точці не існує");
                            }
                            break;
                        }
                    
                }
                if (ok)
                {//Задаємо ппараметри графіку
                    chart1.ChartAreas[0].AxisX.Minimum = x-2;
                    chart1.ChartAreas[0].AxisX.Maximum = x+2;
                    chart1.ChartAreas[0].AxisY.Minimum = Double.Parse(textBox4.Text) - 2;
                    chart1.ChartAreas[0].AxisY.Maximum = Double.Parse(textBox4.Text) + 2;
                    double y = Double.Parse(textBox4.Text);
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = x; //Інтервал у
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Double.Parse(textBox4.Text); //Інтервал х
                    chart1.Series[0].Points.DataBindXY(xz, yz); //Задаємо точки
                    chart1.Series[0].Points[3].Color = Color.Red;                  
                }
            }
            else MessageBox.Show("Введіть аргумент і коефіцієнти");
        }

    }
    public abstract class Function//Загальний австрактний клас функцій
    {
        public abstract string Name();
        public abstract double Value(double x);
    }
    public class Line : Function//Клас лінійної функції
    {
        public Line(double a, double b)
        {
            this._a = a;
            this._b = b;

        }

        public override string Name()
        {
            return "Line";
        }

        public override double Value(double x)
        {     
            return _a * x + _b;
        }
       

        //Змінні класу
        private double _a;
        private double _b;

        
    }
    public class Ellipse : Function//Клас еліпсу
    {
        public Ellipse(double a, double b)
        {
            this._a = a;
            this._b = b;
        }

        public override string Name()
        {
            return "Ellipse";
        }

        public override double Value(double x)
        {//Перевіряє чи більший корінь за 0, якщо ні то повертаємо фіксоване значення
            if (-((((x * x) / (_a * _a)) - 1) * (_b * _b)) > 0) return Math.Pow(-((((x * x) / (_a * _a)) - 1) * (_b * _b)), 0.5);
            else return -989898;
        }
        //Змінні класу
        private double _a;
        private double _b;
    }
    public class Hyperbole : Function//Клас гіперболи
    {
        public Hyperbole(double a, double b)
        {
            this._a = a;
            this._b = b;
        }

        public override string Name()
        {
            return "Hyperbole";
        }

        public override double Value(double x)
        {//Перевіряє чи більший корінь за 0, якщо ні то повертаємо фіксоване значення
            if (((((x * x) / (_a * _a)) - 1) * (_b * _b)) > 0) return Math.Pow(((((x * x) / (_a * _a)) - 1) * (_b * _b)), 0.5);
            else return -989898;
        }
        //Змінні класу
        private double _a;
        private double _b;
    }


}

