using EFCore.BulkExtensions;
using FileWorker.Core;
using FileWorker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FileWorker.ViewModels
{
    internal class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private const int _loadingSize = 50000;
        private const int _NumberOfFiles = 100;
        private DBContext _dbContext;
        private List<StringModel> _DataList;
        private static DirectoryInfo dirInfo = new DirectoryInfo(@"D:\учёба\B1\FileWorker");

        public MainVM()
        {
            _dbContext = new DBContext();
            NumberOfCreatedFiles = 0;
            NumberOfJoinedFiles = 0;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private int _NumberOfCreatedFiles;
        public int NumberOfCreatedFiles
        {
            get { return _NumberOfCreatedFiles; }
            set
            {
                _NumberOfCreatedFiles = value;
                OnPropertyChanged(nameof(NumberOfCreatedFiles));
            }
        }

        private int _NumberOfJoinedFiles;
        public int NumberOfJoinedFiles
        {
            get { return _NumberOfJoinedFiles; }
            set
            {
                _NumberOfJoinedFiles = value;
                OnPropertyChanged(nameof(NumberOfJoinedFiles));
            }
        }

        private int _NumberOfDeletedStrings;
        public int NumberOfDeletedStrings
        {
            get { return _NumberOfDeletedStrings; }
            set
            {
                _NumberOfDeletedStrings = value;
                OnPropertyChanged(nameof(NumberOfDeletedStrings));
            }
        }

        private string _TextToDelete;

        public string TextToDelete
        {
            get { return _TextToDelete; }
            set
            {
                _TextToDelete = value;
                OnPropertyChanged(nameof(TextToDelete));
            }
        }

        private string _CountStatus;

        public string CountStatus
        {
            get { return _CountStatus; }
            set { _CountStatus = value; OnPropertyChanged(nameof(CountStatus)); }
        }


        private int _ExportedFiles;

        public int ExportedFiles
        {
            get { return _ExportedFiles; }
            set 
            { 
                _ExportedFiles = value;
                OnPropertyChanged(nameof(ExportedFiles));
            }
        }

        private int _ExportedStrings;

        public int ExportedStrings
        {
            get { return _ExportedStrings; }
            set 
            { 
                _ExportedStrings = value;
                OnPropertyChanged(nameof(ExportedStrings));
            }
        }

        private int _NumberOfDeletedStrings_DBExport;

        public int NumberOfDeletedStrings_DBExport
        {
            get { return _NumberOfDeletedStrings_DBExport; }
            set 
            { 
                _NumberOfDeletedStrings_DBExport = value; 
                OnPropertyChanged(nameof(NumberOfDeletedStrings_DBExport)); 
            }
        }

        private string _ExportingToDatabaseStatus;

        public string ExportingToDatabaseStatus
        {
            get { return _ExportingToDatabaseStatus; }
            set 
            { 
                _ExportingToDatabaseStatus = value;
                OnPropertyChanged(nameof(ExportingToDatabaseStatus));
            }
        }

        public ICommand CreateFiles
        {
            get
            {
                return new DelegateComand((obj) =>
                Task.Factory.StartNew(() =>
                {
                    TextGenerator generator = new TextGenerator();


                    for (int j = 0; j < _NumberOfFiles; ++j)
                    {
                        var list = generator.GetStringsRange(100000); //geting 100 000 strings for current file

                        string fileName = $@"D:\учёба\B1\FileWorker\{j}.txt"; //current directory

                        try
                        {
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }

                            // Create a new file 
                            using (FileStream fs = File.Create(fileName))
                            {
                                // Adding the strings to file
                                int count = list.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    Byte[] currentText = new UTF8Encoding(true).GetBytes(list[i]);
                                    fs.Write(currentText, 0, currentText.Length);
                                }
                            }

                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine(Ex.ToString());
                        }
                        ++NumberOfCreatedFiles;
                    }
                })
                );
            }
        }

        public ICommand JoinFiles
        {
            get
            {
                return new DelegateComand(obj => Task.Factory.StartNew(() =>
                {
                    var enFils = dirInfo.GetFiles().Where(i => i.Extension == ".txt").ToList();
                    NumberOfDeletedStrings = 0;
                    NumberOfJoinedFiles = 0;

                    try
                    {
                        string fileName = @"D:\учёба\B1\FileWorker\Joined files.txt";

                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        using (FileStream fs = File.Create(fileName))
                        {
                            int len = enFils.Count;
                            for (int i = 0; i < len; i++)
                            {

                                string buff;
                                string[] strings;
                                using (StreamReader sr = new StreamReader(enFils[i].FullName))
                                {
                                    buff = sr.ReadToEnd();
                                }

                                strings = buff.Split('\n');

                                int strSize = strings.Length;


                                for (int l = 0; l < strSize; l++)
                                {
                                    if (_TextToDelete != null && strings[l].Contains(_TextToDelete))
                                    {
                                        ++NumberOfDeletedStrings;
                                        strings[l] = string.Empty;
                                    }
                                    else
                                    {
                                        strings[l] += "\n";
                                    }
                                }

                                foreach (var str in strings)
                                {
                                    Byte[] currentText = new UTF8Encoding(true).GetBytes(str);
                                    fs.Write(currentText, 0, currentText.Length);
                                }
                                NumberOfJoinedFiles++;
                            }
                        }

                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }
                }));
            }
        }

        public ICommand ExportFiles
        {
            get 
            {
                return new DelegateComand(obj => Task.Factory.StartNew(() =>
                {
                    var l = _dbContext.Lines.ToList();
                    var enFils = dirInfo.GetFiles().Where(i => i.Extension == ".txt").ToList();
                    var lines = new List<StringModel>();

                    int len = enFils.Count;
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    for (int i = 0; i < len; ++i)
                    {

                        string buff;
                        var strings = new List<string>();
                        using (StreamReader sr = new StreamReader(enFils[i].FullName))
                        {
                            buff = sr.ReadToEnd();
                        }

                        strings = buff.Split('\n').ToList();
                        int strSize = strings.Count();
                        strings.RemoveAt(strSize-1);
                        --strSize;

                        int j = 0;

                        foreach (var line in strings)
                        {
                            if (_TextToDelete != null && line.Contains(_TextToDelete))
                            {
                                ++NumberOfDeletedStrings_DBExport;
                            }
                            else
                            {
                                //_dbContext.Lines.Add(MapToModel(line));
                                ++j;
                                lines.Add(MapToModel(line));
                                ++ExportedStrings;
                                if (j == _loadingSize)
                                {
                                    ExportingToDatabaseStatus = "Saving local changes to database";
                                    //_dbContext.SaveChanges();
                                    _dbContext.BulkInsert(lines);
                                    ExportingToDatabaseStatus = "Preparing...";
                                    j = 0;
                                }
                            }
                        }
                        ++ExportedFiles;
                    }
                    watch.Stop();
                    MessageBox.Show(watch.ElapsedMilliseconds.ToString());
                }));
            }
        }


        public ICommand RequestProcedure
        {
            get
            {
                return new DelegateComand(obj => Task.Factory.StartNew(async () =>
                {

                    CountStatus = "Calculating...";

                    var result = await CalculateStatisticsAsync();

                    CountStatus = "Done";
                    MessageBox.Show($"Sum of integer: {result.IntegerSum}\nMedian of real: {result.RealMedian}");

                }));
            }
        }
        public async Task<StatisticsResult> CalculateStatisticsAsync()
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "CalculateStatistics";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                _dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var result = new StatisticsResult
                        {
                            IntegerSum = reader.GetInt64(0),    // Для BIGINT используйте GetInt64
                            RealMedian = reader.GetDouble(1)    // Для FLOAT используйте GetDouble
                        };

                        return result;
                    }
                }
            }
            return null;
        }


        private StringModel MapToModel(string str)
        {
            if(str != "")
            {
                var s = str.Split("||");
                return new StringModel
                {
                    Date = s[0],
                    LatinLetters = s[1],
                    KirilicLetters = s[2],
                    IntegerNumber = Convert.ToInt32(s[3]),
                    RealNumber = Convert.ToDouble(s[4]),
                };
            }
            else
            {
                return null;
            }
        }
    }
}
