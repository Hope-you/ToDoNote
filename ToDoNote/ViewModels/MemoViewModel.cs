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

        public MemoViewModel(IMemoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            AddToDoCommand = new DelegateCommand(AddMemo);
            this.service = service;
        }

        private void AddMemo()
        {
            IsRightDrawerOpen = !IsRightDrawerOpen;
        }

        private ObservableCollection<MemoDto> memoDtos;
        private readonly IMemoService service;

        public DelegateCommand AddToDoCommand { get; private set; }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }


        async void GetMemoListData()
        {
            UpdateLoading(true);
            var res = await service.GetAllAsync(new Shared.Parameters.todoQueryParameter
            {
                PageIndex = 0,
                PageSize = 100
            });
            if (res.Status)
            {
                MemoDtos.Clear();
                foreach (var item in res.Result.Items)
                {
                    MemoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetMemoListData();
        }
    }
}
