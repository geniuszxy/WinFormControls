﻿using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormControls
{
	/// <summary>
	/// A button with image
	/// </summary>
	[DefaultProperty("NormalState")]
	public class ImageButton : UserControl
	{
		#region State

		/// <summary>
		/// Main state to modify in properties
		/// </summary>
		[TypeConverter(typeof(StateConverter))]
		public struct State
		{
			public const int MaskTintAdditive = 1 << 31;
			public const int MaskResizeImage = 1 << 30;
			public const int MaskApplyBackColor = 1 << 29;
			public const int MaskSizeDirty = 1 << 28;
			public const int MaskBackColor = 0xfff000;
			public const int MaskImageColor = 0xfff;

			/*
			 * Store flags and color
			 *--------------------------
			 * bit    desc.
			 *--------------------------
			 * 31     Tint Addtive Flag
			 * 30     Resize Image Flag
			 * 29     Apply Background Color Flag
			 * 28     Size Dirty Flag
			 * 20-23  Background Red
			 * 16-19  Background Green
			 * 12-15  Background Blue
			 * 8-11   Image Red
			 * 4-7    Image Green
			 * 0-3    Image Blue
			 */
			private int _data;
			private Image _image;

			public State(int data, Image image)
			{
				_data = data;
				_image = image;
			}

			/// <summary>
			/// Whether the image should be resized to fit the control size
			/// </summary>
			[Description("Whether the image should be resized to fit the control size")]
			public bool ResizeImage
			{
				get { return (_data & MaskResizeImage) != 0; }
				set
				{
					//mark as dirty if this property changed.
					if (value)
						_data = _data | MaskResizeImage | MaskSizeDirty;
					else
						_data = _data & ~MaskResizeImage | MaskSizeDirty;
				}
			}

			/// <summary>
			/// Whether the ImageColor should be addtive or multiply to the image
			/// </summary>
			[Description("Whether the ImageColor should be addtive or multiply to the image")]
			public bool TintAdditive
			{
				get { return (_data & MaskTintAdditive) != 0; }
				set
				{
					if (value)
						_data |= MaskTintAdditive;
					else
						_data &= ~MaskTintAdditive;
				}
			}

			/// <summary>
			/// Image of the state, if not set the image in the first state will be used
			/// </summary>
			[Description("The image of the state, if not set the image in the first state will be used")]
			public Image Image
			{
				get { return _image; }
				set
				{
					_image = value;
					//mark as dirty
					if (value != null)
						_data |= MaskSizeDirty;
				}
			}

			/// <summary>
			/// Addtional color of the image
			/// </summary>
			[Description("The addtional color of the image")]
			public Color ImageColor
			{
				get { return i2c(_data & MaskImageColor); }
				set { _data = (_data & ~MaskImageColor) | c2i(value); }
			}

			/// <summary>
			/// Background color of the state
			/// </summary>
			[Description("Background color of the state")]
			public Color BackColor
			{
				get
				{
					if ((_data & MaskApplyBackColor) == 0)
						return Color.Empty;
					return i2c((_data & MaskBackColor) >> 12);
				}
				set
				{
					if (value.IsEmpty)
						_data &= ~MaskApplyBackColor;
					else
						_data = (_data & ~MaskBackColor) | (c2i(value) << 12) | MaskApplyBackColor;
				}
			}
			private bool ShouldSerializeBackColor() { return (_data & MaskApplyBackColor) != 0; }
			private void ResetBackColor() { _data &= ~MaskApplyBackColor; }

			public int GetData()
			{
				return _data;
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
				if (value is State)
				{
					if (destinationType == typeof(string))
						return string.Empty;
					if (destinationType == typeof(InstanceDescriptor))
					{
						var state = (State)value;
						var ctor = typeof(State).GetConstructor(new Type[] { typeof(int), typeof(Image) });
						if (ctor != null)
							return new InstanceDescriptor(ctor, new object[] { state.GetData(), state.Image });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				if (propertyValues == null)
					return null;

				object state = new State
				{
					ResizeImage = (bool)propertyValues["ResizeImage"],
					TintAdditive = (bool)propertyValues["TintAdditive"],
					Image = (Image)propertyValues["Image"],
					ImageColor = (Color)propertyValues["ImageColor"],
					BackColor = (Color)propertyValues["BackColor"],
				};
				return state;
			}
		}

		//Convert int(12 bits) to Color
		private static Color i2c(int i)
		{
			int r = i & 0xf00;
			int g = i & 0xf0;
			int b = i & 0xf;
			return Color.FromArgb(
				(r >> 4) | (r >> 8),
				g | (g >> 4),
				(b << 4) | b);
		}

		//Convert Color to int(12 bits)
		private static int c2i(Color c)
		{
			int i = c.ToArgb();
			return ((i & 0xf00000) >> 12) | ((i & 0xf000) >> 8) | ((i & 0xf0) >> 4);
		}

		/// <summary>
		/// An extra state for
		/// </summary>
		private class _ImageState
		{
			public Image image;
			public Rectangle area;
		}

		#endregion State

		#region Properties

		/// <summary>
		/// The normal state
		/// </summary>
		[Category("Image Button States")]
		[DisplayName("Normal")]
		public State NormalState
		{
			get { return GetState(0); }
			set { SetState(0, value); }
		}
		private void ResetNormalState() { _ResetState(0); }
		private bool ShouldSerializeNormalState() { return _ShouldSerializeState(0); }

		[Category("Image Button States")]
		[DisplayName("Hover")]
		public State HoverState
		{
			get { return GetState(1); }
			set { SetState(1, value); }
		}
		private void ResetHoverState() { _ResetState(1); }
		private bool ShouldSerializeHoverState() { return _ShouldSerializeState(1); }

		[Category("Image Button States")]
		[DisplayName("Press")]
		public State PressState
		{
			get { return GetState(2); }
			set { SetState(2, value); }
		}
		private void ResetPressState() { _ResetState(2); }
		private bool ShouldSerializePressState() { return _ShouldSerializeState(2); }

		//Override BackColor, to represent the background color of the current state
		[Browsable(false)]
		public override Color BackColor
		{
			get
			{
				int index = _currentState;
				var data = _baseData[index];

				if ((data & State.MaskApplyBackColor) != 0) //apply back color
					return i2c((data & State.MaskBackColor) >> 12);
				else
					return DefaultBackColor;
			}

			set
			{
				int index = _currentState;
				var st = GetState(index);
				st.BackColor = value;
				SetState(index, st);
			}
		}

		#endregion Properties

		#region Private Fields

		private static ImageAttributes _imgAttrs = new ImageAttributes();
		private static ColorMatrix _colorMx = new ColorMatrix();

		//0: normal, 1: hover, 2: press
		private int _currentState;
		private int[] _baseData;
		private _ImageState[] _imageStates;

		#endregion Private Fields

		#region Main

		public ImageButton()
		{
			//initialize states
			int stateCount = StateCount;
			_baseData = new int[stateCount];
			_imageStates = null;
			_currentState = 0;

			//init size
			Size = new Size(50, 50);
		}

		/// <summary>
		/// Override this property if you need more states, default value is 3
		/// </summary>
		protected virtual int StateCount
		{
			get { return 3; }
		}

		#endregion Main

		#region Events

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			_ImageState imgState = GetImageState();
			if (imgState == null || imgState.image == null)
				return;

			SetColorMatrix();
			var img = imgState.image;
			e.Graphics.DrawImage(img, imgState.area, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, _imgAttrs);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			ChangeCurrentState(2);
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			ChangeCurrentState(1);
			base.OnMouseUp(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			ChangeCurrentState(1);
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			ChangeCurrentState(0);
			base.OnMouseLeave(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			RefreshDrawRect();
		}

		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			RefreshDrawRect();
		}

		#endregion Events

		#region Utilities

		/// <summary>
		/// Get a state object at the specific index
		/// </summary>
		protected State GetState(int index)
		{
			var imageStates = _imageStates;
			var image = imageStates != null && index < imageStates.Length ?
				_imageStates[index].image : null;
			return new State(_baseData[index], image);
		}

		/// <summary>
		/// Set a state object at the specific index
		/// </summary>
		protected void SetState(int index, State state)
		{
			int data = state.GetData();
			var imageStates = _imageStates;
			var image = state.Image;
			if (image != null)
			{
				_baseData[index] = data | State.MaskSizeDirty;

				if (imageStates == null)
				{
					// create image states array
					imageStates = _imageStates = index == 0 ?
						new _ImageState[1] : new _ImageState[StateCount];
				}
				else if (index >= imageStates.Length)
				{
					// enlarge image states array
					Array.Resize(ref imageStates, StateCount);
					_imageStates = imageStates;
				}
				else if (imageStates[index] != null)
				{
					// set image
					imageStates[index].image = image;
					goto END_SET_STATE;
				}

				// create image state object
				imageStates[index] = new _ImageState { image = image };
			}
			else
			{
				_baseData[index] = data;

				if (imageStates != null && index < imageStates.Length && imageStates[index] != null)
					imageStates[index].image = null;
			}

		END_SET_STATE:
			// refresh the current state
			if(index == _currentState)
				Invalidate();
		}

		/// <summary>
		/// Change the current state
		/// </summary>
		protected void ChangeCurrentState(int index)
		{
			if (_currentState == index)
				return;

			_currentState = index;
			Invalidate();
		}

		private void SetColorMatrix()
		{
			int data = _baseData[_currentState];
			float r = ((data & 0xf00) >> 8) / 15f;
			float g = ((data & 0xf0) >> 4) / 15f;
			float b = (data & 0xf) / 15f;
			var mx = _colorMx;

			if ((data & State.MaskTintAdditive) != 0)
			{
				mx.Matrix00 = mx.Matrix11 = mx.Matrix22 = 1;
				mx.Matrix40 = r;
				mx.Matrix41 = g;
				mx.Matrix42 = b;
			}
			else
			{
				mx.Matrix00 = r;
				mx.Matrix11 = g;
				mx.Matrix22 = b;
				mx.Matrix40 = mx.Matrix41 = mx.Matrix42 = 0;
			}

			_imgAttrs.SetColorMatrix(mx);
		}

		/// <summary>
		/// Get the current or the first image and its rectangle
		/// </summary>
		private _ImageState GetImageState()
		{
			var states = _imageStates;
			if (states == null)
				return null;

			int index = _currentState;
			if (index >= states.Length || states[index] == null)
				index = 0;

			_ImageState state = states[index];
			int data = _baseData[index];
			if((data & State.MaskSizeDirty) != 0)
			{
				_baseData[index] = data & ~State.MaskSizeDirty;
				RefreshDrawRect(state, (data & State.MaskResizeImage) != 0);
			}
			return state;
		}

		/// <summary>
		/// Mark all states as size dirty
		/// </summary>
		private void RefreshDrawRect()
		{
			var stateCount = StateCount;
			for (int i = 0; i < stateCount; i++)
				_baseData[i] |= State.MaskSizeDirty;
			Invalidate();
		}

		/// <summary>
		/// Compute the rectangle of the image
		/// </summary>
		private void RefreshDrawRect(_ImageState state, bool resize)
		{
			Image image = state.image;
			if (image == null)
				return;

			var pad = Padding;
			int imgW = image.Width;
			int imgH = image.Height;
			int cW = Width - pad.Horizontal;
			int cH = Height - pad.Vertical;

			if (resize)
			{
				if (cW <= 0 || cH <= 0)
				{
					state.area = new Rectangle();
					return;
				}

				double sw = (double)imgW / cW;
				double sh = (double)imgH / cH;
				if (sw < sh)
					sw = sh;
				imgW = (int)Math.Round(imgW / sw);
				imgH = (int)Math.Round(imgH / sw);
			}

			state.area = new Rectangle(
				pad.Left + (cW - imgW) / 2,
				pad.Top + (cH - imgH) / 2,
				imgW,
				imgH
			);
		}

		protected void _ResetState(int index)
		{
			_baseData[index] = 0;

			var imgStates = _imageStates;
			if (imgStates != null && index < imgStates.Length && imgStates[index] != null)
				imgStates[index].image = null;
		}

		protected bool _ShouldSerializeState(int index)
		{
			if (_baseData[index] != 0)
				return true;

			var imgStates = _imageStates;
			return imgStates != null && index < imgStates.Length && imgStates[index] != null
				&& imgStates[index].image != null;
		}

		#endregion Utilities
	}
}
