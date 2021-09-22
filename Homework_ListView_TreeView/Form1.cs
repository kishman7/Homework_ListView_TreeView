using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_ListView_TreeView
{
    public partial class Form1 : Form
    {
        //ListView table;

        public Form1()
        {
            InitializeComponent();

            //var viewingMode = new [] ListView;
            comboBox1.Items.Add(View.Details);
            comboBox1.Items.Add(View.LargeIcon);
            comboBox1.Items.Add(View.List);
            comboBox1.Items.Add(View.SmallIcon);
            comboBox1.Items.Add(View.Details);
            comboBox1.Items.Add(View.Tile);

            //listView1.View = (View)comboBox1.SelectedItem; //виводимо в певному вигляді
            
            listView1.View = View.Tile; //виводимо в певному вигляді

            //Підключаємо в listView1 папку FolderBrowserDialog
            FolderBrowserDialog folderPicker = new FolderBrowserDialog();
            if (folderPicker.ShowDialog() == DialogResult.OK)
            {
                GetOpenPath(folderPicker.SelectedPath); //вказуємо шлях до вибраного елемента
            }

            treeView1.LabelEdit = true; // Дозволяємо редагування вузлів
        }
        void GetOpenPath (string path) //метод, який вертає шлях до файлу/папки
        {
            listView1.Items.Clear();
            var files = new DirectoryInfo(path);
            foreach (var file in files.GetDirectories()) //перевіряємо чи папапка і додаємо її до списку
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                ListViewItem item = new ListViewItem(fileName);
                item.Tag = file.FullName;
                listView1.Items.Add(item); //додаємо папки в список
                item.ImageIndex = 1; //додаємо іконку папки
            }
            foreach (var file in files.GetFiles()) //перевіряємо чи файл і додаємо його до списку
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                ListViewItem item = new ListViewItem(fileName);
                item.Tag = file.FullName;
                listView1.Items.Add(item); //додаємо файли в список
                item.ImageIndex = 0;
                item.SubItems.Add((file.Length / 1024).ToString() + " Kb"); // додаємо розмір файла
                item.SubItems.Add(file.Extension); //додаємо іконку файла

            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            GetOpenPath(listView1.SelectedItems[0].Tag.ToString());
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) //обробляємо видалення елементу
        {
            var delete = "";
            foreach (var item in listView1.SelectedItems)
            {
                delete = (item as ListViewItem).Tag.ToString();
                var result = MessageBox.Show(delete, "Видалити?", // підтвердження видалення через MessageBox
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (Directory.Exists(delete)) //перевіряємо чи папка
                    {
                        try
                        {
                            Directory.Delete(delete, true); // видаляємо папку
                            listView1.Items.Remove(item as ListViewItem); // видаляємо її з listView1
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else if (File.Exists(delete)) //перевіряємо чи файл
                    {
                        try
                        {
                            File.Delete(delete); //видаляємо файл
                            listView1.Items.Remove(item as ListViewItem); //видаляємо його з listView1
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ListViewItem item = listView1.SelectedItems[0];
            //item.SubItems[0].Text = 
        }
    }
}
