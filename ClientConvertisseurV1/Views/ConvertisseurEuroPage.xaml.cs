// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;      
        
        private double montantEuros;
        private ObservableCollection<Devise> devises;
        private Devise selectedDevise;
        private double montantEnDevise;

        public double MontantEuros
        {
            get
            {
                return montantEuros;
            }

            set
            {
                montantEuros = value;
            }
        }

        public ObservableCollection<Devise> Devises
        {
            get
            {
                return devises;
            }

            set
            {
                devises = value;
                OnPropertyChanged(nameof(Devises));
            }
        }


        public Devise SelectedDevise
        {
            get
            {
                return selectedDevise;
            }

            set
            {
                selectedDevise = value;
            }
        }

        public double MontantEnDevise
        {
            get
            {
                return montantEnDevise;
            }

            set
            {
                montantEnDevise = value;
                OnPropertyChanged("MontantEnDevise");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44353/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
                DisplayDialog("Erreur", "API non disponible !");
            else
                Devises = new ObservableCollection<Devise>(result);
        }

        private async void DisplayDialog(string title, string content)
        {
            ContentDialog erreurDevise = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            erreurDevise.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await erreurDevise.ShowAsync();
        }


        private void MessageAsync(string v1, string v2)
        {

        }

        private void convertir(object sender, RoutedEventArgs e)
        {
            if (this.SelectedDevise == null) 
            {
                DisplayDialog("Erreur", "Vous devez selectionner une devise !");
            }
            else
                this.MontantEnDevise = Math.Round(this.montantEuros * this.SelectedDevise.Taux, 2);
        }
    }
}
