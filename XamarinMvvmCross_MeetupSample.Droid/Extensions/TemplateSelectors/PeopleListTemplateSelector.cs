using System;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;
using XamarinMvvmCross_MeetupSample.Core;

namespace XamarinMvvmCross_MeetupSample.Droid
{
	public class PeopleListTemplateSelector : MvxTemplateSelector<Person>
	{
		public PeopleListTemplateSelector()
		{
		}

		protected override int GetItemLayoutId(int fromViewType)
		{
			return fromViewType == 1 ? Resource.Layout.special_person_layout : Resource.Layout.person_layout;
		}

		protected override int SelectItemViewType(Person forItemObject)
		{
			return forItemObject.IsSpecialPerson ? 1 : 2;
		}

	}
}
