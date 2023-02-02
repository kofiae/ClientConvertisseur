using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        /// <summary>
        /// Test constructeur.
        /// </summary>
        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);
        }

        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            ObservableCollection<Devise> devises= new ObservableCollection<Devise>();
         
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));

            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);
            CollectionAssert.AreEqual(devises, convertisseurEuro.Devises, "Erreur liste devises");
        }
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_NonOK_WSNonDemarre()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();

            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            //Assert
            Assert.IsNull(convertisseurEuro.Devises);
        }
        /// <summary>
        /// Test conversion OK
        /// </summary>
        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            convertisseurEuro.MontantEuros = 100;
            Devise d = new Devise(2, "Franc Suisse", 1.07);
            convertisseurEuro.SelectedDevise = d;

            //Act
            convertisseurEuro.ActionSetConversion();

            //Assert
            //Assertion : MontantDevise est égal à la valeur espérée 107
            Assert.AreEqual(107, convertisseurEuro.MontantEnDevise, "La  valeur en devises n'est pas correcte");
        }

        
    }
}