using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace schedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public TblGroup SelectedGroup { get; set; }
        public TblReplacement SelectedDate1 { get; set; }
        public TblWeekday SelectedDate2 { get; set; }
        //public DateTime Today { get; set; }


        public TblGroup SelectedGroup2 { get => selectedGroup2;
            set { selectedGroup2 = value;
                var db = new ScheduleDbContext();
                TblScheduleDb = db.TblScheduleDbs.Where(s => s.Groupid == SelectedGroup2.GroupId).ToList();
            }  }
        public TblWeekday SelectedDay3 { get => selectedDay3; set { selectedDay3 = value; 
                var db = new ScheduleDbContext();
                TblScheduleDb = db.TblScheduleDbs.
                    Where(s => s.Day == SelectedDay3.Day).ToList();
            }
        }

        public TblGroup grud { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        //таблица 1
        public List<TblGroup> Group { get; set; }

        public List<TblReplacement> Replacement { get => replacement1; set { replacement1 = value; Fill();  } }
        private List<TblReplacement> replacement1;

        public List<TblReplacement> Replacement2 { get => replacement12; set { replacement12 = value; Fill(); } }
        private List<TblReplacement> replacement12;

        //таблица 2
        public List<TblGroup> Group2 { get; set; }
        public List<TblScheduleDb> TblScheduleDb { get => TblScheduleDb1; set { TblScheduleDb1 = value; Fill(); } }
        private List<TblScheduleDb> TblScheduleDb1;
        //Combobox4
        public TblWeekday SelectedDay2
        {
            get => SelectedDay1; set
            {
                SelectedDay1 = value;
                var db = new ScheduleDbContext();
                Replacement = db.TblReplacements.Where(s => s.WeekdaysId == SelectedDay2.Id).ToList();
            }
        }
        private TblWeekday SelectedDay1;
        public List<TblWeekday> Day { get; set; }
        public List<TblWeekday> Day3 { get; set; }
        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;

            //поиск

            Search();
            Search2();

            //поиск

            //Today = DateTime.Now;
            //SelectedDate1 = DateTime.Now;



            //вывод данных в таблицах и спискахИзначально это исключение 

            Day3 = DB.GetInstance().TblWeekdays.ToList();
            Day = DB.GetInstance().TblWeekdays.ToList();
            Replacement = DB.GetInstance().TblReplacements.ToList();
            Replacement2 = DB.GetInstance().TblReplacements.ToList();
            TblScheduleDb = DB.GetInstance().TblScheduleDbs.ToList();
            //TblScheduleDb = DB.GetInstance().TblScheduleDbs.Where(s => s.Id == SelectedGroup2.GroupId).ToList();
            //listschedule2.ItemsSource = Replacement;

            FillGroup();
            var user = DB.GetInstance().TblGroups.Include
                (s => s.SemestrNuber).Include
                (s => s.SemestrWeek);
            /* (s => s.CourseId)*/

            Group = DB.GetInstance().TblGroups.ToList();
            Group2 = DB.GetInstance().TblGroups.ToList();
            Combobox.ItemsSource = Group;
            Combobox2.ItemsSource = Group2;
            Combobox4.ItemsSource = Day;
            ComboboxDay.ItemsSource = Day3;


            grud = new TblGroup();
            grud = SelectedGroup;
           

            //вывод данных в таблицах и списках

        }
        //поиск
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        private string searchText = "";
        private string searchText2 = "";
        private TblGroup selectedGroup2;
        private TblWeekday selectedDay3;

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }
        public string SearchText2
        {
            get => searchText2;
            set
            {
                searchText2 = value;
                Search2();
            }
        }
        private void Search()
        {
            var db = new ScheduleDbContext();
            var result = db.TblReplacements.Where(s =>
                    s.Name.Contains(searchText) /*||*/
                );
            if (SelectedDay2 != null && SelectedDay2.Id != 0)
                result = result.Where(s => s.WeekdaysId == SelectedDay2.Id);
            Replacement = result.ToList();
            Signal(nameof(Replacement));
         

        }
        private void Search2()
        {

            
           
                var db = new ScheduleDbContext();
                var result = db.TblScheduleDbs.Where(s => s.Name.Contains(searchText2));
                //var result2 = db.TblScheduleDbs.Where(s => s.Day.Contains(searchText2));
               
                if (SelectedGroup2 != null && SelectedGroup2.GroupId != 0)
                result = result.Where(s => s.Groupid == SelectedGroup2.GroupId);

                //if (SelectedDay3 != null && SelectedDay3.Id != 0)
                //result = result.Where(s => s.Day == SelectedDay3.Day);

                TblScheduleDb = result.ToList();
                //TblScheduleDb = result2.ToList();
                Signal(nameof(TblScheduleDb));

            
            



        }
        //поиск






        private void replacement(object sender, RoutedEventArgs e)
        {
            Replacement replacements = new Replacement();
            replacements.ShowDialog();
            Obnov(sender, e);
        }


        private void FillGroup()
        {
            Group = new List<TblGroup>();
            Group2 = new List<TblGroup>();

            Group.AddRange(DB.GetInstance().TblGroups
                .Include(S => S.Speciality)
                .Include(S => S.SemestrNuber)
                .Include(S => S.Course)
                .Include(S => S.SemestrWeek).ToList());

           

            SelectedGroup = Group.FirstOrDefault();
            SelectedGroup2 = Group.FirstOrDefault();
            SelectedDay2 = Day.FirstOrDefault();
        }


        

        private void Fill([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        //private void perexod(object sender, MouseButtonEventArgs e)
        //{


        //}
        private void Obv(object sender, RoutedEventArgs e)
        {
            using (var db = new ScheduleDbContext())
            {
                TblScheduleDb = db.TblScheduleDbs.Where(s => s.Groupid ==SelectedGroup2.GroupId).ToList();
                //TblScheduleDb = db.TblScheduleDbs.ToList();
                db.SaveChanges();

            }
        }

        private void Obnov(object sender, RoutedEventArgs e)
        {
            //var res = MessageBox.Show("Обновить таблицу?", "Потверждение", MessageBoxButton.YesNo,
            //        MessageBoxImage.Question);
            //if (res == MessageBoxResult.Yes)
            //{
                using (var db = new ScheduleDbContext())
                {
                    Replacement = db.TblReplacements.Where(s=>s.WeekdaysId==SelectedDay2.Id).ToList();
                    //Replacement = db.TblReplacements.ToList();
                    db.SaveChanges();
                }
            //}


        }

        
        private void perexod(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Это та группа?", "Потверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                RedactorSivo redactorSivos = new RedactorSivo(SelectedGroup);
                redactorSivos.ShowDialog();
                Obv(sender, e);
            }
        }

        private void Vce(object sender, RoutedEventArgs e)
        {
            Replacement = DB.GetInstance().TblReplacements.ToList();
        }

        private void VCE(object sender, RoutedEventArgs e)
        {
            TblScheduleDb = DB.GetInstance().TblScheduleDbs.ToList();
        }

       
        
            //MyCombobox4_SelectionChanged
        private void Day1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void VCE2(object sender, RoutedEventArgs e)
        {
            TblScheduleDb = DB.GetInstance().TblScheduleDbs.ToList();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //DayOfWeek selectedDateDayOfWeek = ((DateTime)datePicker.SelectedDate).DayOfWeek;
            //DayOfWeek selectedComboBoxDayOfWeek = ((DayOfWeek)Combobox4.SelectedItem);
            //if (selectedDateDayOfWeek == selectedComboBoxDayOfWeek)
            //{
            //    DateTime? selectedDate = datePicker.SelectedDate;
            //    listschedule2.ItemsSource = Replacement.Where(s => s.Date == selectedDate.Value).ToList();
            //}



            //работает
            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate != null)
            {
                listschedule2.ItemsSource = Replacement.Where(s => s.Date == selectedDate.Value).ToList();
            }
        }
    }
}
