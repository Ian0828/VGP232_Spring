using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2b
{
    [TestFixture]
    public class UnitTests
    {
        // I changed the W into lowerCase
        private WeaponCollection weaponCollection;
        private WeaponCollection empty;
        private string inputPath;
        private string outputPath;

        const string INPUT_FILE = "data2.csv";
        const string OUTPUT_FILE = "output.csv";

        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE);
            weaponCollection = new WeaponCollection();
            empty = new WeaponCollection();
            weaponCollection.Load(inputPath);
        }

        [TearDown]
        public void CleanUp()
        {
            // We remove the output file after we are done.
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            if (File.Exists("weapons.xml"))
            {
                File.Delete("weapons.xml");
            }
            if (File.Exists("weapons.json"))
            {
                File.Delete("weapons.json");
            }
            if (File.Exists("weapons.csv"))
            {
                File.Delete("weapons.csv");
            }
            if (File.Exists("empty.xml"))
            {
                File.Delete("empty.xml");
            }
            if (File.Exists("empty.json"))
            {
                File.Delete("empty.json");
            }
            if (File.Exists("empty.csv"))
            {
                File.Delete("empty.csv");
            }
        }

        // WeaponCollection Unit Tests
        [Test]
        public void WeaponCollection_GetHighestBaseAttack_HighestValue()
        {
            // Expected Value: 48
            // TODO: call WeaponCollection.GetHighestBaseAttack() and confirm that it matches the expected value using asserts.
            int highestBA = weaponCollection.GetHighestBaseAttack();
            Assert.AreEqual(highestBA, 48);
        }

        [Test]
        public void WeaponCollection_GetLowestBaseAttack_LowestValue()
        {
            // Expected Value: 23
            // TODO: call WeaponCollection.GetLowestBaseAttack() and confirm that it matches the expected value using asserts.
            int lowestBA = weaponCollection.GetLowestBaseAttack();
            Assert.AreEqual(lowestBA, 23);
        }

        [TestCase(WeaponType.Sword, 21)]
        public void WeaponCollection_GetAllWeaponsOfType_ListOfWeapons(WeaponType type, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfType(type) and confirm that the weapons list returns Count matches the expected value using asserts.
            List<Weapon> weaponList = weaponCollection.GetAllWeaponOfType(type);
            foreach (var weapon in weaponList)
            {
                Assert.AreEqual(weapon.Type, type);
            }
            Assert.AreEqual(weaponList.Count, 21);
        }

        [TestCase(5, 10)]
        public void WeaponCollection_GetAllWeaponsOfRarity_ListOfWeapons(int stars, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfRarity(stars) and confirm that the weapons list returns Count matches the expected value using asserts.
            List<Weapon> weaponList = weaponCollection.GetAllWeaponOfRarity(stars);
            foreach (var weapon in weaponList)
            {
                Assert.AreEqual(weapon.Rarity, stars);
            }
            Assert.AreEqual(weaponList.Count, 5, 10);
        }

        [Test]
        public void WeaponCollection_LoadThatExistAndValid_True()
        {
            // TODO: load returns true, expect WeaponCollection with count of 95.
            weaponCollection.Clear();
            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.AreEqual(weaponCollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty WeaponCollection
            Assert.IsFalse(weaponCollection.Load("File does NOT exist"));
            weaponCollection.Clear();
            Assert.AreEqual(weaponCollection.Count, 0);
        }

        [Test]
        public void WeaponCollection_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // TODO: save returns true, load returns true, and WeaponCollection is not empty.
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(outputPath));
            Assert.IsTrue(weaponCollection.Count != 0);
        }

        [Test]
        public void WeaponCollection_SaveEmpty_TrueAndEmpty()
        {
            // After saving an empty WeaponCollection, load the file and expect WeaponCollection to be empty.
            weaponCollection.Clear();
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(outputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        // Weapon Unit Tests
        [Test]
        public void Weapon_TryParseValidLine_TruePropertiesSet()
        {
            // TODO: create a Weapon with the stats above set properly
            Weapon expected = null;
            // TODO: uncomment this once you added the Type1 and Type2
            expected = new Weapon()
            {
                Name = "Skyward Blade",
                Type = WeaponType.Sword,
                Image = "https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png",
                Rarity = 5,
                BaseAttack = 46,
                SecondaryStat = "Energy Recharge",
                Passive = "Sky-Piercing Fang"
            };

            string line = "Skyward Blade,Sword,https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png,5,46,Energy Recharge,Sky-Piercing Fang";
            Weapon actual = null;

            // TODO: uncomment this once you have TryParse implemented.
            Assert.IsTrue(Weapon.TryParse(line, out actual));
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.BaseAttack, actual.BaseAttack);
            // TODO: check for the rest of the properties, Image,Rarity,SecondaryStat,Passive
            Assert.AreEqual(expected.Image, actual.Image);
            Assert.AreEqual(expected.Rarity, actual.Rarity);
            Assert.AreEqual(expected.SecondaryStat, actual.SecondaryStat);
            Assert.AreEqual(expected.Passive, actual.Passive);
        }

        [Test]
        public void Weapon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", Weapon.TryParse returns false, and Weapon is null.
            string line = "1,Bulbasaur,A,B,C,65,65";
            Weapon actual = null;
            Assert.IsFalse(Weapon.TryParse(line, out actual));
        }
        // 15 aditional test for assignment 2b 
        // Test LoadJson Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidJson()
        {
            // - Load the data2.csv and Save() it to weapons.json
            // and call Load() output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.json");
            outputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.IsTrue(weaponCollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_Load_ValidJson()
        {
            // - Load the data2.csv and SaveAsJSON() it to weapons.json
            // and call Load() output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.json");
            outputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponCollection.SaveAsJSON(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.IsTrue(weaponCollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_LoadJSON_ValidJson()
        {
            //- Load the data2.csv and SaveAsJSON() it to weapons.json
            //and call LoadJSON() on output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.json");
            outputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponCollection.SaveAsJSON(outputPath));
            Assert.IsTrue(weaponCollection.LoadJSON(inputPath));
            Assert.IsTrue(weaponCollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_Save_LoadJSON_ValidJson()
        {
            // Load the data2.csv and Save() it to weapons.json
            // and call LoadJSON() on output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.json");
            outputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.LoadJSON(inputPath));
            Assert.IsTrue(weaponCollection.Count() == 95);
        }

        // Test LoadCsv Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidCsv()
        {
            // - Load the data2.csv and Save() it to weapons.csv
            // and Load() output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.csv");
            outputPath = CombineToAppPath("weapons.csv");
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
        }

        [Test]
        public void WeaponCollection_Load_SaveAsCSV_LoadCSV_ValidCsv()
        {
            // - Load the data2.csv and SaveAsCSV() it to weapons.csv
            // and LoadCsv() output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.csv");
            outputPath = CombineToAppPath("weapons.csv");
            Assert.IsTrue(weaponCollection.SaveAsCSV(outputPath));
            Assert.IsTrue(weaponCollection.LoadCSV(inputPath));
        }

        // Test LoadXML Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidXml()
        {
            // Load the data2.csv and Save() it to weapons.xml
            // and Load() output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.xml");
            outputPath = CombineToAppPath("weapons.xml");
            Assert.IsTrue(weaponCollection.Save(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
        }

        [Test]
        public void WeaponCollection_Load_SaveAsXML_LoadXML_ValidXml()
        {
            // -Load the data2.csv and SaveAsXML() it to weapons.xml
            // and LoadXML() output and validate that there’s 95 entries
            inputPath = CombineToAppPath("weapons.xml");
            outputPath = CombineToAppPath("weapons.xml");
            Assert.IsTrue(weaponCollection.SaveAsXML(outputPath));
            Assert.IsTrue(weaponCollection.LoadXML(inputPath));
        }

        // Test SaveAsJSON Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidJson()
        {
            // Create an empty WeaponCollection, call SaveAsJSON() to empty.json,
            // and Load() the output and verify the WeaponCollection has a Count of 0
            inputPath = CombineToAppPath("empty.json");
            outputPath = CombineToAppPath("empty.json");
            Assert.IsTrue(empty.SaveAsJSON(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        // Test SaveAsCSV Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidCsv()
        {
            // Create an empty WeaponCollection, call SaveAsCSV() to empty.csv,
            // and Load() the output and verify the WeaponCollection has a Count of 0
            inputPath = CombineToAppPath("empty.csv");
            outputPath = CombineToAppPath("empty.csv");
            Assert.IsTrue(empty.SaveAsJSON(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        // Test SaveAsXML Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidXml()
        {
            // Create an empty WeaponCollection, call SaveAsXML() to empty.xml,
            // and Load and verify the WeaponCollection has a Count of 0
            inputPath = CombineToAppPath("empty.xml");
            outputPath = CombineToAppPath("empty.xml");
            Assert.IsTrue(empty.SaveAsXML(outputPath));
            Assert.IsTrue(weaponCollection.Load(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        // Test Load InvalidFormat
        [Test]
        public void WeaponCollection_Load_SaveJSON_LoadXML_InvalidXml()
        {
            // - Load the data2.csv and SaveAsJSON() it to weapons.json
            // and call LoadXML() output and validate that it returnsfalse,
            // and there’s 0 entries
            inputPath = CombineToAppPath("weapons.json");
            outputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponCollection.SaveAsJSON(outputPath));
            Assert.IsFalse(weaponCollection.LoadXML(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        [Test]
        public void WeaponCollection_Load_SaveXML_LoadJSON_InvalidJson()
        {
            // - Load the data2.csv and SaveAsXML() it to weapons.xml
            // and call LoadJSON() output and validate that it returns false,
            // and there’s 0 entries
            inputPath = CombineToAppPath("weapons.xml");
            outputPath = CombineToAppPath("weapons.xml");
            Assert.IsTrue(weaponCollection.SaveAsXML(outputPath));
            Assert.IsFalse(weaponCollection.LoadJSON(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        } 

        [Test]
        public void WeaponCollection_ValidCsv_LoadXML_InvalidXml()
        {
            // - LoadXML() on the data2.csv
            // and validate that returns false, and there’s 0 entries
            inputPath = CombineToAppPath("data2.csv");
            Assert.IsFalse(weaponCollection.LoadXML(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadJSON_InvalidJson()
        {
            // LoadJSON() on the data2.csv
            // and validate that Load returns false, and there’s 0 entries
            inputPath = CombineToAppPath("data2.csv");
            Assert.IsFalse(weaponCollection.LoadJSON(inputPath));
            Assert.IsTrue(weaponCollection.Count == 0);
        }
    }
}