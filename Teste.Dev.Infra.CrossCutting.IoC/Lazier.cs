﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace Teste.Dev.Infra.CrossCutting.IoC
{
    public class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider) : base(() => provider.GetRequiredService<T>())
        { }
    }
}
