using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents maling service
    /// </summary>
    public class MailService : IMailService
    {
        private IConfigurationProvider _configurationProvider;
        private IMailSender _mailSender;
        private FileSystemWatcher _watcher;

        public MailService(IConfigurationProvider configurationProvider, IMailSender mailSender)
        {
            _configurationProvider = configurationProvider;
            _mailSender = mailSender;
        }

        public async Task Start()
        {
            try
            {
                var directoryPath = _configurationProvider.Directory;
                CheckDirectory(directoryPath);

                _watcher = new FileSystemWatcher(directoryPath);
                _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;

                _watcher.Created += new FileSystemEventHandler(DirectoryChanged);
                _watcher.Error += new ErrorEventHandler(DirectoryChangedError);
                _watcher.EnableRaisingEvents = true;

                while (true)
                {
                    await Task.Delay(5000);
                }
            }
            catch (ArgumentException e)
            {
            }
            catch (Exception)
            {
            }
        }

        private async Task Send(string filePath)
        {
            var mailFrom = _configurationProvider.MailFrom;
            var mailTo = _configurationProvider.MailTo;
            var password = _configurationProvider.Password;
            WaitForAccess(filePath);
            _mailSender.SendMail(mailFrom, mailTo, password, "Subject", "Body message (not html)", filePath);
        }

        private void WaitForAccess(string filePath)
        {
            bool fileIsUsing = false;
            FileStream fileStream;
            FileInfo fileInfo = new FileInfo(filePath);

            while (fileIsUsing)
            {
                try
                {
                    fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read);
                }
                catch (IOException)
                {
                    continue;
                }
                fileIsUsing = false;
                fileStream.Close();
            }
        }

        private void DirectoryChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                WatcherChangeTypes changeTypes = e.ChangeType;
                Send(e.FullPath);

                File.Delete(e.FullPath);
            }
            catch (Exception)
            {
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
