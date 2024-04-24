using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    internal sealed class FinalizerConfiguration
    {
        public TimeSpan ExecutionPeriod { get; init; } = TimeSpan.FromMinutes(5);
    }
}
