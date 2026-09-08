using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;


namespace Menu.Tests
{
    public class Option1to3Test
    {
        MethodInfo[] methods;

        [SetUp]
        public void Setup()
        {
            Type type = typeof(Program);
            methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        }

        [Test]
        public void TestPrintInfoMoreThan200Chars()
        {
            string consoleResult = "";
            StringWriter consoleOutput = new StringWriter();

            Console.SetOut(consoleOutput);
            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("PrintInfoAboutCsharp")).ToArray();

            Assert.That(filteredMethods, Is.Not.Empty, "PrintInfoAboutCsharp should be there");
            MethodInfo method = filteredMethods[0];
            method.Invoke(null, []);
            consoleResult = consoleOutput.ToString().Trim();
            Assert.That(consoleResult.Length, Is.GreaterThan(200), "The length of your C# information should be greater than 200.");
            Console.SetOut(Console.Out);
        }

        [Test]
        public void TestPrintInfoContainsAtLeastThreeSentences()
        {
            string consoleResult = "";
            StringWriter consoleOutput = new StringWriter();

            Console.SetOut(consoleOutput);
            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("PrintInfo")).ToArray();

            Assert.That(filteredMethods, Is.Not.Empty, "The method named PrintInfoAboutCsharp should be there");
            MethodInfo method = filteredMethods[0];
            method.Invoke(null, []);
            consoleResult = consoleOutput.ToString().Trim();
            var sentences = Regex.Matches(consoleResult, @"[^.!?]+[.!?]")
                                .Cast<Match>()
                                .Select(match => match.Value.Trim())
                                .ToList();

            Assert.That(sentences.Count, Is.GreaterThanOrEqualTo(3), "You should have 3 sentences, complete with dot in the end.");
        }

        [Test]
        public void TestConvertCelsiusToFahrenheit()
        {
            double gradeInCelsius = 1.0;
            object[] parameters = [gradeInCelsius];
            double gradeInFahrenheit;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("ConvertCelsiusToFahrenheit")).ToArray();
            MethodInfo method = filteredMethods[0];

            Assert.That(filteredMethods, Is.Not.Empty, "The method named ConvertCelsiusToFahrenheit should be there");
            object resultObject = method.Invoke(null, parameters);
            Assert.That(resultObject.GetType(), Is.EqualTo(typeof(double)), "ConvertCelsiusToFahrenheit should return the type double.");
            gradeInFahrenheit = Convert.ToDouble(resultObject);
            Assert.That(gradeInFahrenheit, Is.EqualTo(33.8), "The temperature 1.0 Celsius should be 33.8 Fahrenheit.");
        }

        [Test]
        public void TestConvertCelsiusToFahrenheit2()
        {
            double gradeInCelsius = 11.344;
            object[] parameters = [gradeInCelsius];
            double gradeInFahrenheit;

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("ConvertCelsiusToFahrenheit")).ToArray();
            MethodInfo method = filteredMethods[0];

            Assert.That(filteredMethods, Is.Not.Empty, "The method named ConvertCelsiusToFahrenheit should be there");
            object resultObject = method.Invoke(null, parameters);
            gradeInFahrenheit = Convert.ToDouble(resultObject);
            Assert.That(gradeInFahrenheit, Is.EqualTo(52.42), "The temperature 11.344 Celsius should be 52.42 Fahrenheit.");
        }

        [Test]
        public void TestMenuRepeatLetters1()
        {
            string consoleResult = "";
            object[] parameters = ["apa"];
            StringWriter consoleOutput = new StringWriter();
            object resultObject;

            Console.SetOut(consoleOutput);
            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("RepeatLetters")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named RepeatLetters should be there");

            MethodInfo method = filteredMethods[0];
            string input = "apa" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                resultObject = method.Invoke(null, null);
            }
            Assert.That(resultObject.GetType(), Is.EqualTo(typeof(string)), "RepeatLetters should return the type string.");
            consoleResult = resultObject.ToString() ?? "";
            Assert.That(consoleResult, Is.EqualTo("a-pp-aaa"), "The input apa should be a-pp-aaa.");
            Console.SetOut(Console.Out);
        }

        [Test]
        public void TestMenuRepeatLetters2()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("RepeatLetters")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named RepeatLetters should be there");

            MethodInfo method = filteredMethods[0];
            string input = "kassler" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo("k-aa-sss-ssss-lllll-eeeeee-rrrrrrr"), "The input kassler should be k-aa-sss-ssss-lllll-eeeeee-rrrrrrr.");
        }

        [Test]
        public void TestMenuRepeatLettersEmptyInput()
        {
            string consoleResult = "";

            MethodInfo[] filteredMethods = methods.Where(method => method.Name.Contains("RepeatLetters")).ToArray();
            Assert.That(filteredMethods, Is.Not.Empty, "The method named RepeatLetters should be there");

            MethodInfo method = filteredMethods[0];
            string input = "" + Environment.NewLine;
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                object resultObject = method.Invoke(null, null);
                consoleResult = resultObject.ToString() ?? "";
            }
            Assert.That(consoleResult, Is.EqualTo(""), "The input empty string should be empty string.");
        }
    }
}
