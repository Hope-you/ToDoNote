using Prism.Commands;
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
    public class ToDoViewModel : BindableBase
    {
        /// <summary>
        /// 单击右侧添加待办展示侧边栏
        /// </summary>
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        public ToDoViewModel()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            AddToDoCommand = new DelegateCommand(AddToDo);
            CreateToDoListData();
        }

        private void AddToDo()
        {
            IsRightDrawerOpen = !IsRightDrawerOpen;
        }

        private ObservableCollection<ToDoDto> toDoDtos;

        public DelegateCommand AddToDoCommand { get; private set; }

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        void CreateToDoListData()
        {
            for (int i = 0; i < 32; i++)
            {
                ToDoDtos.Add(new ToDoDto
                {
                    Title = "标题" + i,
                    Content = i + "内容123123123123",
                    Id = i,
                    CreateDate = DateTime.Now,
                });
            }

        }
    }
}
