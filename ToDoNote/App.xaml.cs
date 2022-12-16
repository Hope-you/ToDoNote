using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToDoNote.ViewModels;
using ToDoNote.Views;

namespace ToDoNote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {

            return this.Container.Resolve<MainView>();
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
        }
    }
}
