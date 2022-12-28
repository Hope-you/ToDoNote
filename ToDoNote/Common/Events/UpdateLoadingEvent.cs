using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Common.Events
{
    public class UpdateModel
    {
        /// <summary>
        /// 窗口打开状态
        /// </summary>
        public bool IsOpen { get; set; }
    }
    /// <summary>
    ///继承了prism的PubSubEvent接口，就会自动注入这种类型的事件就可以在事件聚合器中<UpdateLoadingEvent>获取到
    /// </summary>
    public class UpdateLoadingEvent:PubSubEvent<UpdateModel>
    {
        
    }
}
