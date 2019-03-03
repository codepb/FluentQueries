[![Build Status](https://ninthlight.visualstudio.com/FluentQueries/_apis/build/status/codepb.FluentQueries?branchName=master)](https://ninthlight.visualstudio.com/FluentQueries/_build/latest?definitionId=9&branchName=master)
# FluentQueries
A Fluent API for defining queries. Allows fluent language based linq queries, and easy definition and consumption of Query Objects.

## How to Use FluentQueries

It all starts with the Query class. to begin a new query, just do the following:

```C#
Query<Book>
```

Then you select what you want to query against. If it's the entity itself, then you can use `Is`, if it's a property you can use `Has(b => b.SomeProperty)` and use a lambda expression to select the property. Finally select the predicate to resolve the query. For example:

```C#
Query<Book>.Is.EqualTo(otherBook);
Query<Book>.Has(b => b.Author).NotEqualTo("Oscar Wilde");
Query<Book>.Has(b => b.Title).Containing("The");
Query<Book>.Has(b => b.Available).False();
Query<Book>.Has(b => b.Pages).GreaterThan(1000);
```

Obviously, a large amount of the time, we want to query more than one thing. You can chain queries together using `And` or `Or`:

```C#
Query<Book>.Is.EqualTo(otherBook).Or.Has(b => b.Author).NotEqualTo("Oscar Wilde");
Query<Book>.Has(b => b.Pages).GreaterThan(500).And.Has(b => b.Available).True();
```

Once you've defined your query you can compile to an Expression:

```C#
Query<Book>.Has(b.Title).EqualTo("The Picture of Dorian Gray").AsExpression();
Context.Books.Where(Query<Book>.Has(b.Title).EqualTo("The Picture of Dorian Gray").AsExpression()).ToList();
```

You can also check if any object satisfies a query, either by using the `IsSatisfiedBy` or the extension method `Satisfies` on an object.

```C#
var query = Query<Book>.Has(b.Title).StartingWith("The");
query.IsSatisfiedBy(aBook);
anotherBook.Satisfies(query);
```

The idea of FluentQueries is to help the readability of LINQ queries. One way to do this is to utilise the FluentQueries framework to create classes that represent queries:

```C#
public class AuthorHasName : Query<Book>
{
    public AuthorHasName(string firstName, string lastName)
    {
    	Define(
    		Has(b => b.Author)
    		.EqualTo($"{firstName} {lastName}")
    	);
    }
}
```

You now have a nice, explicit and reusable class for querying the authors name:

```C#
aBook.Satisfies(new AuthorHasName("Charles", "Dickens"));
```

Finally you can combine existing queries with new ones:

```C#
Query<Book>.Has(b => b.Pages).GreaterThan(1000).And.Is.Satisfying(new AuthorHasName("William", "Shakespeare"));
```

Or combine lambda expressions with queries:

```C#
Query<Book>.Has(b => b.Title).EndingWith("Wild").And.Has(b => b.Pages).Satisfying(p => p > 1000);
```

This means if there is anything not supported by FluentQueries you can still use a lambda expression combined with Queries.