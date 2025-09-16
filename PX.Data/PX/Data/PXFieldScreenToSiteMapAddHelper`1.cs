// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldScreenToSiteMapAddHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data;

public class PXFieldScreenToSiteMapAddHelper<T> : PXScreenToSiteMapAddHelperBase<T> where T : class, IBqlTable, new()
{
  private readonly string[] _urlPrefixes;
  private readonly PXFieldScreenToSiteMapAddHelper<T>.Field[][] _fieldSets;

  /// <summary>Creates new instance of the helper.</summary>
  /// <param name="screenIdPrefix">First two letter of ScreenID.</param>
  /// <param name="urlPrefix">Base url.</param>
  /// <param name="titleField">Title field for site map node.</param>
  /// <param name="screenIDField">Read-only field for showing ScreenID. Can be null.</param>
  /// <param name="fieldSets">Collection of the field sets. URL is created by appending each set of parameters filled by fields values.</param>
  public PXFieldScreenToSiteMapAddHelper(
    PXGraph graph,
    IScreenInfoCacheControl screenInfoCacheControl,
    string screenIdPrefix,
    string urlPrefix,
    System.Type nameField,
    System.Type titleField,
    System.Type screenIDField,
    params PXFieldScreenToSiteMapAddHelper<T>.Field[][] fieldSets)
    : this(graph, screenInfoCacheControl, screenIdPrefix, (IEnumerable<string>) new string[1]
    {
      urlPrefix
    }, nameField, titleField, screenIDField, fieldSets)
  {
  }

  internal PXFieldScreenToSiteMapAddHelper(
    PXGraph graph,
    IScreenInfoCacheControl screenInfoCacheControl,
    string screenIdPrefix,
    IEnumerable<string> urlPrefixes,
    System.Type nameField,
    System.Type titleField,
    System.Type screenIDField,
    params PXFieldScreenToSiteMapAddHelper<T>.Field[][] fieldSets)
    : base(graph, screenInfoCacheControl, screenIdPrefix, nameField, titleField, screenIDField)
  {
    this._urlPrefixes = (urlPrefixes ?? throw new ArgumentNullException(nameof (urlPrefixes))).ToArray<string>();
    if (this._urlPrefixes.Length == 0)
      throw new ArgumentOutOfRangeException(nameof (urlPrefixes));
    this._fieldSets = fieldSets;
    if (this._fieldSets == null || this._fieldSets.Length == 0)
    {
      this._fieldSets = new PXFieldScreenToSiteMapAddHelper<T>.Field[1][];
      this._fieldSets[0] = this.Cache.Keys.Select<string, PXFieldScreenToSiteMapAddHelper<T>.Field>((Func<string, PXFieldScreenToSiteMapAddHelper<T>.Field>) (k => new PXFieldScreenToSiteMapAddHelper<T>.Field(k))).ToArray<PXFieldScreenToSiteMapAddHelper<T>.Field>();
    }
    this.SiteMapCache.RowPersisted += new PXRowPersisted(((PXScreenToSiteMapAddHelperBase<T>) this).SiteMap_RowPersisted);
  }

  public override string BuildUrl(T row)
  {
    bool flag = this._urlPrefixes.Length == 1;
    string url = (string) null;
    foreach (string urlPrefix in this._urlPrefixes)
    {
      url = this.BuildUrl(row, this._fieldSets[0], urlPrefix);
      if (flag || this.FindNodes(url).Any<PXSiteMapNode>())
        return url;
    }
    return url;
  }

  protected override void SiteMap_InsertedRowPersisting(PXCache sender, PX.SM.SiteMap row)
  {
    base.SiteMap_InsertedRowPersisting(sender, row);
    if (!(this.Cache.Current is T current))
      return;
    sender.SetValue<PX.SM.SiteMap.url>((object) row, (object) this.BuildUrl(current));
  }

  protected override void SiteMap_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal row = e.Row as PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal;
    if (this.ScreenIDField != null && row != null && e.TranStatus == PXTranStatus.Completed && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
      this.Cache.SetValue(this.Cache.Current, this.ScreenIDField, this.IsScreenFieldGuid ? (object) row.NodeID : (object) row.ScreenID);
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert && e.TranStatus == PXTranStatus.Aborted && this.Cache.Current is T current && row != null)
      sender.SetValue<PX.SM.SiteMap.url>((object) row, (object) this.BuildUrl(current));
    base.SiteMap_RowPersisted(sender, e);
  }

  protected virtual string BuildUrl(
    T row,
    PXFieldScreenToSiteMapAddHelper<T>.Field[] fields,
    string urlPrefix)
  {
    if ((object) row == null)
      return (string) null;
    StringBuilder stringBuilder = new StringBuilder(urlPrefix);
    bool flag = true;
    foreach (PXFieldScreenToSiteMapAddHelper<T>.Field field in fields)
    {
      object obj = this.Cache.GetValue((object) row, field.FieldName);
      if (obj != null)
      {
        stringBuilder.Append(flag ? '?' : '&').Append(field.ParameterName).Append('=').Append(HttpUtility.UrlEncode(obj.ToString()));
        flag = false;
      }
    }
    return flag ? (string) null : stringBuilder.ToString();
  }

  public override IEnumerable<PXSiteMapNode> FindNodes(T row)
  {
    return ((IEnumerable<PXFieldScreenToSiteMapAddHelper<T>.Field[]>) this._fieldSets).SelectMany<PXFieldScreenToSiteMapAddHelper<T>.Field[], PXSiteMapNode>((Func<PXFieldScreenToSiteMapAddHelper<T>.Field[], IEnumerable<PXSiteMapNode>>) (fields => this.FindNodes(row, fields)));
  }

  private IEnumerable<PXSiteMapNode> FindNodes(
    T row,
    PXFieldScreenToSiteMapAddHelper<T>.Field[] fields)
  {
    if ((object) row != null)
    {
      string[] strArray = this._urlPrefixes;
      for (int index = 0; index < strArray.Length; ++index)
      {
        string urlPrefix = strArray[index];
        foreach (PXSiteMapNode node in this.FindNodes(this.BuildUrl(row, fields, urlPrefix)))
          yield return node;
      }
      strArray = (string[]) null;
    }
  }

  private IEnumerable<PXSiteMapNode> FindNodes(string url)
  {
    if (url != null)
    {
      PXSiteMap.Provider.ClearThreadSlot();
      PXSiteMapNode siteMapNodeUnsecure = PXSiteMap.Provider.FindSiteMapNodeUnsecure(url);
      int unum = 0;
      for (int result = 0; siteMapNodeUnsecure != null && (unum == 0 || int.TryParse(PXUrl.GetParameter(siteMapNodeUnsecure.Url, "unum"), NumberStyles.Integer, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result) && unum == result); siteMapNodeUnsecure = PXSiteMap.Provider.FindSiteMapNodeUnsecure(url))
      {
        yield return siteMapNodeUnsecure;
        url = url.AppendUrlParameter("unum", (object) ++unum);
      }
    }
  }

  public class Field
  {
    public string FieldName { get; private set; }

    public string ParameterName { get; set; }

    public Field(string fieldName)
    {
      this.FieldName = !string.IsNullOrEmpty(fieldName) ? fieldName : throw new ArgumentNullException(nameof (fieldName));
      this.ParameterName = fieldName;
    }

    public Field(string fieldName, string parameterName)
      : this(fieldName)
    {
      this.ParameterName = !string.IsNullOrEmpty(parameterName) ? parameterName : throw new ArgumentNullException(nameof (parameterName));
    }
  }

  public class Field<TField> : PXFieldScreenToSiteMapAddHelper<T>.Field where TField : IBqlField
  {
    public Field()
      : base(typeof (TField).Name)
    {
    }

    public Field(string parameterName)
      : base(typeof (TField).Name, parameterName)
    {
    }
  }
}
