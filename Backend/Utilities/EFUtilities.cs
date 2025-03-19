namespace Backend.Utilities;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public static class EFUtilities
{
    //Solution used from https://stackoverflow.com/questions/34906437/how-to-construct-order-by-expression-dynamically-in-entity-framework
    public static IQueryable<TEntity> ApplyFilter<TEntity>(
        this IQueryable<TEntity> query, string? filterBy, string? filterValue)
    {
        if (filterBy is null || filterValue is null) return query;

        var parameter = Expression.Parameter(typeof(TEntity), "p");
        var property = Expression.Property(parameter, filterBy);
        var constant = Expression.Constant(filterValue);
        var equals = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

        return query.Where(lambda);
    }
     public static IQueryable<TEntity> ApplyOrderBy<TEntity>(
        this IQueryable<TEntity> query, string? orderBy, string orderDirection)
{
        if (orderBy is null) return query;
        
        query = orderDirection == "Asc"
            ? query.OrderBy(p => EF.Property<TEntity>(p!, orderBy))
            : query.OrderByDescending(p => EF.Property<TEntity>(p!, orderBy));

        return query;
 }

    public static IQueryable<T> OrderByPropertyOrField<T>(this IQueryable<T> queryable, string propertyOrFieldName, bool ascending = true)
{
        var elementType = typeof (T);
        var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

        var parameterExpression = Expression.Parameter(elementType);
        var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
        var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

        var orderByExpression = Expression.Call(typeof (Queryable), orderByMethodName,
                                                new[] {elementType, propertyOrFieldExpression.Type}, queryable.Expression, selector);

        return queryable.Provider.CreateQuery<T>(orderByExpression);
}
}