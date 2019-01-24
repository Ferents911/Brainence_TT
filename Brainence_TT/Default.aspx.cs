using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;

namespace Brainence_TT
{
    public partial class _Default : Page
    {
        SqlConnection sqlConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("~/App_Data/") + FileUpload1.FileName);      //зберігаємо файл в локальну папку проекту
                string path = Server.MapPath("~/App_Data/") + FileUpload1.FileName;             // генеруємо новий шлях до файлу
                string text = File.ReadAllText(path);                       //зчитуємо увесь текст з файлу в змінну
                string[] split_text = text.Split(new char[] { '.' });       // сплітимо текст , розділяючи його на речення (місце від крапки до крапки)
                if (StringHelper.CorrectString(search_box.Text))           // перевірка на пробіли
                {
                    foreach (string s in split_text)    // починаємо перебирати речення 
                    {
                        string r = s;   //так як s - коефіцієнт ітерації, і доступ до його використання обмежений , то створюємо нову змінну
                        r += ".";       // спліт видаляє символ , яким розділяє, тому кожному реченню потрібно додати крапку.
                        if ((r.ToLower().Contains((search_box.Text).ToLower() + " ")) ||
                           (r.ToLower().Contains((search_box.Text).ToLower() + ".")) ||
                           (r.ToLower().Contains((search_box.Text).ToLower() + ",")) ||   // перевірка на те , що знайдений текст - слово
                           (r.ToLower().Contains((search_box.Text).ToLower() + "?")) ||   // оскільки після слова заважди йдуть символи, які є в перевірці
                           (r.ToLower().Contains((search_box.Text).ToLower() + "!")) ||   // також робимо шуканий та текст у реченнях нижнього регістру
                           (r.ToLower().Contains((search_box.Text).ToLower() + "...")))   // це дозволить уникнути конфлікту регістрів і зберегти сенс слова  
                        {
                            DBrecording(r);
                            Label1.Text += r + "<br/>";
                        }
                    }
                }
                else
                {
                    Label1.Text = "Your request is empty, please try again<br/>";
                }
                File.Delete(path);
            }
        }

        public void DBrecording(string data)
        {
            
            
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);

             sqlConnection.Open(); // відкриваємо доступ та з*єднання з БД

            //SqlDataReader Sqlreader = null;
            SqlCommand command = new SqlCommand("INSERT INTO [Sentences_table] (Sentences)VALUES(@Sentences)", sqlConnection);
            command.Parameters.AddWithValue("@Sentences", StringHelper.ReverseString(data));   
   
            command.ExecuteNonQuery(); 
        }

        static class StringHelper
        {
            public static string ReverseString(string s)
            {
                char[] arr = s.ToCharArray();
                Array.Reverse(arr);
                return new string(arr);
            }

            public static bool CorrectString(string b)
            {
                bool chk;
                if ((string.IsNullOrEmpty(b)) || (string.IsNullOrWhiteSpace(b)))
                {
                    chk = false;
                }
                else
                {
                    chk = true;
                }
                return chk;
            }

        }
    }
}