using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
using System.Windows.Shapes;

namespace schedule
{
    /// <summary>
    /// Логика взаимодействия для RedactorSivo.xaml
    /// </summary>
    public partial class RedactorSivo : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;



        private TblGroup SelectedGroup { get; set; }
        public TblGroup grud { get; set; }

        //1 таблица
        //public List<TblScheduleDb> tbl1 { get => tbl11; set { tbl11 = value; Fill(); } }
        //private List<TblScheduleDb> tbl11;

        public TblScheduleDb item1 { get => item11; set { item11 = value; Fill(); } }
        private TblScheduleDb item11 = new TblScheduleDb();

        //2 таблица
        public List<TblScheduleDb> tbl2 { get => tbl21; set { tbl21 = value; Fill(); } }
        private List<TblScheduleDb> tbl21;

        public TblScheduleDb item2 { get => item21; set { item21 = value; Fill(); } }
        private TblScheduleDb item21 = new TblScheduleDb();

        public DbSet<Pair1> pairs { get; set; }

        public List<TblObpred> DataGrid1 { get; set; }

        public List<TblWeekday> Selected4 { get; set; }
        public TblWeekday SelectedDay { get; set; }

        public RedactorSivo(TblGroup selectedGroup)
        {
            InitializeComponent();
            DataContext = this;


            SelectedGroup = selectedGroup;
            grud = new TblGroup();
            grud = selectedGroup;
            tbl2 = DB.GetInstance().TblScheduleDbs.Where(s => s.Groupid == grud.GroupId).ToList();
            DataGrid1 = DB.GetInstance().TblObpreds.Where(s => s.CourseId == grud.CourseId).ToList();
            //DataGrid1 = DB.GetInstance().TblGroups.Where(s => s.CourseId == grud.CourseId).ToList();

            Selected4 = DB.GetInstance().TblWeekdays.ToList();
            Combobox5.ItemsSource = Selected4;

            var user = DB.GetInstance().TblCourses.Include

             (s => s.TblObpreds);
        }


        private void Fill([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            Close();
           
        }

        private void Dobav(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Save(object sender, RoutedEventArgs e)
        {

            using (var db = new ScheduleDbContext())
            {
                var resul = MessageBox.Show("Добавить запись в бд?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                //сохраняет в выбранную группу
                if (resul == MessageBoxResult.Yes)
                {
                    item1.Groupid = SelectedGroup.GroupId;
                    db.TblScheduleDbs.Add(item1);
                    db.SaveChanges();
                    //tbl2 = DB.GetInstance().TblScheduleDbs.ToList();
                    tbl2 = db.TblScheduleDbs.Where(s => s.Groupid == grud.GroupId).ToList();
                    item1 = new TblScheduleDb();
                    //OBN(sender, e);
                }

            }
        }

        private void OBN(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Хотите обновить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Запись обновлена", "Готово!", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                var db = new ScheduleDbContext();
                {
                    tbl2 = db.TblScheduleDbs.Where(s => s.Groupid == grud.GroupId).ToList();
                    db.SaveChanges();

                }



            }



        }

        private void Delite(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Хотите удалить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Запись удалена из БД", "Готово!", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                using (var db = new ScheduleDbContext())
                {
                    DB.GetInstance().TblScheduleDbs.Remove(item2);
                    //db.TblScheduleDbs.Update(item2);
                    DB.GetInstance().SaveChanges();
                    tbl2 = DB.GetInstance().TblScheduleDbs.Where(s=>s.Groupid==SelectedGroup.GroupId).ToList();
                



                }
            }


        }

        private void saved(object sender, RoutedEventArgs e)
        {
            DB.GetInstance().SaveChanges();
        }

        private void Day_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
