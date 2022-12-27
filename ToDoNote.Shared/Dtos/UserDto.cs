using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoNote.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        private string userName;
        private string account;
        private string passWord;

        public string UserName { get => userName; set { userName = value; onPropertyChanged(); } }
        public string Account { get => account; set { account = value; onPropertyChanged(); } }
        public string PassWord { get => passWord; set { passWord = value; onPropertyChanged(); } }
    }
}
