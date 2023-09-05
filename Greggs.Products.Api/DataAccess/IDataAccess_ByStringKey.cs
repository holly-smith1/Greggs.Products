using System;
using System.Collections.Generic;

namespace Greggs.Products.Api.DataAccess;

public interface IDataAccess_ByStringKey<out T>
{
    T Get(string key);
}