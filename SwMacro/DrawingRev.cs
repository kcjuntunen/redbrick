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