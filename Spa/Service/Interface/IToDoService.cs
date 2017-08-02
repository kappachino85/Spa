using Spa.Domain;
using Spa.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spa.Service.Interface
{
    public interface IToDoService
    {
        int Insert(ToDoAddRequest model);

        void UpdateById(ToDoUpdateRequest model);

        List<ToDo> SelectAll();

        ToDo SelectById(int id);
    }
}
