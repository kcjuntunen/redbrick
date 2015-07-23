using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace redbrick.csproj
{
    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn() : base(new CalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a Calendar Cell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
