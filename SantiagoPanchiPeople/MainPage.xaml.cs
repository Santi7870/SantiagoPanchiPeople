using SantiagoPanchiPeople.Models;
using SantiagoPanchiPeople.ViewModels;


namespace SantiagoPanchiPeople
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;


            spanchi_people_list.ItemSelected -= OnItemSelected;
            spanchi_people_list.ItemSelected += OnItemSelected;

        }

        public void OnNewButtonClicked(object sender, EventArgs args)
        {

            if (!string.IsNullOrEmpty(spanchi_new_person_entry.Text))
            {
                _viewModel.AddPersonCommand.Execute(spanchi_new_person_entry.Text);
            }
        }



        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            _viewModel.RefreshPeopleCommand.Execute(null);
        }


        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Person person)
            {
     
                spanchi_people_list.SelectedItem = null;

             
                bool confirm = await DisplayAlert("Eliminar registro",
                    $"¿Estás seguro de que deseas eliminar a {person.Name}?",
                    "Sí", "No");

                if (confirm)
                {
             
                    _viewModel.DeletePersonCommand.Execute(person);

                    
                    await DisplayAlert("Registro eliminado",
                        $"Santiago Panchi acaba de eliminar un registro.",
                        "OK");
                }
            }
        }

    }
}
