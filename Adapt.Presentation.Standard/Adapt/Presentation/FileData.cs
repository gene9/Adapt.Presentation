﻿using System;
using System.IO;

namespace Adapt.Presentation
{
    /// <summary>
    /// The object used as a wrapper for the file picked by the user
    /// </summary>
    public class FileData : IDisposable
    {
        public string FileName { get; set; }
        public Stream  FileStream { get; set; }

        public void Dispose()
        {
            FileStream?.Dispose();
        }
    }
}