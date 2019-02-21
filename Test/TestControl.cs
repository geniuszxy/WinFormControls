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
	[DefaultProperty("MyState")]
	public partial class TestControl : UserControl
	{
		public class StateConverter : TypeConverter
		{
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				var ps = TypeDescriptor.GetProperties(value, attributes);
				return ps;
			}

			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}

		[TypeConverter(typeof(StateConverter))]
		public class State
		{
			public State ()
			{
			}

			public State(State myState)
			{
				MyColor = myState.MyColor;
				MyColor2 = myState.MyColor2;
			}

			public Color MyColor { get; set; }
			public Color MyColor2 { get; set; }

			[Browsable(false)]
			public virtual Image MyImage
			{
				get { return null; }
				set { }
			}
		}

		public class SubState : State
		{
			private Image _image;

			public SubState(State myState) : base(myState)
			{
				_image = myState.MyImage;
			}

			[Browsable(true)]
			public override Image MyImage
			{
				get { return _image; }
				set { _image = value; }
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public State MyState { get; private set; } = new State();

		[RefreshProperties(RefreshProperties.All)]
		public bool UseSubState
		{
			get { return MyState is SubState; }
			set
			{
				if (value)
				{
					if(!(MyState is SubState))
					{
						MyState = new SubState(MyState);
					}
				}
				else
				{
					if (MyState is SubState)
					{
						MyState = new State(MyState);
					}
				}
			}
		}

		public TestControl()
		{
			InitializeComponent();
		}
	}
}
