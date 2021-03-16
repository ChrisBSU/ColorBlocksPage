using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Xamarin.Forms;

namespace ColorBlocksPage
{
	public class ColorBlockDemo : ContentPage
	{
		public ColorBlockDemo()
		{
			StackLayout stackLayout = new StackLayout();

			//Loop through all the Fields in the Color Struct
			foreach(FieldInfo info in typeof(Color).GetRuntimeFields())
			{
				//Skip the obsolete (i.e. misspelled) colors.
				if(info.GetCustomAttribute<ObsoleteAttribute>() != null)
				{
					continue;
				}
				if(info.IsPublic && info.IsStatic && info.FieldType == typeof(Color))
				{
					stackLayout.Children.Add(CreateColorView((Color)info.GetValue(null), info.Name));
				}
			}

			//Loop through the Color struct properties
			foreach(PropertyInfo info in typeof(Color).GetRuntimeProperties())
			{
				MethodInfo methodInfo = info.GetMethod;
				if(methodInfo.IsPublic && methodInfo.IsStatic && methodInfo.ReturnType == typeof(Color))
				{
					stackLayout.Children.Add(CreateColorView((Color)info.GetValue(null), info.Name));
				}
			}

			Padding = new Thickness(10);

			Content = new ScrollView
			{
				Content = stackLayout,
			};
			
		}

		private View CreateColorView(Color color, string name)
		{
			return new Frame
			{
				BorderColor = Color.Accent,
				Padding = new Thickness(10),

				Content = new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Spacing = 15,
					Children =
					{
						new BoxView
						{
							Color = color,
						},

						new Label
						{
							Text = name,
							FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
							FontAttributes = FontAttributes.Bold,
							VerticalOptions = LayoutOptions.Center,
							HorizontalOptions = LayoutOptions.StartAndExpand,
						},

						new StackLayout
						{
							HorizontalOptions = LayoutOptions.End,
							Children =
							{
								new Label
								{
									//$ is shorthand for String.Format()
									Text = $"{(int)(255*color.R):X2}-{(int)(255*color.G):X2}-{(int)(255*color.B):X2}",
									VerticalOptions = LayoutOptions.CenterAndExpand,
									IsVisible = (color != Color.Default),
								},
								new Label
								{
									//$ is shorthand for String.Format()
									Text = $"{(int)(360*color.Hue):F0}-{(int)(100*color.Saturation):F0}-{(int)(100*color.Luminosity):F0}",
									VerticalOptions = LayoutOptions.CenterAndExpand,
									IsVisible = (color != Color.Default),
								},
							},							
						},
					},
				},
			};
		}
	}
}