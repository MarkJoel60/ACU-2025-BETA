// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.ExportContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class ExportContext
{
  public Guid WikiID;
  public string currentShared;
  public List<bool> listlevel;
  public bool IsFirstContainerInList;
  public bool IsTable;
  public bool IsHeader;
  public bool IsFigure;
  public bool IsLink;
  public bool IsPre;
  public bool IsPh;
  public bool IsShared;
  public bool IsCodeBlock;
  public bool IsRelatedLink;
  public StringBuilder Title;
  public List<string> ImageLinks;
  public bool firsttitle;
  public StringBuilder WikiPageTitle;
  public Guid topicid;
  public StringBuilder parentname;
  public int Number;
  public Dictionary<string, string> _ConRef;

  public ExportContext()
  {
    this.WikiID = new Guid();
    this.listlevel = new List<bool>();
    this.IsFirstContainerInList = false;
    this.IsHeader = false;
    this.IsFigure = false;
    this.IsLink = false;
    this.IsRelatedLink = false;
    this.IsPre = false;
    this.IsPh = false;
    this.IsShared = false;
    this.IsCodeBlock = false;
    this.IsTable = false;
    this.Title = new StringBuilder();
    this.ImageLinks = new List<string>();
    this.firsttitle = false;
    this.topicid = new Guid("00000000-0000-0000-0000-000000000000");
    this.parentname = new StringBuilder();
    this.Number = 0;
    this._ConRef = new Dictionary<string, string>();
    this.currentShared = "";
  }
}
