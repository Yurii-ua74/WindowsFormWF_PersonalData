using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WF_PersonalData
{
    public partial class Form1 : Form
    {
        int k = 0;
        string path = "text.txt";
        PersonalData pdt = new PersonalData();
        List<PersonalData> pdtList = new List<PersonalData>();
        public Form1()
        {
            InitializeComponent();
            button4.Visible = false;
            pdtList = new List<PersonalData>
            {
                 new PersonalData
                 {
                     Name = "Василь",
                     Surname = "Василько",
                     Phonenumber = "0676465555",
                     E_mail = "vasilii@i.ua"
                 },

                 new PersonalData
                 {
                     Name = "Добриня",
                     Surname = "Дробня",
                     Phonenumber = "0973331111",
                     E_mail = "drobnia@3g.ua"  
                 },

                 new PersonalData
                 {
                     Name = "Назар",
                     Surname = "Носичук",
                     Phonenumber = "0635559999",
                     E_mail = "nosichuk@gmail.com"
                 }
            };
            listBox1.Visible = false;            
            if (k < 100) ChangeImage();
        }
       
        async public void ChangeImage()
        {           
            do
            {
                k++;
                switch (k)
                {
                    case 2: { pictureBox1.Image = Image.FromFile("PP2.png"); await Task.Delay(100); break; }
                    case 3: { pictureBox1.Image = Image.FromFile("PP3.png"); await Task.Delay(100); break; }
                    case 4: { pictureBox1.Image = Image.FromFile("PP4.png"); await Task.Delay(100); break; }
                    case 1: { pictureBox1.Image = Image.FromFile("PP1.png"); await Task.Delay(100); break; }
                    case 5: { pictureBox1.Image = Image.FromFile("PP5.png"); await Task.Delay(100); break; }
                    case 6: { pictureBox1.Image = Image.FromFile("PP6.png"); await Task.Delay(100); break; }
                    case 7: { pictureBox1.Image = Image.FromFile("PP7.png"); await Task.Delay(100); break; }
                    case 8: { pictureBox1.Image = Image.FromFile("PP8.png"); await Task.Delay(100); break; }
                    case 9: { pictureBox1.Image = Image.FromFile("PP9.png"); await Task.Delay(100); break; }
                    default: { k = 0; break; }
                }
            } while (k < 100);                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (k >= 100) k = 0;
            if(k < 100 && k != 0) k = 100;
            switch(k)
            {
                case 0:
                    {
                        button1.BackColor = Color.Red;
                        button1.Text = "STOP"; 
                        ChangeImage(); break;
                    }            
                case 100:
                    {                       
                        button1.BackColor = Color.LimeGreen;
                        button1.Text = "START"; break;                      
                    }
            }
        }
        
        async private void button2_Click(object sender, EventArgs e)
        {
            int x, y;
            listBox1.Visible = true;
            listBox1.Width = 1;
            listBox1.Height = 1;            
            
            for (x = 0; x < 400; x+=5, await Task.Delay(5))
            {
                listBox1.Width = x;
                listBox1.Height = 400;
            }
            if (maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && maskedTextBox3.Text != "" && maskedTextBox4.Text != "")
            {
                string num = "";
                string mail = "";
                if (pdtList.Count != 0)
                {
                    string pat = @"\D"; // хочу прибрати дужки в номері телефона, бо просто хочу десь регулярні вирази втикнути
                    string targ = "";
                    Regex reg = new Regex(pat);
                    num = reg.Replace(maskedTextBox3.Text, targ); // здається прибрав

                    foreach (PersonalData ph in pdtList)     // хочу перевірити чи є ще ......
                    {                        
                        if (ph.Phonenumber == num)       // такий номер телефону
                        {
                            MessageBox.Show($"Такий номер телефону {maskedTextBox3.Text} вже існує!", "ПОМИЛКА!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                              maskedTextBox3.Text = ""; num = "";
                        }
                        if (maskedTextBox4.Text != "")
                        {
                            string patt = @"^\w+@\w+\.\w+$";      // простенький формат ввода e-mail 
                            mail = maskedTextBox4.Text;
                            if (!Regex.IsMatch(mail, patt))       // перевірив
                            {
                                MessageBox.Show($"Невірний формат ввода e-mail (символи/@/символи/./символи)", "ПОМИЛКА!!!", MessageBoxButtons.OK, MessageBoxIcon.Error); // упс 
                                maskedTextBox4.Text = "";
                            }
                            if (ph.E_mail == maskedTextBox4.Text)            // чи є таке таке мило
                            {
                                MessageBox.Show($"Такий e_mail {maskedTextBox4.Text} вже існує!", "ПОМИЛКА!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                maskedTextBox4.Text = "";
                            }
                        }
                    }
                }
                if (maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && num != "" && maskedTextBox4.Text != "")
                {
                    pdtList.Add(new PersonalData            // якщо немає повтору занести дані до колекції List
                    {
                        Name = maskedTextBox1.Text,
                        Surname = maskedTextBox2.Text,
                        Phonenumber = num,
                        E_mail = maskedTextBox4.Text,
                    });
                    maskedTextBox1.Text = ""; maskedTextBox2.Text = ""; maskedTextBox3.Text = ""; maskedTextBox4.Text = "";
                }
            }                      
            listBox1.Items.Clear();
            for (int i = 0; i < pdtList.Count; i++)
            {
                listBox1.Items.Add($"# {i+1}");
                listBox1.Items.Add($"Им'я: {pdtList[i].Name}");
                listBox1.Items.Add($"Прізвище: {pdtList[i].Surname}");
                listBox1.Items.Add($"Номер телефону: {pdtList[i].Phonenumber}");
                listBox1.Items.Add($"e-mail: {pdtList[i].E_mail}");
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int xleft = 0, xright = 0;    // індексація колекції
            for (; xleft < 1000; xleft += 5)
            {
                xright = xleft + 5;
                int select = listBox1.SelectedIndex;
                if (select >= xleft && select < xright)
                    break;  
            }
            DialogResult res = MessageBox.Show($"Видалити дані за номером списку {(xleft / 5 + 1).ToString()}?", "ПІДТВЕРДЖЕННЯ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                pdtList.Remove(pdtList[xleft / 5]); // видалив з ліста, потім перезапише сам в listBox
            else
            {
                DialogResult res1 = MessageBox.Show($"А!!! То Ви хочете відредагувати дані за номером {(xleft / 5 + 1).ToString()}?", "ПІДТВЕРДЖЕННЯ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res1 == DialogResult.Yes) 
                {                   
                    maskedTextBox1.Text = pdtList[xleft / 5].Name;//listBox1.Items[xleft / 5 + 1].ToString();
                    maskedTextBox2.Text = pdtList[xleft / 5].Surname;//listBox1.Items[xleft / 5 + 2].ToString();
                    maskedTextBox3.Text = pdtList[xleft / 5].Phonenumber;//listBox1.Items[xleft / 5 + 3].ToString();
                    maskedTextBox4.Text = pdtList[xleft / 5].E_mail;//listBox1.Items[xleft / 5 + 4].ToString();
                    pdtList.Remove(pdtList[xleft / 5]);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter stream = new StreamWriter(file))
                {
                    foreach (var st in pdtList)                    
                        stream.WriteLine($"{st.Name} {st.Surname} {st.Phonenumber} {st.E_mail}");                                                                                              
                          MessageBox.Show("Done!!!");
                }
            }
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = "";
            listBox1.Items.Clear();
            listBox1.Items.Add("Вміст файлу: ");
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader stream = new StreamReader(file))
                {
                    do
                    {
                        text = stream.ReadLine();
                        if(text != null)
                        listBox1.Items.Add(text);
                    } while (text != null);
                }                
            }
            MessageBox.Show("Done!!!");
            
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}                     