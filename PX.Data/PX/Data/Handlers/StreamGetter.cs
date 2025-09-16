// Decompiled with JetBrains decompiler
// Type: PX.Data.Handlers.StreamGetter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Compilation;
using System.Web.SessionState;

#nullable disable
namespace PX.Data.Handlers;

public class StreamGetter : IHttpHandler, IRequiresSessionState
{
  private const string boundary = "<zaxscdvfbgnhmjklklq1w2e3r4t5y6u7i8o9p0>";

  public bool IsReusable => true;

  public void ProcessRequest(HttpContext context)
  {
    string typeName = context.Request.QueryString["type"];
    string path = context.Request.QueryString["name"];
    if (string.IsNullOrEmpty(path))
      path = "data.bin";
    if (context.Server.ScriptTimeout < 3600)
      context.Server.ScriptTimeout = 3600;
    context.Response.AddHeader("cache-control", "no-store, private");
    context.Response.AppendHeader("Content-Disposition", $"attachment; filename=\"{path}\"");
    context.Response.ContentType = "application/octet-stream";
    System.Type type = PXBuildManager.GetType(typeName, false);
    if (type == (System.Type) null)
      return;
    object instance = Activator.CreateInstance(type);
    if (instance == null || !(instance is IStreamGetterSource streamGetterSource1))
      return;
    Dictionary<string, string> args = new Dictionary<string, string>();
    foreach (string allKey in context.Request.QueryString.AllKeys)
    {
      if (!(allKey == "type") && !(allKey == "name"))
        args[allKey] = context.Request.QueryString[allKey];
    }
    context.Response.Clear();
    context.Response.BufferOutput = false;
    try
    {
      streamGetterSource1.Initialise(args);
      string mimeType = MimeTypes.GetMimeType(Path.GetExtension(path));
      List<KeyValuePair<long, long>> keyValuePairList = new List<KeyValuePair<long, long>>();
      if (context.Request.Headers["Range"] != null && context.Request.Headers["Range"].StartsWith("bytes="))
      {
        string str1 = context.Request.Headers["Range"].Substring(6);
        char[] separator = new char[1]{ ',' };
        foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        {
          char[] chArray = new char[1]{ '-' };
          string[] strArray = str2.Split(chArray);
          if (strArray.Length != 0)
          {
            long result1 = 0;
            long result2;
            long.TryParse(strArray[0].Trim(), out result2);
            if (strArray.Length > 1 && (!long.TryParse(strArray[1].Trim(), out result1) || result1 >= streamGetterSource1.Length))
              result1 = streamGetterSource1.Length - 1L;
            keyValuePairList.Add(new KeyValuePair<long, long>(result2, result1));
          }
        }
      }
      if (keyValuePairList.Count < 2)
      {
        if (keyValuePairList.Count == 1)
        {
          context.Response.StatusCode = 206;
          context.Response.StatusDescription = "Partial Content";
          context.Response.AddHeader("Content-Type", mimeType);
          context.Response.AddHeader("Content-Range", $"bytes {keyValuePairList[0].Key}-{keyValuePairList[0].Value}/{streamGetterSource1.Length}");
          HttpResponse response = context.Response;
          long num = keyValuePairList[0].Value;
          KeyValuePair<long, long> keyValuePair = keyValuePairList[0];
          long key1 = keyValuePair.Key;
          string str = (num - key1 + 1L).ToString();
          response.AddHeader("Content-Length", str);
          IStreamGetterSource streamGetterSource2 = streamGetterSource1;
          Stream outputStream = context.Response.OutputStream;
          keyValuePair = keyValuePairList[0];
          long key2 = keyValuePair.Key;
          keyValuePair = keyValuePairList[0];
          long stop = keyValuePair.Value;
          streamGetterSource2.Read(outputStream, key2, stop);
        }
        else
        {
          context.Response.AddHeader("Content-Type", mimeType);
          context.Response.AddHeader("Content-Length", streamGetterSource1.Length.ToString());
          streamGetterSource1.Read(context.Response.OutputStream, 0L, streamGetterSource1.Length - 1L);
        }
      }
      else
      {
        context.Response.StatusCode = 206;
        context.Response.StatusDescription = "Partial Content";
        context.Response.AddHeader("Content-Type", "multipart/byteranges; boundary=<zaxscdvfbgnhmjklklq1w2e3r4t5y6u7i8o9p0>");
        List<string> stringList = new List<string>(keyValuePairList.Count);
        long num1 = 0;
        for (int index = 0; index < keyValuePairList.Count; ++index)
        {
          KeyValuePair<long, long> keyValuePair = keyValuePairList[index];
          string str = $"--<zaxscdvfbgnhmjklklq1w2e3r4t5y6u7i8o9p0>\r\nContent-Type: {mimeType}\nContent-Range: bytes {$"{keyValuePair.Key}-{keyValuePair.Value}/{streamGetterSource1.Length}"}\r\n\r\n";
          stringList.Add(str);
          num1 = num1 + (long) str.Length + keyValuePair.Value - keyValuePair.Key + 3L;
        }
        long num2 = num1 + (long) "<zaxscdvfbgnhmjklklq1w2e3r4t5y6u7i8o9p0>".Length + 10L;
        context.Response.AddHeader("Content-Length", num2.ToString());
        for (int index = 0; index < keyValuePairList.Count; ++index)
        {
          context.Response.Write(stringList[index]);
          IStreamGetterSource streamGetterSource3 = streamGetterSource1;
          Stream outputStream = context.Response.OutputStream;
          KeyValuePair<long, long> keyValuePair = keyValuePairList[index];
          long key = keyValuePair.Key;
          keyValuePair = keyValuePairList[index];
          long stop = keyValuePair.Value;
          streamGetterSource3.Read(outputStream, key, stop);
        }
        context.Response.Write("--<zaxscdvfbgnhmjklklq1w2e3r4t5y6u7i8o9p0>--\r\n\r\n");
      }
    }
    catch (Exception ex)
    {
      context.Response.StatusCode = 500;
      context.Response.StatusDescription = ex.Message;
      throw ex;
    }
    finally
    {
      streamGetterSource1?.Dispose();
    }
  }
}
