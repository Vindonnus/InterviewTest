namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public interface IMapper<TSource, T>
    {
        T Map(TSource obj);
        TSource ReverseMap(T obj);
    }
}
