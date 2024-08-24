using System.Text;

namespace Test_task_1
{
    public partial class Form1 : Form
    {
        public string dirPath = Path.Combine(Environment.CurrentDirectory, "GeneratedFiles");
        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateFiles()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "GeneratedFiles");

            Directory.CreateDirectory(path);

            for (int fileIndex = 0; fileIndex <= 100; fileIndex++)
            {
                //�������� �����
                string filePath = Path.Combine(path, $"File_�{fileIndex}.txt");

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    for (int lineIndex = 0; lineIndex <= 100000; lineIndex++)
                    {
                        string date = GenerateRandomDate();
                        string latinChars = GenerateRandomLatinSymbols(10);
                        string cyrillicChars = GenerateRandomCyrillicSymbols(10);
                        int evenNumber = GenerateRandomEven();
                        double decimalNumber = GenerateRandomFloat();

                        string line = $"{date} || {latinChars} || {cyrillicChars} || {evenNumber} || {decimalNumber:F8}";
                        writer.WriteLine(line);

                        //����� ���-�� �����, ������� ���� ���������
                        if (lineIndex % 1000 == 0)
                        {
                            progressTextBox.AppendText($"File {fileIndex}: {lineIndex / 1000}k lines written" + Environment.NewLine);
                            progressTextBox.ScrollToCaret();
                        }
                    }
                }
                //���������� � ���, ��� ���� ��� ��������
                progressTextBox.AppendText($"File {fileIndex} completed" + Environment.NewLine);
                progressTextBox.ScrollToCaret();
            }
            progressTextBox.AppendText("All files have been generated successfully!" + Environment.NewLine);
            progressTextBox.ScrollToCaret();
        }

        //��������� ��������� ���� � ��������� (������� ���� - 5 ���, ������� ����)
        private string GenerateRandomDate()
        {
            Random random = new Random();
            DateTime start = DateTime.Now.AddYears(-5);
            DateTime end = DateTime.Now;
            int range = (end - start).Days;
            DateTime randomDate = start.AddDays(random.Next(range));
            return randomDate.ToString("dd.MM.yyyy");
        }

        //��������� ������ �� ���������� ��������� ��������
        private string GenerateRandomLatinSymbols(int lenght)
        {
            Random rand = new Random();
            const string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder stringBuilder = new StringBuilder(lenght);
            for (int i = 0; i < lenght; i++)
            {
                stringBuilder.Append(symbols[rand.Next(symbols.Length)]);
            }
            return stringBuilder.ToString();
        }

        //��������� ������ �� ���������� ��������� ���������
        private string GenerateRandomCyrillicSymbols(int lenght)
        {
            Random rand = new Random();
            const string symbols = "�����Ũ������������������������������������������������������";
            StringBuilder stringBuilder = new StringBuilder(lenght);
            for (int i = 0; i < lenght; i++)
            {
                stringBuilder.Append(symbols[rand.Next(symbols.Length)]);
            }
            return stringBuilder.ToString();
        }

        //��������� ������� �����
        private int GenerateRandomEven()
        {
            Random rand = new Random();
            int number = rand.Next(1, 50000000) * 2;

            return number;
        }

        //��������� ����� � ��������� �������
        private double GenerateRandomFloat()
        {
            Random rand = new Random();
            double number = rand.NextDouble() * 19 + 1;

            return Math.Round(number, 8);
        }

        private void generateFilesButton_Click(object sender, EventArgs e)
        {
            progressTextBox.Clear();
            GenerateFiles();
        }
        //������������ ������, ��� �������� ���� ������ �� ����� "Generated files"
        private void deleteFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                FileCleaner fileCleaner = new FileCleaner(dirPath);
                fileCleaner.DeleteAllFiles();
                progressTextBox.Clear();
                progressTextBox.Text = "All files in directory `Generated files` have been deleted!" + Environment.NewLine;
            }
            catch (Exception ex)
            {
                progressTextBox.Clear();
                progressTextBox.Text = $"Error: {ex.Message}";
            }
        }

        private void mergeButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a text to filter out.");
                return;
            }

            progressTextBox.Clear();
            MergeFiles(searchText);
        }

        private void MergeFiles(string searchText)
        {
            //��������, ���������� �� ����������
            if (!Directory.Exists(dirPath))
            {
                MessageBox.Show("Directory does not exist.");
                return;
            }

            //�������� ����� MergedFile.txt
            string mergedFilePath = Path.Combine(dirPath, "MergedFile.txt");
            int removedLinesCount = 0;
            try
            {
                using (StreamWriter writer = new StreamWriter(mergedFilePath, false, Encoding.UTF8))
                {
                    //��������� ������� ������
                    string[] files = Directory.GetFiles(dirPath);
                    foreach (string file in files)
                    {
                        //������������� ����������, ������� ����� ������� �������� ����� ��� ������������ ������
                        string fileName = Path.GetFileName(file);
                        progressMergedTextBox.AppendText($"Searching line {searchText} in file {fileName}" + Environment.NewLine);
                        //���� ���� �� ��, ��� ���� MergedFile.txt ��� ������ ������ ����� ��������������
                        if (file.Equals(mergedFilePath, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains(searchText))
                                {
                                    progressMergedTextBox.AppendText($"Searched text have been found in line: " + Environment.NewLine + line + Environment.NewLine);
                                    removedLinesCount++;
                                    progressMergedTextBox.AppendText($"Removed lines count = {removedLinesCount}" + Environment.NewLine);
                                }
                                else
                                {
                                    writer.WriteLine(line);
                                }
                            }
                        }
                    }
                }

                progressMergedTextBox.AppendText($"Files merged into {mergedFilePath}" + Environment.NewLine);
                progressMergedTextBox.AppendText($"Total removed lines: {removedLinesCount}" + Environment.NewLine);
                progressMergedTextBox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                progressMergedTextBox.AppendText($"Error during merging: {ex.Message}" + Environment.NewLine);
                progressMergedTextBox.ScrollToCaret();
            }
        }
    }
}