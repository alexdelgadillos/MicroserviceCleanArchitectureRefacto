﻿using Alta.DTOs;
using Alta.Entities.Interfaces.Repositories;
using Alta.Entities.POCOs;
using Alta.UseCasesPorts.Interfaces;
using AutoMapper;
using MassTransit;
using System.Threading.Tasks;

namespace Alta.UseCases.Interactors
{
    public class RequestConfirmInteractor : IRequestConfirmInputPort
    {
        private readonly ILoadDetailRepository _loadDetailRepository;
        private readonly ILoadErrorRepository _loadErrorRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public RequestConfirmInteractor(
            ILoadDetailRepository loadDetailRepository,
            ILoadErrorRepository loadErrorRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint
        ) =>
            (_loadDetailRepository, _loadErrorRepository, _mapper, _publishEndpoint) = (
                loadDetailRepository,
                loadErrorRepository,
                mapper,
                publishEndpoint
            );

        public async Task Handle(RequestConfirmDTO requestConfirmDTO)
        {
            //TODO REMOVE IF's
            if (requestConfirmDTO is LoadDetailDTO)
            {
                await _loadDetailRepository.Insert(
                    _mapper.Map<LoadDetail>((LoadDetailDTO)requestConfirmDTO)
                );
                await _publishEndpoint.Publish<LoadDetailDTO>(requestConfirmDTO);
            }
            else if (requestConfirmDTO is LoadErrorDTO)
            {
                await _loadErrorRepository.Insert(
                    _mapper.Map<LoadError>((LoadErrorDTO)requestConfirmDTO)
                );
                await _publishEndpoint.Publish<LoadErrorDTO>(requestConfirmDTO);
            }
        }
    }
}
