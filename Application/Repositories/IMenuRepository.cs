using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    internal interface IMenuRepository
    {
        Task<List<MenuPosition>> ReadMenuAsync();
    }
}
