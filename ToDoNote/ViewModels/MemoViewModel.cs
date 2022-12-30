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
using System.Windows.Data;
using ToDoNote.Service;
using ToDoNote.Shared.Dtos;

namespace ToDoNote.ViewModels
{
    public class MemoViewModel : NavigationViewModel
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
        private MemoDto currentMemoDto;

        /// <summary>
        /// 编辑选中的todo对象
        /// </summary>
        public MemoDto CurrentMemoDto
        {
            get => currentMemoDto; private set { currentMemoDto = value; RaisePropertyChanged(); }
        }

        public string Search { get => search; set { search = value; RaisePropertyChanged(); } }


        public MemoViewModel(IMemoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(ExecuteToDo);
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            DeleteCommand = new DelegateCommand<MemoDto>(DeleteData);
            this.service = service;
        }

        private async void Selected(MemoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var memoRes = await service.GetFirsrOrDefaultAsync(obj.Id);
                if (memoRes.Status)
                {
                    CurrentMemoDto = memoRes.Result;
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

        private async void DeleteData(MemoDto obj)
        {
            var res = await service.DeleteAsync(obj.Id);
            if (res.Status)
            {
                var thisTodo = MemoDtos.FirstOrDefault(t => t.Id.Equals(res.Result.Id));
                if (thisTodo != null)
                    MemoDtos.Remove(thisTodo);
            }
        }

        private async void SaveData()
        {
            if (string.IsNullOrWhiteSpace(currentMemoDto.Title) || string.IsNullOrWhiteSpace(currentMemoDto.Content)) return;
            UpdateLoading(true);
            try
            {

                //判断是新增还是更新，
                if (CurrentMemoDto.Id > 0)
                {
                    var updateTodo = await service.UpdateAsync(CurrentMemoDto);
                    if (updateTodo.Status && updateTodo.Result != null)
                    {
                        var todo = MemoDtos.FirstOrDefault(o => o.Id == updateTodo.Result.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentMemoDto.Title;

                            todo.Content = CurrentMemoDto.Content;
                            IsRightDrawerOpen = false;
                        }
                    }

                }
                else
                {
                    CurrentMemoDto.CreateDate = DateTime.Now;
                    currentMemoDto.UpdateDate = DateTime.Now;
                    var addres = await service.AddAsync(CurrentMemoDto);
                    if (addres.Status)
                    {
                        MemoDtos.Add(addres.Result);
                        IsRightDrawerOpen = false;
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
            CurrentMemoDto = new MemoDto();
            IsRightDrawerOpen = !IsRightDrawerOpen;
        }

        private ObservableCollection<MemoDto> memoDtos;
        private string search;
        private readonly IMemoService service;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }


        async void GetDataAsync()
        {
            try
            {
                UpdateLoading(true);
                var res = await service.GetAllAsync(new Shared.Parameters.QueryParameter
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Search = Search,
                });
                if (res.Status)
                {
                    MemoDtos.Clear();
                    foreach (var item in res.Result.Items)
                    {
                        if (item != null)
                            MemoDtos.Add(item);
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

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }
    }
}
