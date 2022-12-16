using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Common.Models;
using ToDoNote.Extension;

namespace ToDoNote.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }


        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public MainViewModel(IRegionManager regionManager)
        {
            menuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            this.regionManager = regionManager;

            NavigateCommand = new DelegateCommand<MenuBar>(Navigate); GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();

                }
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });
        }


        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }



        /// <summary>
        /// 导航动作
        /// </summary>
        /// <param name="obj"></param>
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
        }
        /// <summary>
        /// 创建数据
        /// </summary>
        void CreateMenuBar()
        {
            menuBars.Add(new MenuBar { Icon = "HomeCircleOutline", Title = "首页", NameSpace = "IndexView" });
            menuBars.Add(new MenuBar { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            menuBars.Add(new MenuBar { Icon = "Head", Title = "备忘录", NameSpace = "MemoView" });
            menuBars.Add(new MenuBar { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

    }
}
