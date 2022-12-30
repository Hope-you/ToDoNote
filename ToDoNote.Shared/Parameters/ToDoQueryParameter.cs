using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Shared.Parameters
{
    public class ToDoQueryParameter : QueryParameter
    {
        /// <summary>
        /// 筛选状态
        /// </summary>
        public int? Status { get; set; }
    }
}
