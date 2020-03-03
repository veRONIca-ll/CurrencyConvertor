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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;


namespace attempt
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.todayEUR.Text = Convert.ToString(Math.Round(Currencies("EUR"), 3));
            this.todayUSD.Text = Convert.ToString(Math.Round(Currencies("USD"), 3));
            this.usdBox.IsChecked = true;
            this.usdBoxInv.IsChecked = true;
        }

        public  string radioCur;
        public string radioCurInv;
        

        private void calcButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.inputData.Text =="")
            {
                this.resultOut.Text = "";
            }
            else if (this.inputData.Text != "")
            {
                double curValue = Currencies(radioCur);
                try
                {
                    double result = Math.Round(Convert.ToDouble(this.inputData.Text) / curValue,3);
                    if (this.resultOut.Text == "")
                    {
                        this.resultOut.Text = Convert.ToString(result);
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("wrong input in RUB!", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            

            if (Convert.ToString(this.inputDataInv.Text) !=  "")
            {
                double curValue2 = Currencies(radioCurInv);
                try
                {
                    double result2 = Math.Round(Convert.ToDouble(this.inputDataInv.Text) * curValue2,3);
                    if (this.resultOutINv.Text == "")
                    {
                        this.resultOutINv.Text = Convert.ToString(result2);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("wrong input in USD/EUR!", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            radioCur = pressed.Content.ToString();
        }

        public double Currencies( string curName)
        {
            XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp");
            string USDxml = "";
            string EURxml = "";

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "Valute")
                        {
                            if (reader.HasAttributes)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "ID")
                                    {
                                        if (reader.Value == "R01235")
                                        {
                                            reader.MoveToElement();
                                            USDxml = reader.ReadOuterXml();
                                        }
                                    }

                                    if (reader.Name == "ID")
                                    {
                                        if (reader.Value == "R01239")
                                        {
                                            reader.MoveToElement();
                                            EURxml = reader.ReadOuterXml();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            XmlDocument usdDoc = new XmlDocument();
            usdDoc.LoadXml(USDxml);

            XmlDocument eurDoc = new XmlDocument();
            eurDoc.LoadXml(EURxml);

            XmlNode xmlNode = usdDoc.SelectSingleNode("Valute/Value");
            double usdValue = Convert.ToDouble(xmlNode.InnerText);

            xmlNode = eurDoc.SelectSingleNode("Valute/Value");
            double eurValue = Convert.ToDouble(xmlNode.InnerText);

            if (curName == "USD")
            {
                return usdValue;
            }
            else
            {
                return eurValue;
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultOut.Text = this.inputData.Text = this.resultOutINv.Text= this.inputDataInv.Text =  "";
            this.usdBox.IsChecked = this.eurBox.IsChecked = this.usdBoxInv.IsChecked = this.eurBoxInv.IsChecked = false;
            this.usdBoxInv.IsChecked = true;
            this.usdBox.IsChecked = true;
        }

        private void usdBoxInv_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed2 = (RadioButton)sender;
            radioCurInv = pressed2.Content.ToString();
        }

        

        
    }
}

