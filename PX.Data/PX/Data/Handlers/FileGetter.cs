// Decompiled with JetBrains decompiler
// Type: PX.Data.Handlers.FileGetter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Wiki.Parser;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace PX.Data.Handlers;

/// <exclude />
public class FileGetter : IHttpHandler, IRequiresSessionState
{
  public bool IsReusable => true;

  public void ProcessRequest(HttpContext context)
  {
    if (context.Server.ScriptTimeout < 3600)
      context.Server.ScriptTimeout = 3600;
    context.Response.AddHeader("cache-control", "no-store, private");
    bool flag = context.Request.QueryString["cache"] == "yes";
    System.DateTime result1;
    if (flag && System.DateTime.TryParse(context.Request.Headers["If-Modified-Since"], out result1) && result1 <= System.DateTime.Now.AddMinutes(-10.0))
    {
      context.Response.StatusCode = 304;
    }
    else
    {
      string fileName = context.Request.QueryString["file"];
      string str1 = context.Request.QueryString["fileID"];
      string s1 = context.Request.QueryString["revision"];
      string s2 = context.Request.QueryString["width"];
      string s3 = context.Request.QueryString["height"];
      string str2 = context.Request.QueryString["fdwnld"];
      string str3 = context.Request.QueryString["inmemory"];
      string str4 = context.Request.QueryString["second"];
      string str5 = context.Request.QueryString["chk"];
      int result2 = 0;
      int result3 = 0;
      int result4 = -1;
      if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(str1))
        return;
      if (!string.IsNullOrEmpty(s2))
        int.TryParse(s2, out result2);
      if (!string.IsNullOrEmpty(s3))
        int.TryParse(s3, out result3);
      if (!string.IsNullOrEmpty(s1))
        int.TryParse(s1, out result4);
      UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
      instance.Filter.Current.HashAccess = str5;
      PX.SM.FileInfo fileInfo;
      try
      {
        if (!string.IsNullOrEmpty(str1))
        {
          int length = str1.LastIndexOf('.');
          string str6 = length > -1 ? str1.Substring(0, length) : str1;
          Guid fileID = new Guid(str6);
          fileInfo = !(str3 == "1") ? (result4 < 0 ? instance.GetFile(fileID) : instance.GetFile(fileID, result4)) : PXContext.SessionTyped<PXSessionStatePXData>().FileInfo.Pop<PX.SM.FileInfo>(str6);
        }
        else
          fileInfo = instance.GetFile(fileName);
      }
      catch
      {
        fileInfo = (PX.SM.FileInfo) null;
      }
      if (fileInfo == null)
        throw new PXSetPropertyException("The file is not found, or you don't have enough rights to see the file.");
      if (fileInfo.BinData == null)
        throw new PXSetPropertyException("The content of the file is not found, or you don't have enough rights to see the content.");
      if (!WebConfig.EnableConcurrentSessionAccess || str3 == "1")
        PXSessionStateStore.CommitSession(context);
      string str7 = MimeTypes.GetMimeType(Path.GetExtension(fileInfo.Name));
      context.Response.Clear();
      context.Response.Cache.SetCacheability(HttpCacheability.Private);
      System.DateTime now = System.DateTime.Now;
      context.Response.Cache.SetExpires(flag ? now.AddMinutes(10.0) : now.AddSeconds(2.0));
      context.Response.Cache.SetValidUntilExpires(true);
      if (flag)
        context.Response.Cache.SetLastModified(now);
      context.Response.AddHeader("Connection", "Keep-Alive");
      context.Response.AddHeader("X-Content-Type-Options", "nosniff");
      context.Response.BufferOutput = false;
      if (str2 != "1" && str7.Contains("shockwave") && str4 != "1")
      {
        string s4 = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\" >\n<head><style type=\"text/css\"> * {{margin:0; padding:0; border:none; }} html, body {{height:100%;width:100%;}}</style>\n<title>\n{(string.IsNullOrEmpty(fileInfo.Comment) ? "Product Videos" : HttpUtility.HtmlEncode(fileInfo.Comment))}\n</title></head>\n<body>\n<object id=\"swf1\" width=\"100%\" height=\"99%\" type=\"application/x-shockwave-flash\"\n{(context.Request.Browser.Browser == "Firefox" ? $"data=\"{context.Request.GetExternalUrl().AbsoluteUri}&second=1\" SCALE=\"default\">\n" : ">\n")}<param name=\"movie\" value=\"{context.Request.GetExternalUrl().AbsoluteUri}&second=1\" />\n<param name=\"SCALE\" value=\"exactfit\" />\n<a href=\"http://www.adobe.com/go/getflashplayer\">\nTo view this video you are required to install Adobe Flash<br />\n<img src=\"http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif\" alt=\"Install\" /></a>\n</object>\n</body>\n</html>\n";
        foreach (string allKey in context.Request.Cookies.AllKeys)
          context.Response.Cookies.Add(context.Request.Cookies[allKey]);
        context.Response.AddHeader("content-type", "text/html");
        context.Response.AddHeader("Accept-Ranges", "bytes");
        context.Response.AddHeader("Content-Length", s4.Length.ToString());
        context.Response.Write(s4);
      }
      else
      {
        if (str7.Contains("image"))
        {
          context.Response.Cache.SetExpires(System.DateTime.Now.AddMinutes(5.0));
          if (result2 != 0)
          {
            byte[] numArray = Drawing.ScaleImageFromBytes(fileInfo.BinData, result2, result3);
            if (numArray != null)
            {
              fileInfo.BinData = numArray;
              string empty = string.Empty;
              for (int index = 0; index < System.Math.Min(numArray.Length, 6); ++index)
                empty += char.ConvertFromUtf32((int) numArray[index]);
              if (empty.Contains("GIF"))
                str7 = "image/gif";
              else if (empty.Contains("PNG"))
                str7 = "image/png";
            }
          }
        }
        context.Response.AddHeader("Accept-Ranges", "bytes");
        context.Response.AddHeader("ETag", fileInfo.UID.ToString());
        string str8 = (context.Request.Browser.Browser == "IE" ? 1 : (context.Request.UserAgent == null ? 0 : (context.Request.UserAgent.Contains(" Edge/") ? 1 : 0))) != 0 ? HttpUtility.UrlEncode(fileInfo.Name).Replace('+', ' ') : fileInfo.Name;
        if (str2 == "1")
          context.Response.AddHeader("content-disposition", $"attachment;filename=\"{str8}\"");
        else if (str4 != "1")
          context.Response.AddHeader("content-disposition", $"inline;filename=\"{str8}\"");
        long length = (long) fileInfo.BinData.Length;
        List<KeyValuePair<long, long>> keyValuePairList = new List<KeyValuePair<long, long>>();
        if (context.Request.Headers["Range"] != null && context.Request.Headers["Range"].StartsWith("bytes="))
        {
          string str9 = context.Request.Headers["Range"].Substring(6);
          char[] separator = new char[1]{ ',' };
          foreach (string str10 in str9.Split(separator, StringSplitOptions.RemoveEmptyEntries))
          {
            char[] chArray = new char[1]{ '-' };
            string[] strArray = str10.Split(chArray);
            if (strArray.Length != 0)
            {
              long result5;
              long.TryParse(strArray[0].Trim(), out result5);
              long result6 = 0;
              if (strArray.Length > 1 && (!long.TryParse(strArray[1].Trim(), out result6) || result6 >= length))
                result6 = length - 1L;
              keyValuePairList.Add(new KeyValuePair<long, long>(result5, result6));
            }
          }
        }
        if (keyValuePairList.Count < 2)
        {
          if (keyValuePairList.Count == 1)
          {
            context.Response.StatusCode = 206;
            context.Response.StatusDescription = "Partial Content";
            context.Response.AddHeader("Content-Type", str7);
            HttpResponse response1 = context.Response;
            KeyValuePair<long, long> keyValuePair = keyValuePairList[0];
            // ISSUE: variable of a boxed type
            __Boxed<long> key1 = (ValueType) keyValuePair.Key;
            keyValuePair = keyValuePairList[0];
            // ISSUE: variable of a boxed type
            __Boxed<long> local1 = (ValueType) keyValuePair.Value;
            // ISSUE: variable of a boxed type
            __Boxed<long> local2 = (ValueType) length;
            string str11 = $"bytes {key1}-{local1}/{local2}";
            response1.AddHeader("Content-Range", str11);
            HttpResponse response2 = context.Response;
            keyValuePair = keyValuePairList[0];
            long num = keyValuePair.Value;
            keyValuePair = keyValuePairList[0];
            long key2 = keyValuePair.Key;
            string str12 = (num - key2 + 1L).ToString();
            response2.AddHeader("Content-Length", str12);
            byte[] binData = fileInfo.BinData;
            keyValuePair = keyValuePairList[0];
            long key3 = keyValuePair.Key;
            keyValuePair = keyValuePairList[0];
            long stopIndex = keyValuePair.Value;
            PXBufferedResponse.WriteBinary(binData, key3, stopIndex);
          }
          else
          {
            context.Response.AddHeader("Content-Type", str7);
            context.Response.AddHeader("Content-Length", length.ToString());
            PXBufferedResponse.WriteBinary(fileInfo.BinData, 0L, length - 1L);
          }
        }
        else
        {
          context.Response.StatusCode = 206;
          context.Response.StatusDescription = "Partial Content";
          string str13 = "<zaxscdvfbgnhmjklklq1w2e3r4t5y6u7i8o9p0>";
          context.Response.AddHeader("Content-Type", "multipart/byteranges; boundary=" + str13);
          List<string> stringList = new List<string>(keyValuePairList.Count);
          long num1 = 0;
          for (int index = 0; index < keyValuePairList.Count; ++index)
          {
            KeyValuePair<long, long> keyValuePair = keyValuePairList[index];
            string str14 = $"--{str13}\r\nContent-Type: {str7}\nContent-Range: bytes {$"{keyValuePair.Key}-{keyValuePair.Value}/{length}"}\r\n\r\n";
            stringList.Add(str14);
            num1 = num1 + (long) str14.Length + keyValuePair.Value - keyValuePair.Key + 3L;
          }
          long num2 = num1 + (long) str13.Length + 10L;
          context.Response.AddHeader("Content-Length", num2.ToString());
          for (int index = 0; index < keyValuePairList.Count; ++index)
          {
            context.Response.Write(stringList[index]);
            PXBufferedResponse.WriteBinary(fileInfo.BinData, keyValuePairList[index].Key, keyValuePairList[index].Value);
            context.Response.Write("\r\n");
          }
          context.Response.Write($"--{str13}--\r\n\r\n");
        }
      }
    }
  }
}
