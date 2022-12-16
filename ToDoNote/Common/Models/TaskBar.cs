using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Common.Models
{
    public class TaskBar:BindableBase
    {
        private string icon;
        private string title;
        private string content;
        private string color;
        private string target;


        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// 触发的目标模块
        /// </summary>
        public string Target
        {
            get { return target; }
            set { target = value; }
        }


    }
}
