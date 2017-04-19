using System;
using MvvmCross.Droid.Support.V7.RecyclerView.Grouping;
using MvvmCross.Droid.Support.V7.RecyclerView.Grouping.DataConverters;
using XamarinMvvmCross_MeetupSample.Core;

namespace XamarinMvvmCross_MeetupSample.Droid
{
	public class PeopleGroupedDataConverter : IMvxGroupedDataConverter
	{
		public PeopleGroupedDataConverter()
		{
		}

		public MvxGroupedData ConvertToMvxGroupedData(object item)
		{
			var groupedItem = item as GroupedData;

			return new MvxGroupedData()
			{
				Items = groupedItem.Items,
				Key = groupedItem.Key
			};
		}
	}
}
