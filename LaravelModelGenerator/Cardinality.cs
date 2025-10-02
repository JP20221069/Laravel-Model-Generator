using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelModelGenerator
{
    public enum Cardinality
    {
        OneToOne,
        OneToMany,
        ManyToOne,
        ManyToMany,
        SelfReferenced
    }
}
