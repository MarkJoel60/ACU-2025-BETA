// Decompiled with JetBrains decompiler
// Type: PX.SM.FileInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;

#nullable disable
namespace PX.SM;

[Serializable]
public class FileInfo
{
  private const string FileParName = "file";
  private int? revId;
  private Guid? uID;
  private string name;
  private string link;
  private byte[] binData;
  private string comment;
  private string originalName;
  private bool isPublic;

  public int? RevisionId
  {
    get => this.revId;
    set => this.revId = value;
  }

  public Guid? UID
  {
    get => this.uID;
    set => this.uID = value;
  }

  public string Name => FileInfo.GetShortName(this.name);

  public string FullName
  {
    get => this.name;
    set => this.name = value;
  }

  public string Link
  {
    get => this.link;
    set => this.link = value;
  }

  public byte[] BinData
  {
    get => this.binData;
    set => this.binData = value;
  }

  public string Comment
  {
    get => this.comment;
    set => this.comment = value;
  }

  public bool IsLinkValid
  {
    get
    {
      if (string.IsNullOrEmpty(this.Link) || HttpContext.Current == null || !Uri.IsWellFormedUriString(Uri.EscapeUriString(this.Link), UriKind.RelativeOrAbsolute))
        return false;
      string host = HttpContext.Current.Request.GetWebsiteAuthority().Host;
      Uri result;
      return Uri.TryCreate(this.Link, UriKind.Absolute, out result) && host == result.Host && this.ParseLink(result, out string _);
    }
  }

  public string OriginalName
  {
    get => this.originalName != null ? this.originalName : this.Name;
    set => this.originalName = value;
  }

  public bool IsPublic
  {
    get => this.isPublic;
    set => this.isPublic = value;
  }

  public bool IsSystem { get; set; }

  public DateTime? RevisionDate { get; set; }

  public int? Size { get; set; }

  public bool ImportIncludeData { get; set; }

  public bool TestPassBeforeImport { get; set; }

  public bool CheckIn { get; set; }

  private FileInfo()
  {
  }

  public FileInfo(Guid uid, string name, string link, byte[] data)
    : this(new Guid?(uid), new int?(), name, link, data, (string) null)
  {
  }

  public FileInfo(Guid uid, string name, string link, byte[] data, string comment)
    : this(new Guid?(uid), new int?(), name, link, data, comment)
  {
  }

  public FileInfo(string name, string link, byte[] data)
    : this(new Guid?(), new int?(), name, link, data, (string) null)
  {
  }

  public FileInfo(Guid? uID, int? revid, string name, string link, byte[] data, string comment)
  {
    this.uID = uID;
    this.revId = revid;
    this.name = name;
    this.link = link;
    this.binData = data;
    this.comment = comment;
  }

  public FileInfo(
    Guid? uID,
    int? revid,
    string name,
    string originalName,
    string link,
    byte[] data,
    string comment)
    : this(uID, revid, name, link, data, comment)
  {
    this.originalName = originalName;
  }

  public FileInfo(
    Guid? uID,
    int? revid,
    string name,
    string originalName,
    string link,
    byte[] data,
    string comment,
    int? size,
    DateTime? revDate)
    : this(uID, revid, name, originalName, link, data, comment)
  {
    this.Size = size;
    this.RevisionDate = revDate;
  }

  public override string ToString()
  {
    string parsedLink;
    if (!string.IsNullOrEmpty(this.Name) || !this.IsLinkValid)
      parsedLink = this.Name;
    else
      this.ParseLink(new Uri(this.Link), out parsedLink);
    return HttpUtility.UrlDecode(parsedLink);
  }

  private bool ParseLink(Uri link, out string parsedLink)
  {
    parsedLink = (string) null;
    if (link.Query.Length <= "file".Length + 1 || !link.Query.Contains("file="))
      return false;
    string str1 = link.Query.Remove(0, 1);
    char[] chArray1 = new char[1]{ '&' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ '=' };
      string[] strArray = str2.Split(chArray2);
      if (strArray.Length == 2 && string.Compare(strArray[0], "file", true) == 0)
      {
        parsedLink = this.RemoveId(strArray[1]);
        return true;
      }
    }
    return false;
  }

  private string RemoveId(string str)
  {
    string[] strArray = str.Split(':');
    return strArray.Length > 1 ? strArray[1] : strArray[0];
  }

  public static string GetShortName(string fullName)
  {
    if (!string.IsNullOrEmpty(fullName) && fullName.Length > 1)
    {
      int num1 = fullName.IndexOf('\\');
      if (num1 != -1 && num1 != fullName.Length - 1)
        fullName = fullName.Substring(num1 + 1);
      int num2 = fullName.IndexOf('\\');
      if (num2 == -1)
        num2 = fullName.IndexOf('/');
      if (num2 != -1 && num2 != fullName.Length - 1)
        fullName = fullName.Substring(num2 + 1);
    }
    return fullName;
  }
}
