using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApp.Utils
{
	public interface ISecretRevealer
	{
		(string user, string pass) Reveal();
	}
}
