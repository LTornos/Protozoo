using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;
using NUnit.Framework;

namespace GN.Sanidad.LEIRE.Test
{

    [TestFixture]
    public class EjemploTestSelenium
    {

        private ISelenium selenium;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*iexplore", "http://localhost/LEIRE/GN.Sanidad.LEIRE.WebUI/");
            selenium.Start();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        /// <summary>
        /// Valida que cuando la página se le carga al usuario, la información necesaria ha sido guardada.
        /// </summary>
        [Test]
        public void CargaInicialPagina()
        {
            selenium.Open("Principal.aspx");
            if (selenium.GetSelectOptions("ctl00_Contenido_cmbPacientes").Length == 0)
            {
                throw new Exception("Prueba no superada. cmbPacientes no se cargo correctamente.");
            }

            if (selenium.GetSelectOptions("ctl00_Contenido_cmbDietas").Length == 0)
            {
                throw new Exception("Prueba no superada. cmbDietas no se cargo correctamente.");
            }

        }


    }

}
