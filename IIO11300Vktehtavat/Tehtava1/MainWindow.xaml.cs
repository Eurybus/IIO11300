/*
* Copyright (C) JAMK/IT/Esa Salmikangas
* This file is part of the IIO11300 course project.
* Created: 12.1.2016 Modified: 13.1.2016
* Authors: Atro Lähdemäki, Esa Salmikangas
asd
*/
using System;

using System.Windows;

namespace Tehtava1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            try
            {
                double resultPer, resultArea, resultBzlArea;

                double width = double.Parse(txtWidht.Text);
                double height = double.Parse(txtHeight.Text);
                double bzlWidth = double.Parse(txtBzlWidth.Text);

                resultBzlArea = BusinessLogicWindow.BezelCalculateArea(bzlWidth, width, height);
                resultPer = BusinessLogicWindow.CalculatePerimeter(width, height);
                resultArea = BusinessLogicWindow.CalculateArea(width, height) - resultBzlArea;
                

                txtArea.Text = resultArea.ToString();
                txtPer.Text = resultPer.ToString();
                txtBzlArea.Text = resultBzlArea.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //yield to an user that everything okay
            }
        }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
            Environment.Exit(0);
    }

        private void btnAreaOO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Lasketaan Pinta-Ala Ikkuna-Olion avulla
                //Luodaan luokasta olio
                JAMK.IT.IIO11300.Ikkuna Ikk = new JAMK.IT.IIO11300.Ikkuna();
                Ikk.Korkeus = double.Parse(txtHeight.Text);
                Ikk.Leveys = double.Parse(txtWidht.Text);
                //tulos käyttäjälle
                //VE metodilla
                //MessageBox.Show(Ikk.LaskePintaAla().ToString());
                //Ve property
                MessageBox.Show(Ikk.PintaAla.ToString());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }

    public class BusinessLogicWindow
    {
    /// <summary>
    /// CalculatePerimeter calculates the perimeter of a window
    /// </summary>
    public static double CalculatePerimeter(double width, double height)
        {
            return width * 2 + height * 2;
        }
        /// <summary>
        /// CalculateArea calculates the area of a window
        /// </summary>
        public static double CalculateArea(double width, double height)
        {
            return width * height;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bzlwidth">Width of the bezel</param>
        /// <param name="xLength">Length of the horizontal bezel</param>
        /// <param name="yLength">Length of the vertical bezel</param>
        /// <returns></returns>
        public static double BezelCalculateArea(double bzlwidth, double xLength, double yLength)
        {
            return ((bzlwidth * xLength)*2)+((bzlwidth*yLength)*2);
        }
    }
    
}
