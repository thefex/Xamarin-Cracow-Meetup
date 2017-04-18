﻿using System;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using XamarinMvvmCross_MeetupSample.Core;
using XamarinMvvmCross_MeetupSample.Core.ViewModels;

namespace XamarinMvvmCross_MeetupSample.Droid
{
	[MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
	public class PeopleListFragment : MvxFragment<PeopleListViewModel>
	{
		public PeopleListFragment()
		{
		}

		public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var inflatedView = this.BindingInflate(Resource.Layout.people_list_fragment, container, false);
			ViewModel.Load();

			var recyclerView = inflatedView.FindViewById<MvxRecyclerView>(Resource.Id.recyclerView);
			recyclerView.AddOnScrollListener(new IncrementalLoadingRecyclerViewScrollListener(ViewModel));

			return inflatedView;
		}
	}
}
