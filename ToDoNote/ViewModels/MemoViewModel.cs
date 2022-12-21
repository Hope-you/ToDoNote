using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ToDoNote.Common.Models;

namespace ToDoNote.ViewModels
{
    public class MemoViewModel: BindableBase
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

        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            AddToDoCommand = new DelegateCommand(AddMemo);
            CreateToDoListData();
        }

        private void AddMemo()
        {
            IsRightDrawerOpen = !IsRightDrawerOpen;
        }

        private ObservableCollection<MemoDto> memoDtos;

        public DelegateCommand AddToDoCommand { get; private set; }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        void CreateToDoListData()
        {
            for (int i = 0; i < 32; i++)
            {
                MemoDtos.Add(new MemoDto
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
