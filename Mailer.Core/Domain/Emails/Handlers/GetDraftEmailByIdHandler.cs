﻿using Ardalis.Result;
using AutoMapper;
using Mailer.Core.Data;
using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Emails.Requests;
using Mailer.Core.Domain.Emails.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Handlers
{
    public class GetDraftEmailByIdHandler : IRequestHandler<GetDraftEmailByIdRequest, Result<EmailDto>>
    {
        private readonly IRepository<QueuedEmail> _repository;
        private readonly IMapper _mapper;

        public GetDraftEmailByIdHandler(IRepository<QueuedEmail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<EmailDto>> Handle(GetDraftEmailByIdRequest request, CancellationToken cancellationToken)
        {
            var spec = new GetQueuedDraftEmailByIdSpecification(request.DraftEmailId);
            var entity = await _repository.GetBySpecAsync(spec);
            if (entity == null)
                return Result<EmailDto>.NotFound();

            return Result<EmailDto>.Success(_mapper.Map<EmailDto>(entity));
        }
    }
}
