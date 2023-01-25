using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.User;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users.GetUserById;

/// <summary>
/// Handler for <see cref="GetUserByIdQuery" />.
/// </summary>
internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    /// <param name="mapper">Automapper instance.</param>
    public GetUserByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Users.AsQueryable();

        var user = await mapper.ProjectTo<UserDto>(query).GetAsync(x=>x.Id == request.UserId, cancellationToken);

        return user;
    }
}
