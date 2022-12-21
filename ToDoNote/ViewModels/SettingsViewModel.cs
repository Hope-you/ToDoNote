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
    public class SettingsViewModel : BindableBase
    {
        private ObservableCollection<MenuBar> menuBars;

        private readonly IRegionManager regionManager;

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate); CreateMenuBar();
        }

        /// <summary>
        /// 导航动作
        /// </summary>
        /// <param name="obj"></param>
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;
            regionManager.Regions[PrismManager.SettingViewRegionName].RequestNavigate(obj.NameSpace);
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        void CreateMenuBar()
        {
            menuBars.Add(new MenuBar { Icon = "HomeCircleOutline", Title = "个性化", NameSpace = "SkinView" });
            menuBars.Add(new MenuBar { Icon = "NotebookOutline", Title = "系统设置", NameSpace = "" });
            menuBars.Add(new MenuBar { Icon = "Head", Title = "关于更多", NameSpace = "AboutView" });
        }
    }
}
