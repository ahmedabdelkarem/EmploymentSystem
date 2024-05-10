using AutoMapper;
using Employment.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{
    public class GenericService : IGenericService
    {
        protected readonly IMapper _mapper;
        public GenericService(IMapper mapper)
        {
                _mapper = mapper;
        }

    }
}
