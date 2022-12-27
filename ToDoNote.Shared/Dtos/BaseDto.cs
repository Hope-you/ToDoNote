using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Shared.Dtos
{
    public class BaseDto : INotifyPropertyChanged
    {
        private DateTime createDate;
        private DateTime updateDate;

        public int Id { get; set; }
        public DateTime CreateDate { get => createDate; set { createDate = value; onPropertyChanged(); } }
        public DateTime UpdateDate { get => updateDate; set { updateDate = value; onPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
