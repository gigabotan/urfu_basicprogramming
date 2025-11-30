using System.Collections.Generic;
using System.Collections.ObjectModel;
using AngryBirds.TestCases;
using Avalonia.Controls;
using Avalonia.Media;
using System.Threading.Tasks;

namespace AngryBirds.UI;

public partial class MainWindow : Window
{
	private ObservableCollection<string> TestCasesDisplay { get; }
	private List<TestCase> TestCasesContexts { get; }

	private readonly Brush passTest = new SolidColorBrush(Color.FromArgb(0xFF, 0x7F, 0xFF, 0xD4));
	private readonly Brush failTest = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xB6, 0xC1));

	private readonly TestCaseUI testCaseUi;
	private readonly Canvas canvas;
	private readonly ListBox list;

	private int failedCount;
	private int passedCount;

	public MainWindow()
	{
		InitializeComponent(true);

		canvas = this.FindControl<Canvas>("TestView");
		list = this.FindControl<ListBox>("Tests");

		testCaseUi = new TestCaseUI(this.FindControl<TextBlock>("Log"), canvas);

		TestCasesDisplay = new ObservableCollection<string>();
		TestCasesContexts = new List<TestCase>();

		list.ItemsSource = TestCasesDisplay;

		list.SelectionChanged += async (_, __) => await TestCaseClick();
		Opened += async (_, __) => await OnOpen();
	}

	private async Task OnOpen()
	{
		var firstFailed = TestCasesContexts.FindIndex(c => c.LastResult != TestResult.Passed);
		list.SelectedIndex = firstFailed > -1
			? firstFailed
			: TestCasesContexts.Count - 1;

		if(list.SelectedIndex == -1) 
			list.SelectedIndex = 0;
	}

	private async Task TestCaseClick()
	{
		if (list.SelectedIndex < 0 || list.SelectedIndex >= TestCasesContexts.Count)
			return;

		var testCase = TestCasesContexts[list.SelectedIndex];
		testCaseUi.Clear();
		await testCase.Run();
		testCase.Visualize(testCaseUi);
		canvas.Background = testCase.LastResult == TestResult.Passed ? passTest : failTest;
	}

	public void CreateTestsCases(IEnumerable<TestCase> testCases)
	{
		foreach (var testCase in testCases)
		{
			testCase.Run().GetAwaiter().GetResult();
			var passed = testCase.LastResult == TestResult.Passed;

			TestCasesDisplay.Add((passed ? "✅" : "❌") + testCase.Name);
			TestCasesContexts.Add(testCase);

			if (passed) passedCount++;
			else failedCount++;
		}

		Title += $" (passed: {passedCount}, failed: {failedCount})";
	}
}