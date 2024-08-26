using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Xml.Linq;
using Test_task_1.Model;
using Test_task_1.Util;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Test_task_1
{
    public partial class Form1 : Form
    {

        public string dirPath = Path.Combine(Environment.CurrentDirectory, "GeneratedFiles");
        AppContext db = new AppContext();
        public Form1()
        {
            InitializeComponent();
        }

        /*Задание 1
        Сгенерировать 100 текстовых файлов со следующей структурой, каждый из которых содержит 100 000 строк
            случайная дата за последние 5 лет
         || случайный набор 10 латинских символов
         || случайный набор 10 русских символов
         || случайное положительное четное целочисленное число в диапазоне от 1 до 100 000 000
         || случайное положительное число с 8 знаками после запятой в диапазоне от 1 до 20*/

        #region[Task1]
        private void GenerateFiles()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "GeneratedFiles");

            Directory.CreateDirectory(path);

            for (int fileIndex = 0; fileIndex <= 100; fileIndex++)
            {
                //создание файла
                string filePath = Path.Combine(path, $"File_№{fileIndex}.txt");

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

                        //вывод кол-ва строк, которые были добавлены
                        if (lineIndex % 1000 == 0)
                        {
                            progressTextBox.AppendText($"File {fileIndex}: {lineIndex / 1000}k lines written" + Environment.NewLine);
                            progressTextBox.ScrollToCaret();
                        }
                    }
                }
                //оповещение о том, что файл был заполнен
                progressTextBox.AppendText($"File {fileIndex} completed" + Environment.NewLine);
                progressTextBox.ScrollToCaret();
            }
            progressTextBox.AppendText("All files have been generated successfully!" + Environment.NewLine);
            progressTextBox.ScrollToCaret();
        }

        //генерация случайной даты в диапазоне (текущая дата - 5 лет, текущая дата)
        private string GenerateRandomDate()
        {
            Random random = new Random();
            DateTime start = DateTime.Now.AddYears(-5);
            DateTime end = DateTime.Now;
            int range = (end - start).Days;
            DateTime randomDate = start.AddDays(random.Next(range));
            return randomDate.ToString("dd.MM.yyyy");
        }

        //генерация строки со случайными символами латиницы
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

        //генерация строки со случайными символами кириллицы
        private string GenerateRandomCyrillicSymbols(int lenght)
        {
            Random rand = new Random();
            const string symbols = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЭЮЯабвгдеёжзийклмнопрстуфхцчшщьюя";
            StringBuilder stringBuilder = new StringBuilder(lenght);
            for (int i = 0; i < lenght; i++)
            {
                stringBuilder.Append(symbols[rand.Next(symbols.Length)]);
            }
            return stringBuilder.ToString();
        }

        //генерация четного числа
        private int GenerateRandomEven()
        {
            Random rand = new Random();
            int number = rand.Next(1, 50000000) * 2;

            return number;
        }

        //генерация числа с плавающей запятой
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
#endregion

        //опциональная кнопка, для удаления всех файлов из папки "Generated files"
        #region[OptionalDelete]
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

