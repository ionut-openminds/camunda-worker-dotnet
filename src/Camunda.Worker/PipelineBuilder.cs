using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Worker.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace Camunda.Worker
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly IList<Func<ExternalTaskDelegate, ExternalTaskDelegate>> _middlewareList =
            new List<Func<ExternalTaskDelegate, ExternalTaskDelegate>>();

        public PipelineBuilder(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
        }

        public IServiceProvider ApplicationServices { get; }

        public IPipelineBuilder Use(Func<ExternalTaskDelegate, ExternalTaskDelegate> middleware)
        {
            _middlewareList.Add(middleware);
            return this;
        }

        public ExternalTaskDelegate Build(ExternalTaskDelegate lastDelegate)
        {
            Guard.NotNull(lastDelegate, nameof(lastDelegate));

            return _middlewareList.Reverse()
                .Aggregate(lastDelegate, (current, middleware) => middleware(current));
        }
    }
}
