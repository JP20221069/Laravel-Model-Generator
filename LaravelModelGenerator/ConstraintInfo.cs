using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelModelGenerator
{
    public class ConstraintInfo
    {
        string table_name;
        string column_name;
        string referenced_table_name;
        string referenced_column_name;
        ConstraintType constraint_type;

        public string TableName { get => this.table_name; set => this.table_name = value; }
        public string ColumnName { get => this.column_name; set => this.column_name = value; }
        public string ReferencedTableName { get => this.referenced_table_name; set => this.referenced_table_name = value; }
        public string ReferencedColumnName { get => this.referenced_column_name; set => this.referenced_column_name = value; }
        public ConstraintType ConstraintType { get => this.constraint_type; set => this.constraint_type = value; }

        public ConstraintInfo()
        {
            
        }

        public ConstraintInfo(string table_name, string column_name, string referenced_table_name, string referenced_column_name, ConstraintType constraint_type)
        {
            this.table_name = table_name;
            this.column_name = column_name;
            this.referenced_table_name = referenced_table_name;
            this.referenced_column_name = referenced_column_name;
            this.constraint_type = constraint_type;
        }
    }
}
