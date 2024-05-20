using Riok.Mapperly.Abstractions;

namespace LyMarket.Mapper;

[Mapper]
public static partial class ModelMapper
{
    public static partial TTarget Map<TTarget, TSource>(TSource source);

}
