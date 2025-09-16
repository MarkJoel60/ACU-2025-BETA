// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiArticleTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
public class WikiArticleTypeAttribute : WikiArticleType.ListAttribute
{
  public static readonly int _KB_ARTICLE_TYPE = 100;

  public WikiArticleTypeAttribute()
  {
    int[] numArray = new int[this._AllowedValues.Length + 1];
    string[] strArray = new string[this._AllowedLabels.Length + 1];
    this._AllowedValues.CopyTo((Array) numArray, 0);
    numArray[this._AllowedValues.Length] = WikiArticleTypeAttribute._KB_ARTICLE_TYPE;
    this._AllowedLabels.CopyTo((Array) strArray, 0);
    strArray[this._AllowedLabels.Length] = "Knowledge Base";
    this._AllowedValues = numArray;
    this._AllowedLabels = strArray;
  }

  /// <exclude />
  public sealed class kb : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleTypeAttribute.kb>
  {
    public kb()
      : base(WikiArticleTypeAttribute._KB_ARTICLE_TYPE)
    {
    }
  }
}
