using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace BkTextFormatter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void PrependTextToLinesButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string textToPrepend = TextToPrependTextBox.Text;
			if (!string.IsNullOrEmpty(textToPrepend))
			{
				var sb = new StringBuilder();

				using (StringReader reader = new StringReader(LinesTextBox.Text))
				{
					string line = null;
					do
					{
						line = reader.ReadLine();
						if (line != null)
						{
							string appendedLine = textToPrepend + line;
							sb.AppendLine(appendedLine);
						}
					} while (line != null);
				}
				LinesTextBox.Text = sb.ToString();
			}
		}

		private void AppendTextToLinesButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string textToAppend = TextToAppendTextBox.Text;
			if (!string.IsNullOrEmpty(textToAppend))
			{
				var sb = new StringBuilder();

				using (StringReader reader = new StringReader(LinesTextBox.Text))
				{
					string line = null;
					do
					{
						line = reader.ReadLine();
						if (line != null)
						{
							string appendedLine = line + textToAppend;
							sb.AppendLine(appendedLine);
						}
					} while (line != null);
				}
				LinesTextBox.Text = sb.ToString();
			}
		}

		private void RemoveBlankLinesButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (line != null)
					{
						if (!string.IsNullOrWhiteSpace(line))
						{
							sb.AppendLine(line);
						}
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}

		private void RemoveEolsButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (line != null)
					{
						sb.Append(line); // Append with no EOL.
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}

		private void NewLineAfterDelimiterButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string delimiter = DelimiterForNewLineAfterDelimiterTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (line != null)
					{
						if (!string.IsNullOrWhiteSpace(line))
						{
							int delimiterCount = Regex.Matches(line, delimiter).Count;
							if (delimiterCount > 0)
							{

								string[] splitText = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

								int index = 0;
								foreach (string part in splitText)
								{
									sb.Append(part);
									if (index < delimiterCount)
									{
										sb.Append(delimiter);
									}
									sb.AppendLine();
									++index;
								}
							}
							else
							{
								sb.AppendLine(line);
							}
						}
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}

		private void NewLineAtDelimiterButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string delimiter = DelimiterForNewLineAtDelimiterTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (line != null)
					{
						if (!string.IsNullOrWhiteSpace(line))
						{
							int delimiterCount = Regex.Matches(line, delimiter).Count;
							if (delimiterCount > 0)
							{
								string[] splitText = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

								foreach (string part in splitText)
								{
									sb.AppendLine(part);
								}
							}
							else
							{
								sb.AppendLine(line);
							}
						}
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}

		private void NewLineToDelimiterButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string delimiter = DelimiterForNewLineToDelimiterTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (line != null)
					{
						if (!string.IsNullOrWhiteSpace(line))
						{
							if (sb.Length > 0)
							{
								sb.Append(delimiter);
							}
							sb.Append(line);
						}
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}

		private void DeleteTextOnLinesBeforeEndOfMarkTextButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string markText = DeleteTextOnLinesBeforeEndOfMarkTextTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (!string.IsNullOrWhiteSpace(line))
					{
						if (line.Contains(markText))
						{
							string[] splitText = line.Split(new string[] { markText }, StringSplitOptions.RemoveEmptyEntries);

							// Skip the part before the markText.
							for (int i = 1; i < splitText.Length; i++)
							{
								string part = splitText[i];
								sb.AppendLine(part);
							}
						}
						else
						{
							// No change for this line.
							sb.AppendLine(line);
						}
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}

		private void DeleteTextOnLinesAfterStartOfMarkTextTextButton_Click(object sender, RoutedEventArgs e)
		{
			string inText = LinesTextBox.Text;

			string markText = DeleteTextOnLinesAfterStartOfMarkTextTextBox.Text;

			var sb = new StringBuilder();

			using (StringReader reader = new StringReader(LinesTextBox.Text))
			{
				string line = null;
				do
				{
					line = reader.ReadLine();
					if (!string.IsNullOrWhiteSpace(line))
					{
						if (line.Contains(markText))
						{
							if (line.StartsWith(markText))
							{
								// Result is blank line.
								sb.AppendLine();
							}
							else
							{
								string[] splitText = line.Split(new string[] { markText }, StringSplitOptions.RemoveEmptyEntries);

								// Only do the first part, before the markText.
								string part = splitText[0];
								sb.AppendLine(part);
							}
						}
						else
						{
							// No change for this line.
							sb.AppendLine(line);
						}
					}
				} while (line != null);
			}
			LinesTextBox.Text = sb.ToString();
		}
	}
}
