﻿using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.GenericCommands
{
    public record class GenericCreateCommand<TRequest, TResponse>(TRequest Entity) : IRequest<TResponse> where TRequest : class where TResponse : class;
}
