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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ExaEstructuras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Arbol arbol1 = new Arbol();
   
        public MainWindow()
        {
            InitializeComponent();
            SetTree();
        }

      
        private void SetTree()
        {
            Nodo nodo1 = new Nodo(15,"CN 217","Edificio 1",true);
            Nodo nodo2 = new Nodo(25,"SL 125","Edificio 2",false);
            Nodo nodo3 = new Nodo(10,"CN 108","Edificio 1",true);
            Nodo nodo4 = new Nodo(40,"HU 203","Edificio 3",true);
            Nodo nodo5 = new Nodo(20,"SL 220","Edificio 2",false);


            List<Nodo> list = new List<Nodo>();

            list.Add(nodo1);
            list.Add(nodo2);
            list.Add(nodo3);
            list.Add(nodo4);
            list.Add(nodo5);


            for (int i = 0; i < list.Count; i++)
            {
                arbol1.Root = arbol1.Insert(arbol1.Root, list[i]);
                Console.WriteLine(arbol1.Root.Data);
            }
        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {

            TexBlockMessages.Text = String.Empty;
            if (CantidadTextBox.Text == "" || SalonIDTextBox.Text == "" || FacultadIDTextBox.Text == "")
            {
                TexBlockMessages.Text = "No valid input";
            }
            else
            {
                int.TryParse(CantidadTextBox.Text, out int nCantidad);

                Boolean proyector = false;

                if(ProyectorCheck.IsChecked == true)
                {
                    proyector = true;
                }

                Nodo nodo = new Nodo(nCantidad, SalonIDTextBox.Text, FacultadIDTextBox.Text, proyector);

                arbol1.Root = arbol1.Insert(arbol1.Root, nodo);


                CantidadTextBox.Clear();
                SalonIDTextBox.Clear();
                FacultadIDTextBox.Clear();
                TexBlockMessages.Text = "Done";
            }

            
        }

        private void CantidadTextBoxPrev(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int result);
        }


        //BUSCAR button
        private void InsertarButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            TexBlockMessages.Text = String.Empty;
           
            ListBox.Items.Clear();
            int.TryParse(CantidadBuscarTxtBox.Text, out int cantidad);

            int nodoCapacidad = arbol1.encontrarValor(arbol1.Root, cantidad);
            
            Nodo nodo = arbol1.Search(nodoCapacidad);

            if (nodo != null)
            {
                // nodo encontrado

                ListBox.Items.Add(nodo);
            }
            else
            {
                // nodo no encontrado
                TexBlockMessages.Text = "*No hay xd";
            }

            CantidadBuscarTxtBox.Clear();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            TexBlockMessages.Text = String.Empty;
            Nodo selectedNode = (Nodo)ListBox.SelectedItem;

            if (selectedNode == null)
            {
                TexBlockMessages.Text = $"No salon selected";
            }
            else
            {
                TexBlockMessages.Text = $"Salon {selectedNode.AulaID} ha sido asignado";

                arbol1.Root = arbol1.Delete(arbol1.Root, selectedNode.Data);

                ListBox.Items.Clear();
            }

        }

        private void ProyectorRadioButtonYES_Checked(object sender, RoutedEventArgs e)
        {
       
        }

        private void MostrarTodoButton_Click(object sender, RoutedEventArgs e)
        {
            TexBlockMessages.Text = String.Empty;
            ListBox.Items.Clear();

            List<Nodo> nodos = new List<Nodo>();

            nodos = arbol1.getNodos(arbol1.Root);

            foreach(Nodo nodo in nodos)
            {
                ListBox.Items.Add(nodo);
            }


        }

        private void ProyectorCheck_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
