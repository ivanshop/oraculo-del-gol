namespace Oraculo.Application.Interfaces
{
    public interface IGetRankingUseCase
    {
        Task<IEnumerable<string>> ExecuteAsync();
    }
}
