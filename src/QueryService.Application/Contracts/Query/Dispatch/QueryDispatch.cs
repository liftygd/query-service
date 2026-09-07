using Domain.Enums;

namespace Application.Contracts.Query;

public record QueryDispatch(
    Guid QueryId,
    QueryType QueryType);