// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXFileLogProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;

#nullable disable
namespace PX.Data.Update;

[Obsolete("This object is obsolete and will be removed from public API. Rewrite your code without this object or contact your partner for assistance.")]
[Serializable]
internal class PXFileLogProvider
{
  private readonly string _filename;

  public PXFileLogProvider()
  {
    this._filename = Path.Combine(PXInstanceHelper.AppDataFolder, "maintlog.txt");
  }

  public void Write(PXUpdateEvent message)
  {
    using (StreamWriter streamWriter = new StreamWriter(this._filename, true))
    {
      streamWriter.WriteLine("=========={0}=========={1}==========", (object) message.Level, (object) System.DateTime.Now);
      if (!string.IsNullOrEmpty(message.Message))
        streamWriter.WriteLine(message.Message);
      if (message.Error != null)
      {
        if (!string.IsNullOrEmpty(message.Message))
          streamWriter.WriteLine("---------------");
        streamWriter.WriteLine(message.Error.Message);
        streamWriter.WriteLine(message.Error.StackTrace);
        for (Exception innerException = message.Error.InnerException; innerException != null; innerException = innerException.InnerException)
        {
          streamWriter.WriteLine();
          streamWriter.WriteLine(innerException.Message);
          streamWriter.WriteLine(innerException.StackTrace);
        }
      }
      if (string.IsNullOrEmpty(message.Script))
        return;
      streamWriter.WriteLine("---------------");
      streamWriter.WriteLine(message.Script);
    }
  }

  public string GetLog()
  {
    if (!File.Exists(this._filename))
      return (string) null;
    int num = 10000000;
    using (StreamReader streamReader = new StreamReader(this._filename))
    {
      if (streamReader.BaseStream.Length > (long) num)
        streamReader.BaseStream.Seek((long) -num, SeekOrigin.End);
      return streamReader.ReadToEnd();
    }
  }

  public bool HasLog() => File.Exists(this._filename);

  public void ClearLog()
  {
    if (!File.Exists(this._filename))
      return;
    File.WriteAllText(this._filename, string.Empty);
  }
}
