using System;
using System.Collections.Generic;
using System.Text;

namespace Navigation_solution_
{
    public class Entity
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public int? ParentID { get; set; }
        public bool IsHidden { get; set; }
        public string LinkURL { get; set; }
    }
}
