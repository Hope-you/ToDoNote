using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoNote.Common.Models;
using ToDoNote.Shared.Dtos;

namespace ToDoNote.ViewModels
{
    public class IndexViewModel : BindableBase, IRegionMemberLifetime
    {
        private string welecomTitle;
        public DelegateCommand CopyOneSaying { get; private set; }
        public DelegateCommand RefreshOnesaying { get; private set; }

        public string WelecomTitle
        {
            get { return welecomTitle; }
            set { welecomTitle = value; RaisePropertyChanged(); }
        }

        public IndexViewModel()
        {
            WelecomTitle = "";
            TaskBarsList = new ObservableCollection<TaskBar>();
            CopyOneSaying = new DelegateCommand(Copy);
            RefreshOnesaying = new DelegateCommand(ReFresh);
            CreateTaskBar();
            CreateTestDate();
        }

        private async void ReFresh()
        {
            WelecomTitle = await openSaying();
        }

        private void Copy()
        {
            Clipboard.SetText(WelecomTitle);
        }

        private async Task<string> openSaying()
        {
            var client = new HttpClient();
            var res = await client.SendAsync(new HttpRequestMessage() { RequestUri = new Uri("https://v1.jinrishici.com/rensheng.txt") });
            return await res.Content.ReadAsStringAsync();
        }

        private ObservableCollection<TaskBar> taskBarList;

        public ObservableCollection<TaskBar> TaskBarsList
        {
            get { return taskBarList; }
            set { taskBarList = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        public bool KeepAlive => false;

        async void CreateTaskBar()
        {
            TaskBarsList.Add(new TaskBar { Icon = "Alarm", Title = "汇总", Color = "#0097ff", Content = "9", Target = "" });
            TaskBarsList.Add(new TaskBar { Icon = "Check", Title = "已完成", Color = "#10b138", Content = "10", Target = "" });
            TaskBarsList.Add(new TaskBar { Icon = "SineWave", Title = "完成率", Color = "#00b4df", Content = "11", Target = "" });
            TaskBarsList.Add(new TaskBar { Icon = "NotebookHeart", Title = "备忘录", Color = "#ffa000", Content = "100%", Target = "" });

            WelecomTitle = await openSaying();
        }

        void CreateTestDate()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            for (int i = 0; i < 100; i++)
            {
                ToDoDtos.Add(new ToDoDto { Id = i, Content = i + "content 正在处理", Title = "待办" });
                MemoDtos.Add(new MemoDto { Id = i, Title = "备忘", Content = "我的密码是" });
            }
        }

    }
}
