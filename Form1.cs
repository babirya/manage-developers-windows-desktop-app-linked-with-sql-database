using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace developers
{
    public partial class Form1 : Form
    { 
        // 1 calling ado class 

        ado d = new ado(); 
        
        // 2 methode remplir dataGridView  
        public void remplir ()
        {  
            // this  one for eviter repitation 
            if(d.dt.Rows.Count!=null)
            {
                d.dt.Rows.Clear(); 
            }

            d.conecter();
            d.cmd.CommandText = "select * from developers ";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            dataGridView1.DataSource = d.dt;
            d.dr.Close(); 


        } 

        // 3 methode vider 
        public void vider (Control f)
        {
              foreach(Control ct in f.Controls)
              {
                  if(ct.GetType()==typeof(TextBox))
                  {
                      ct.Text = ""; 
                  } 
                  
                  // this for sous controls

                    if(ct.Controls.Count!=0)
                    {
                        vider(ct); 
                    }
              }
            

        } 
        
        // 4 methode number 
        public int number ()
        {
            d.conecter();
            d.cmd.CommandText = "select count (id) from developers where id ='"+textBox1.Text+"'";
            d.cmd.Connection = d.con;
            int cpt;  
            cpt= (int) d.cmd.ExecuteScalar();
            return cpt;
        }  
        // 5 methode for ajouter  
        public bool ajouter()
         {
             if (number() == 0)
             {
                 d.cmd.CommandText = "insert into developers values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text +"') ";

                 d.cmd.Connection = d.con;
                 d.cmd.ExecuteNonQuery();
                 
                 
                 return true; 
             }
             return false; 

          }
        // 6 methode for suprimrer  
        public bool modifier()
        {
             if(number()!=0)
             {
                 if(MessageBox.Show("vuez modifier ce developer ","confirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
                 {
                     d.cmd.CommandText = "update developers set name='" + textBox2.Text + "',mission= '" + textBox3.Text + "', age='" + textBox4.Text + "' where id='" + textBox1.Text + "'  ";
                     d.cmd.Connection = d.con;
                     d.cmd.ExecuteNonQuery();
                     return true;
                     
                 }
             }
             return false; 
        }

        public bool suprimmer()
        {
            if (number() != 0)
            { 
                if(MessageBox.Show("vouez suprimer ce developer ?","comfirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {

               
                d.cmd.CommandText = " delete from developers where id = '"+textBox1.Text+"'";

                d.cmd.Connection = d.con;
                d.cmd.ExecuteNonQuery();


                return true; 
                }
            }
            return false;

        }

        // 7 methode for modifier

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            remplir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("vous avez quitter","comfirmation",MessageBoxButtons.YesNo)==DialogResult.Yes) 
            { 
              d.deconecter();
              this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("vous avez vider ", "comfirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                vider(this); 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            // pour ajouter 
            //1 we must make function number with id 
            // 2 control de saisie here 
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
             {
                 MessageBox.Show("s'ill vous plait remplir everything");
                 return; 
             } 
            if(ajouter()==true)
            {
                MessageBox.Show("develope ajouter avec success");
                remplir(); 
            }else
            {
                MessageBox.Show("ce developer est deja exist  ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            // for supprimer 
           if(textBox1.Text=="")
           {
               MessageBox.Show("s'ill vous plait enter id ");
               return; 
           } 

            if(suprimmer()==true)
            {

                MessageBox.Show("DEVELOPER SUPPRIMER AVEC SUCCESS");
                remplir();
            }
            else
            {
                MessageBox.Show("cette developers ne pas exist ");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            // for modifier 
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("s'ill vous plait remplir toutes les champs");
                return;
            }
            if (modifier() == true)
            {
                MessageBox.Show("developer modifier avec success");
                remplir();
            }
            else
            {
                MessageBox.Show("ce developer ne existe pas");
            }
        }
    }
}
