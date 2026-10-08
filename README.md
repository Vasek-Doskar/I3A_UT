# Unit Testy – Kalkulačka

Tento projekt obsahuje ukázku unit testů (jednotkových testů) v C# pomocí frameworku **MSTest** pro třídu `Kalkulacka`.

---

## 🧪 Co je to Unit Test?

**Unit test (jednotkový test)** je automatizovaný test, který ověřuje správnost **jedné malé části kódu** (obvykle jedné metody nebo třídy) v izolaci od zbytku systému.

### Hlavní výhody unit testů:
- **Rychlá zpětná vazba:** Během několika sekund zjistíte, zda kód funguje správně.
- **Prevence regresí:** Při úpravách kódu v budoucnu máte jistotu, že jste nerozbili již existující funkcionalitu.
- **Dokumentace kódu:** Testy slouží jako živá dokumentace toho, jak se má daná třída nebo metoda chovat.

---

## 📐 Návrhový vzor AAA (Arrange - Act - Assert)

Všechny testy v tomto projektu dodržují standardní konvenci **AAA**:

1. **Arrange (Příprava):** Nastavení testovacích dat a příprava testovaných objektů.
2. **Act (Provedení):** Zavolání testované metody.
3. **Assert (Ověření):** Kontrola, zda návratová hodnota nebo stav objektu odpovídá očekávání.

---

## 💻 Kompletní kód testovací třídy

```csharp
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
            // Zde se pro každý test vytvoří vždy nová instance
            // Díky tomu máme kód čistý a testy se navzájem neovlivňují
            _kalkulacka = new Kalkulacka();
        }

        [TestMethod]
        [DataRow(1.5, 1.5, 3.0)]
        [DataRow(1.5, -1.0, 0.5)]
        [DataRow(-1.5, -1.0, -2.5)]
        public void Soucet_test(double a, double b, double expected)
        {
            // Arrange & Act
            double actual = _kalkulacka.Soucet(a, b);

            // Assert
            Assert.AreEqual(expected, actual, 0.01);
        }

        [TestMethod]
        [DataRow(10.0, 5.0, 2d)]
        [DataRow(10.0, -2.0, -5d)]
        [DataRow(-10.0, -5.0, 2d)]
        public void Podil_test(double a, double b, double expected)
        {
            // Arrange & Act (díky inicializaci v Setup a předání dat z DataRow)
            double actual = _kalkulacka.Podil(a, b);

            // Assert
            Assert.AreEqual(expected, actual, 0.01);
        }

        [TestMethod]
        public void DivisionZeroException_test()
        {
            // Arrange
            double a = 5d, b = 0d;

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() =>
            {
                _kalkulacka.Podil(a, b);
            });
        }

        [TestMethod]
        public void SetName_Test()
        {
            // Arrange
            string name = "Kalkulacka_I3A";

            // Act
            _kalkulacka.SetName(name);

            // Assert
            Assert.AreEqual(_kalkulacka.Name, name);
        }
    }
}
```

---

## 🔍 Co se v tomto kódů děje?

### 1. Inicializace testů (`[TestInitialize]`)

```csharp
[TestInitialize]
public void Setup()
{
    _kalkulacka = new Kalkulacka();
}
```

- Před spuštěním **každé** testovací metody se automaticky zavolá metoda `Setup()`.
- Tím je zajištěno, že každý test pracuje s **novou a čistou instancí** třídy `Kalkulacka` a testy se navzájem neovlivňují.
- Zde probíhá společný krok **Arrange** pro všechny testovací případy.

---

### 2. Parametrizované testy (`[DataRow]`)

```csharp
[TestMethod]
[DataRow(1.5, 1.5, 3.0)]
[DataRow(1.5, -1.0, 0.5)]
[DataRow(-1.5, -1.0, -2.5)]
public void Soucet_test(double a, double b, double expected)
{
    double actual = _kalkulacka.Soucet(a, b);
    Assert.AreEqual(expected, actual, 0.01);
}
```

- Testy `Soucet_test` a `Podil_test` využívají atribut `[DataRow]`.
- Místo psaní samostatného testu pro každou kombinaci vstupů se stejný test spustí opakovaně s různými hodnotami (`a`, `b`) a očekávaným výsledkem (`expected`).
- Krok **Arrange** je vyřešen inicializací a samotným předáním dat v parametrech metody.
- `Assert.AreEqual(expected, actual, 0.01)` ověřuje výsledek s tolerancí `0.01` (kvůli přesnosti desetinných čísel typu `double`).

---

### 3. Testování výjimek (Dělení nulou)

```csharp
[TestMethod]
public void DivisionZeroException_test()
{
    double a = 5d, b = 0d;

    Assert.Throws<DivideByZeroException>(() =>
    {
        _kalkulacka.Podil(a, b);
    });
}
```

- Ověřuje chování aplikace při neplatném vstupu.
- Test projde pouze tehdy, pokud metoda `Podil` při dělení nulou vyhodí výjimku `DivideByZeroException`.

---

### 4. Testování změny stavu objektu (`void` metoda)

```csharp
[TestMethod]
public void SetName_Test()
{
    string name = "Kalkulacka_I3A"; // Arrange
    _kalkulacka.SetName(name);       // Act
    Assert.AreEqual(_kalkulacka.Name, name); // Assert
}
```

- Metoda `SetName` nevrací žádnou hodnotu (`void`).
- Test proto neověřuje návratovou hodnotu, ale kontrolovanou **změnu vnitřního stavu** objektu (vlastnost `_kalkulacka.Name`).
