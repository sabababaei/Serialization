﻿using EasyMicroservices.Serialization.Interfaces;
using EasyMicroservices.Serialization.Tests.Providers.Models;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.Serialization.Tests.Providers.TextSerialization
{
    /// <summary>
    /// base class for test
    /// </summary>
    public abstract class BaseTextSerilizationTest
    {
        /// <summary>
        /// text serilaze provider
        /// </summary>
        public ITextSerialization _provider { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public BaseTextSerilizationTest(ITextSerialization provider)
        {
            _provider = provider;
        }

        [Theory]
        [InlineData("Mahdi", 30, Gender.Male, "{\"Name\":\"Mahdi\",\"Age\":30,\"Gender\":1}")]
        [InlineData("Maryam", 15, Gender.Female, "{\"Name\":\"Maryam\",\"Age\":15,\"Gender\":2}")]
        [InlineData("ali", 15, Gender.None, "{\"Name\":\"ali\",\"Age\":15,\"Gender\":0}")]
        public async Task Serilize(string name, int age, Gender gender, string expected)
        {
            var request = new ClassToSerialize()
            {
                Name = name,
                Age = age,
                Gender = gender
            };
            var result = _provider.Serialize(request);

            Assert.Equal(result, expected);
        }
        /// <summary>
        /// میخواهیم یک متن جیسون را به کلاس تبدیل کنیم
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        [InlineData("{\"Name\":\"Mahdi\",\"Age\":30,\"Gender\":1}", "Mahdi", 30, Gender.Male)]
        [InlineData("{\"Name\":\"Maryam\",\"Age\":15,\"Gender\":2}", "Maryam", 15, Gender.Female)]
        [InlineData("{\"Name\":\"ali\",\"Age\":15,\"Gender\":0}", "ali", 15, Gender.None)]
        [Theory]
        public async Task Deserilize(string json, string name, int age, Gender gender)
        {
            var result = _provider.Deserialize<ClassToSerialize>(json);

            Assert.True(result.Name == name && result.Age == age && result.Gender == gender);
        }

    }
}
