using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
	public interface ISecretRevealer
	{
		(string user, string pass) Reveal();
	}
}
