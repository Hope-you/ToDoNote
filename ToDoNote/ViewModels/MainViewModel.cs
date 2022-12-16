using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Common.Models;

namespace ToDoNote.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel()
        {
            menuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
        }
        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }


        void CreateMenuBar()
        {
            menuBars.Add(new MenuBar { Icon = "HomeCircleOutline", Title = "首页",NameSpace="IndexView" });
            menuBars.Add(new MenuBar { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            menuBars.Add(new MenuBar { Icon = "Head", Title = "备忘录", NameSpace = "MemoView" });
            menuBars.Add(new MenuBar { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

    }
}
