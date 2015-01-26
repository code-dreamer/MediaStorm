using System;
using System.Windows;

namespace MediaStorm
{
	// Entrypoint
	static class Program
	{
		[STAThread]
		private static int Main(/*string[] args*/)
		{
			return new App().Run();
		}
	}
}