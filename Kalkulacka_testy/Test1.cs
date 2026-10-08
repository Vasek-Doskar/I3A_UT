using I3A_UT;

namespace Kalkulacka_testy
{
    [TestClass]
    public sealed class Test1
    {
        private Kalkulacka _kalkulacka;

        [TestInitialize]
        public void Setup()
        {
            //zde se nám pro každý test (testovací metodu) vytvoří vždy nová instance třídy kalkulacka
            //díky tomu máme objekt i kód vždy čistý, a víme že neprošel nějakou nečekanou změnou
            //Arange pro všechny případy s DataRow

            _kalkulacka = new Kalkulacka();
        }

        [TestMethod]
        [DataRow(1.5, 1.5, 3.0)]
        [DataRow(1.5, -1.0, 0.5)]
        [DataRow(-1.5, -1.0, -2.5)]
        public void Soucet_test(double a, double b, double expected)
        {
            //AAA
            //Arrange & Act
            double actual = _kalkulacka.Soucet(a, b);
            //Assert
            Assert.AreEqual(expected, actual, 0.01);
        }

        [TestMethod]
        [DataRow(10.0, 5.0, 2d)]
        [DataRow(10.0, -2.0, -5d)]
        [DataRow(-10.0, -5.0, 2d)]
        public void Podil_test(double a, double b, double expected)
        {
            //AAA
            //zde máme Arrange a Act v jednom kroku, díky inicializaciv Setup
            double actual = _kalkulacka.Podil(a, b);
            //Assert
            Assert.AreEqual(expected, actual, 0.01);
        }

        [TestMethod]
        public void DivisionZeroException_test()
        {
            //arrange
            double a = 5d, b = 0d;
            //act && assert
            Assert.Throws<DivideByZeroException>(() =>
            {
                _kalkulacka.Podil(a, b);
            });
        }

        // Nyní otestujeme metodu typu void. Zde nemáme k dispozici návratovou hodnotu,
        //takže musíme testovat změnu stavu objektu.
        //V SetName nemáme žádnou validační logiku, takže použijeme jen test rovnosti
        //Kdybychom měli kontrolu délky textu, a následné uložení podle zadané podmínky,
        //museli bychom napsat testy i pro nevyhovující stav(například pro stav, kdy se jméno
        //nesmí uložit protože nevyhovuje (je null, prázdný text atd)...

        [TestMethod]
        public void SetName_Test()
        {
            string name = "Kalkulacka_I3A"; //Arrange
            _kalkulacka.SetName(name); // Act
            Assert.AreEqual(_kalkulacka.Name, name); //Assert
        }
    }
}
