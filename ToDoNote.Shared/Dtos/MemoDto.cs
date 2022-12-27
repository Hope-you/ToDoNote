using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Shared.Dtos
{
    public class MemoDto : BaseDto
    {
        private string title;
        private string content;

        public string Title { get => title; set { title = value; onPropertyChanged(); } }
        public string Content { get => content; set { content = value; onPropertyChanged(); } }
    }
}
