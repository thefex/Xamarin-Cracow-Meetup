using System;
using System.Collections.Generic;

namespace XamarinMvvmCross_MeetupSample.Core
{
	public class MockedDataGenerator
	{
		static Random _randomGenerator = new Random();

		string[] groups = new string[] { "InsaneLab", "Xamarin", "Cracow", "MvvmCross" };
		string[] name = new string[] { "John", "Thomas", "Cassandra", "Sandra", "Jan", "Mike" };
		string[] surname = new string[] { "Deer", "Tyson", "Kowalski", "Gates", "Cracow" };

		public MockedDataGenerator()
		{
		}

		public IEnumerable<Person> GenerateMockedPersonList()
		{
			int randomCount = _randomGenerator.Next(5, 20);

			for (int i = 0; i < randomCount; ++i)
			{
				int groupIndex = _randomGenerator.Next(0, groups.Length);
				int nameIndex = _randomGenerator.Next(0, name.Length);
				int surnameIndex = _randomGenerator.Next(0, surname.Length);

				yield return new Person()
				{
					FirstName = name[nameIndex],
					LastName = surname[surnameIndex] + " " + _randomGenerator.Next(50000),
					GroupName = groups[groupIndex],
					PicturePath = "http://lorempixel.com/300/300?uniqueId=" + _randomGenerator.Next(50000).ToString(),
					IsSpecialPerson = _randomGenerator.Next(100) >= 50
				};
			}
		}
	}
}
