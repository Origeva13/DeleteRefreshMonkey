﻿using DeleeRefreshMonkey.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeleeRefreshMonkey.Services;

namespace DeleeRefreshMonkey.ViewModels
{
    public class MonkeyViewModel:ViewModelBase
    {
        private ObservableCollection<Monkey> monkeys;
        public ObservableCollection<Monkey> Monkeys
        {
            get
            {
                return this.monkeys;
            }
            set
            {
                this.monkeys = value;
                OnPropertyChanged();
            }
        }
        public MonkeyViewModel()
        {
            monkeys = new ObservableCollection<Monkey>();
            IsRefreshing = false;
            ReadMonkeys();
        }

        private async void ReadMonkeys()
        {
            MonkeyService service = new MonkeyService();
            List<Monkey> list = await service.GetMonkeys();
            this.Monkeys = new ObservableCollection<Monkey>(list);
        }

        public ICommand DeleteCommand => new Command<Monkey>(RemoveMonkey);

        void RemoveMonkey(Monkey st)
        {
            if (Monkeys.Contains(st))
            {
                Monkeys.Remove(st);
            }
        }

   
        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            ReadMonkeys();

            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
