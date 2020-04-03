using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< HEAD
using System.Globalization;
using System.ComponentModel.Design.Serialization;
=======
>>>>>>> 04dca1956c81afb3f69fa8a31e7650d93d3eafe6

namespace Test
{
	public partial class TestControl : UserControl
	{
<<<<<<< HEAD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public State MyState
		{
			get { return new State(this); }
			set { _internalState0 = value.Data; }
		}

		internal int _internalState0;

		[TypeConverter(typeof(StateConverter))]
		public struct State
		{
			private TestControl _myControl;
			public State(TestControl _) { _myControl = _; }
			public int Data
			{
				get { return _myControl == null ? -1 : _myControl._internalState0; }
				set { if (_myControl != null) _myControl._internalState0 = value; }
			}
		}
	}

	public class StateConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is TestControl.State)
			{
				if (destinationType == typeof(string))
					return "State";
				if (destinationType == typeof(InstanceDescriptor))
				{
					var ctor = typeof(TestControl.State).GetConstructor(new Type[] { typeof(TestControl) });
					return new InstanceDescriptor(ctor, new object[] { null });
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
=======
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
>>>>>>> 04dca1956c81afb3f69fa8a31e7650d93d3eafe6
	}
}
