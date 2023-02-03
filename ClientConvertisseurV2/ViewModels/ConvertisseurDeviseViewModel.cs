using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurDeviseViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        #region properties
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        public IRelayCommand BtnSetConversion { get; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        #endregion

        public ConvertisseurDeviseViewModel()
        {
            GetDataOnLoadAsync();

            //Boutons
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

        public async void GetDataOnLoadAsync()
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
            erreurDevise.XamlRoot = App.MainRoot.XamlRoot;
            ContentDialogResult result = await erreurDevise.ShowAsync();
        }

        public void ActionSetConversion()
        {
            if (this.SelectedDevise == null)
            {
                DisplayDialog("Erreur", "Vous devez selectionner une devise !");
            }
            else
                this.MontantEnDevise = Math.Round(this.montantEuros / this.SelectedDevise.Taux, 2);
        }
    }
}
