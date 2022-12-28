using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Common.Events;

namespace ToDoNote.Extension
{
    public static class DialogExtension
    {
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="updateModel"></param>
        public static void  UpdateLoading(this IEventAggregator aggregator,UpdateModel updateModel)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(updateModel);
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Register(this IEventAggregator aggregator, Action<UpdateModel> action)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }
    }
}
