namespace Oraculo.Application.Responses;

public enum CodeResponseType
{
    Ok = 200,
    Created = 201,
    NotContent = 204,
    BadRequest = 400,
    NotFound = 404,
    Conflict = 409,
    Locked = 423,
    InternalError = 500
}