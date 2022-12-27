using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ToDoNote.Common.Models;
using ToDoNote.Service;
using ToDoNote.Shared.Dtos;

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


        public ToDoViewModel(IToDoService service)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            AddToDoCommand = new DelegateCommand(AddToDo);
            this.service = service;
            CreateToDoListData();
        }

        private void AddToDo()
        {
            IsRightDrawerOpen = !IsRightDrawerOpen;
        }

        private ObservableCollection<ToDoDto> toDoDtos;
        private bool toDoIsOpen;
        private readonly IToDoService service;

        public DelegateCommand AddToDoCommand { get; private set; }

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        async void CreateToDoListData()
        {

            var res = await service.GetAllAsync(new Shared.Parameters.QueryParameter
            {
                PageIndex = 0,
                PageSize = 100
            });
            if (res.Status)
            {
                foreach (var item in res.Result.Items)
                {
                    if (item != null)
                        ToDoDtos.Add(item);
                }
            }
        }
    }
}
