using System;
using System.IO;
using System.Threading;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents maling service
    /// </summary>
    public class MailService : IMailService
    {
        //private IConfigurationProvider _configurationProvider;
        private IMailSender _mailSender;
        private FileSystemWatcher _watcher;

        //TODO Add confuguration provider
        public MailService(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public void Start()
        {
            try
            {
                var directoryPath = "TODO PATH";
                CheckDirectory(directoryPath);

                _watcher = new FileSystemWatcher(directoryPath);
                _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;

                _watcher.Created += new FileSystemEventHandler(DirectoryChanged);
                _watcher.Error += new ErrorEventHandler(DirectoryChangedError);
                _watcher.EnableRaisingEvents = true;

                while (true)
                {
                    Thread.Sleep(5000);
                }
            }
            catch (ArgumentException e)
            {
                //_logger.Critical(e.Message);
            }
            catch (Exception)
            {
                //_logger.Critical($"Undefined troubles while customizing handlers");
            }
        }

        private void Send(string filePath)
        {
            var mailFrom = "from";
            var mailTo = "to";
            var password = "password";
            WaitForAccess(filePath);
            _mailSender.SendMail(mailFrom, mailTo, password, "Subject", "Body message (not html)", filePath);
        }

        private void WaitForAccess(string filePath)
        {
            bool fileIsUsing = false;
            FileStream fileStream;
            FileInfo fileInfo = new FileInfo(filePath);

            while(fileIsUsing)
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
                //_logger.Critical($"Some troubles while proccessing file {e.FullPath}");
            }

        }

        private void DirectoryChangedError(object sender, ErrorEventArgs e)
        {
           // _logger.Critical($"Undefined troubles while monitoring directory");
        }

        private void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Check your directory!", nameof(path));
        }
    }
}
