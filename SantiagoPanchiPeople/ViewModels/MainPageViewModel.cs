using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

using SantiagoPanchiPeople.Models;

namespace SantiagoPanchiPeople.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Person> People { get; set; }

        public ICommand AddPersonCommand { get; }

        public ICommand DeletePersonCommand { get; }

        public ICommand RefreshPeopleCommand { get; }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public MainPageViewModel()
        {
            People = new ObservableCollection<Person>();

            AddPersonCommand = new Command<string>(AddPerson);
            DeletePersonCommand = new Command<Person>(DeletePerson);
            RefreshPeopleCommand = new Command(RefreshPeople);


            RefreshPeople();
        }

        private void AddPerson(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre Invalido");


                App.PersonRepo.AddNewPerson(name);


                People.Clear();
                People.Add(new Person { Name = name });

                StatusMessage = $"{name} Añadido Exitosamente.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al añadir una persona: {ex.Message}";
            }
        }



        private void DeletePerson(Person person)
        {
            try
            {
                App.PersonRepo.DeletePerson(person.Id);

                StatusMessage = $"Santiago Panchi acaba de eliminar un registro: {person.Name}.";

      
                RefreshPeople();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar persona: {ex.Message}";
            }
        }


        private void RefreshPeople()
        {
            try
            {
                var peopleList = App.PersonRepo.GetAllPeople();
                People.Clear();

             
                foreach (var person in peopleList)
                {
                    People.Add(person);
                }

                StatusMessage = $"Registradas {peopleList.Count} personas.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al registrar la persona: {ex.Message}";
            }
        }

    }

}
