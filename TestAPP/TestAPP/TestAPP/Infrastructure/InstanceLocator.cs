using System;
using System.Collections.Generic;
using System.Text;
using TestAPP.ViewModels;
namespace TestAPP.Infrastructure
{
	public class InstanceLocator
	{
		#region Properties

		public MainViewModel Main { get; set; }

		#endregion

		#region Constructor
		public InstanceLocator()
		{
			this.Main = new MainViewModel();
		}
		#endregion
	}
}
