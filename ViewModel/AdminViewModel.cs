namespace FoodApp.ViewModel
{
    class AdminViewModel : NotificationVM
    {
        private object _currentView;

        public object CurrentViewModel
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public AdminViewModel()
        {
            CurrentViewModel = new UserViewModel();
        }
    }
}
