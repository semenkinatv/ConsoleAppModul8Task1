using System;
using System.IO;
namespace Modul8Task1 
{
    class Program
    {

        static void Main(string[] args)
        {
            string delpath = @"/Semenkina/task1"; //Зададим каталог для очистки
            ClearDirect(delpath);
        }  

        static void ClearDirect(string Delpath)
        {   //чистит нужную нам папку от файлов  и папок, которые не использовались более 30 минут 
            string delpath = Delpath;

            try
            {
                if (Directory.Exists(delpath))
                {
                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(delpath);// Получим все файлы корневого каталога

                    foreach (string s in files)   // Выведем их все
                    {
                        /* GetLastAccessTime() в windows не отражает дату последнего использования
                          будем использовать дату последнего изменения файла File.GetLastWriteTime()
                        */
                        Console.Write($"Файл {s} - изменен {File.GetLastWriteTime(s)}");

                        if (DateTime.Now - File.GetLastWriteTime(s) > TimeSpan.FromMinutes(30))
                        {

                            File.Delete(s);
                            Console.Write(" - УДАЛЕН.");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                    Console.WriteLine("Папки:");
                    string[] dirs = Directory.GetDirectories(delpath);  // Получим все директории корневого каталога

                    foreach (string d in dirs) // Выведем их все
                    {
                        Console.Write($"Папка {d} - изменена {Directory.GetLastWriteTime(d)}");

                        if (DateTime.Now - Directory.GetLastWriteTime(d) > TimeSpan.FromMinutes(30))
                        {

                            Directory.Delete(d, true);

                            Console.Write(" - УДАЛЕНА.");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Очистка невозможна. Папка по заданному адресу ({delpath}) - не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
