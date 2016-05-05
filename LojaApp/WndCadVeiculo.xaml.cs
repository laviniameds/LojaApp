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
    /// Interaction logic for WndCadVeiculo.xaml
    /// </summary>
    public partial class WndCadVeiculo : Window
    {
        public WndCadVeiculo()
        {
            InitializeComponent();
        }

        private LojaDataContext dc = new LojaDataContext();

        private void selectFabricantes()
        {
            var r = from f in dc.Fabricantes orderby f.Descricao select f;
            cbFab.ItemsSource = r.ToList();
            cbFab.SelectedValuePath = "Id";
            cbFab.DisplayMemberPath = "Descricao";
        }

    private void Window_Activated(object sender, EventArgs e)
        {
            selectFabricantes();
            dpDataCompra.SelectedDate = DateTime.Now;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var r = from v in dc.Veiculos select new
            {
                v.Id,
                v.Modelo,
                v.Ano,
                v.Fabricante.Descricao,
                v.DataCompra,
                v.PrecoVenda
            };
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = r.ToList();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Veiculo v = new Veiculo();
            v.Id = int.Parse(txtId.Text);
            v.Modelo = txtModelo.Text;
            v.Ano = int.Parse(txtAno.Text);
            v.IdFabricante = (int)cbFab.SelectedValue;
            v.DataCompra = dpDataCompra.SelectedDate;
            v.ValorCompra = decimal.Parse(txtValorCompra.Text);
            v.PrecoVenda = decimal.Parse(txtPrecoVenda.Text);
            dc.Veiculos.InsertOnSubmit(v);
            dc.SubmitChanges();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Veiculo r = (from v in dc.Veiculos where v.Id == int.Parse(txtId.Text) select v).Single();
            r.Modelo = txtModelo.Text;
            r.Ano = int.Parse(txtAno.Text);
            r.IdFabricante = (int)cbFab.SelectedValue;
            r.DataCompra = dpDataCompra.SelectedDate;
            r.ValorCompra = decimal.Parse(txtValorCompra.Text);
            r.PrecoVenda = decimal.Parse(txtPrecoVenda.Text);
            dc.SubmitChanges();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Veiculo r = (from v in dc.Veiculos where v.Id == int.Parse(txtId.Text) select v).Single();
            dc.Veiculos.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }
    }
}
