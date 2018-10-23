// Copyright (c) Alexey Malinin. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace Camunda.Worker
{
    public class Variable
    {
        public object Value { get; set; }
        public VariableType Type { get; set; }
    }
}
