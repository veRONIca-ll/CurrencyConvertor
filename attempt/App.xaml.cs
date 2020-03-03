//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Xml;

//namespace attempt
//{
//    /// <summary>
//    /// Логика взаимодействия для App.xaml
//    /// </summary>
//    public partial class App : Application
//    {
//        public string radioCur;
//        public string radioCurInv;

//        private void calcButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (this.inputData.Text == "")
//            {
//                this.resultOut.Text = "0";
//            }
//            else if (this.inputData.Text != "")
//            {
//                double curValue = Currencies(radioCur);
//                try
//                {
//                    double result = Convert.ToDouble(this.inputData.Text) / curValue;
//                    if (this.resultOut.Text == "")
//                    {
//                        this.resultOut.Text = Convert.ToString(result);
//                    }
//                }
//                catch (Exception)
//                {

//                    MessageBox.Show("wrong input in RUB!", "error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }


//            if (Convert.ToString(this.inputDataInv.Text) != "")
//            {
//                double curValue2 = Currencies(radioCurInv);
//                try
//                {
//                    double result2 = Convert.ToDouble(this.inputDataInv.Text) * curValue2;
//                    if (this.resultOutINv.Text == "")
//                    {
//                        this.resultOutINv.Text = Convert.ToString(result2);
//                    }
//                }
//                catch (Exception)
//                {
//                    MessageBox.Show("wrong input in USD/EUR!", "error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }


//        }

//        private void RadioButton_Checked(object sender, RoutedEventArgs e)
//        {
//            RadioButton pressed = (RadioButton)sender;
//            radioCur = pressed.Content.ToString();
//            //if (radioCur == "")
//            //{
//            //    MessageBox.Show("choose currency!", "error", MessageBoxButton.OK, MessageBoxImage.Error);
//            //}
//        }

//        public double Currencies(string curName)
//        {
//            XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp");
//            string USDxml = "";
//            string EURxml = "";

//            while (reader.Read())
//            {
//                switch (reader.NodeType)
//                {
//                    case XmlNodeType.Element:
//                        if (reader.Name == "Valute")
//                        {
//                            if (reader.HasAttributes)
//                            {
//                                while (reader.MoveToNextAttribute())
//                                {
//                                    if (reader.Name == "ID")
//                                    {
//                                        if (reader.Value == "R01235")
//                                        {
//                                            reader.MoveToElement();
//                                            USDxml = reader.ReadOuterXml();
//                                        }
//                                    }

//                                    if (reader.Name == "ID")
//                                    {
//                                        if (reader.Value == "R01239")
//                                        {
//                                            reader.MoveToElement();
//                                            EURxml = reader.ReadOuterXml();
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        break;
//                }
//            }

//            XmlDocument usdDoc = new XmlDocument();
//            usdDoc.LoadXml(USDxml);

//            XmlDocument eurDoc = new XmlDocument();
//            eurDoc.LoadXml(EURxml);

//            XmlNode xmlNode = usdDoc.SelectSingleNode("Valute/Value");
//            double usdValue = Convert.ToDouble(xmlNode.InnerText);

//            xmlNode = eurDoc.SelectSingleNode("Valute/Value");
//            double eurValue = Convert.ToDouble(xmlNode.InnerText);

//            if (curName == "USD")
//            {
//                return usdValue;
//            }
//            else
//            {
//                return eurValue;
//            }
//        }

//        private void resetButton_Click(object sender, RoutedEventArgs e)
//        {
//            this.resultOut.Text = this.inputData.Text = this.resultOutINv.Text = this.inputDataInv.Text = "";
//            this.usdBox.IsChecked = this.eurBox.IsChecked = this.usdBoxInv.IsChecked = this.eurBoxInv.IsChecked = false;

//        }

//        private void usdBoxInv_Checked(object sender, RoutedEventArgs e)
//        {
//            RadioButton pressed2 = (RadioButton)sender;
//            radioCurInv = pressed2.Content.ToString();

            
//        }


//    }
//}

    
