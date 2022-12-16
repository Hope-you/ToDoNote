using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Common.Models;

namespace ToDoNote.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private string welecomTitle;

        public string WelecomTitle
        {
            get { openSaying(); return welecomTitle; }
            set { welecomTitle = value; }
        }

        public IndexViewModel()
        {
            TaskBarsList = new ObservableCollection<TaskBar>();
            CreateTaskBar();
        }

        private async void openSaying()
        {
            var client = new HttpClient();
            var res = client.Send(new HttpRequestMessage() { RequestUri = new Uri("https://v1.jinrishici.com/rensheng.txt") });
            WelecomTitle = await res.Content.ReadAsStringAsync();
        }

        private ObservableCollection<TaskBar> taskBarList;

        public ObservableCollection<TaskBar> TaskBarsList
        {
            get { return taskBarList; }
            set { taskBarList = value; }
        }
        void CreateTaskBar()
        {
            TaskBarsList.Add(new TaskBar { Icon = "Alarm", Title = "汇总", Color = "#0097ff", Content = "9", Target = "" });
            TaskBarsList.Add(new TaskBar { Icon = "Check", Title = "已完成", Color = "#10b138", Content = "10", Target = "" });
            TaskBarsList.Add(new TaskBar { Icon = "SineWave", Title = "完成率", Color = "#00b4df", Content = "11", Target = "" });
            TaskBarsList.Add(new TaskBar { Icon = "NotebookHeart", Title = "备忘录", Color = "#ffa000", Content = "100%", Target = "" });
        }

    }
}
