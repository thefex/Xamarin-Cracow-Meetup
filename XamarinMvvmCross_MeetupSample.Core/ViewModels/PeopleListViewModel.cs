using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using DynamicData;
using PropertyChanged;
using XamarinMvvmCross_MeetupSample.Core.ViewModels;

namespace XamarinMvvmCross_MeetupSample.Core
{
	[ImplementPropertyChanged]
	public class PeopleListViewModel : BaseMvxViewModel, IIncrementalLoading
	{
		protected readonly IList<IDisposable> DisposableItems = new List<IDisposable>();
		readonly MockedDataGenerator mockedDataGenerator = new MockedDataGenerator();
		SourceList<Person> _itemsSource = new SourceList<Person>();

		public PeopleListViewModel()
		{
		}

		public ReadOnlyObservableCollection<GroupedData> Items { get; private set; }

		public override void Appearing()
		{
			base.Appearing();
			BuildAndSetObservable();
		}

		public override void Disappearing()
		{
			base.Disappearing();
		}

		public override async System.Threading.Tasks.Task Load()
		{
			await base.Load();

			_itemsSource.AddRange(mockedDataGenerator.GenerateMockedPersonList());
		}

		protected virtual void BuildAndSetObservable()
		{
			if (DisposableItems.Count > 0)
				return;

			var personComparer = Comparer<Person>.Create((x, y) =>
			{
				return x.ToString().CompareTo(y.ToString());
			});

			ReadOnlyObservableCollection<GroupedData> observableCollection;
			DisposableItems.Add(_itemsSource
								.Connect()
								.Sort(personComparer)
			                    .Distinct()
								.GroupOn(x => x.GroupName)
								.Transform(CreatePersonGroup)
								.ObserveOn(SynchronizationContext.Current)
								.Bind(out observableCollection)
								.DisposeMany()
								.Subscribe());

			Items = observableCollection;
		}

		private GroupedData CreatePersonGroup(IGroup<Person, string> group)
		{
			var personGroup = new GroupedData() { Key = group.GroupKey };

			ReadOnlyObservableCollection<Person> personItemsCollection;
			var subscriptions = group.List
				  .Connect()
				  .Bind(out personItemsCollection)
				  .Subscribe();
			DisposableItems.Add(subscriptions);

			personGroup.Items = personItemsCollection;
			return personGroup;
		}

		public override void Destroy()
		{
			base.Destroy();

			var itemsToDispose = DisposableItems.ToList();
			DisposableItems.Clear();
			foreach (var disposableItem in itemsToDispose)
				disposableItem.Dispose();
		}

		public Task LoadMoreData()
		{
			_itemsSource.AddRange(mockedDataGenerator.GenerateMockedPersonList());
			return Task.FromResult(true);
		}

		public bool ShouldLoadMoreData()
		{
			return _itemsSource.Count < 75;
		}
	}
}
