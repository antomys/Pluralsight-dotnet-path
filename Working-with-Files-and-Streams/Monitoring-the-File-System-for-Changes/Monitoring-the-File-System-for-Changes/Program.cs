﻿using System;
using System.IO;

namespace Monitoring_the_File_System_for_Changes
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if(args.Length == 0)
                return;
            args[0].Init();
        }

        private static void Init(this string directoryName)
        {
            if (!Directory.Exists(directoryName))
                throw new ArgumentNullException(directoryName);
            using var fileSystemWatcher = new FileSystemWatcher(directoryName)
            {
                IncludeSubdirectories = true, 
                Filter = "*.*",
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                        | NotifyFilters.FileName
                                                        | NotifyFilters.CreationTime,
                // Without this it will not generate events. By default - false.
                EnableRaisingEvents = true
            };

            fileSystemWatcher.InitializeMethods();
            
            Console.WriteLine("Beginning to watch\nPress any key to exit.");
            Console.ReadKey();
        }

        private static void InitializeMethods(this FileSystemWatcher fileSystemWatcher)
        {
            fileSystemWatcher.Created += FileSystemWatcherExtension.FileSystemWatcherOnCreated;
            fileSystemWatcher.Deleted += FileSystemWatcherExtension.FileSystemWatcherOnDeleted;
            fileSystemWatcher.Renamed += FileSystemWatcherExtension.FileSystemWatcherOnRenamed;
            fileSystemWatcher.Error += FileSystemWatcherExtension.FileSystemWatcherOnError;
            fileSystemWatcher.Disposed += FileSystemWatcherExtension.FileSystemWatcherOnDisposed;
        }
        
        [Obsolete("Removed due to fileSystemWatcher")]
        private static void ProcessFile(string fileName)
        {
            var processor = new FileProcessor(fileName);
            processor.ProcessFile();
        }
        [Obsolete("Removed due to fileSystemWatcher")]
        private static void ProcessDirectory(string directoryName, string fileType)
        {
            var processor = new FileProcessor(fileType,directoryName);
            processor.ProcessDirectory();
        }
    }
}