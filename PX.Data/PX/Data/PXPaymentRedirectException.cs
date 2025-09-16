// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPaymentRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>Is used to invoke a hosted form for creating or editing of the information about a credit card payment.</summary>
public class PXPaymentRedirectException : PXBaseRedirectException
{
  private string _Url;
  private string _Caption;
  private string _Token;
  private bool _UseGetMethod;
  private Dictionary<string, string> _Parameters;

  public bool DisableTopLevelNavigation { get; set; }

  public double PercentageHeight { get; set; }

  public double PercentageWidth { get; set; }

  /// <summary>Instantiates an exception with the specified data. In this constructor, you can specify whether the GET HTTP method should be used to obtain a form from the
  /// credit card processing center.</summary>
  /// <param name="caption">The caption to be displayed for the form obtained from the credit card processing center.</param>
  /// <param name="url">The URL to obtain the form from.</param>
  /// <param name="useGetMethod">If set to true, makes the system use the GET HTTP method to obtain a form from the credit card processing center. If set to false, the POST HTTP method is
  /// used.</param>
  /// <param name="token">A token to access the form.</param>
  /// <param name="parameters">A dictionary of parameters that are necessary to access the form.</param>
  public PXPaymentRedirectException(
    string caption,
    string url,
    bool useGetMethod,
    string token,
    Dictionary<string, string> parameters)
    : base(url)
  {
    this._Caption = caption;
    this._Parameters = parameters;
    this._Token = token;
    this._UseGetMethod = useGetMethod;
    if (parameters != null && parameters.Count > 0)
    {
      url += "?";
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        if (!url.EndsWith("?"))
          url += "&";
        url += $"{parameter.Key}={WebUtility.UrlEncode(parameter.Value)}";
      }
    }
    this.SetMessage(this._Url = url);
  }

  /// <summary>Instantiates an exception with the specified data.</summary>
  /// <param name="caption">The caption to be displayed for the form obtained from the credit card processing center.</param>
  /// <param name="url">The URL to obtain the form from.</param>
  /// <param name="token">A token to access the form.</param>
  /// <param name="parameters">A dictionary of parameters that are necessary to access the form.</param>
  public PXPaymentRedirectException(
    string caption,
    string url,
    string token,
    Dictionary<string, string> parameters)
    : this(caption, url, false, token, parameters)
  {
  }

  public PXPaymentRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXPaymentRedirectException>(this, info);
  }

  /// <exclude />
  public string Token => this._Token;

  /// <exclude />
  public string Url => this._Url;

  /// <exclude />
  public string Caption => this._Caption;

  /// <exclude />
  public bool UseGetMethod => this._UseGetMethod;

  /// <exclude />
  public Dictionary<string, string> Parameters => this._Parameters;

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXPaymentRedirectException>(this, info);
    base.GetObjectData(info, context);
  }
}
