﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
        public TblObpred SelectedPrepod { get; set; }
        public TblAudit SelectedAudit { get; set; }
        public TblPair SelectedPair { get; set; }
        public TblPredmet2 SelectedPred { get; set; }

        //1 таблица
        

        public TblScheduleDb item1 { get => item11; set { item11 = value; Fill(); } }
        private TblScheduleDb item11 = new TblScheduleDb();


        public DbSet<TblScheduleDb> Schedules { get; set; }
        //2 таблица
        public List<TblScheduleDb> tbl2 { get => tbl21; set { tbl21 = value; Fill(); } }
        private List<TblScheduleDb> tbl21;

        public TblScheduleDb item2 { get => item21; set { item21 = value; Fill(); } }
        private TblScheduleDb item21 = new TblScheduleDb();

        public DbSet<Pair1> pairs { get; set; }

        public List<TblObpred> DataGrid1 { get; set; }

        public List<TblWeekday> Day { get; set; }
        public List<TblName> Name { get; set; }
        public List<TblPair> pair { get; set; }
        public List<TblPredmet2> Predmet2 { get; set; }
        public List<TblAudit> audit { get; set; }

        //private int hours;
        //private int pairCount;
        //public int Hours
        //{
        //    get { return hours; }
        //    set
        //    {
        //        hours = value;
        //        RaisePropertyChanged("Hours");
        //        RaisePropertyChanged("PairCount");

        //    }
        //}
        //public int PairCount
        //{
        //    get { return hours * 2; }
        //}
        //private void RaisePropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
      

        public TblWeekday SelectedDay { get => selectedDay; set { selectedDay = value; Fill(); } }
        private TblWeekday selectedDay;

        public bool Editable { get; set; }
        public string? SelectedItem { get; private set; }

        public RedactorSivo(TblGroup selectedGroup)
        {
            InitializeComponent();
            DataContext = this;

            SelectedGroup = selectedGroup;
            grud = new TblGroup();
            grud = selectedGroup;

            Day = DB.GetInstance().TblWeekdays.ToList();
            Name = DB.GetInstance().TblNames.ToList();
            pair = DB.GetInstance().TblPairs.ToList();
            Predmet2 = DB.GetInstance().TblPredmet2s.ToList();
            audit = DB.GetInstance().TblAudits.ToList();

            tbl2 = DB.GetInstance().TblScheduleDbs.Where(s => s.Groupid == grud.GroupId).ToList();
            DataGrid1 = DB.GetInstance().TblObpreds.Where(s => s.Groupid == grud.GroupId).ToList();
            //DataGrid1 = DB.GetInstance().TblGroups.Where(s => s.CourseId == grud.CourseId).ToList();


            Combobox5.ItemsSource = Day;
            ComboboxPrepod.ItemsSource = Name;
            ComboboxPair.ItemsSource = pair;
            ComboboxPred.ItemsSource = Predmet2;
            ComboboxAudit.ItemsSource = audit;
            

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
                    


                   
                    if(Combobox5.SelectedIndex==-1 || ComboboxPrepod.SelectedIndex==-1 || 
                        ComboboxPair.SelectedIndex==-1 || ComboboxPred.SelectedIndex==-1
                        || ComboboxAudit.SelectedIndex==-1 )
                    {
                        MessageBox.Show("Не все данные выбраны", "Предупреждение", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                    else
                    {
                        item1.Groupid = SelectedGroup.GroupId;
                        item1.Day = ((TblWeekday)Combobox5.SelectedItem).Day;
                        item1.Predmet = ((TblPredmet2)ComboboxPred.SelectedItem).Predmet2;
                        item1.Name = ((TblName)ComboboxPrepod.SelectedItem).Name;
                        item1.Pair = ((TblPair)ComboboxPair.SelectedItem).Pair;
                        item1.Cabinet = ((TblAudit)ComboboxAudit.SelectedItem).Audit;

                        db.TblScheduleDbs.Add(item1);
                        db.SaveChanges();
                        tbl2 = db.TblScheduleDbs.Where(s => s.Groupid == grud.GroupId).ToList();
                        item1 = new TblScheduleDb();

                    }
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
                    tbl2 = DB.GetInstance().TblScheduleDbs.Where(s => s.Groupid == SelectedGroup.GroupId).ToList();

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

