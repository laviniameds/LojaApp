using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LojaApp
{
    /// <summary>
    /// Interaction logic for WndCadFabricante.xaml
    /// </summary>
    public partial class WndCadFabricante : Window
    {
        public WndCadFabricante()
        {
            InitializeComponent();
        }

        private LojaDataContext dc = new LojaDataContext();

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var r = from f in dc.Fabricantes select new { f.Id, f.Descricao };
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = r.ToList();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Fabricante f = new Fabricante();
            f.Id = int.Parse(txtId.Text);
            f.Descricao = txtDes.Text;
            dc.Fabricantes.InsertOnSubmit(f);
            dc.SubmitChanges();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Fabricante r = (from f in dc.Fabricantes where f.Id == int.Parse(txtId.Text)select f).Single();
            r.Descricao = txtDes.Text;
            dc.SubmitChanges();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Fabricante r = (from f in dc.Fabricantes where f.Id == int.Parse(txtId.Text) select f).Single();
            dc.Fabricantes.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }
    }
}
