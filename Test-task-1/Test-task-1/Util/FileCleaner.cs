using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_task_1
{
    public class FileCleaner
    {
        private string directoryPath;

        public FileCleaner(string path)
        {
            this.directoryPath = path;
        }
        public void DeleteAllFiles()
        {
            if (Directory.Exists(directoryPath))
            {
                try
                {
                    // Получаем все файлы в указанной директории
                    string[] files = Directory.GetFiles(directoryPath);

                    // Удаляем каждый файл
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    Console.WriteLine("All file have been successfully deleted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurs while deleting files: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Directory not found.");
            }
        }
    }
}
