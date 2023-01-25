using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Position;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Positions.Commands.AddOrEditPosition;

/// <summary>
/// Add or edit position command handler.
/// </summary>
internal class AddOrEditPositionCommandHandler : AsyncRequestHandler<AddOrEditPositionCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public AddOrEditPositionCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(AddOrEditPositionCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Position.Name))
        {
            throw new ValidationException("Position name can not be empty");
        }

        if (request.Position.Id == default)
        {
            await AddNewPositionAsync(request.Position, cancellationToken);
            return;
        }

        await EditExistingPositionAsync(request.Position, cancellationToken);
    }

    /// <summary>
    /// Add new position.
    /// </summary>
    /// <param name="positionDto">Position DTO.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ValidationException">If position with the same name already exists or
    /// is causing circular dependenciy in position hierarchy.</exception>
    private async Task AddNewPositionAsync(CreateOrEditPositionDto positionDto, CancellationToken cancellationToken)
    {
        if (await appDbContext.Positions.AnyAsync(pos => pos.Name == positionDto.Name, cancellationToken: cancellationToken))
        {
            throw new ValidationException("Position name can not be repeating");
        }

        var parentPosition = await GetParentPosition(positionDto.ParentId, cancellationToken);
        var childPositions = await GetChildPositions(positionDto.ChildIds, cancellationToken);
        if (parentPosition != null)
        {
            if (ArePositionsCircularyDependent(parentPosition, childPositions))
            {
                throw new ValidationException($"Provided positions cause circular dependenciy in position hierarchy");
            }
        }

        var position = new Position()
        {
            Name = positionDto.Name,
            ChildPositions = childPositions,
            ParentPosition = parentPosition
        };
        await appDbContext.Positions.AddAsync(position, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Edit existing position.
    /// </summary>
    /// <param name="positionDto">Position DTO.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ValidationException">If position with the same name already exists or if provided
    /// position relations cause circular dependency.</exception>
    private async Task EditExistingPositionAsync(CreateOrEditPositionDto positionDto, CancellationToken cancellationToken)
    {
        var position = await appDbContext.Positions.GetAsync(pos => pos.Id == positionDto.Id, cancellationToken);

        if (position.Name != positionDto.Name &&
            await appDbContext.Positions.AnyAsync(pos => pos.Name == positionDto.Name, cancellationToken))
        {
            throw new ValidationException("Position name can not be repeating");
        }

        var parentPosition = await GetParentPosition(positionDto.ParentId, cancellationToken);
        var childPositions = await GetChildPositions(positionDto.ChildIds, cancellationToken);
        if (parentPosition != null)
        {
            if (ArePositionsCircularyDependent(parentPosition, childPositions))
            {
                throw new ValidationException($"Provided positions cause circular dependency in position hierarchy");
            }
        }

        position.Name = positionDto.Name;
        position.ChildPositions = childPositions;
        position.ParentPosition = parentPosition;

        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Get parent position.
    /// </summary>
    /// <param name="id">Parent position id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Position.</returns>
    private async Task<Position> GetParentPosition(int id, CancellationToken cancellationToken)
    {
        return await appDbContext.Positions.FirstOrDefaultAsync(pos => pos.Id == id, cancellationToken);
    }

    /// <summary>
    /// Get child positions.
    /// </summary>
    /// <param name="ids">Child position ids.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ICollection of positions.</returns>
    private async Task<ICollection<Position>> GetChildPositions(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var childPositions = new List<Position>();
        foreach (var childId in ids)
        {
            var childPosition = await appDbContext.Positions.GetAsync(pos => pos.Id == childId, cancellationToken);
            childPositions.Add(childPosition);
        }
        return childPositions;
    }

    /// <summary>
    /// Method to check if positions cause circular dependency.
    /// </summary>
    /// <param name="parentPosition">Parent position.</param>
    /// <param name="childPositions">Child positions.</param>
    /// <returns>True if positions cause circular dependency. If not, false.</returns>
    private bool ArePositionsCircularyDependent(Position parentPosition, IEnumerable<Position> childPositions)
    {
        foreach (var childPosition in childPositions)
        {
            if (IsPositionParent(parentPosition, childPosition) || IsPositionChild(childPosition, parentPosition))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Method to check if one position is a parent to another.
    /// </summary>
    /// <param name="checkedPosition">Position at which the parent positions are being checked.</param>
    /// <param name="checkingPosition">Position that is being tested for parenthood.</param>
    /// <returns>If checking position is parent of the one being checked, returns true.</returns>
    private static bool IsPositionParent(Position checkedPosition, Position checkingPosition)
    {
        if (checkedPosition != checkingPosition)
        {
            if (checkedPosition.ParentPosition == null)
            {
                return false;
            }
            return IsPositionParent(checkedPosition.ParentPosition, checkingPosition);
        }
        return true;
    }

    /// <summary>
    /// Method to check if one position is a child to another.
    /// </summary>
    /// <param name="checkedPosition">Position at which the child positions are being checked.</param>
    /// <param name="checkingPosition">Position that is being tested for childhood.</param>
    /// <returns>If checking position is child of the one being checked, returns true.</returns>
    private static bool IsPositionChild(Position checkedPosition, Position checkingPosition)
    {
        if (checkedPosition.ChildPositions.Contains(checkingPosition))
        {
            return true;
        }
        foreach (var childPosition in checkedPosition.ChildPositions)
        {
            if (IsPositionChild(childPosition, checkingPosition))
            {
                return true;
            }
        }
        return false;
    }
}
