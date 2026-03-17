using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TT
{
	public class PublicVariables
	{
		public static Dictionary<string, Dictionary<string, string>> i18nStringsTable;
		public static string selectedCulture = "en-US";
	}

    public enum PracticeDiffculty
    {
		Easy = 26,
		Medium = 62,
		Hard = 100,
		VeryHard = 125
    }

    internal class PracticeGenerator
	{
		public PracticeGenerator(PracticeDiffculty practiceDiffculty) {
			charCount = (int)practiceDiffculty;
        }
        public class OnePractice {
			public string GeneratedString { get; set; }
			public TimeSpan TimeTaken { get; set; }
			public YAMLDateTimeWithTimeZone CreateDateAndTime { get; set; }
		}
        public class YAMLDateTimeWithTimeZone
        {
			public string DateAndTime { get; set; }
			public string TimeZone { get; set; }
        }
        Random _randomAlg = new();
		protected static string _charList = "~!@#$%^&*()_+QWERTYUIOP{}|ASDFGHJKL:\"ZXCVBNM<>?`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./";
        protected int charCount = _charList.Length;
        protected string RandomStringConstructor()
		{
			char[] resultCharSequence = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				resultCharSequence[i] = _charList[_randomAlg.Next(_charList.Length)];
			}
			return new string(resultCharSequence);
		}
        public void Invoke()
        {
            string randomString = RandomStringConstructor();
            while (true)
            {
                var start = DateTime.Now;
                Console.WriteLine(string.Format(PublicVariables.i18nStringsTable[PublicVariables.selectedCulture]["generated_msg"], randomString));
                string repeat = Console.ReadLine() ?? "";
                if (repeat == randomString)
                {
                    var end = DateTime.Now;
                    TimeSpan timeSpan = (DateTime.Now - start);
                    var PCTT =
                        (timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 +
                        timeSpan.Minutes * 60 + timeSpan.TotalSeconds) / charCount;
                    Console.WriteLine(string.Format(PublicVariables.i18nStringsTable[PublicVariables.selectedCulture]["challenge_successfully"], charCount));
                    Console.WriteLine("Total: " +
                        timeSpan.ToString("g", new CultureInfo(PublicVariables.selectedCulture)) +
                        ", Per Character Time Taken (in seconds): " + PCTT);
                    var saveComfirmPrompt = new ConfirmationPrompt("Save this?");
                    saveComfirmPrompt.ChoicesStyle = Style.Parse("chartreuse2 bold");
                    saveComfirmPrompt.DefaultValueStyle = Style.Parse("mediumspringgreen bold");
                    bool saveComfirm = AnsiConsole.Prompt(saveComfirmPrompt);
                    if (saveComfirm) SaveGame(randomString, end - start, start);
                    return;
                }
                else
                {
                    Console.WriteLine(PublicVariables.i18nStringsTable[PublicVariables.selectedCulture]["incorrect"]);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(intercept: true);
                    Console.Clear();
                    continue;
                }
            }
        }
        public void SaveGame(string generatedString, TimeSpan timeTaken, DateTime createdTime) { 
			OnePractice save = new OnePractice()
			{
				GeneratedString = generatedString,
				TimeTaken = timeTaken,
				CreateDateAndTime = new YAMLDateTimeWithTimeZone()
				{
					DateAndTime = createdTime.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
					TimeZone = TimeZoneConverter.TZConvert.WindowsToIana(TimeZoneInfo.Local.Id)
                }
            };
			var yamlSerializer = new SerializerBuilder()
				.WithNamingConvention(HyphenatedNamingConvention.Instance)
				.Build();
			var yaml = yamlSerializer.Serialize(save);
			File.WriteAllText($"{DateTime.Now:yyyyMMdd_HHmmss`fff}-{TimeZoneConverter.TZConvert.WindowsToIana(TimeZoneInfo.Local.Id).Replace("/","_")}.ttsave", yaml);

		}
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			PublicVariables.i18nStringsTable = new Dictionary<string, Dictionary<string, string>>()
			{
				{ "en-US", new Dictionary<string, string>()
					{
						{ "generated_msg", "Please type the following string: {0}" },
						{ "challenge_successfully", "Congratulations! You have successfully completed the challenge with {0} characters!" },
						{ "incorrect", "Incorrect input. Please try again." },
						{ "select_difficulty", "please select [greenyellow]practice difficulty for you[/]" },
						{ "try_again", "Try Again?" }
					}
				},
				{ "zh-TW", new Dictionary<string, string>()
					{
						{ "generated_msg", "請輸入以下字串: {0}" },
						{ "challenge_successfully", "恭喜！你已成功完成包含 {0} 個字元的挑戰！" },
						{ "incorrect", "輸入錯誤。請再試一次。" },
						{ "select_difficulty", "請選擇適合你的[greenyellow]練習難度[/]" },
						{ "try_again", "再試一次？" }
					}
				},
				{ "yue-Hant-HK", new Dictionary<string, string>()
					{
						{ "generated_msg", "請輸入以下字串: {0}" },
						{ "challenge_successfully", "恭喜！你已成功完成包含 {0} 個字元嘅挑戰！" },
						{ "incorrect", "輸入錯誤。請再試過。" },
						{ "select_difficulty", "請揀啱你嘅[greenyellow]練習難度[/]" },
						{ "try_again", "係咪再試多次？" }
					}
				},
                {
					"zh-CN", new Dictionary<string, string>()
					{
						{ "generated_msg", "请输入文本: {0}" },
						{ "challenge_successfully", "恭喜！你已成功完成包含 {0} 个字的练习！" },
						{ "incorrect", "输入错误！请检查后再试。" },
						{ "select_difficulty", "请选择你的[greenyellow]练习难度[/]" },
						{ "try_again", "再来一[strikethrough dim]瓶[/]次？" }
					}
				}
			};
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.Clear();
			language_choice:
			Console.Clear();
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Select your [greenyellow]language[/] / 選擇你的[greenyellow]語言[/] / 請揀你嘅[greenyellow]語言[/] / 选择你的[greenyellow]语言[/]")
					.AddChoices(new[] { "English", "繁體中文（臺灣）", "粵語（繁體）", "简体中文" })
					.HighlightStyle("Chartreuse3 on DarkOliveGreen1_1 bold")
			);
			switch (choice)
			{
				case "English":
					PublicVariables.selectedCulture = "en-US";
					break;
				case "繁體中文（臺灣）":
					PublicVariables.selectedCulture = "zh-TW";
					break;
				 case "粵語（繁體）":
					PublicVariables.selectedCulture = "yue-Hant-HK";
					break;
				case "简体中文":
					PublicVariables.selectedCulture = "zh-CN";
					break;
			}
			while (true) {
				Console.Clear();
				practice_diffculty:
				PracticeGenerator practice;
				choice = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.Title(PublicVariables.i18nStringsTable[PublicVariables.selectedCulture]["select_difficulty"])
						.AddChoices(new[] { "Easy (26 alphabets and number)", "Medium (62 alphabets and number)", "Hard (100 alphabets and number)", "Very Hard (125 alphabets and number)", "Debug" })
						.HighlightStyle("chartreuse1 bold")
				);
				switch (choice)
				{
					case "Easy (26 alphabets and number)":
						practice = new PracticeGenerator(PracticeDiffculty.Easy);
						AnsiConsole.MarkupLine("[bold greenyellow]You will into a easy practice: 3 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold yellow]You will into a easy practice: 2 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold orange1]You will into a easy practice: 1 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						practice.Invoke();
						break;
					case "Medium (62 alphabets and number)":
						practice = new PracticeGenerator(PracticeDiffculty.Medium);
						AnsiConsole.MarkupLine("[bold greenyellow]You will into a medium practice: 3 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold yellow]You will into a medium practice: 2 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold orange1]You will into a medium practice: 1 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						practice.Invoke();
						break;
					case "Hard (100 alphabets and number)":
						practice = new PracticeGenerator(PracticeDiffculty.Hard);
						AnsiConsole.MarkupLine("[bold greenyellow]You will into a hard practice: 3 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold yellow]You will into a hard practice: 2 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold orange1]You will into a hard practice: 1 seconds!![/]");
						break;
					case "Very Hard (125 alphabets and number)":
						practice = new PracticeGenerator(PracticeDiffculty.VeryHard);
                        AnsiConsole.MarkupLine("[bold greenyellow]You will into a very hard practice: 3 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold yellow]You will into a very hard practice: 2 seconds[/]");
						Thread.Sleep(1000);
						Console.Clear();
						AnsiConsole.MarkupLine("[bold orange1]You will into a very hard practice: 1 seconds!![/]");
						Thread.Sleep(1000);
						Console.Clear();
						practice.Invoke();
						break;
					case "Debug":
					debug:
						var gotoPart = AnsiConsole.Prompt(
							new SelectionPrompt<string>()
							.Title("Please select a part to run")
							.AddChoices(new[] { "Language Choose", "Practice Difficulty Choose", "Debug mode (There)", "\"Try Again\" Confirmation", "Traslation Table of Current Language" })
							.HighlightStyle("chartreuse1 bold"));
						Console.Clear();
						switch (gotoPart)
						{
							case "Language Choose":
								goto language_choice;
							case "Practice Difficulty Choose":
								goto practice_diffculty;
							case "Debug mode (There)":
								goto debug;
							case "\"Try Again\" Confirmation":
								goto try_again_confirmation;
							case "Traslation Table of Current Language":
								Table table = new Table();
								table.Border = TableBorder.Square;
								table.AddColumn("Key");
								table.AddColumn("Value");
                                foreach (var item in PublicVariables.i18nStringsTable[PublicVariables.selectedCulture])
                                {
									table.AddRow(item.Key, item.Value);
                                }
								AnsiConsole.Write(table);
                                break;
                        }
						break;
				}
				try_again_confirmation:
				bool confirm = AnsiConsole.Confirm(
					prompt: PublicVariables.i18nStringsTable[PublicVariables.selectedCulture]["try_again"], 
					defaultValue: true
				);
				if (!confirm) {
					break;
				}
			}
		}
	}
}
