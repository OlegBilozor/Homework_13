using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Homework_13
{
    public class SaveToXmlCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            bool flag = parameter is ItemCollection collec  && collec.Count>0;
            return flag;
        }

        public void Execute(object parameter)
        {
            var collection = parameter as ItemCollection;
            var fs = new FileStream("books.xml", FileMode.Create, FileAccess.Write);
            var xs = new XmlSerializer(typeof(List<Book>));
            List<Book> books = new List<Book>();
            foreach (Book o in collection)
            {
                books.Add(o);
            }
            xs.Serialize(fs, books);
            fs.Close();
            MessageBox.Show("GridView was saved to XML");
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
