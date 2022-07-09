namespace AutoMapp
{
    public interface IMapper
    {
        TTarget MapTo<TSource, TTarget>(TSource source) where TTarget : new();
    }
}