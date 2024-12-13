using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Todolist2
{
    public partial class MainPage : ContentPage
    {
        public class TaskItem  // задача 
        {
            public string Title { get; set; } // текст 
            public bool IsCompleted { get; set; } // флаг на завершение задачи 
        }

        public class MainViewModel : INotifyPropertyChanged // (объявление класса) определяет новый класс объявление чертежа 
        {
            private string _newTaskTitle; // текст заголовка новой задачи
            public string NewTaskTitle  
            {
                get => _newTaskTitle; // предоставляет доступ к новой задачи 
                set
                {
                    _newTaskTitle = value; // установка нового значения 
                    OnPropertyChanged();  // уведомление 
                    
                    

                }
            }

            public ObservableCollection<TaskItem> Tasks { get; set; } // команда обновление чтение и запись 

            public ICommand AddTaskCommand { get; }  // команда создание задачи чтение     сделать так чтобы кнопка была не активна 
            public ICommand RemoveTaskCommand { get; }  // команда  удаление задачи чтение

           

            private void AddTask (string TaskText)  // метод добавляет новую задачу в коллекцию
            {
                if (!string.IsNullOrWhiteSpace(NewTaskTitle)) // проверяет пустая ли строка
                {
                    Tasks.Add(new TaskItem { Title = NewTaskTitle });  // создает новый объект с заголовком 
                    NewTaskTitle = string.Empty; // после добавления задачи поле ввода стало пустым
                }
                
            }

            

            private void RemoveTask(TaskItem task)  // метод  Удаляет задачу из коллекции
            {
                if (Tasks.Contains(task)) // находится в ли задача в коллекции 
                {
                    Tasks.Remove(task); // удаление 
                }
            } 
            public MainViewModel() // (конструктор класса) инициализация команд  метод который строит объект по чертежу 
            {
                Tasks = new ObservableCollection<TaskItem>(); //уведомляет о изменениях создает новый объект 
                AddTaskCommand = new Command<string>(AddTask, CanAddTask); // Добавляем валидацию
                RemoveTaskCommand = new Command<TaskItem>(RemoveTask);
            }

            private bool CanAddTask(string itemText) => !string.IsNullOrWhiteSpace(itemText);


            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }



    }
}
