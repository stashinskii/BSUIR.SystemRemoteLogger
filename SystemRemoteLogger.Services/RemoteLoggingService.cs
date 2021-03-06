﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents service for watching events and log them using SRL API Services
    /// </summary>
    public class RemoteLoggingService
    {
        public delegate Task EncryptedDataEventHandler(object sender, EncodingEventArgs e);
        public event EncryptedDataEventHandler NewMessageOn;

        private IConfigurationProvider _configurationProvider;
        private FileSystemWatcher _watcher;

        public RemoteLoggingService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// Starts async watching of processes and directories
        /// </summary>
        public async Task Start()
        {
            try
            {
                var directoryPath = _configurationProvider.Directory;
                CheckDirectory(directoryPath);

                _watcher = new FileSystemWatcher(directoryPath);
                _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;

                _watcher.Created += new FileSystemEventHandler(DirectoryChanged);
                _watcher.Changed += new FileSystemEventHandler(DirectoryChanged);
                _watcher.Error += new ErrorEventHandler(DirectoryChangedError);
                _watcher.EnableRaisingEvents = true;
              
                while (true)
                {
                    await Task.Delay(5000);
                }
            }
            catch (ArgumentException e)
            {
                await NewMessageOn(this, new EncodingEventArgs() { data = "Mistakes!" });
            }
            catch (Exception)
            {
                await NewMessageOn(this, new EncodingEventArgs() { data = "Mistakes!" });
            }
        }

        private void DirectoryChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
               
                WatcherChangeTypes changeTypes = e.ChangeType;
                NewMessageOn(this, new EncodingEventArgs() { data = e.FullPath});
            }
            catch (Exception)
            {
                NewMessageOn(this, new EncodingEventArgs() { data = "Mistakes!" });
            }

        }

        private void DirectoryChangedError(object sender, ErrorEventArgs e)
        {
        }

        private void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Check your directory!", nameof(path));
        }
    }
}
 