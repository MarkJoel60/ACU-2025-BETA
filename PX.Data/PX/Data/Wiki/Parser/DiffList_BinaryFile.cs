// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DiffList_BinaryFile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class DiffList_BinaryFile : IDiffList
{
  private byte[] _byteList;

  public DiffList_BinaryFile(string fileName)
  {
    FileStream input = (FileStream) null;
    BinaryReader binaryReader = (BinaryReader) null;
    try
    {
      input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
      int length = (int) input.Length;
      binaryReader = new BinaryReader((Stream) input);
      this._byteList = binaryReader.ReadBytes(length);
    }
    catch (Exception ex)
    {
      throw ex;
    }
    finally
    {
      binaryReader?.Close();
      input?.Close();
    }
  }

  public int Count() => this._byteList.Length;

  public IComparable GetByIndex(int index) => (IComparable) this._byteList[index];
}
