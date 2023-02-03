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
    public class ConvertisseurDeviseViewModelTests
    {
        [TestMethod()]
        public void ConvertisseurDeviseViewModelTest()
        {
            ConvertisseurDeviseViewModel convertisseurDevise = new ConvertisseurDeviseViewModel();
            Assert.IsNotNull(convertisseurDevise);
        }

        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurDeviseViewModel convertisseurDevise = new ConvertisseurDeviseViewModel();
            ObservableCollection<Devise> devises = new ObservableCollection<Devise>();

            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));

            //Act
            convertisseurDevise.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            //Assert
            Assert.IsNotNull(convertisseurDevise.Devises);
            CollectionAssert.AreEqual(devises, convertisseurDevise.Devises, "Erreur liste devises");
        }

        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            ConvertisseurDeviseViewModel convertisseurDevise = new ConvertisseurDeviseViewModel();
            convertisseurDevise.MontantEnDevise = 107;
            Devise d = new Devise(2, "Franc Suisse", 1.07);
            convertisseurDevise.SelectedDevise = d;

            //Act
            convertisseurDevise.ActionSetConversion();

            //Assert
            //Assertion : MontantDevise est égal à la valeur espérée 100
            Assert.AreEqual(100, convertisseurDevise.MontantEuros, "La  valeur en devises n'est pas correcte");
        }
    }
}