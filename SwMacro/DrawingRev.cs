using System;
using System.Collections.Generic;
using System.Text;

namespace redbrick.csproj
{
    class DrawingRev
    {
        public DrawingRev(SwProperty rev, SwProperty eco, SwProperty desc, SwProperty list, SwProperty date)
        {
            this.Revision = rev;
            this.Eco = eco;
            this.Description = desc;
            this.List = list;
            this.Date = date;
        }

        public override string ToString()
        {
            string o = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n", 
                this.Revision.ToString(),
                this.Eco.ToString(),
                this.Description.ToString(),
                this.List.ToString(),
                this.Date.ToString());

            return o;
        }

        private SwProperty _rev;

        public SwProperty Revision
        {
            get { return _rev; }
            set { _rev = value; }
        }

        private SwProperty _eco;

        public SwProperty Eco
        {
            get { return _eco; }
            set { _eco = value; }
        }

        private SwProperty _description;

        public SwProperty Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private SwProperty _list;

        public SwProperty List
        {
            get { return _list; }
            set { _list = value; }
        }

        private SwProperty _date;

        public SwProperty Date
        {
            get { return _date; }
            set { _date = value; }
        }
	
    }
}
