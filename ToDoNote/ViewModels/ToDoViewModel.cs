using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
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
    public class ToDoViewModel : NavigationViewModel
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

        /// <summary>
        /// 更新和新增的单个对象
        /// </summary>
        private ToDoDto currentTodoDto;

        /// <summary>
        /// 编辑选中的todo对象
        /// </summary>
        public ToDoDto CurrentTodoDto
        {
            get => currentTodoDto; private set { currentTodoDto = value; RaisePropertyChanged(); }
        }

        public string Search { get => search; set { search = value; RaisePropertyChanged(); } }


        public ToDoViewModel(IToDoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            ExecuteCommand = new DelegateCommand<string>(ExecuteToDo);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            this.service = service;
        }

        private async void Selected(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var todoRes = await service.GetFirsrOrDefaultAsync(obj.Id);
                if (todoRes.Status)
                {
                    CurrentTodoDto = todoRes.Result;
                    //打开右侧栏
                    IsRightDrawerOpen = true;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private void ExecuteToDo(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": SaveData(); break;
            }
        }

        private async void SaveData()
        {
            if (string.IsNullOrWhiteSpace(currentTodoDto.Title) || string.IsNullOrWhiteSpace(currentTodoDto.Content)) return;
            UpdateLoading(true);
            try
            {

                //判断是新增还是更新，
                if (CurrentTodoDto.Id > 0)
                {
                    var updateTodo = await service.UpdateAsync(CurrentTodoDto);
                    if (updateTodo.Status && updateTodo.Result != null)
                    {
                        var todo = ToDoDtos.FirstOrDefault(o => o.Id == updateTodo.Result.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentTodoDto.Title;

                            todo.Content = CurrentTodoDto.Content;

                            todo.Status = CurrentTodoDto.Status;
                        }
                    }

                }
                else
                {
                    var addres = await service.AddAsync(CurrentTodoDto);
                    if (addres.Status)
                    {
                        ToDoDtos.Add(addres.Result);
                        isRightDrawerOpen = false;
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private void Add()
        {

            IsRightDrawerOpen = !IsRightDrawerOpen;
        }

        private ObservableCollection<ToDoDto> toDoDtos;
        private string search;
        private readonly IToDoService service;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        async void GetDataAsync()
        {
            UpdateLoading(true);
            var res = await service.GetAllAsync(new Shared.Parameters.QueryParameter
            {
                PageIndex = 0,
                PageSize = 100,
                Search = search
            });
            if (res.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in res.Result.Items)
                {
                    if (item != null)
                        ToDoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }
    }
}
