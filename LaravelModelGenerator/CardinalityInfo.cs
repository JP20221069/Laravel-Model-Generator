using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelModelGenerator
{
    public class CardinalityInfo
    {
        string table;
        string referencedtable;
        string column;
        Cardinality relationship;
        public string Table { get => this.table; set => this.table = value; }
        public string ReferencedTable { get => this.referencedtable; set => this.referencedtable = value; }
        public string Column { get => this.column; set => this.column = value; }
        public Cardinality Relationship { get => this.relationship; set => this.relationship = value; }
        
        public CardinalityInfo()
        {
            
        }

        public CardinalityInfo(string table, string referencedtable, string column, Cardinality relationship)
        {
            this.table = table;
            this.referencedtable = referencedtable;
            this.column = column;
            this.relationship = relationship;
        }
    }
}
