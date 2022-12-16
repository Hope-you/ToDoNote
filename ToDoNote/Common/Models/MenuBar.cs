using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Common.Models
{
	/// <summary>
	/// 系统导航菜单实体类 
	/// </summary>
    public class MenuBar:BindableBase
    {
		private string icon;

		/// <summary>
		/// 图标
		/// </summary>
		public string Icon
        {
			get { return icon; }
			set { icon = value; }
		}

		/// <summary>
		/// 菜单名称
		/// </summary>
		private string title;

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		//菜单命名控件
		private string nameSapce;

		public string NameSpace
		{
			get { return nameSapce; }
			set { nameSapce = value; }
		}


	}
}
