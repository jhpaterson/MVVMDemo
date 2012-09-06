using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using MVVMDemo.Model;
using MVVMDemo.Data;

namespace MVVMDemo.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PersonRepository _repository;

        public MainWindowViewModel()
        {       
            // set up commands
            SaveButtonCommand = new RelayCommand(new Action<object>(Save));
            NewButtonCommand = new RelayCommand(new Action<object>(New));

            // get model objects
            _repository = new PersonRepository();
            _persons = new ObservableCollection<Person>(_repository.GetAll());

            // set state for UI elements
            FirstNameVisibility = false;
            LastNameVisibility = false;
        }

        // raise PropertyChanged event
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler=PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // properties which the View can bind to
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set {
                if (_firstName != value)
                {
                    _firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }

        private string _lastName; 
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }

        private bool _firstNameVisibility;
        public bool FirstNameVisibility 
        {
            get { return _firstNameVisibility; }
            set {
                if (_firstNameVisibility != value)
                {
                    _firstNameVisibility = value;
                    NotifyPropertyChanged("FirstNameVisibility");
                }
            }
        }

        private bool _lastNameVisibility;
        public bool LastNameVisibility 
             {
                 get { return _lastNameVisibility; }
            set {
                if (_lastNameVisibility != value)
                {
                    _lastNameVisibility = value;
                    NotifyPropertyChanged("LastNameVisibility");
                }
            }
        }

        // commands which the View can bind to
        private ICommand m_SaveButtonCommand;
        public ICommand SaveButtonCommand
        {
            get
            {
                return m_SaveButtonCommand;
            }
            set
            {
                m_SaveButtonCommand = value;
            }
        }

        private ICommand m_NewButtonCommand;
        public ICommand NewButtonCommand
        {
            get
            {
                return m_NewButtonCommand;
            }
            set
            {
                m_NewButtonCommand = value;
            }
        }

        // action methods called by commands
        // these modify properties, and hence update the View through bindings
        public void Save(object obj)
        {
            Person newPerson = new Person { FirstName = this.FirstName, LastName = this.LastName };
            _persons.Add(newPerson);        // add to observable collection for View
            _repository.Save(newPerson);    // add to repository to persist
            FirstNameVisibility = false;
            LastNameVisibility = false;
        }

        public void New(object obj)
        {
            FirstName = null;
            LastName = null;
            FirstNameVisibility = true;
            LastNameVisibility = true;
        }
    }
}
