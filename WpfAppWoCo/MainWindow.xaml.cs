using System.IO;
using Microsoft.Win32;
using System.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Globalization;

namespace WpfAppWoCo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Repository _repository;

    public MainWindow()
    {
        InitializeComponent();

        InitializeSettings();

        _repository = new Repository(); // Initialize the repository
        DataContext = this;
    }

    private static void InitializeSettings()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
    }

    private void XmlFileButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*",
            Title = "Open XML File"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string fileName = openFileDialog.FileName;
            FileNameLabel.Visibility = Visibility.Visible;
            FileNameLabel.Content = $"Bestand: {Path.GetFileName(fileName)}";
            DetailPanel.Visibility = Visibility.Visible;

            _repository.LoadFromXml(fileName);

            LocationLabel.Content = $"Locatie: {_repository.Location.Name}";
            LocationLabel.Visibility = Visibility.Visible;

            SpaceDataGrid.ItemsSource = _repository.GetAllSummerized();
        }
    }

    private void SpaceDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        var selected = SpaceDataGrid.SelectedItem as SpaceViewModel;

        if (selected != null)
        {
            ValideerButton.Visibility = Visibility.Visible;
            ToonModelButton.Visibility = Visibility.Visible;

            NameTextBlock.Text = selected.Name;
            SurfaceTextBlock.Text = selected.SurfaceType;
            AreaTextBlock.Text = selected.Area.ToString();
            VolumeTextBlock.Text = selected.Volume.ToString();
            PointsDataGrid.ItemsSource = selected.CartesianPoints?.Select(c => new { X = c.X, Y = c.Y, Z = c.Z });

        }
    }

    private void ValideerButton_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Het valideren van het volume en de oppervlakte is nog niet gemaakt. Ik kon geen geschikte library vinden, die dat kon doen", 
            "Valideren", 
            MessageBoxButton.OK, 
            MessageBoxImage.Information);
    }

    private void ToonModelButton_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Het tonen van het model is nog niet gemaakt. Ik kon geen geschikte library vinden, die dat kon doen",
            "Model tonen",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}