// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.MimeTypes
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Contains a list of MIME Types.</summary>
public static class MimeTypes
{
  /// <summary>Retrieves a MIME type for the given file extension.</summary>
  /// <param name="ext">An extension to retrieve MIME type for.</param>
  /// <returns>A string containing MIME type.</returns>
  public static string GetMimeType(string ext)
  {
    string str = "";
    ext = ext.ToLowerInvariant();
    return MimeTypes.Types.TryGetValue(ext, out str) ? str : "application/octet-stream";
  }

  public static string GetExtension(string mimetype)
  {
    foreach (KeyValuePair<string, string> type in MimeTypes.Types)
    {
      if (string.Compare(type.Value, mimetype, true) == 0)
        return type.Key;
    }
    return (string) null;
  }

  /// <summary>Gets the list of the MIME Types.</summary>
  public static ReadOnlyDictionary<string, string> Types
  {
    get
    {
      return PXDatabase.GetSlot<MimeTypes.MimeTypeDict>("MimeTypeDict", typeof (UploadAllowedFileTypes)).mtypes;
    }
  }

  private class MimeTypeDict : IPrefetchable, IPXCompanyDependent
  {
    public ReadOnlyDictionary<string, string> mtypes;

    private static void AddDefault(Dictionary<string, string> types, string extension, string type)
    {
      if (types.ContainsKey(extension) && !string.IsNullOrEmpty(types[extension]))
        return;
      types[extension] = type;
    }

    public void Prefetch()
    {
      Dictionary<string, string> types = new Dictionary<string, string>();
      foreach (PXResult<UploadAllowedFileTypes> pxResult in PXSelectBase<UploadAllowedFileTypes, PXSelect<UploadAllowedFileTypes>.Config>.Select(new PXGraph()))
      {
        UploadAllowedFileTypes allowedFileTypes = (UploadAllowedFileTypes) pxResult;
        if (!types.ContainsKey(allowedFileTypes.FileExt))
          types.Add(allowedFileTypes.FileExt, allowedFileTypes.DefApplication);
      }
      MimeTypes.MimeTypeDict.AddDefault(types, ".jpg", "image/jpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".jpeg", "image/jpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".jpe", "image/jpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".gif", "image/gif");
      MimeTypes.MimeTypeDict.AddDefault(types, ".png", "image/png");
      MimeTypes.MimeTypeDict.AddDefault(types, ".bmp", "image/bmp");
      MimeTypes.MimeTypeDict.AddDefault(types, ".tif", "image/tiff");
      MimeTypes.MimeTypeDict.AddDefault(types, ".tiff", "image/tiff");
      MimeTypes.MimeTypeDict.AddDefault(types, ".svg", "image/svg+xml");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ico", "image/x-icon");
      MimeTypes.MimeTypeDict.AddDefault(types, ".txt", "text/plain");
      MimeTypes.MimeTypeDict.AddDefault(types, ".htm", "text/html");
      MimeTypes.MimeTypeDict.AddDefault(types, ".html", "text/html");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xhtml", "text/xhtml");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xml", "application/octet-stream");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xsl", "text/xsl");
      MimeTypes.MimeTypeDict.AddDefault(types, ".dtd", "application/xml-dtd");
      MimeTypes.MimeTypeDict.AddDefault(types, ".css", "text/css");
      MimeTypes.MimeTypeDict.AddDefault(types, ".rtf", "text/rtf");
      MimeTypes.MimeTypeDict.AddDefault(types, ".sql", "text/sql");
      MimeTypes.MimeTypeDict.AddDefault(types, ".zip", "application/zip");
      MimeTypes.MimeTypeDict.AddDefault(types, ".tar", "application/x-tar");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ogg", "application/ogg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".swf", "application/x-shockwave-flash");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mpga", "audio/mpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mp2", "audio/mpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mp3", "audio/mpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".m3u", "audio/x-mpegurl");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ram", "audio/x-pn-realaudio");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ra", "audio/x-pn-realaudio");
      MimeTypes.MimeTypeDict.AddDefault(types, ".rm", "application/vnd.rn-realmedia");
      MimeTypes.MimeTypeDict.AddDefault(types, ".wav", "application/x-wav");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mpg", "video/mpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mpeg", "video/mpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mpe", "video/mpeg");
      MimeTypes.MimeTypeDict.AddDefault(types, ".mov", "video/quicktime");
      MimeTypes.MimeTypeDict.AddDefault(types, ".qt", "video/quicktime");
      MimeTypes.MimeTypeDict.AddDefault(types, ".avi", "video/x-msvideo");
      MimeTypes.MimeTypeDict.AddDefault(types, ".pdf", "application/pdf");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ai", "application/postscript");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ps", "application/postscript");
      MimeTypes.MimeTypeDict.AddDefault(types, ".eps", "application/postscript");
      MimeTypes.MimeTypeDict.AddDefault(types, ".doc", "application/msword");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template");
      MimeTypes.MimeTypeDict.AddDefault(types, ".potx", "application/vnd.openxmlformats-officedocument.presentationml.template");
      MimeTypes.MimeTypeDict.AddDefault(types, ".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow");
      MimeTypes.MimeTypeDict.AddDefault(types, ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
      MimeTypes.MimeTypeDict.AddDefault(types, ".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide");
      MimeTypes.MimeTypeDict.AddDefault(types, ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
      MimeTypes.MimeTypeDict.AddDefault(types, ".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xlam", "application/vnd.ms-excel.addin.macroEnabled.12");
      MimeTypes.MimeTypeDict.AddDefault(types, ".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12");
      foreach (string key in types.Keys.ToArray<string>())
      {
        if (string.IsNullOrEmpty(types[key]))
          types[key] = "application/octet-stream";
      }
      this.mtypes = new ReadOnlyDictionary<string, string>((IDictionary<string, string>) types);
    }
  }
}