#endregion

        //Задание 2
        //Реализовать объединение файлов в один. При объединении должна быть возможность удалить из всех файлов строки с заданным
        //сочетанием символов, например, «abc» с выводом информации о количестве удаленных строк
        #region[Task2]
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
            //проверка, существует ли директория
            if (!Directory.Exists(dirPath))
            {
                MessageBox.Show("Directory does not exist.");
                return;
            }

            //создание файла MergedFile.txt
            string mergedFilePath = Path.Combine(dirPath, "MergedFile.txt");
            int removedLinesCount = 0;
            try
            {
                using (StreamWriter writer = new StreamWriter(mergedFilePath, false, Encoding.UTF8))
                {
                    //получение массива файлов
                    string[] files = Directory.GetFiles(dirPath);
                    foreach (string file in files)
                    {
                        //инициализация переменной, которая будет хранить название файла для читабельного вывода
                        string fileName = Path.GetFileName(file);
                        progressMergedTextBox.AppendText($"Searching line {searchText} in file {fileName}" + Environment.NewLine);
                        //блок кода на то, что файл MergedFile.txt при поиске строки будет игнорироваться
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
#endregion

        //Задание 3
        //Создать процедуру импорта файлов с таким набором полей в таблицу в СУБД.
        //При импорте должен выводится ход процесса (сколько строк импортировано, сколько осталось)
        #region[Task3]
        private void databaseTransferButton_Click(object sender, EventArgs e)
        {
            databaseTransferedInfoTextBox.Clear();
            ImportFiles();

        }

        private void ImportFiles()
        {
            if (!Directory.Exists(dirPath))
            {
                progressTextBox.AppendText("Directory does not exist." + Environment.NewLine);
                return;
            }
            //получение файлов в виде массива
            string[] files = Directory.GetFiles(dirPath);
            databaseTransferedInfoTextBox.AppendText("Retrieving files" + Environment.NewLine);
            //получение суммарного количества строк файла
            int totalRows = files
                .Where(file => !Path.GetFileName(file).Equals("MergedFile.txt", StringComparison.OrdinalIgnoreCase))
                .Sum(file => CountLinesInFile(file));
            databaseTransferedInfoTextBox.AppendText("Retrieving a sum of a strings!" + Environment.NewLine);

            //счетчик строк, которые были импортированы
            int importedRows = 0;
            foreach (string file in files)
            {
                if (Path.GetFileName(file).Equals("MergedFile.txt", StringComparison.OrdinalIgnoreCase)) continue;
                importedRows += ImportFile(file);
                //остаток строк
                int remainingRows = totalRows - importedRows;
                databaseTransferedInfoTextBox.AppendText($"Imported {importedRows} rows. {remainingRows} rows remaining." + Environment.NewLine);
            }

            databaseTransferedInfoTextBox.AppendText("Import completed." + Environment.NewLine);
        }

        private int CountLinesInFile(string filePath)
        {
            int lineCount = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            return lineCount;
        }
        private int ImportFile(string filePath)
        {
            int rowCount = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //разделение строки из файла на части
                    string[] parts = line.Split('|');

                    string[] cleanedArray = parts.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    if (cleanedArray.Length != 5)
                    {
                        // обработка некорректного формата
                        continue;
                    }

                    //создание сущности
                    var data = new FileEntity
                    {
                        Date = DateTime.Parse(cleanedArray[0]),
                        LatinChars = cleanedArray[1],
                        CyrillicChars = cleanedArray[2],
                        EvenNumber = int.Parse(cleanedArray[3]),
                        FloatNumber = decimal.Parse(cleanedArray[4])
                    };
                    //добавление в таблицу Files
                    db.Files.Add(data);
                    rowCount++;
                }

                db.SaveChanges(); //сохранение изменений

            }
            return rowCount;
        }
#endregion

        //Задание 4
        //Реализовать хранимую процедуру в БД (или скрипт с внешним sql-запросом),
        //который считает сумму всех целых чисел и медиану всех дробных чисел
        #region[Task4]
        private void calculateSumNMedian_Click(object sender, EventArgs e)
        {
            calculationResultTextBox.Clear();
            CalculateSumAndMedian();
        }

        private void CalculateSumAndMedian()
        {
            var connectionString = "Server=DESKTOP-1D3P714\\SQLEXPRESS;Database=HundredFiles;Trusted_Connection=True;MultipleActiveResultSets=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("CalculateSumAndMedian", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var sumEvenNumbers = reader.GetInt64(reader.GetOrdinal("SumEvenNumbers"));
                            var medianFloatNumber = reader.IsDBNull(reader.GetOrdinal("MedianFloatNumber"))
                                ? (decimal?)null
                                : reader.GetDecimal(reader.GetOrdinal("MedianFloatNumber"));

                            // Отображаем результаты в текстовом поле
                            calculationResultTextBox.AppendText($"Sum of Even Numbers: {sumEvenNumbers}" + Environment.NewLine);
                            calculationResultTextBox.AppendText($"Median of Float Numbers: {(medianFloatNumber.HasValue ? medianFloatNumber.Value.ToString() : "N/A")}" + Environment.NewLine);
                        }
                    }
                }
            }
        }
        #endregion

    }
}