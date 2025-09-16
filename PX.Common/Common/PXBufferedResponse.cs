// Decompiled with JetBrains decompiler
// Type: PX.Common.PXBufferedResponse
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.IO;
using System.Web;

#nullable disable
namespace PX.Common;

public static class PXBufferedResponse
{
  /// <summary>
  /// Writes binary data in streaming mode to the current response.
  /// </summary>
  /// <param name="data">Binary data to write.</param>
  /// <param name="startIndex">Start index to write</param>
  public static void WriteBinary(byte[] data, long startIndex, long stopIndex)
  {
    long num = stopIndex - startIndex + 1L;
    HttpResponse response = HttpContext.Current.Response;
    using (MemoryStream memoryStream1 = new MemoryStream(data))
    {
      memoryStream1.Position = startIndex;
      byte[] buffer1 = new byte[81920 /*0x014000*/];
      response.BufferOutput = true;
      while (num > 0L)
      {
        MemoryStream memoryStream2 = memoryStream1;
        byte[] buffer2 = buffer1;
        int count1 = 81920 /*0x014000*/ < (int) num ? 81920 /*0x014000*/ : (int) num;
        int count2;
        if ((count2 = memoryStream2.Read(buffer2, 0, count1)) <= 0 || !response.IsClientConnected)
          break;
        num -= (long) count2;
        response.OutputStream.Write(buffer1, 0, count2);
        if (!response.IsClientConnected)
          break;
        response.Flush();
      }
    }
  }
}
