using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Windows.Markup;
using System.Text.RegularExpressions;

namespace schedule
{
    /// <summary>
    /// Логика взаимодействия для Replacement.xaml
    /// </summary>
    public partial class Replacement : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TblReplacement> tReplacements { get; set; } = new ObservableCollection<TblReplacement>();
        public ObservableCollection<Schedule> schedules { get; set; } = new ObservableCollection<Schedule>();


        //public List<TblReplacement> tblRepl { get; set; }

        public List<TblReplacement> Tbl_replacement { get; set; }
        public DbSet<TblReplacement> tblReplacements { get; set; }


        //не нужное
        public CustomCommand SimpleCommand { get; set; }
        public CustomCommand TipleCommand { get; set; }//сохранить
        public CustomCommand PipleCommand { get; set; }//добавить
        public CustomCommand FlipCommand { get; set; }



        //нужное
        public CustomCommand GipleCommand { get; set; }//удалить
        




        public Schedule SelectedSavod { get; set; }
        public Schedule ChangeSavod { get; set; }
        public Schedule SelectedSchedule { get; set; }
        public Schedule ChangeSchedule { get; set; }


        private Visibility visibility = Visibility.Hidden;
        public Visibility kreating
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;

                Fill("kreating");
            }
        }

       


        public Schedule schedule { get => selected; set { selected = value; Fill(); } }
        public Schedule Sel { get => sel; set { sel = value; Fill(); } }


        private Schedule selected = new Schedule();
       
        public TblReplacement Selected 
        {
            get => selected1;
            set
            { 
                selected1 = value;
                Fill(); 
            } 
        }
        private TblReplacement selected1;

        private Schedule sel = new Schedule();

      
        private string search;






        //Поиск
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        private string searchText = "";


        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }
        private void Search()
        {
            var db = new ScheduleDbContext();
            var result = db.TblReplacements.Where(s =>
                    s.Name.Contains(searchText) /*||*/
                );
            tbl_replacement = result.ToList();
            Signal(nameof(tbl_replacement));

        }



        public List<TblWeekday> Day { get; set; }

        public TblWeekday SelectedDay
        {
            get => SelectedDay1; set
            {
                SelectedDay1 = value;
                var db = new ScheduleDbContext();
                tbl_replacement = db.TblReplacements.Where(s => s.WeekdaysId == SelectedDay.Id).ToList();
            }
        }

        public TblReplacement replacement { get => replacement1; set { replacement1 = value; Fill(); } }
        private TblReplacement replacement1 = new TblReplacement();

        public List<TblReplacement> tbl_replacement { get => tbl_replacement1; set { tbl_replacement1 = value; Fill(); } }
        private List<TblReplacement> tbl_replacement1;

        private TblWeekday SelectedDay1;
        public List<TblReplacement> TblReplacement { get => TblReplacement1; set { TblReplacement1 = value; Fill(); } }
        private List<TblReplacement> TblReplacement1;

        public List<TblGroup> Group1 { get; set; }
        public List<TblName> Name { get; set; }
        public List<TblPair> pair { get; set; }
        public List<TblPredmet2> Predmet2 { get; set; }
        public List<TblAudit> audit { get; set; }

        public int Group { get; set; }
        public int Pair { get; set; }
        public string Predmet { get; set; }
        public int Cabinet { get; set; }

        public Replacement()
        {
            InitializeComponent();
            DataContext = this;
            //поиск
            Search();




            //показывает данные дни недели
            Day = DB.GetInstance().TblWeekdays.ToList();
            Combobox3.ItemsSource = Day;

            TblReplacement = DB.GetInstance().TblReplacements.ToList();
            tbl_replacement = DB.GetInstance().TblReplacements.ToList();

            Group1 = DB.GetInstance().TblGroups.ToList();
            Name = DB.GetInstance().TblNames.ToList();
            pair = DB.GetInstance().TblPairs.ToList();
            Predmet2 = DB.GetInstance().TblPredmet2s.ToList();
            audit = DB.GetInstance().TblAudits.ToList();


            ComboboxGroup.ItemsSource = Group1;
            ComboboxPrepod.ItemsSource = Name;
            ComboboxPair.ItemsSource = pair;
            ComboboxPred.ItemsSource = Predmet2;
            ComboboxAudit.ItemsSource = audit;


            //скрывает все данные в List View tbl_replacement
            //tbl_replacement = DB.GetInstance().TblReplacements.Where(s => s.Equals(SelectedDay)).ToList();


            //tbl2 = DB.GetInstance().TblScheduleDbs.Where(s => s.Groupid == grud.GroupId).ToList();

            GipleCommand = new CustomCommand(
               () => { kreating = Visibility.Visible; });
        }



        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listschedule.ItemsSource).Refresh();
        }

        private void Fill([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
        private void Dob(object sender, RoutedEventArgs e)
        {
            if(SelectedDay == null)
            {
                MessageBox.Show("Выберите день недели", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //var result = MessageBox.Show("Хотите выполнить действие?",
            //    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            else 
            {


                MessageBox.Show("Запись добавлена в БД", "Готово!", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                using (var db = new ScheduleDbContext())
                {
                        kreating = Visibility.Visible;
                        Fill(nameof(tReplacements));


                    if (ComboboxGroup.SelectedIndex == -1 || ComboboxPrepod.SelectedIndex == -1 ||
                    ComboboxPair.SelectedIndex == -1 || ComboboxPred.SelectedIndex == -1
                    || ComboboxAudit.SelectedIndex == -1)
                    {
                        MessageBox.Show("Не все данные выбраны", "Предупреждение",
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                    else
                    {
                        replacement.WeekdaysId = SelectedDay.Id;
                        replacement.Group = ((TblGroup)ComboboxGroup.SelectedItem).Group;
                        replacement.Predmet = ((TblPredmet2)ComboboxPred.SelectedItem).Predmet2;
                        replacement.Name = ((TblName)ComboboxPrepod.SelectedItem).Name;
                        replacement.Pair = ((TblPair)ComboboxPair.SelectedItem).Pair;
                        replacement.Cabinet = (int?)((TblAudit)ComboboxAudit.SelectedItem).Audit;
                        replacement.Date = DateTime.Now;
                        db.TblReplacements.Add(replacement);
                        db.SaveChanges();
                        tbl_replacement = DB.GetInstance().TblReplacements.Where
                        (s => s.WeekdaysId == SelectedDay.Id).ToList();
                        replacement = new TblReplacement { Date = DateTime.Now }
                        /*{ Date = DateTime.Now}*/;
                    }



                }
            }
           
        }


        private void nasad(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delet(object sender, RoutedEventArgs e)
        {
            //if (Selected == null)
            //{
            //    MessageBox.Show("Ошибка, нельзя удалить!", "Ошибка 404", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

          
            var result = MessageBox.Show("Хотите удалить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question); 

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Запись удалена из БД", "Готово!", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                using (var db = new ScheduleDbContext())
                {

                    //удаляет по id в combobox
                    var remove = DB.GetInstance().TblReplacements.Find(Selected.Id);
                    DB.GetInstance().TblReplacements.Remove(remove);
                    DB.GetInstance().SaveChanges();
                    tbl_replacement = DB.GetInstance().TblReplacements.Where(s => s.WeekdaysId == SelectedDay.Id).ToList();


                    //tbl_replacement = DB.GetInstance().TblReplacements.ToList();
                }
            }
        }

        private void saved(object sender, RoutedEventArgs e)
        {
            Selected.Group = Group;
            DB.GetInstance().TblReplacements.Update(Selected);
            DB.GetInstance().SaveChanges();

            //if (SelectedDay != null)
            //{
            //    MessageBox.Show("Мы даем вам 2 шанс на изменение", "Рассуждение ");

            //}
            //else
            //{
            //    tbl_replacement = DB.GetInstance().TblReplacements.Where(s => s.WeekdaysId == SelectedDay.Id).ToList();



            //}



        }

        private void vce(object sender, RoutedEventArgs e)
        {
            tbl_replacement = DB.GetInstance().TblReplacements.ToList();
        }
    }
}
