using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Extension;

namespace ToDoNote.ViewModels
{
    /// <summary>
    /// 这是基类
    /// </summary>
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        public readonly IEventAggregator aggregator;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
            //从容器中拿到这个事件聚合器（就是说有事件的一个集合）
            aggregator = containerProvider.Resolve<IEventAggregator>();
        }

        /// <summary>
        /// 是否可以导航
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// 离开导航时调用
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// 进入导航的时候调用
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public void UpdateLoading(bool isOpen)
        {
            aggregator.UpdateLoading(new Common.Events.UpdateModel
            {
                IsOpen = isOpen
            });
        }
    }
}
