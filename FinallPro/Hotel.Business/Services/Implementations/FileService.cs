﻿using Hotel.Business.Exceptions;
using Hotel.Business.Services.Interfaces;
using Hotel.Business.Utilities;
using Microsoft.AspNetCore.Http;

namespace Hotel.Business.Services.Implementations;

public class FileService : IFileService

{
 

    public void RemoveFile(string root, string filePath)
    {
        string fileroot = Path.Combine(root, filePath);
        if (File.Exists(fileroot))
        {
            File.Delete(fileroot);
        }
    }

    public async Task<string> UploadFile(IFormFile file, string root, int kb, params string[] folders)
    {
        if (!file.CheckFileSize(kb))
        {
            throw new FileSizeException("Size is not correct");
        }
        if (!file.CheckFileType("image"))
        {
            {
                throw new FileTypeException("Choose correect type");
            }
        }
        string folderRoot = string.Empty;
        foreach (var folder in folders)
        {
            folderRoot = Path.Combine(folderRoot, folder);
        }
        string filename = await file.UploadFile(root, folderRoot);
        return filename;
    }
}
