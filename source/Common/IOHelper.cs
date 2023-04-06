using System.IO;

namespace Tasler.RenewedPowerCommands.Common
{
	public static class IOHelper
	{
		public static string SanitizePath(string path)
		{
			string text = path;
			if (text.IndexOf(" ") > -1)
			{
				text = "\"" + text + "\"";
			}
			return text;
		}

		public static string GetFileWithoutExtension(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				string[] array = Path.GetFileName(fileName).Split('.');
				if (array.Length > 0)
				{
					return array[0];
				}
			}
			return null;
		}

		public static string GetFileExtension(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				string[] array = Path.GetFileName(fileName).Split('.');
				if (array.Length > 0)
				{
					return "." + string.Join(".", array, 1, array.Length - 1);
				}
			}
			return null;
		}
	}
}
