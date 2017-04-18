using MvvmCross.Core.ViewModels;
using PropertyChanged;
using XamarinMvvmCross_MeetupSample.Core.Services.Authentication;

namespace XamarinMvvmCross_MeetupSample.Core.ViewModels
{
	[ImplementPropertyChanged]
	public class LoginViewModel
		: BaseMvxViewModel
	{
		readonly IAuthenticationService authService;

		public LoginViewModel(IAuthenticationService authService)
		{
			this.authService = authService;
		}

		public string Username { get; set; }

		public string Password { get; set; }

		public bool CanSignIn => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

		public MvxCommand SignIn => new SignInCommandBuilder(authService, this).BuildCommand();
	}
}
