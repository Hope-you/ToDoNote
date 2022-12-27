using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Shared.Dtos
{
    /// <summary>
    /// 待办事项 数据实体
    /// </summary>
    public class ToDoDto : BaseDto
    {
        private string title;
        private string content;
        private int status;

        public string Title
        {
            get => title;
            set { title = value; onPropertyChanged(); }
        }
        public string Content
        {
            get => content;
            set { content = value; onPropertyChanged(); }
        }
        public int Status
        {
            get => status;
            set { status = value; onPropertyChanged(); }
        }
    }
}
