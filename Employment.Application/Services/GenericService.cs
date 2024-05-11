using AutoMapper;
using Employment.Application.IServices;
using Microsoft.Extensions.Logging;
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
        protected readonly ILogger _logger;
		
		public GenericService(IMapper mapper,ILogger logger)
        {
                _mapper = mapper;
                _logger = logger;
        }

    }
}
