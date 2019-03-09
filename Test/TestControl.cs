using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
	public partial class TestControl : UserControl
	{
		[Category("Nobody")]
		public State MyState
		{
			get; set;
		}
		 = new State();

		[Category("Nobody")]
		public State[] MyStates { get; set; }
	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class State
	{
		public int Data { get; set; }
	}
}
