using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ParamTable
    {
        public ParamTable()
        {
            ParamDetail = new HashSet<ParamDetail>();
        }

        public int PkParamTable { get; set; }
        public string Code { get; set; }
        public string LibelleLong { get; set; }
        public string LibelleCourt { get; set; }
        public string CodeDefaut { get; set; }

        public ICollection<ParamDetail> ParamDetail { get; set; }
    }
}
