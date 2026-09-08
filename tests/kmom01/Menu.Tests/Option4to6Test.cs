using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Menu.Tests
{
    public class Option4to6Test
    {
        MethodInfo[] methods;

        [SetUp]
        public void Setup()
        {
            Type type = typeof(Program);
            methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        }

        [Test]
        public void TestCreateAcronym1()
        {
            string consoleResult = "";
            object resultObject;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CreateAcronym")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CreateAcronym should be there");

            MethodInfo method = filteredMethods[0];
            string input = "BRöderna Ivarsson Osby" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                resultObject = method.Invoke(null, null);
            }
            Assert.That(resultObject.GetType(), Is.EqualTo(typeof(string)), "CreateAcronym should return the type string.");
            consoleResult = resultObject.ToString() ?? "";
            Assert.That(consoleResult, Is.EqualTo("BRIO"), "Input: BRöderna Ivarsson Osby => output: BRIO.");
        }

        [Test]
        public void TestCreateAcronym2()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CreateAcronym")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CreateAcronym should be there");

            MethodInfo method = filteredMethods[0];
            string input = "Ingvar Kamprad Elmtaryd Agunnaryd" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo("IKEA"), "Input: Ingvar Kamprad Elmtaryd Agunnaryd => output: IKEA.");
        }

        [Test]
        public void TestCreateAcronymNameWithoutCapitals()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CreateAcronym")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CreateAcronym should be there");

            MethodInfo method = filteredMethods[0];
            string input = "john doe" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo(""), "Input: john doe => output: tom sträng");
        }

        [Test]
        public void TestCreateAcronymNameWithOneCapital()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CreateAcronym")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CreateAcronym should be there");

            MethodInfo method = filteredMethods[0];
            string input = "John doe" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo("J"), "Input: john doe => output: J");
        }

        [Test]
        public void TestCreateAcronymNameWithOnlyCapitals()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CreateAcronym")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CreateAcronym should be there");

            MethodInfo method = filteredMethods[0];
            string input = "JOHN DOE" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo("JOHNDOE"), "Input: JOHN DOE => output: JOHNDOE");
        }

        [Test]
        public void TestCountSumAndAverage1()
        {
            string consoleResult = "";
            object resultObject;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CountSumAndAverage")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CountSumAndAverage should be there");

            MethodInfo method = filteredMethods[0];
            string input = "1" + Environment.NewLine;
            input += "2" + Environment.NewLine;
            input += "3" + Environment.NewLine;
            input += "klar" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                resultObject = method.Invoke(null, null);
            }
            Assert.That(resultObject.GetType(), Is.EqualTo(typeof(string)), "CreateAcronym should return the type string.");
            consoleResult = resultObject.ToString() ?? "";
            Assert.That(consoleResult, Is.EqualTo("Summan=6 och medelvärdet=2"), "Input: 1, input: 2, input: 3; input klar => output: Summan=6 och medelvärdet=2");
        }

        [Test]
        public void TestCountSumAndAverage2()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("CountSumAndAverage")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named CountSumAndAverage should be there");

            MethodInfo method = filteredMethods[0];
            string input = "1" + Environment.NewLine;
            input += "1" + Environment.NewLine;
            input += "5" + Environment.NewLine;
            input += "klar" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo("Summan=7 och medelvärdet=2,33").Or.EqualTo("Summan=7 och medelvärdet=2.33"), "Input: 1, input: 1, input: 5; input klar => output: Summan=7 och medelvärdet=2,33");
        }

        [Test]
        public void TestValidateSsn1()
        {
            bool consoleResult = false;
            object resultObject;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("ValidateSsn")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named ValidateSsn should be there");

            MethodInfo method = filteredMethods[0];
            string input = "811218-9876" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                resultObject = method.Invoke(null, null);
            }
            Assert.That(resultObject.GetType(), Is.EqualTo(typeof(bool)), "ValidateSsn should return the type bool.");
            consoleResult = Convert.ToBoolean(resultObject);
            Assert.That(consoleResult, Is.True, "Input: 811218-9876 => return: true");
            Console.SetOut(Console.Out);
        }

        [Test]
        public void TestValidateSsn2()
        {
            bool consoleResult = true;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("ValidateSsn")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named ValidateSsn should be there");

            MethodInfo method = filteredMethods[0];
            string input = "231218-9874" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = Convert.ToBoolean(resultObject);
            }
            Assert.That(consoleResult, Is.False, "Input: 231218-9874 => return: false");
            Console.SetOut(Console.Out);
        }

        [Test]
        public void TestValidateSsn3()
        {
            bool consoleResult = false;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("ValidateSsn")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named ValidateSsn should be there");

            MethodInfo method = filteredMethods[0];
            string input = "8181818181" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = Convert.ToBoolean(resultObject);
            }
            Assert.That(consoleResult, Is.True, "Input: 8181818181 => return: true");
            Console.SetOut(Console.Out);
        }

        [Test]
        public void TestValidateSsnEmpty()
        {
            bool consoleResult = true;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("ValidateSsn")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named ValidateSsn should be there");

            MethodInfo method = filteredMethods[0];
            string input = "" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = Convert.ToBoolean(resultObject);
            }
            Assert.That(consoleResult, Is.False, "Input: empty string => return: false");
            Console.SetOut(Console.Out);
        }
    }
}
